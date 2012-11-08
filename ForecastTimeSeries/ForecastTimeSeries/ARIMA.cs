using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ForecastTimeSeries
{
    public enum DecayPartern
    {
        SLOW_DECAY,
        EXPONENTIAL_DECAY,
        ABRUPT_DECAY
    }

    public static class Configuration
    {
        public static int MAX_REGULAR_ARMA_ORDER = 10;
        public static int MAX_SEASON_ARMA_ORDER = 2;
    }

    public class ARIMA
    {
        public List<double> _originSeries;
        public List<double> _processSeries;
        public List<double> _errorSeries;
        public int _startIndex; 

        public int _pRegular;
        public int _qRegular;
        public int _regularDifferencingLevel;

        public int _pSeason;
        public int _qSeason;
        public int _seasonDifferencingLevel;
        public int _seasonPartern;

        List<double> _listArimaCoef;

        //Don't use
        //List<double> _listSeasonArimaCoef;
        //List<double> _listRegularArimaCoef;

        #region statistic library

        private void ComputeAutocorrelation(List<double> series, int startIndex, out List<double> listAutocorrelation)
        {
            listAutocorrelation = new List<double>();
            int numAutocorrelation = ((series.Count - startIndex) / 4 > 50 ? 50 : (series.Count - startIndex) / 4);

            double mean = 0;
            for (int i = startIndex; i < series.Count; i++)
            {
                mean += series[i];
            }
            mean /= series.Count;

            double variance = 0;
            for (int i = startIndex; i < series.Count; i++)
            {
                variance += Math.Pow(series[i] - mean, 2);
            }
            variance /= series.Count;

            listAutocorrelation.Add(1);
            for (int lag = 1; lag < numAutocorrelation; lag++)
            {
                double temp = 0;
                for (int i = startIndex; i < series.Count - lag; i++)
                {
                    temp += (series[i] - mean) * (series[i + lag] - mean);
                }
                temp /= (series.Count - startIndex) * variance;
                listAutocorrelation.Add(temp);
            }
        }

        private void ComputeConfidenceLimit(List<double> listAutocorrelation, int dataSize, out List<double> listConfidenceLimit)
        {
            listConfidenceLimit = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                double temp = 0;
                for (int j = 0; j < i; j++)
                {
                    temp += Math.Pow(listAutocorrelation[j], 2);
                }
                temp = Math.Sqrt((1 + 2 * temp) / dataSize);
                listConfidenceLimit.Add(temp);
            }
        }

        private double GetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            return listPartialCorrelation[index];
        }

        private void SetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j, double value)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            listPartialCorrelation[index] = value;
        }

        private void ComputePartialAutocorrelation(List<double> listAutocorrelation, out List<double> listPartialAutocorrelation)
        {
            int lag = listAutocorrelation.Count;
            int numPartialCorrelation = (int)(lag * (lag + 1) / 2);

            listPartialAutocorrelation = new List<double>();
            for (int i = 0; i < numPartialCorrelation; i++)
            {
                listPartialAutocorrelation.Add(0);
            }
            for (int i = 1; i < lag; i++)
            {
                double numerator = 0;
                double denominator = 0;
                double temp = 0;
                for (int j = 1; j <= i - 1; j++)
                {
                    temp += GetPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) * listAutocorrelation[i - j];
                }
                numerator = listAutocorrelation[i] - temp;
                temp = 0;
                for (int j = 1; j <= i - 1; j++)
                {
                    temp += GetPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) * listAutocorrelation[j];
                }
                denominator = 1 - temp;
                temp = numerator / denominator;
                SetPartialCorrelationAt(listPartialAutocorrelation, i, i, temp);

                for (int j = 1; j < i; j++)
                {
                    temp = GetPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) - GetPartialCorrelationAt(listPartialAutocorrelation, i, i) * GetPartialCorrelationAt(listPartialAutocorrelation, i - 1, i - j);
                    SetPartialCorrelationAt(listPartialAutocorrelation, i, j, temp);
                }
            }

            List<double> result = new List<double>();
            for (int i = 1; i <= lag; i++)
            {
                result.Add(GetPartialCorrelationAt(listPartialAutocorrelation, i, i));
            }
            listPartialAutocorrelation.Clear();
            for (int i = 0; i < result.Count; i++)
            {
                listPartialAutocorrelation.Add(result[i]);
            }
        }

        private void ComputeDifference(ref List<double> series, ref int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
        {
            for (int i = 0; i < regularDifferencingLevel; i++)
            {
                startIndex += 1;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - 1];
                }
            }

            for (int i = 0; i < seasonDifferencingLevel; i++)
            {
                startIndex += seasonPartern;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - seasonPartern];
                }
            }
        }

        private void RevertDifference(ref List<double> series, ref int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
        {
            for (int i = 0; i < seasonDifferencingLevel; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - seasonPartern];
                }
                startIndex -= seasonPartern;
            }

            for (int i = 0; i < regularDifferencingLevel; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - 1];
                }
                startIndex -= 1;
            }
        }

        private void DrawPartialAutocorrelation(List<double> listPartialAutocorrelation, double confidenceLimit)
        {
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listPartialAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            DrawAutocorrelation(listPartialAutocorrelation, listConfidenceLimit, true);
        }

        private void DrawAutocorrelation(List<double> listAutocorrelation, List<double> listConfidenceLimit, bool isPACF = false)
        {
            Plot_Form form = new Plot_Form();
            if (!isPACF)
            {
                form.chart1.Titles["Title1"].Text = "Autocorrelation Function";
                form.chart1.ChartAreas["ChartArea1"].Axes[0].Title = "Lag";
                form.chart1.ChartAreas["ChartArea1"].Axes[1].Title = "ACF";
                form.chart1.Series[0].Name = "ACF";
            }
            else
            {
                form.chart1.Titles["Title1"].Text = "Partial Autocorrelation Function";
                form.chart1.ChartAreas["ChartArea1"].Axes[0].Title = "Lag";
                form.chart1.ChartAreas["ChartArea1"].Axes[1].Title = "PACF";
                form.chart1.Series[0].Name = "PACF";
            }

            int numData = listAutocorrelation.Count;
            form.chart1.ChartAreas[0].AxisX.Interval = Math.Ceiling(1.0 * numData / 20);
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.IsVisibleInLegend = false;

            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.IsVisibleInLegend = false;

            int start = 0;
            if (isPACF)
            {
                start = 1;
            }
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series.Color = System.Drawing.Color.Blue;
                series.Points.AddXY(start + i, 0.0);
                series.Points.AddXY(start + i, listAutocorrelation[i]);
                series.IsVisibleInLegend = false;
                form.chart1.Series.Add(series);

                series1.Points.AddXY(start + i, listConfidenceLimit[i]);
                series2.Points.AddXY(start + i, -listConfidenceLimit[i]);
            }

            form.chart1.Series.Add(series1);
            form.chart1.Series.Add(series2);

            form.ShowDialog();
        }

        private void DrawSeriesData(List<double> series, int startIndex)
        {
            Plot_Form form = new Plot_Form();
            form.chart1.ChartAreas[0].AxisX.Interval = Math.Ceiling(1.0 * (series.Count - startIndex) / 20);
            form.chart1.Titles["Title1"].Text = "Time series";
            form.chart1.Series["Data"].Color = System.Drawing.Color.Blue;
            for (int i = startIndex; i < series.Count; i++)
            {
                form.chart1.Series["Data"].Points.AddXY(i + 1, series.ElementAt(i));
            }
            form.ShowDialog();
        }

        private void DrawTwoSeriesData(List<double> firstSeries, int firstStartIndex, List<double> secondSeries, int secondStartIndex)
        {
            Plot_Form form = new Plot_Form();

            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.IsVisibleInLegend = false;

            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.IsVisibleInLegend = false;

            form.chart1.ChartAreas[0].AxisX.Interval = Math.Ceiling(1.0 * (firstSeries.Count - firstStartIndex) / 20);
            form.chart1.Titles["Title1"].Text = "Time series";
            form.chart1.Series["Data"].Color = System.Drawing.Color.Blue;
            for (int i = firstStartIndex; i < firstSeries.Count; i++)
            {
                series1.Points.AddXY(i + 1, firstSeries[i]);
            }
            form.chart1.Series.Add(series1);

            for (int i = secondStartIndex; i < secondSeries.Count; i++)
            {
                series2.Points.AddXY(i + 1, secondSeries[i]);
            }
            form.chart1.Series.Add(series2);

            form.ShowDialog();
        }

        private void DrawForecastSeriesData(List<double> firstSeries, int firstStartIndex, List<double> secondSeries)
        {
            Plot_Form form = new Plot_Form();

            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.IsVisibleInLegend = false;

            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.IsVisibleInLegend = false;

            form.chart1.ChartAreas[0].AxisX.Interval = Math.Ceiling(1.0 * (firstSeries.Count - firstStartIndex) / 20);
            form.chart1.Titles["Title1"].Text = "Time series";
            form.chart1.Series["Data"].Color = System.Drawing.Color.Blue;
            for (int i = firstStartIndex; i < firstSeries.Count; i++)
            {
                series1.Points.AddXY(i + 1, firstSeries[i]);
            }
            form.chart1.Series.Add(series1);

            series2.Points.AddXY(firstSeries.Count, firstSeries[firstSeries.Count-1]);
            for (int i = 0; i < secondSeries.Count; i++)
            {
                series2.Points.AddXY(i + 1 + firstSeries.Count, secondSeries[i]);
            }
            form.chart1.Series.Add(series2);

            form.ShowDialog();
        }

        #endregion



        #region ARIMA algorithm

        private DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, double confidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            decayPartern = ComputeDecayPartern(listAutocorrelation, listConfidenceLimit);
            return decayPartern;
        }

        private DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i]) * 1.3)
                {
                    listHighAutocorrelation.Add(listAutocorrelation[i]);
                }
            }
            if (listHighAutocorrelation.Count <= 1)
            {
                decayPartern = DecayPartern.ABRUPT_DECAY;
                return decayPartern;
            }

            double averageRateExchange = 0;
            for (int i = 0; i < listHighAutocorrelation.Count - 1; i++)
            {
                averageRateExchange += Math.Abs(Math.Abs(listHighAutocorrelation[i]) - Math.Abs(listHighAutocorrelation[i + 1])) / Math.Abs(listHighAutocorrelation[i]);
            }
            averageRateExchange /= (listHighAutocorrelation.Count - 1);

            if (averageRateExchange > 0.45)
            {
                decayPartern = DecayPartern.ABRUPT_DECAY;
            }
            else if (averageRateExchange < 0.1)
            {
                decayPartern = DecayPartern.SLOW_DECAY;
            }
            else
            {
                decayPartern = DecayPartern.EXPONENTIAL_DECAY;
            }
            return decayPartern;
        }

        private void GetHighCorrelationLocation(List<double> listAutocorrelation, int begin, ref List<int> listHighestCorrelationIndex)
        {
            if (begin >= listAutocorrelation.Count)
            {
                return;
            }

            while (begin < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[begin] <= 0)
                {
                    begin++;
                }
                else if (listAutocorrelation[begin + 1] >= listAutocorrelation[begin])
                {
                    begin++;
                }
                else
                {
                    if ((begin + 2) == listAutocorrelation.Count || (listAutocorrelation[begin + 2] < listAutocorrelation[begin]))
                    {
                        listHighestCorrelationIndex.Add(begin);
                        break;
                    }
                    else
                    {
                        begin = begin + 2;
                    }
                }
            }

            while (begin < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[begin] <= 0)
                {
                    begin++;
                }
                else if (listAutocorrelation[begin + 1] < listAutocorrelation[begin])
                {
                    begin++;
                }
                else
                {
                    if ((begin + 2) == listAutocorrelation.Count || (listAutocorrelation[begin + 2] > listAutocorrelation[begin]))
                    {
                        break;
                    }
                    else
                    {
                        begin = begin + 2;
                    }
                }
            }

            GetHighCorrelationLocation(listAutocorrelation, begin + 1, ref listHighestCorrelationIndex);

        }

        private void GetLastSignificant(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i])*1.3)
                {
                    lag = i;
                }
            }
        }

        private void GetLastSignificant(List<double> listAutocorrelation, double confidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(confidenceLimit)*1.3)
                {
                    lag = i + 1;
                }
            }
        }

        private void EstimateARIMAModel(List<double> series, int startIndex, int seasonPartern, out int pRegular, out int qRegular, out int pSeason, out int qSeason)
        {
            pRegular = qRegular = pSeason = qSeason = 0;

            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            List<double> listPartialAutocorrelation = new List<double>();

            List<double> listSeasonAutocorrelation = new List<double>();
            List<double> listSeasonConfidenceLimit = new List<double>();
            List<double> listSeasonPartialCorrelation = new List<double>();

            List<double> listRegularAutocorrelation = new List<double>();
            List<double> listRegularConfidenceLimit = new List<double>();
            List<double> listRegularPartialCorrelation = new List<double>();

            ComputeAutocorrelation(series, startIndex, out listAutocorrelation);
            ComputePartialAutocorrelation(listAutocorrelation, out listPartialAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, series.Count - startIndex, out listConfidenceLimit);
            double confidenceLimit = 1.96 / Math.Sqrt(series.Count - startIndex);

            int regularAutocorrelationLengh = seasonPartern;
            if (seasonPartern == 0)
            {
                regularAutocorrelationLengh = listAutocorrelation.Count;
            }
            for (int i = 0; i < regularAutocorrelationLengh; i++)
            {
                listRegularAutocorrelation.Add(listAutocorrelation[i]);
                listRegularConfidenceLimit.Add(listConfidenceLimit[i]);
                listRegularPartialCorrelation.Add(listPartialAutocorrelation[i]);
            }
            //DrawAutocorrelation(listRegularAutocorrelation, listRegularConfidenceLimit);
            //DrawPartialAutocorrelation(listRegularPartialCorrelation, confidenceLimit);

            DecayPartern decayACF = ComputeDecayPartern(listRegularAutocorrelation, listRegularConfidenceLimit);
            DecayPartern decayPACF = ComputeDecayPartern(listRegularPartialCorrelation, confidenceLimit);

            if (decayACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularAutocorrelation, listRegularConfidenceLimit, out qRegular);
            }
            if (decayPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularPartialCorrelation, confidenceLimit, out pRegular);
            }
            if (decayACF != DecayPartern.ABRUPT_DECAY && decayPACF != DecayPartern.ABRUPT_DECAY)
            {
                pRegular = qRegular = 1;
            }

            pRegular = Math.Min(pRegular, Configuration.MAX_REGULAR_ARMA_ORDER);
            qRegular = Math.Min(qRegular, Configuration.MAX_REGULAR_ARMA_ORDER);

            if (seasonPartern == 0)
            {
                return;
            }
            for (int i = 0; i < Math.Floor(1.0 * listAutocorrelation.Count / seasonPartern); i++)
            {
                listSeasonAutocorrelation.Add(listAutocorrelation[i * seasonPartern]);
                listSeasonConfidenceLimit.Add(listConfidenceLimit[i * seasonPartern]);
                listSeasonPartialCorrelation.Add(listPartialAutocorrelation[i * seasonPartern]);
            }

            //DrawAutocorrelation(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            //DrawPartialAutocorrelation(listSeasonPartialCorrelation, confidenceLimit);

            DecayPartern decaySeasonACF = ComputeDecayPartern(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            DecayPartern decaySeasonPACF = ComputeDecayPartern(listSeasonPartialCorrelation, confidenceLimit);

            if (decaySeasonACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonAutocorrelation, listSeasonConfidenceLimit, out qSeason);
            }
            if (decaySeasonPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonPartialCorrelation, confidenceLimit, out pSeason);
            }
            if (decaySeasonACF != DecayPartern.ABRUPT_DECAY && decaySeasonPACF != DecayPartern.ABRUPT_DECAY)
            {
                pSeason = qSeason = 1;
            }

            pSeason = Math.Min(pSeason, Configuration.MAX_REGULAR_ARMA_ORDER);
            qSeason = Math.Min(qSeason, Configuration.MAX_REGULAR_ARMA_ORDER);
        }

        private void EstimateARIMACoef(List<double> series, int startIndex, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, out List<double> listArimaCoeff)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < series.Count; i++)
            {
                errors.Add(0);
            }

            Matrix observationVector = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix parameterVector = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix gainFactor = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix invertedCovarianceMatrix = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1 + pRegular + qRegular + pSeason + qSeason);
            double prioriPredictionError;
            double posterioriPredictionError;

            //Phase 1 - Set Initial Conditions
            //the observation vector
            observationVector[0, 0] = 1;
            for (int i = 1; i < pRegular + 1; i++)
            {
                observationVector[i, 0] = series[(i - 1)];
            }
            for (int i = 1; i < qRegular + 1; i++)
            {
                observationVector[pRegular + i, 0] = 0;
            }
            for (int i = 1; i < pSeason + 1; i++)
            {
                observationVector[pRegular + qRegular + i, 0] = series[(i - 1) * seasonPartern];
            }
            for (int i = 1; i < qSeason + 1; i++)
            {
                observationVector[pRegular + qRegular + pSeason + i, 0] = 0;
            }

            for (int i = 0; i < 1 + pRegular + qRegular + pSeason + qSeason; i++)
            {
                invertedCovarianceMatrix[i, i] = Math.Pow(10, 6);
            }

            int begin = ComputeMax(pRegular, qRegular, pSeason * seasonPartern, qSeason * seasonPartern, startIndex);

            for (int i = begin; i < series.Count; i++)
            {
                //Phase 1
                observationVector[0, 0] = 1;
                for (int j = 1; j < pRegular + 1; j++)
                {
                    observationVector[j, 0] = series[i - j];
                }
                for (int j = 1; j < qRegular + 1; j++)
                {
                    observationVector[pRegular + j, 0] = errors[i - j];
                }
                for (int j = 1; j < pSeason + 1; j++)
                {
                    observationVector[j, 0] = series[i - j * seasonPartern];
                }
                for (int j = 1; j < qSeason + 1; j++)
                {
                    observationVector[pRegular + j, 0] = errors[i - j * seasonPartern];
                }

                //Phase 2 - Estimate Parameters
                prioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
                double temp = 1 + (Matrix.Transpose(observationVector) * invertedCovarianceMatrix * observationVector)[0, 0];
                gainFactor = (invertedCovarianceMatrix * observationVector) / temp;
                parameterVector += gainFactor * prioriPredictionError;

                //Phase 3 - Prepare for Next Estimation 
                posterioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
                invertedCovarianceMatrix = invertedCovarianceMatrix - gainFactor * Matrix.Transpose(observationVector) * invertedCovarianceMatrix;
                errors[i] = posterioriPredictionError;
            }


            listArimaCoeff = new List<double>();
            for (int i = 0; i < 1 + pRegular + qRegular + pSeason + qSeason; i++)
            {
                listArimaCoeff.Add(parameterVector[i, 0]);
            }
        }

        private void RemoveNonstationarity(ref List<double> series, ref int startIndex, out int regularDifferencingLevel)
        {
            int dataSize = series.Count - startIndex;
            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();

            regularDifferencingLevel = 0;
            while (true)
            {
                listAutocorrelation.Clear();
                listConfidenceLimit.Clear();
                dataSize = series.Count - startIndex;
                ComputeAutocorrelation(series, startIndex, out listAutocorrelation);
                ComputeConfidenceLimit(listAutocorrelation, dataSize, out listConfidenceLimit);
                //DrawSeriesData(series, startIndex);
                //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);
                DecayPartern decayPartern = ComputeDecayPartern(listAutocorrelation, listConfidenceLimit);
                if (decayPartern != DecayPartern.SLOW_DECAY)
                {
                    break;
                }

                startIndex += 1;
                regularDifferencingLevel++;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - 1];
                }
            }
        }

        private void EstimateSeasonPartern(List<double> listAutocorrelation, out int seasonPartern)
        {
            seasonPartern = 0;
            List<int> listHighestCorrelationIndex = new List<int>();
            GetHighCorrelationLocation(listAutocorrelation, 0, ref listHighestCorrelationIndex);
            List<int> levelOneDistance = new List<int>();
            for (int i = 0; i < listHighestCorrelationIndex.Count - 1; i++)
            {
                levelOneDistance.Add(listHighestCorrelationIndex[i + 1] - listHighestCorrelationIndex[i]);
            }

            List<int> listDistinctLocation = levelOneDistance.Distinct().ToList();

            if (listDistinctLocation.Count == 0)
            {
                return;
            }
            int hightFrequencyDistance = 0;
            int frequency = 0;
            for (int i = 0; i < listDistinctLocation.Count; i++)
            {
                int newFrequency = levelOneDistance.Count(item => item == listDistinctLocation[i]);
                if (newFrequency > frequency && listDistinctLocation[i] != 1)
                {
                    hightFrequencyDistance = listDistinctLocation[i];
                    frequency = newFrequency;
                }
            }

            if (1.0 * frequency / levelOneDistance.Count > 0.5)
            {
                seasonPartern = hightFrequencyDistance;
                return;
            }
        }

        private void RemoveSeasonality(ref List<double> processSeries, ref int startIndex, out int seasonPartern, out int seasonDifferencingLevel)
        {
            seasonPartern = 0;
            seasonDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            List<int> levelLocation = new List<int>();

            while (true)
            {
                listAutocorrelation.Clear();
                ComputeAutocorrelation(processSeries, startIndex, out listAutocorrelation);

                //DrawSeriesData(series, startIndex);
                //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);

                int newSeasonPartern;
                EstimateSeasonPartern(listAutocorrelation, out newSeasonPartern);
                if (newSeasonPartern == 0 || (seasonPartern != 0 && newSeasonPartern != seasonPartern))
                {
                    break;
                }
                seasonPartern = newSeasonPartern;
                seasonDifferencingLevel++;
                startIndex += seasonPartern;

                for (int j = processSeries.Count - 1; j >= startIndex; j--)
                {
                    processSeries[j] = processSeries[j] - processSeries[j - seasonPartern];
                }
            }
        }

        private int ComputeMax(params int[] data)
        {
            int max = data.Max();
            return max;
        }

        private void ComputError(List<double> processSeries, int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, List<double> listArimaCoeff, out List<double> error)
        {
            error = new List<double>();
            List<double> currentSeries = processSeries.FindAll(item => true);
            List<double> testSeries = new List<double>();

            int begin = ComputeMax(pRegular, qRegular, pSeason * seasonPartern, qSeason * seasonPartern, startIndex);

            for (int i = 0; i < begin; i++)
            {
                testSeries.Add(processSeries[i]);
            }

            for (int i = begin; i < processSeries.Count; i++)
            {
                double temp = listArimaCoeff[0];
                for (int j = 1; j <= pRegular; j++)
                {
                    temp += listArimaCoeff[j] * processSeries[i - j];
                }
                for (int j = 1; j <= pSeason; j++)
                {
                    temp += listArimaCoeff[pRegular + qRegular + j] * processSeries[i - j * seasonPartern];
                }
                testSeries.Add(temp);

            }

            for (int i = 0; i < currentSeries.Count; i++)
            {
                error.Add(testSeries[i] - currentSeries[i]);
            }

            int originIndex = startIndex;
            int testIndex = startIndex;
        }

        private void RevertDiffTestSeries(ref List<double> series, ref List<double> testSeries, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < D; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - s];
                    testSeries[j] = testSeries[j] + series[j - s];
                }
                startIndex -= s;
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - 1];
                    testSeries[j] = testSeries[j] + series[j - 1];
                }
                startIndex -= 1;
            }
        }


        private void ForecastARIMA(List<double> processSeries, List<double> errors, int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, List<double> listArimaCoeff, int nHead, out List<double> forecastSeries)
        {
            List<double> currentSeries = processSeries.FindAll(item => true);
            List<double> currentErrors = errors.FindAll(item => true);
            int begin = processSeries.Count;
            for (int i = 0; i < nHead; i++)
            {
                currentSeries.Add(0);
                currentErrors.Add(0);
            }
            for (int i = begin; i < currentSeries.Count; i++)
            {
                currentSeries[i] = listArimaCoeff[0];

                for (int j = 1; j <= pRegular; j++)
                {
                    currentSeries[i] += currentSeries[i - j] * listArimaCoeff[j];
                }
                for (int j = 1; j <= pSeason; j++)
                {
                    currentSeries[i] += currentSeries[i - j * seasonPartern] * listArimaCoeff[pRegular + qRegular + j];
                }

                // add error
            }
            RevertDifference(ref currentSeries, ref startIndex, regularDifferencingLevel, seasonDifferencingLevel, seasonPartern);
            forecastSeries = new List<double>();
            for (int i = begin; i < currentSeries.Count; i++)
            {
                forecastSeries.Add(currentSeries[i]);
            }
        }
       
        #endregion ARIMA algorithm




        #region back up
        
        ////Not use in current version

 
        //private void TestRegularARIMA(List<double> processSeries, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff, out List<double> error)
        //{
        //    error = new List<double>();
        //    List<double> currentSeries = processSeries.FindAll(item => true);
        //    List<double> regularSeries = new List<double>();

        //    int startTest = ComputeMax(pCoef, qCoef, startIndex);

        //    for (int i = 0; i < startTest; i++)
        //    {
        //        regularSeries.Add(processSeries[i]);
        //        //error.Add(0);
        //    }

        //    for (int i = startTest; i < processSeries.Count; i++)
        //    {
        //        double temp = listArimaCoeff[0];
        //        for (int j = 1; j <= pCoef; j++)
        //        {
        //            temp += listArimaCoeff[j] * processSeries[i - j];
        //        }
        //        //for (int j = 1; j <= qCoef; j++)
        //        //{
        //        //    temp += listArimaCoeff[j + p - 1] * error[i - j];
        //        //}
        //        regularSeries.Add(temp);
        //        //error.Add(processSeries[i] - temp);
        //    }

        //    int originIndex = startIndex;
        //    int testIndex = startIndex;
        //    for (int i = 0; i < currentSeries.Count; i++)
        //    {
        //        error.Add(regularSeries[i] - currentSeries[i]);
        //    }
        //    RevertDiffTestSeries(ref currentSeries, ref regularSeries, ref originIndex, diff, 0, 0);

        //    List<double> forecastSeries;
        //    Forecast(12, out forecastSeries);
        //    foreach (double item in forecastSeries)
        //    {
        //        regularSeries.Add(item);
        //    }
        //    DrawTwoSeriesData(currentSeries, originIndex, regularSeries, testIndex);
        //    WriteLogSeries(error);

        //}

        //private void ForecastRegularARIMA(List<double> processSeries, List<double> errors, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff, int nHead, out List<double> forecastSeries)
        //{
        //    List<double> currentSeries = processSeries.FindAll(item => true);
        //    List<double> currentErrors = errors.FindAll(item => true);
        //    int originIndex = startIndex;
        //    int forecastIndex = startIndex;
        //    int begin = processSeries.Count;
        //    for (int i = 0; i < nHead; i++)
        //    {
        //        currentSeries.Add(0);
        //        currentErrors.Add(0);
        //    }
        //    for (int i = begin; i < currentSeries.Count; i++)
        //    {
        //        currentSeries[i] = listArimaCoeff[0];
        //        for (int j = 1; j <= pCoef; j++)
        //        {
        //            currentSeries[i] += currentSeries[i - j] * listArimaCoeff[j];
        //        }
        //        for (int j = 1; j <= qCoef; j++)
        //        {
        //            //currentSeries[i] += currentErrors[i - j] * listArimaCoeff[j + pCoef];
        //        }
        //        //errors.Add(series[i] - temp);
        //    }
        //    RevertDifference(ref currentSeries, ref startIndex, diff, 0, 0);
        //    forecastSeries = new List<double>();
        //    for (int i = begin; i < currentSeries.Count; i++)
        //    {
        //        forecastSeries.Add(currentSeries[i]);
        //    }
        //}

        //private void EstimateRegularARIMACoef(List<double> series, int startIndex, int pCoef, int qCoef, out List<double> listArimaCoeff)
        //{
        //    EstimateSeasonARIMACoef(series, startIndex, 1, pCoef, qCoef, out listArimaCoeff);
        //}

        //private void EstimateSeasonARIMACoef(List<double> series, int startIndex, int season, int pCoef, int qCoef, out List<double> listArimaCoeff)
        //{
        //    List<double> errors = new List<double>();
        //    for (int i = 0; i < series.Count; i++)
        //    {
        //        errors.Add(0);
        //    }

        //    Matrix observationVector = new Matrix(1 + pCoef + qCoef, 1);
        //    Matrix parameterVector = new Matrix(1 + pCoef + qCoef, 1);
        //    Matrix gainFactor = new Matrix(1 + pCoef + qCoef, 1);
        //    Matrix invertedCovarianceMatrix = new Matrix(1 + pCoef + qCoef, 1 + pCoef + qCoef);
        //    double prioriPredictionError;
        //    double posterioriPredictionError;

        //    //Phase 1 - Set Initial Conditions
        //    //the observation vector
        //    observationVector[0, 0] = 1;
        //    for (int i = 1; i < pCoef + 1; i++)
        //    {
        //        observationVector[i, 0] = series[(i - 1) * season];
        //    }
        //    for (int i = 1; i < qCoef + 1; i++)
        //    {
        //        observationVector[pCoef + i, 0] = 0;
        //    }
        //    for (int i = 0; i < pCoef + qCoef + 1; i++)
        //    {
        //        invertedCovarianceMatrix[i, i] = Math.Pow(10, 6);
        //    }

        //    for (int k = 0; k < 10; k++)
        //    {
        //        for (int i = pCoef * season; i < series.Count; i++)
        //        {
        //            //Phase 1
        //            observationVector[0, 0] = 1;
        //            for (int j = 1; j < pCoef + 1; j++)
        //            {
        //                observationVector[j, 0] = series[i - j * season];
        //            }
        //            for (int j = 1; j < qCoef + 1; j++)
        //            {
        //                if (i - j * season >= 0)
        //                {
        //                    observationVector[pCoef + j, 0] = errors[i - j * season];
        //                }
        //                else
        //                {
        //                    observationVector[pCoef + j, 0] = 0;
        //                }
        //            }

        //            //Phase 2 - Estimate Parameters
        //            prioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
        //            double temp = 1 + (Matrix.Transpose(observationVector) * invertedCovarianceMatrix * observationVector)[0, 0];
        //            gainFactor = (invertedCovarianceMatrix * observationVector) / temp;
        //            parameterVector += gainFactor * prioriPredictionError;

        //            //Phase 3 - Prepare for Next Estimation 
        //            posterioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
        //            invertedCovarianceMatrix = invertedCovarianceMatrix - gainFactor * Matrix.Transpose(observationVector) * invertedCovarianceMatrix;
        //            errors[i] = posterioriPredictionError;
        //        }
        //    }

        //    listArimaCoeff = new List<double>();
        //    for (int i = 0; i < 1 + pCoef + qCoef; i++)
        //    {
        //        listArimaCoeff.Add(parameterVector[i, 0]);
        //    }
        //}

        #endregion back up


        #region test

        private void WriteLogSeries(List<double> series)
        {
            StreamWriter file = new StreamWriter("result_test.dat", true);
            foreach (double data in series)
            {
                file.Write(String.Format("{0:0.00}", data) + "\t");
            }
            file.WriteLine("\n");
            file.Flush();
            file.Close();
        }

        private void WriteLogSeries(string log)
        {
            StreamWriter file = new StreamWriter("result_test.dat", true);
            file.WriteLine(log);
            file.WriteLine("\n");
            file.Flush();
            file.Close();
        }

        #endregion test


        public ARIMA()
        {
            _startIndex = 0;
            _seasonDifferencingLevel = _seasonPartern = _pSeason = _qSeason = 0;
            _regularDifferencingLevel = _pRegular = _qRegular = 0;

            _originSeries = new List<double>();
            _processSeries = new List<double>();
            _errorSeries = new List<double>();

            _listArimaCoef = new List<double>();
        }

        public void InitTraining()
        {
            _startIndex = 0;
            _seasonDifferencingLevel = _seasonPartern = _pSeason = _qSeason = 0;
            _regularDifferencingLevel = _pRegular = _qRegular = 0;

            _processSeries.Clear();
            _errorSeries.Clear();

            _listArimaCoef.Clear();

            _processSeries = _originSeries.FindAll(item => true);
        }

        public void SetData(List<double> series)
        {
            _originSeries.Clear();
            _processSeries.Clear();
            _errorSeries.Clear();
            for (int i = 0; i < series.Count; i++)
            {
                _originSeries.Add(series[i]);
                _processSeries.Add(series[i]);
            }
        }

        public void GetError(out List<double> errors)
        {
            errors = new List<double>();
            errors = _errorSeries.FindAll(item => true);
        }

        public void GetModel(out string model)
        {
            model = string.Empty;
            StringBuilder result = new StringBuilder();
            result.Append(String.Format("ARIMA({0}, {1}, {2})({3}, {4}, {5}){6}\n", 
                _pRegular, _regularDifferencingLevel, _qRegular, _pSeason, _seasonDifferencingLevel, _qSeason, _seasonPartern));
            result.Append(String.Format("Perception\t|{0}\n", _listArimaCoef[0]));
            result.Append(String.Format("Order\t\t|"));
            for (int i = 0; i < ComputeMax(_pRegular, _qRegular, _pSeason, _qSeason); i++)
            {
                result.Append(String.Format("  {0}\t|", i + 1));
            }
            result.Append("\n");

            result.Append(String.Format("AR Regular coef\t|"));
            for (int i = 0; i < _pRegular; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + 1]));
            }
            result.Append("\n");

            result.Append(String.Format("MA Regular coef\t|"));
            for (int i = 0; i < _qRegular; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular]));
            }
            result.Append("\n");

            result.Append(String.Format("AR Season coef\t|"));
            for (int i = 0; i < _pSeason; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular + _qRegular]));
            }
            result.Append("\n");

            result.Append(String.Format("MA Season coef\t|"));
            for (int i = 0; i < _qSeason; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular + _qRegular + _pSeason]));
            }

            model = result.ToString();
        }

        public void GetModel(out List<int> model)
        {
            model = new List<int>();
            model.Add(_pRegular);
            model.Add(_regularDifferencingLevel);
            model.Add(_qRegular);
            model.Add(_pSeason);
            model.Add(_seasonDifferencingLevel);
            model.Add(_qSeason);
            model.Add(_seasonPartern);
        }

        public void RemoveTrendSeasonality(int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
        {
            InitTraining();
            _regularDifferencingLevel = regularDifferencingLevel;
            _seasonDifferencingLevel = seasonDifferencingLevel;
            _seasonPartern = seasonPartern;
            ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
        }

        public void Test()
        {
            List<double> currentSeries = _processSeries.FindAll(item => true);
            List<double> testSeries = new List<double>();
            for (int i = 0; i < _processSeries.Count; i++)
            {
                testSeries.Add(_processSeries[i] + _errorSeries[i]);
            }
            DrawTwoSeriesData(currentSeries, _startIndex, testSeries, _startIndex);

            int startIndex = _startIndex;
            RevertDiffTestSeries(ref currentSeries, ref testSeries, ref startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
            DrawTwoSeriesData(currentSeries, startIndex, testSeries, startIndex);
        }

        public void AutomaticTraining()
        {
            InitTraining();
            RemoveNonstationarity(ref _processSeries, ref _startIndex, out _regularDifferencingLevel);
            RemoveSeasonality(ref _processSeries, ref _startIndex, out _seasonPartern, out _seasonDifferencingLevel);

            EstimateARIMAModel(_processSeries, _startIndex, _seasonPartern, out _pRegular, out _qRegular, out _pSeason, out _qSeason);
            EstimateARIMACoef(_processSeries, _startIndex, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, out _listArimaCoef);

            ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);
        }

        public void ManualTraining(int pRegular, int regularDifferencing, int qRegular, int pSeason, int seassonDifferencing, int qSeason, int seasonPartern)
        {
            InitTraining();
            this._regularDifferencingLevel = regularDifferencing;
            this._pRegular = pRegular;
            this._qRegular = qRegular;
            this._pSeason = pSeason;
            this._qSeason = qSeason;
            this._seasonDifferencingLevel = seassonDifferencing;
            this._seasonPartern = seasonPartern;

            ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
            EstimateARIMACoef(_processSeries, _startIndex, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, out _listArimaCoef);

            ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);
        }


        public void Forecast(int nHead, out List<double> forecastSeries)
        {
            ForecastARIMA(_processSeries, _errorSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel,
                _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, nHead, out forecastSeries);
        }

        public void Forecast(int nHead)
        {
            List<double> forecastSeries;
            Forecast(nHead, out forecastSeries);
            DrawForecastSeriesData(_originSeries, 0, forecastSeries);
        }

        public void DrawSeriesData()
        {
            DrawSeriesData(_processSeries, _startIndex);
        }

        public void DrawAutocorrelation()
        {
            List<double> listAutocorrelation;
            List<double> listConfidenceLimit;
            ComputeAutocorrelation(_processSeries, _startIndex, out listAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, _processSeries.Count, out listConfidenceLimit);
            DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);
        }

        public void DrawPartialAutocorrelation()
        {
            List<double> listAutocorrelation;
            List<double> listPartialAutocorrelation;
            ComputeAutocorrelation(_processSeries, _startIndex, out listAutocorrelation);
            ComputePartialAutocorrelation(listAutocorrelation, out listPartialAutocorrelation);
            double confidenceLimit = 1.96 / Math.Sqrt(_processSeries.Count);
            DrawPartialAutocorrelation(listPartialAutocorrelation, confidenceLimit);
        }

        public bool Export(string pathFile)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("ARIMA");
            doc.AppendChild(root);
            //save number of Input, Hidden, Output Nodes
            XmlElement regularDifference = doc.CreateElement("RegularDifferencing");
            regularDifference.InnerText = Convert.ToString(this._regularDifferencingLevel);
            XmlElement seasonDifference = doc.CreateElement("SeasonDifferencing");
            seasonDifference.InnerText = Convert.ToString(this._seasonDifferencingLevel);
            XmlElement seasonPartern = doc.CreateElement("SeasonPartern");
            seasonPartern.InnerText = Convert.ToString(this._seasonPartern);

            XmlElement pRegular = doc.CreateElement("ARRegular");
            pRegular.InnerText = Convert.ToString(this._pRegular);
            XmlElement qRegular = doc.CreateElement("MARegular");
            qRegular.InnerText = Convert.ToString(this._qRegular);
            XmlElement pSeason = doc.CreateElement("ARSeason");
            pSeason.InnerText = Convert.ToString(this._pSeason);
            XmlElement qSeason = doc.CreateElement("MASeason");
            qSeason.InnerText = Convert.ToString(this._qSeason);


            root.AppendChild(regularDifference);
            root.AppendChild(seasonDifference);
            root.AppendChild(seasonPartern);
            root.AppendChild(pRegular);
            root.AppendChild(qRegular);
            root.AppendChild(pSeason);
            root.AppendChild(qSeason);

            XmlElement ArimaCoef = doc.CreateElement("ARIMACoef");

            string arimaCoefStr = string.Empty;

            foreach (double coef in _listArimaCoef)
            {
                arimaCoefStr += "|" + coef;
            }
            if (arimaCoefStr.Length > 0)
            {
                arimaCoefStr = arimaCoefStr.Substring(1);
            }
            ArimaCoef.InnerText = arimaCoefStr;

            root.AppendChild(ArimaCoef);
            doc.Save(pathFile);
            return true;
        }

        public bool Import(string pathFile)
        {
            InitTraining();
            XmlDocument input = new XmlDocument();
            try
            {
                input.Load(pathFile);
                XmlNode root = input.FirstChild;
                //Get number of input, hidden, output nodes
                this._regularDifferencingLevel = Int32.Parse(root.SelectSingleNode("descendant::RegularDifferencing").InnerText);
                this._seasonDifferencingLevel = Int32.Parse(root.SelectSingleNode("descendant::SeasonDifferencing").InnerText);
                this._seasonPartern = Int32.Parse(root.SelectSingleNode("descendant::SeasonPartern").InnerText);
                this._pRegular = Int32.Parse(root.SelectSingleNode("descendant::ARRegular").InnerText);
                this._qRegular = Int32.Parse(root.SelectSingleNode("descendant::MARegular").InnerText);
                this._pSeason = Int32.Parse(root.SelectSingleNode("descendant::ARSeason").InnerText);
                this._qSeason = Int32.Parse(root.SelectSingleNode("descendant::MASeason").InnerText);

                string arimaCoef = root.SelectSingleNode("descendant::ARIMACoef").InnerText;
                string[] listArimaCoef = arimaCoef.Split('|');
                _listArimaCoef = new List<double>();
                foreach (string coef in listArimaCoef)
                {
                    double temp = Double.Parse(coef);
                    _listArimaCoef.Add(temp);
                }

                ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
                ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);

            }
            catch
            {
            }
            return true;
        }
    }


}
