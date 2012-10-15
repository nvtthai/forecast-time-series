using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public static int MAX_ARMA_ORDER = 25;
    }
    public class ARIMA
    {
        public List<double> originSeries;
        public List<double> processSeries;
        public int startIndex; // use for processSeries
        public List<double> errorSeries;
        public int p;
        public int q;
        public int P;
        public int Q;
        public int seasonPartern;
        public int regularDifferencingLevel;
        public int seasonDifferencingLevel;
        List<double> listArimaCoeff;
        List<double> listSeasonArimaCoeff;

        public ARIMA()
        {
            originSeries = new List<double>();
            processSeries = new List<double>();
        }

        public void ComputeAutocorrelation(List<double> series, int startIndex, out List<double> listAutocorrelation)
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

        public void ComputeConfidenceLimit(List<double> listAutocorrelation, int dataSize, out List<double> listConfidenceLimit)
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

        public double GetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            return listPartialCorrelation[index];
        }

        public void SetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j, double value)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            listPartialCorrelation[index] = value;
        }

        public void ComputePartialAutocorrelation(List<double> listAutocorrelation, out List<double> listPartialAutocorrelation)
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

        public void DrawPartialAutocorrelation(List<double> listAutocorrelation, double confidenceLimit)
        {
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            DrawAutocorrelation(listAutocorrelation, listConfidenceLimit, true);
        }

        public void DrawAutocorrelation(List<double> listAutocorrelation, List<double> listConfidenceLimit, bool isPACF = false)
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

        public void DrawSeriesData(List<double> series, int startIndex)
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

        public void DrawTwoSeriesData(List<double> firstSeries, int firstStartIndex, List<double> secondSeries, int secondStartIndex)
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

        public void ComputeDifference(List<double> series, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < d; i++)
            {
                startIndex += 1;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - 1];
                }
            }

            for (int i = 0; i < D; i++)
            {
                startIndex += s;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - s];
                }
            }
        }

        public void RevertDifference(List<double> series, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < D; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - s];
                }
                startIndex -= s;
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - 1];
                }
                startIndex -= 1;
            }
        }

        public DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, double confidenceLimit)
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

        public DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, List<double> listConfidenceLimit)
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

        public void GetHighCorrelationLocation(List<double> listAutocorrelation, int startIndex, List<int> listHighestCorrelationIndex)
        {
            if (startIndex >= listAutocorrelation.Count)
            {
                return;
            }

            while (startIndex < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[startIndex] <= 0)
                {
                    startIndex++;
                }
                else if (listAutocorrelation[startIndex + 1] >= listAutocorrelation[startIndex])
                {
                    startIndex++;
                }
                else
                {
                    if ((startIndex + 2) == listAutocorrelation.Count || (listAutocorrelation[startIndex + 2] < listAutocorrelation[startIndex]))
                    {
                        listHighestCorrelationIndex.Add(startIndex);
                        break;
                    }
                    else
                    {
                        startIndex = startIndex + 2;
                    }
                }
            }

            while (startIndex < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[startIndex + 1] < listAutocorrelation[startIndex])
                {
                    startIndex++;
                }
                else
                {
                    if ((startIndex + 2) == listAutocorrelation.Count || (listAutocorrelation[startIndex + 2] > listAutocorrelation[startIndex]))
                    {
                        break;
                    }
                    else
                    {
                        startIndex = startIndex + 2;
                    }
                }
            }

            GetHighCorrelationLocation(listAutocorrelation, startIndex + 1, listHighestCorrelationIndex);

        }

        public void EstimateSeasonPartern(List<double> listAutocorrelation, out int seasonPartern)
        {
            seasonPartern = 0;
            List<int> listHighestCorrelationIndex = new List<int>();
            GetHighCorrelationLocation(listAutocorrelation, 0, listHighestCorrelationIndex);
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

        public void GetLastSignificant(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 1; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i]) * 1.3)
                {
                    lag = i + 1;
                }
            }
        }

        public void GetLastSignificant(List<double> listAutocorrelation, double confidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 1; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(confidenceLimit) * 1.3)
                {
                    lag = i + 1;
                }
            }
        }

        public void EstimateArimaCoef(List<double> series, int startIndex, int seasonPartern, out int p, out int q, out int P, out int Q)
        {
            p = q = P = Q = 0;

            List<double> listConfidenceLimit = new List<double>();
            List<double> listAutocorrelation = new List<double>();
            List<double> listPartialAutocorrelation = new List<double>();

            List<double> listSeasonConfidenceLimit = new List<double>();
            List<double> listSeasonAutocorrelation = new List<double>();
            List<double> listSeasonPartialCorrelation = new List<double>();

            List<double> listRegularConfidenceLimit = new List<double>();
            List<double> listRegularAutocorrelation = new List<double>();
            List<double> listRegularPartialCorrelation = new List<double>();

            ComputeAutocorrelation(series, startIndex, out listAutocorrelation);
            ComputePartialAutocorrelation(listAutocorrelation, out listPartialAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, series.Count - startIndex, out listConfidenceLimit);
            double confidenceLimit = 1.96 / Math.Sqrt(series.Count - startIndex);

            int autocorrelationLengh = seasonPartern;
            if (seasonPartern == 0)
            {
                autocorrelationLengh = listAutocorrelation.Count;
            }
            for (int i = 0; i < autocorrelationLengh; i++)
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
                GetLastSignificant(listRegularAutocorrelation, listRegularConfidenceLimit, out q);
            }
            if (decayPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularPartialCorrelation, confidenceLimit, out p);
            }
            if (decayACF != DecayPartern.ABRUPT_DECAY && decayPACF != DecayPartern.ABRUPT_DECAY)
            {
                p = q = 1;
            }

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
                GetLastSignificant(listSeasonAutocorrelation, listSeasonConfidenceLimit, out Q);
            }
            if (decaySeasonPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonPartialCorrelation, confidenceLimit, out P);
            }
            if (decaySeasonACF != DecayPartern.ABRUPT_DECAY && decaySeasonPACF != DecayPartern.ABRUPT_DECAY)
            {
                P = Q = 1;
            }
        }

        public static void EstimateRegularARIMAModel(List<double> series, int startIndex, int pCoef, int qCoef, out List<double> listArimaCoeff)
        {
            EstimateSeasonARIMAModel(series, startIndex, 1, pCoef, qCoef, out listArimaCoeff);
        }

        public static void EstimateSeasonARIMAModel(List<double> series, int startIndex, int season, int pCoef, int qCoef, out List<double> listArimaCoeff)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < series.Count; i++)
            {
                errors.Add(0);
            }

            Matrix observationVector = new Matrix(1 + pCoef + qCoef, 1);
            Matrix parameterVector = new Matrix(1 + pCoef + qCoef, 1);
            Matrix gainFactor = new Matrix(1 + pCoef + qCoef, 1);
            Matrix invertedCovarianceMatrix = new Matrix(1 + pCoef + qCoef, 1 + pCoef + qCoef);
            double prioriPredictionError;
            double posterioriPredictionError;

            //Phase 1 - Set Initial Conditions
            //the observation vector
            observationVector[0, 0] = 1;
            for (int i = 1; i < pCoef + 1; i++)
            {
                observationVector[i, 0] = series[(i - 1) * season];
            }
            for (int i = 1; i < qCoef + 1; i++)
            {
                observationVector[pCoef + i, 0] = 0;
            }
            for (int i = 0; i < pCoef + qCoef + 1; i++)
            {
                invertedCovarianceMatrix[i, i] = Math.Pow(10, 6);
            }


            for (int i = pCoef * season; i < series.Count; i++)
            {
                //Phase 1
                observationVector[0, 0] = 1;
                for (int j = 1; j < pCoef + 1; j++)
                {
                    observationVector[j, 0] = series[i - j * season];
                }
                for (int j = 1; j < qCoef + 1; j++)
                {
                    if (i - j * season >= 0)
                    {
                        observationVector[pCoef + j, 0] = errors[i - j * season];
                    }
                    else
                    {
                        observationVector[pCoef + j, 0] = 0;
                    }
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
            for (int i = 0; i < 1 + pCoef + qCoef; i++)
            {
                listArimaCoeff.Add(parameterVector[i, 0]);
            }
        }

        public void RemoveNonstationarity(ref List<double> series, ref int startIndex, out int regularDifferencingLevel)
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

        public void RemoveSeasonality(ref List<double> series, ref int startIndex, out int seasonPartern, out int seasonDifferencingLevel)
        {
            seasonPartern = 0;
            seasonDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            List<int> levelLocation = new List<int>();

            while (true)
            {
                listAutocorrelation.Clear();
                ComputeAutocorrelation(series, startIndex, out listAutocorrelation);

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

                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - seasonPartern];
                }
            }
        }

        public int ComputeMax(params int[] data)
        {
            int max = data.Max();
            return max;
        }

        public void WriteLogSeries(List<double> series)
        {
            StreamWriter file = new StreamWriter("result_test.dat", true);
            foreach (double data in series)
            {
                file.Write(String.Format("{0:0.00}", data) + "\t");
            }
            file.WriteLine("");
            file.Flush();
            file.Close();
        }

        public void WriteLogSeries(string log)
        {
            StreamWriter file = new StreamWriter("result_test.dat", true);
            file.WriteLine(log);
            file.Flush();
            file.Close();
        }

        public void TestARMA(List<double> series, int startIndex, int diff, int seasonDiff, int pCoef, int qCoef, int season, int PCoef, int QCoef, List<double> listArimaCoeff, List<double> listSeasonArimaCoeff)
        {
            List<double> originSeries = series.FindAll(item => true);
            List<double> regularSeries = new List<double>();
            List<double> seasonSeries = new List<double>();
            List<double> testSeries = new List<double>();

            int startTest = ComputeMax(pCoef, qCoef, PCoef * season, QCoef * season, startIndex);

            for (int i = 0; i < startTest; i++)
            {
                regularSeries.Add(series[i]);
            }

            for (int i = startTest; i < series.Count; i++)
            {
                double temp = listArimaCoeff[0];
                for (int j = 1; j <= pCoef; j++)
                {
                    temp += listArimaCoeff[j] * series[i - j];
                }
                regularSeries.Add(temp);

            }

            if (seasonDiff != 0)
            {
                for (int i = 0; i < startTest; i++)
                {
                    seasonSeries.Add(series[i]);
                }

                for (int i = startTest; i < series.Count; i++)
                {
                    double temp = listSeasonArimaCoeff[0];
                    for (int j = 1; j <= PCoef; j++)
                    {
                        temp += listSeasonArimaCoeff[j] * series[i - j * season];
                    }
                    seasonSeries.Add(temp);
                }
            }


            for (int i = 0; i < startTest; i++)
            {
                testSeries.Add(series[i]);
            }
            for (int i = startTest; i < series.Count; i++)
            {
                if (seasonDiff != 0)
                {
                    testSeries.Add((regularSeries[i] + seasonSeries[i]) / 2);
                }
                else
                {
                    testSeries.Add(regularSeries[i]);
                }
            }

            int originIndex = startIndex;
            int testIndex = startIndex;

            WriteLogSeries("Original series before revert diff");
            WriteLogSeries(originSeries);
            WriteLogSeries("Regular series before revert diff");
            WriteLogSeries(regularSeries);
            WriteLogSeries("Season series before revert diff");
            WriteLogSeries(seasonSeries);
            WriteLogSeries("Test series before revert diff");
            WriteLogSeries(testSeries);

            RevertDifference(originSeries, ref originIndex, diff, seasonDiff, season);
            RevertDifference(testSeries, ref testIndex, diff, seasonDiff, season);
            DrawTwoSeriesData(originSeries, originIndex, testSeries, testIndex);
        }

        public void RevertDiffTestSeries(List<double> series, List<double> testSeries, ref int startIndex, int d, int D, int s)
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

        public void TestRegularARIMA(List<double> processSeries, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff, out List<double> error)
        {
            error = new List<double>();
            List<double> originSeries = processSeries.FindAll(item => true);
            List<double> regularSeries = new List<double>();

            int startTest = ComputeMax(pCoef, qCoef, startIndex);

            for (int i = 0; i < startTest; i++)
            {
                regularSeries.Add(processSeries[i]);
            }

            for (int i = startTest; i < processSeries.Count; i++)
            {
                double temp = listArimaCoeff[0];
                for (int j = 1; j <= pCoef; j++)
                {
                    temp += listArimaCoeff[j] * processSeries[i - j];
                }
                regularSeries.Add(temp);

            }

            int originIndex = startIndex;
            int testIndex = startIndex;
            for (int i = 0; i < originSeries.Count; i++)
            {
                error.Add(regularSeries[i] - originSeries[i]);
            }
            RevertDiffTestSeries(originSeries, regularSeries, ref originIndex, diff, 0, 0);
            DrawTwoSeriesData(originSeries, originIndex, regularSeries, testIndex);
        }

        public void ForecastRegularARIMA(List<double> series, List<double> errors, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff, int nHead)
        {
            List<double> originSeries = series.FindAll(item => true);
            List<double> forecastSeries = series.FindAll(item => true);
            List<double> forecastErrors = errors.FindAll(item => true);
            int originIndex = startIndex;
            int forecastIndex = startIndex;
            int begin = series.Count;
            for (int i = 0; i < nHead; i++)
            {
                forecastSeries.Add(0);
                forecastErrors.Add(0);
            }
            for (int i = begin; i < forecastSeries.Count; i++)
            {
                forecastSeries[i] = listArimaCoeff[0];
                for (int j = 1; j <= pCoef; j++)
                {
                    forecastSeries[i] += forecastSeries[i - j] * listArimaCoeff[j];
                }
                for (int j = 1; j <= qCoef; j++)
                {
                    forecastSeries[i] += forecastErrors[i - j] * listArimaCoeff[j + pCoef];
                }
                //errors.Add(series[i] - temp);
            }
            RevertDifference(originSeries, ref originIndex, diff, 0, 0);
            RevertDifference(forecastSeries, ref forecastIndex, diff, 0, 0);
            DrawTwoSeriesData(originSeries, originIndex, forecastSeries, forecastIndex);
        }

        public void SetData(List<double> series)
        {
            for (int i = 0; i < series.Count; i++)
            {
                originSeries.Add(series[i]);
                processSeries.Add(series[i]);
            }
        }

        public void Run()
        {
            RemoveNonstationarity(ref processSeries, ref startIndex, out regularDifferencingLevel);
            //RemoveSeasonality(ref processSeries, ref startIndex, out seasonPartern, out seasonDifferencingLevel);

            EstimateArimaCoef(processSeries, startIndex, seasonPartern, out p, out q, out P, out Q);
            EstimateRegularARIMAModel(processSeries, startIndex, p, q, out listArimaCoeff);
            //EstimateSeasonARIMAModel(processSeries, startIndex, seasonPartern, P, Q, out listSeasonArimaCoeff);

            TestRegularARIMA(processSeries, startIndex, regularDifferencingLevel, p, q, listArimaCoeff, out errorSeries);
            ForecastRegularARIMA(processSeries, errorSeries, startIndex, regularDifferencingLevel, p, q, listArimaCoeff, 30);
        }

    }


}
