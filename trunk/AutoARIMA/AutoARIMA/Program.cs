using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoARIMA
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

    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"E:\PROJECT\FINAL PROJECT\Other\Test\airpass.dat";
            List<double> series = new List<double>();
            List<double> errorsSeason = new List<double>();
            List<double> errors = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            try
            {
                file = new System.IO.StreamReader(fileName);
                while ((line = file.ReadLine()) != null)
                {
                    series.Add(Double.Parse(line));
                    errorsSeason.Add(Double.Parse(line));
                    errors.Add(0);
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                series = null;
            }

            List<double> listAutocorrelation = new List<double>();
            List<double> listPartialAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            List<double> listArimaCoeff = new List<double>();
            List<double> listSeasonArimaCoeff = new List<double>();
            int regularDifferencingLevel = 0;
            int seasonPartern = 0;
            int seasonDifferencingLevel = 0;
            int dataSize = series.Count;
            int p,q,P,Q;

            RemoveNonstationarity(series, ref dataSize, out regularDifferencingLevel);
            RemoveSeasonality(series, ref dataSize, out seasonPartern, out seasonDifferencingLevel);
            ComputeArima(series, dataSize, seasonPartern, out p, out q, out P, out Q);

            ComputeARMA(series, errors, p, q, listArimaCoeff);
            ComputeARMA(series, errorsSeason, P, Q, listSeasonArimaCoeff);

            int x = 0;
        }

        public static void ComputeAR(List<double> series, int pCoef)
        {
            int dataSize = series.Count;
            int nPredict = dataSize - pCoef;
            Matrix obMatrix = new Matrix(dataSize - pCoef, 1);
            Matrix coefMatrix = new Matrix(pCoef + 1, 1);
            Matrix paraMatrix = new Matrix(pCoef + 1, dataSize - pCoef);

            for (int i = 0; i < dataSize - pCoef; i++)
            {
                obMatrix[i, 0] = series[dataSize - i - 1];
            }

            for (int j = 0; j < dataSize - pCoef; j++)
            {
                for (int i = 0; i <= pCoef; i++)
                {
                    paraMatrix[i, j] = series[dataSize - j - i - 1];
                }
                paraMatrix[0, j] = 1;
            }

            coefMatrix = Matrix.Inverse((paraMatrix * Matrix.Transpose(paraMatrix))) * paraMatrix * obMatrix;
            Console.WriteLine(Matrix.PrintMat(coefMatrix));
        }

        public static void ComputeARMA(List<double> series, List<double> errors, int pCoef, int qCoef, List<double> listArimaCoeff)
        {
            int dataSize = series.Count;
            Matrix observationVector = new Matrix(1 + pCoef + qCoef, 1);
            Matrix parameterVector = new Matrix(1 + pCoef + qCoef, 1);
            Matrix gainFactor = new Matrix(1 + pCoef + qCoef, 1);
            Matrix invertedCovarianceMatrix = new Matrix(1 + pCoef + qCoef, 1 + pCoef + qCoef);
            double prioriPredictionError;
            double posterioriPredictionError;

            //Phase 1 - Set Initial Conditions
            //the observation vector
            observationVector[0, 0] = 1;
            for (int i = 1; i <= pCoef; i++)
            {
                observationVector[i, 0] = series[i - 1];
            }
            for (int i = 1; i <= qCoef; i++)
            {
                observationVector[pCoef + i, 0] = 0;
            }
            for (int i = 0; i <= pCoef + qCoef; i++)
            {
                invertedCovarianceMatrix[i, i] = Math.Pow(10, 6);
            }


            for (int i = pCoef; i < series.Count; i++)
            {
                //Phase 1
                observationVector[0, 0] = 1;
                for (int j = 1; j <= pCoef; j++)
                {
                    observationVector[j, 0] = series[i - pCoef + j - 1];
                }
                for (int j = 1; j <= qCoef; j++)
                {
                    observationVector[pCoef + j, 0] = errors[i - qCoef + j - 1];
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

            for (int i = 0; i < 1 + pCoef + qCoef; i++)
            {
                listArimaCoeff.Add(parameterVector[i, 0]);
            }
        }

        public static void ComputeDifference(List<double> series, int d, int D, int s)
        {
            int endIndex = series.Count;
            for (int i = 0; i < d; i++)
            {
                endIndex -= 1;
                for (int j = 0; j < endIndex; j++)
                {
                    series[j] = series[j + 1] - series[j];
                }
            }
            for (int i = 0; i < D; i++)
            {
                endIndex -= s;
                for (int j = 0; j < endIndex; j++)
                {
                    series[j] = series[j + s] - series[j];
                }
            }
        }

        public static void RevertDifference(List<double> series, int d, int D, int s)
        {
            int endIndex = series.Count - d - s * D;
            for (int i = 0; i < D; i++)
            {
                for (int j = endIndex - 1; j >= 0; j--)
                {
                    series[j] = series[j + s] - series[j];
                }
                endIndex += s;
            }
            for (int i = 0; i < d; i++)
            {
                for (int j = endIndex - 1; j >= 0; j--)
                {
                    series[j] = series[j + 1] - series[j];
                }
                endIndex += 1;
            }
        }

        public static void DrawAutocorrelation(List<double> listAutocorrelation, double confidenceLimit)
        {
            List<double> listConfidenceLimit = new List<double>();
            for(int i=0; i<listAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);
        }

        public static void DrawAutocorrelation(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            Plot_Form form = new Plot_Form();
            form.chart1.Titles["Title1"].Text = "Autocorrelation Function";
            form.chart1.ChartAreas["ChartArea1"].Axes[0].Title = "Lag";
            form.chart1.ChartAreas["ChartArea1"].Axes[1].Title = "ACF";

            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            //series1.Points.AddXY(0.0, limitAutocorrelation);
            series1.IsVisibleInLegend = false;

            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            //series2.Points.AddXY(0.0, -limitAutocorrelation);
            series2.IsVisibleInLegend = false;


            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                series.Color = System.Drawing.Color.Blue;
                series.Points.AddXY(i, 0.0);
                series.Points.AddXY(i, listAutocorrelation[i]);
                series.IsVisibleInLegend = false;
                form.chart1.Series.Add(series);

                series1.Points.AddXY(i, listConfidenceLimit[i]);
                series2.Points.AddXY(i, -listConfidenceLimit[i]);
            }

            form.chart1.Series.Add(series1);
            form.chart1.Series.Add(series2);

            form.ShowDialog();
        }
        
        public static void DrawSeriesData(List<double> series, int dataSize)
        {
            Plot_Form form = new Plot_Form();
            form.chart1.Series["Data"].Color = System.Drawing.Color.Blue;
            for (int i = 0; i < dataSize; i++)
            {
                form.chart1.Series["Data"].Points.AddXY(i + 1, series.ElementAt(i));
            }
            form.ShowDialog();
        }

        public static void ComputeAutocorrelation(List<double> series, List<double> listAutocorrelation)
        {
            int numAutocorrelation = (series.Count / 4 > 50 ? 50 : series.Count / 4);
            listAutocorrelation.Add(1);

            double mean = 0;
            for (int i = 0; i < series.Count; i++)
            {
                mean += series[i];
            }
            mean /= series.Count;

            double variance = 0;
            for (int i = 0; i < series.Count; i++)
            {
                variance += Math.Pow(series[i] - mean, 2);
            }
            variance /= series.Count;

            for (int lag = 1; lag < numAutocorrelation; lag++)
            {
                double temp = 0;
                for (int i = 0; i < series.Count - lag; i++)
                {
                    temp += (series[i] - mean) * (series[i + lag] - mean);
                }
                temp /= series.Count * variance;
                listAutocorrelation.Add(temp);
            }
        }

        public static void ComputeConfidenceLimit(List<double> listAutocorrelation, int dataSize, List<double> listConfidenceLimit)
        {
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

        public static double getPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j)
        {
            int index = 1;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j+1);
            }
            return listPartialCorrelation[index];
        }

        public static void setPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j, double value)
        {
            int index = 1;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j+1);
            }
            listPartialCorrelation[index] = value;
        }

        public static void ComputePartialAutocorrelation(List<double> listAutocorrelation, int dataSize, List<double> listPartialAutocorrelation)
        {
            int lag = (dataSize / 4 > 50 ? 50 : dataSize / 4);
            int numPartialCorrelation = (int)(lag * (lag + 1) / 2) + 1;
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
                    temp += getPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) * listAutocorrelation[i - j];
                }
                numerator = listAutocorrelation[i] - temp;
                temp = 0;
                for (int j = 1; j <= i - 1; j++)
                {
                    temp += getPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) * listAutocorrelation[j];
                }
                denominator = 1 - temp;
                temp = numerator / denominator;
                setPartialCorrelationAt(listPartialAutocorrelation, i, i, temp);

                for (int j = 1; j < i; j++)
                {
                    temp = getPartialCorrelationAt(listPartialAutocorrelation, i - 1, j) - getPartialCorrelationAt(listPartialAutocorrelation, i, i) * getPartialCorrelationAt(listPartialAutocorrelation, i - 1, i - j);
                    setPartialCorrelationAt(listPartialAutocorrelation, i, j, temp);
                }
            }

            List<double> result = new List<double>();
            result.Add(0);
            for (int i = 1; i < lag; i++)
            {
                result.Add(getPartialCorrelationAt(listPartialAutocorrelation, i, i));
            }
            listPartialAutocorrelation.Clear();
            for (int i = 1; i < lag; i++)
            {
                listPartialAutocorrelation.Add(result[i]);
            }
        }

        public static void ComputeSeason(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int seasonPartern)
        {
            List<int> listHightFreLocation = new List<int>();
            List<int> listHightPosFreLocation = new List<int>();
            List<int> listHightNegFreLocation = new List<int>();
            List<int> levelOneLocation = new List<int>();
            seasonPartern = 0;

            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (listAutocorrelation[i] > listConfidenceLimit[i]*1.2)
                {
                    listHightFreLocation.Add(i);
                    listHightPosFreLocation.Add(i);
                }
                else if (listAutocorrelation[i] < -listConfidenceLimit[i]*1.2)
                {
                    listHightFreLocation.Add(i);
                    listHightNegFreLocation.Add(i);
                }
            }

            List<int> listSignificantDistance = new List<int>();
            for (int i = 0; i < listHightPosFreLocation.Count - 1; i++)
            {
                if (listHightPosFreLocation[i + 1] - listHightPosFreLocation[i] != 1)
                {
                    listSignificantDistance.Add(listHightPosFreLocation[i + 1] - listHightPosFreLocation[i]);
                }
            }
            for (int i = 0; i < listHightNegFreLocation.Count - 1; i++)
            {
                if (listHightNegFreLocation[i + 1] - listHightNegFreLocation[i] != 1)
                {
                    listSignificantDistance.Add(listHightNegFreLocation[i + 1] - listHightNegFreLocation[i]);
                }
            }

            List<int> listDistinctSignificantLocation = listSignificantDistance.Distinct().ToList();
            if (listDistinctSignificantLocation.Count == 0)
            {
                return;
            }
            int hightFrequencyDistance = listDistinctSignificantLocation.ElementAt(0);
            int frequency = listSignificantDistance.Count(item => item == hightFrequencyDistance);
            for (int i = 1; i < listDistinctSignificantLocation.Count; i++)
            {
                int newFrequency = listSignificantDistance.Count(item => item == listDistinctSignificantLocation[i]);
                if ((newFrequency > frequency) || (newFrequency == frequency && listDistinctSignificantLocation[i] > hightFrequencyDistance))
                {
                    hightFrequencyDistance = listDistinctSignificantLocation[i];
                    frequency = newFrequency;
                }
            }

            if (1.0 * frequency / listDistinctSignificantLocation.Count > 0.5)
            {
                seasonPartern = hightFrequencyDistance;
                return;
            }

            #region continue
            //tempList.Clear();
            //for (int i = 0; i < listAutocorrelationLocation.Count - 1; i++)
            //{
            //    levelLocation.Add(listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i]);
            //    if (listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i] != 1)
            //    {
            //        tempList.Add(listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i]);
            //    }
            //}

            //tempList1 = tempList.Distinct().ToList();
            //if (tempList1.Count == 0)
            //{
            //    return;
            //}
            //hightFrequencyValue = tempList1.ElementAt(0);
            //frequency = tempList.Count(item => item == hightFrequencyValue);
            //for (int i = 1; i < tempList1.Count; i++)
            //{
            //    int newFrequency = tempList.Count(item => item == tempList1[i]);
            //    if (newFrequency > frequency)
            //    {
            //        hightFrequencyValue = i;
            //        frequency = newFrequency;
            //    }
            //}

            //if (1.0 * frequency / tempList1.Count > 0.5)
            //{
            //    season = hightFrequencyValue;
            //    return;
            //}
            #endregion

        }

        public static DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, double confidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            listHighAutocorrelation = listAutocorrelation.FindAll(item => Math.Abs(item) > confidenceLimit).ToList();
            double averageRateExchange = 0;
            if (listHighAutocorrelation.Count <= 1)
            {
                decayPartern = DecayPartern.ABRUPT_DECAY;
                return decayPartern;
            }

            for (int i = 0; i < listHighAutocorrelation.Count-1; i++)
            {
                averageRateExchange += Math.Abs(Math.Abs(listHighAutocorrelation[i]) - Math.Abs(listHighAutocorrelation[i + 1])) / Math.Abs(listHighAutocorrelation[i]);
            }
            averageRateExchange /= (listHighAutocorrelation.Count -1);

            if (averageRateExchange > 0.65)
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

        public static DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i]))
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
            averageRateExchange /= (listHighAutocorrelation.Count -1);

            if (averageRateExchange > 0.65)
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

        public static void RemoveNonstationarity(List<double> series, ref int dataSize, out int regularDifferencingLevel)
        {
            int endIndex = dataSize;           
            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();

            regularDifferencingLevel = 0;
            while (true)
            {
                listAutocorrelation.Clear();
                listConfidenceLimit.Clear();
                ComputeAutocorrelation(series, listAutocorrelation);
                ComputeConfidenceLimit(listAutocorrelation, endIndex, listConfidenceLimit);
                DecayPartern decayPartern = ComputeDecayPartern(listAutocorrelation, listConfidenceLimit);
                if (decayPartern != DecayPartern.SLOW_DECAY)
                {
                    break;
                }

                endIndex -= 1;
                regularDifferencingLevel++;
                for (int j = 0; j < endIndex; j++)
                {
                    series[j] = series[j + 1] - series[j];
                }
            }

            dataSize -= regularDifferencingLevel;
        }

        public static void RemoveSeasonality(List<double> series, ref int dataSize, out int seasonPartern, out int seasonDifferencingLevel)
        {
            seasonPartern = 0;
            seasonDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            List<int> listAutocorrelationLocation = new List<int>();
            List<int> levelLocation = new List<int>();

            int endIndex = dataSize;

            while (true)
            {
                listAutocorrelation.Clear();
                listConfidenceLimit.Clear();
                ComputeAutocorrelation(series, listAutocorrelation);
                ComputeConfidenceLimit(listAutocorrelation, endIndex, listConfidenceLimit);
                int newSeasonPartern;
                ComputeSeason(listAutocorrelation, listConfidenceLimit, out newSeasonPartern);
                if (newSeasonPartern == 0)
                {
                    break;
                }
                seasonPartern = newSeasonPartern;
                seasonDifferencingLevel++;
                endIndex -= seasonPartern;
                for (int j = 0; j < endIndex; j++)
                {
                    series[j] = series[j + seasonPartern] - series[j];
                }
            }

            dataSize -= seasonPartern * seasonDifferencingLevel;
        }

        public static void GetLastSignificant(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 1; i < listAutocorrelation.Count; i++)
            {
                if (listAutocorrelation[i] > listConfidenceLimit[i] || listAutocorrelation[i] <- listConfidenceLimit[i])
                {
                    lag = i + 1;
                }
            }
        }

        public static void GetLastSignificant(List<double> listAutocorrelation, double confidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 1; i < listAutocorrelation.Count; i++)
            {
                if (listAutocorrelation[i] > confidenceLimit || listAutocorrelation[i] < -confidenceLimit)
                {
                    lag = i + 1;
                }
            }
        }

        public static void ComputeArima(List<double> series, int dataSize, int seasonPartern, out int p, out int q, out int P, out int Q)
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

            ComputeAutocorrelation(series, listAutocorrelation);
            ComputePartialAutocorrelation(listAutocorrelation, dataSize, listPartialAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, dataSize, listConfidenceLimit);
            double confidenceLimit = 1.96 / Math.Sqrt(dataSize);

            for (int i = 0; i < seasonPartern; i++)
            {
                listRegularAutocorrelation.Add(listAutocorrelation[i]);
                listRegularConfidenceLimit.Add(listConfidenceLimit[i]);
                listRegularPartialCorrelation.Add(listPartialAutocorrelation[i]);
            }

            for (int i = 0; i < Math.Floor(1.0 * listAutocorrelation.Count / seasonPartern); i++)
            {
                listSeasonAutocorrelation.Add(listAutocorrelation[i * seasonPartern]);
                listSeasonConfidenceLimit.Add(listConfidenceLimit[i * seasonPartern]);
                listSeasonPartialCorrelation.Add(listPartialAutocorrelation[i * seasonPartern]);
            }

            //DrawAutocorrelation(listRegularAutocorrelation, listRegularConfidenceLimit);
            //DrawAutocorrelation(listRegularPartialCorrelation, confidenceLimit);
            //DrawAutocorrelation(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            //DrawAutocorrelation(listSeasonPartialCorrelation, confidenceLimit);

            DecayPartern decayACF = ComputeDecayPartern(listRegularAutocorrelation, listRegularConfidenceLimit);
            DecayPartern decayPACF = ComputeDecayPartern(listRegularPartialCorrelation, confidenceLimit);
            DecayPartern decaySeasonACF = ComputeDecayPartern(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            DecayPartern decaySeasonPACF = ComputeDecayPartern(listSeasonPartialCorrelation, confidenceLimit);

            if (decayACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularAutocorrelation, listRegularConfidenceLimit, out p);
            }
            if (decayPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularPartialCorrelation, confidenceLimit, out q);
            }
            if (decayACF != DecayPartern.ABRUPT_DECAY && decayPACF != DecayPartern.ABRUPT_DECAY && p * q != 0)
            {
                p = q = 1;
            }
            if (decaySeasonACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonAutocorrelation, listSeasonConfidenceLimit, out P);
            }
            if (decaySeasonPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonPartialCorrelation, confidenceLimit, out Q);
            }
            if (decaySeasonACF != DecayPartern.ABRUPT_DECAY && decaySeasonPACF != DecayPartern.ABRUPT_DECAY && P * Q != 0)
            {
                P = Q = 1;
            }
            int test = 0;
        }
    }
}
