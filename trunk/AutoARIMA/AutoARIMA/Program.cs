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
        ABRUPT_DEAY
    }

    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"E:\PROJECT\FINAL PROJECT\Other\Test\airpass.dat";
            List<double> series = new List<double>();
            List<double> test = new List<double>();
            List<double> errors = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            try
            {
                file = new System.IO.StreamReader(fileName);
                while ((line = file.ReadLine()) != null)
                {
                    series.Add(Double.Parse(line));
                    test.Add(Double.Parse(line));
                    errors.Add(0);
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                series = null;
            }

            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            int regularDifferencingLevel = 0;
            int seasonPartern = 0;
            int seasonDifferencingLevel = 0;
            int dataSize = series.Count;

            //RemoveNonstationarity(series, ref dataSize, out regularDifferencingLevel);
            //RemoveSeasonality(series, ref dataSize, out seasonPartern, out seasonDifferencingLevel);     
            //ComputeDifference(series, 0, 1, 12);
            DrawSeriesData(series, dataSize);
            ComputeAutocorrelation(series, listAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, dataSize, listConfidenceLimit);
            DrawAutocorrelation(listAutocorrelation, 0.3);
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

        public static void ComputeARMA(List<double> series, List<double> errors, int pCoef, int qCoef)
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

        public static void ComputePartialAutocorrelation(List<double> series, List<double> listAutocorrelation, List<double> listPartialAutocorrelation)
        {
            int lag = (series.Count / 4 > 50 ? 50 : series.Count / 4);
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

        public static void ComputeSeason(List<double> listAutocorrelation, double limitAutocorrelation, ref int seasonPartern)
        {
            List<int> listAutocorrelationLocation = new List<int>();
            List<int> levelLocation = new List<int>();
            int newSeasonPartern = 0;

            for(int i=0; i<listAutocorrelation.Count; i++)
            {
                if(listAutocorrelation[i]>limitAutocorrelation)
                {
                    listAutocorrelationLocation.Add(i);
                }               
            }

            List<int> tempList = new List<int>();
            for (int i = 0; i < listAutocorrelationLocation.Count - 1; i++)
            {
                levelLocation.Add(listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i]);
                if (listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i] != 1)
                {
                    tempList.Add(listAutocorrelationLocation[i + 1] - listAutocorrelationLocation[i]);
                }
            }

            List<int> tempList1 = tempList.Distinct().ToList();
            if (tempList1.Count == 0)
            {
                return;
            }
            int hightFrequencyValue = tempList1.ElementAt(0);
            int frequency = tempList.Count(item => item == hightFrequencyValue);
            for (int i = 1; i < tempList1.Count; i++)
            {
                int newFrequency = tempList.Count(item => item == tempList1[i]);
                if (newFrequency > frequency)
                {
                    hightFrequencyValue = i;
                    frequency = newFrequency;
                }
            }

            if (1.0 * frequency / tempList1.Count > 0.5)
            {
                newSeasonPartern = hightFrequencyValue;
                if(seasonPartern == 0 || (seasonPartern != 0 && newSeasonPartern == seasonPartern))
                {
                    seasonPartern = newSeasonPartern;
                }
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

        public static DecayPartern ComputeDecayParterb(List<double> listAutocorrelation, double confidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            listHighAutocorrelation = listAutocorrelation.FindAll(item => item > confidenceLimit).ToList();
            double averageRateExchange = 0;
            for (int i = 0; i < listHighAutocorrelation.Count-1; i++)
            {
                averageRateExchange += Math.Abs(Math.Abs(listHighAutocorrelation[i]) - Math.Abs(listHighAutocorrelation[i + 1])) / Math.Abs(listHighAutocorrelation[i]);
            }
            averageRateExchange /= listHighAutocorrelation.Count;
            if (listHighAutocorrelation.Count == 1)
            {
                decayPartern = DecayPartern.ABRUPT_DEAY;
            }
            else if (averageRateExchange > 0.65)
            {
                decayPartern = DecayPartern.ABRUPT_DEAY;
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

        public static DecayPartern ComputeDecayParterb(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (listAutocorrelation[i] > listConfidenceLimit[i])
                {
                    listHighAutocorrelation.Add(listAutocorrelation[i]);
                }
            }
            double averageRateExchange = 0;
            for (int i = 0; i < listHighAutocorrelation.Count - 1; i++)
            {
                averageRateExchange += Math.Abs(Math.Abs(listHighAutocorrelation[i]) - Math.Abs(listHighAutocorrelation[i + 1])) / Math.Abs(listHighAutocorrelation[i]);
            }
            averageRateExchange /= listHighAutocorrelation.Count;
            if (listHighAutocorrelation.Count == 1)
            {
                decayPartern = DecayPartern.ABRUPT_DEAY;
            }
            else if (averageRateExchange > 0.65)
            {
                decayPartern = DecayPartern.ABRUPT_DEAY;
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
            regularDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            double limitAutocorrelation = 1.96 * Math.Sqrt(1.0 / series.Count);
            int endIndex = dataSize;

            while (true)
            {
                listAutocorrelation.Clear();
                ComputeAutocorrelation(series, listAutocorrelation);
                DecayPartern decayPartern = ComputeDecayParterb(listAutocorrelation, limitAutocorrelation);
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
            List<int> listAutocorrelationLocation = new List<int>();
            List<int> levelLocation = new List<int>();

            double limitAutocorrelation = 1.96 * Math.Sqrt(1.0 / series.Count);
            int endIndex = dataSize;

            while (true)
            {
                listAutocorrelation.Clear();
                ComputeAutocorrelation(series, listAutocorrelation);
                ComputeSeason(listAutocorrelation, limitAutocorrelation, ref seasonPartern);
                if (seasonPartern == 0)
                {
                    break;
                }
                seasonDifferencingLevel++;
                endIndex -= seasonPartern;
                for (int j = 0; j < endIndex; j++)
                {
                    series[j] = series[j + seasonPartern] - series[j];
                }
            }

            dataSize -= seasonPartern * seasonDifferencingLevel;
        }

    }
}
