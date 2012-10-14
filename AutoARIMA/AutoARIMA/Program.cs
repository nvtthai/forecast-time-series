using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            List<double> originSeries = new List<double>();
            List<double> errorsSeason = new List<double>();
            List<double> errors = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            try
            {
                file = new System.IO.StreamReader(fileName);
                while ((line = file.ReadLine()) != null)
                {
                    originSeries.Add(Double.Parse(line));
                    errorsSeason.Add(0);
                    errors.Add(0);
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                originSeries = null;
            }

            List<double> listAutocorrelation = new List<double>();
            List<double> listPartialAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            List<double> listArimaCoeff = new List<double>();
            List<double> listSeasonArimaCoeff = new List<double>();
            int regularDifferencingLevel = 0;
            int seasonPartern = 0;
            int seasonDifferencingLevel = 0;
            int startIndex = 0;
            int dataSize = originSeries.Count;
            int p, q, P, Q;
            double confidenceLimit;
            int nHead = 70;

            List<double> series = originSeries.FindAll(item => true);
            WriteLogSeries("Original data");
            WriteLogSeries(series);

            RemoveNonstationarity(series, ref startIndex, out regularDifferencingLevel);

            //RemoveSeasonality(series, ref startIndex, out seasonPartern, out seasonDifferencingLevel);

            ComputeArima(series, startIndex, seasonPartern, out p, out q, out P, out Q);

            //p = q = 4;

            EstimateRegularARMA(series, startIndex, p, q, listArimaCoeff);
            //EstimateSeasonARMA(series, startIndex, seasonPartern, P, Q, listSeasonArimaCoeff);

            TestRegularARMA(series, errors, startIndex, regularDifferencingLevel, p, q, listArimaCoeff);
            ForecastRegularARMA(series, errors, startIndex, regularDifferencingLevel, p, q, listArimaCoeff, nHead);

            int x = 0;
        }

        public static int ComputeMax(params int[] data)
        {
            int max = data.Max();
            return max;
        }

        public static void WriteLogSeries(List<double> series)
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

        public static void WriteLogSeries(string log)
        {
            StreamWriter file = new StreamWriter("result_test.dat", true);
            file.WriteLine(log);
            file.Flush();
            file.Close();
        }

        public static void TestARMA(List<double> series, int startIndex, int diff, int seasonDiff, int pCoef, int qCoef, int season, int PCoef, int QCoef, List<double> listArimaCoeff, List<double> listSeasonArimaCoeff)
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

        public static void ForecastARMA(List<double> series, List<double> errors, int startIndex, int diff, int seasonDiff, int pCoef, int qCoef, int season, int PCoef, int QCoef, List<double> listArimaCoeff, List<double> listSeasonArimaCoeff, int nHead)
        {
            List<double> regularSeries = new List<double>();
            List<double> seasonSeries = new List<double>();
            List<double> forecastSeries = series.FindAll(item => true);

            int begin = series.Count;

            for (int i = 0; i < nHead; i++)
            {
                forecastSeries.Add(0);
                errors.Add(0);
            }

            for (int i = begin; i < forecastSeries.Count; i++)
            {
            }
        }

        //Don't change series, errors, listArimaCoeff
        //errors is compute and return
        public static void ForecastRegularARMA(List<double> series, List<double> errors, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff, int nHead)
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
                    forecastSeries[i] += forecastErrors[i - j] * listArimaCoeff[j+pCoef];
                }
                //errors.Add(series[i] - temp);
            }
            RevertDifference(originSeries, ref originIndex, diff, 0, 0);
            RevertDifference(forecastSeries, ref forecastIndex, diff, 0, 0);
            DrawTwoSeriesData(originSeries, originIndex, forecastSeries, forecastIndex);
        }

        //Don't change series, listArimaCoeff 
        //errors is compute and return
        public static void TestRegularARMA(List<double> series, List<double> errors, int startIndex, int diff, int pCoef, int qCoef, List<double> listArimaCoeff)
        {
            errors.Clear();
            List<double> originSeries = series.FindAll(item => true);
            List<double> testSeries = new List<double>();

            int originIndex = startIndex;
            int testIndex = startIndex;
            int begin = ComputeMax(startIndex, pCoef, qCoef);

            for (int i = 0; i < begin; i++)
            {
                testSeries.Add(originSeries[i]);
            }
            for (int i = begin; i < originSeries.Count; i++)
            {
                double temp = listArimaCoeff[0];
                for (int j = 1; j <= pCoef; j++)
                {
                    temp += originSeries[i - j] * listArimaCoeff[j];
                }
                //In test mode all priori error is 0
                //for (int j = 1; j <= qCoef; j++)
                //{
                //    temp += testErrors[i - j] * listArimaCoeff[j + pCoef];
                //}
                testSeries.Add(temp);
            }
            RevertDiffTestSeries(originSeries, testSeries, ref originIndex, diff, 0, 0);
            for (int i = 0; i < originSeries.Count; i++)
            {
                errors.Add(originSeries[i] - testSeries[i]);
            }
            DrawTwoSeriesData(originSeries, originIndex, testSeries, testIndex);
        }

        public static void RevertDiffTestSeries(List<double> series, List<double> testSeries, ref int startIndex, int d, int D, int s)
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

        #region backup code

        public static void ComputeSeason(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int seasonPartern)
        {
            List<int> listHightFreLocation = new List<int>();
            List<int> levelOneDistance = new List<int>();
            seasonPartern = 0;
            double averageSignificantCorrelation = 0.0;
            int numSignificantCorrelation = 0;
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (listAutocorrelation[i] > listConfidenceLimit[i] || listAutocorrelation[i] < -listConfidenceLimit[i])
                {
                    averageSignificantCorrelation += Math.Abs(listAutocorrelation[i]);
                    numSignificantCorrelation++;
                }
            }
            averageSignificantCorrelation /= numSignificantCorrelation;

            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > averageSignificantCorrelation)
                {
                    listHightFreLocation.Add(i);
                }
            }

            for (int i = 0; i < listHightFreLocation.Count - 1; i++)
            {
                levelOneDistance.Add(listHightFreLocation[i + 1] - listHightFreLocation[i]);
            }

            List<int> listDistinctLocation = levelOneDistance.Distinct().ToList();

            //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);

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

        public static void GetLastSignificant(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 1; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i])*1.3)
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
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(confidenceLimit) * 1.3)
                {
                    lag = i + 1;
                }
            }
        }

        #endregion

        public static void ComputeSeason(List<double> listAutocorrelation, out int seasonPartern)
        {
            seasonPartern = 0;
            List<int> listHighestCorrelationIndex = new List<int>();
            ComputeHighestCorrelation(listAutocorrelation, 0, listHighestCorrelationIndex);
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

        public static void ComputeHighestCorrelation(List<double> listAutocorrelation, int startIndex, List<int> listHighestCorrelationIndex)
        {
            if(startIndex >= listAutocorrelation.Count)
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

            ComputeHighestCorrelation(listAutocorrelation, startIndex + 1, listHighestCorrelationIndex);

        }

        //Tested: OK
        public static void DrawSeriesData(List<double> series, int startIndex)
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

        //Tested: OK
        public static void DrawTwoSeriesData(List<double> firstSeries, int firstStartIndex, List<double> secondSeries, int secondStartIndex)
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

        //Tested: OK
        public static void ComputeDifference(List<double> series, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < d; i++)
            {
                startIndex += 1;
                for (int j = series.Count-1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j-1];
                }
            }

            for (int i = 0; i < D; i++)
            {
                startIndex += s;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j-s];
                }
            }
        }

        //Tested: OK
        public static void RevertDifference(List<double> series, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < D; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j-s];
                }
                startIndex -= s;
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j-1];
                }
                startIndex -= 1;
            }
        }

        //Tested: OK
        public static void ComputeAutocorrelation(List<double> series, int startIndex, List<double> listAutocorrelation)
        {
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

            listAutocorrelation.Clear();
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

        //Tested: OK
        public static void ComputeConfidenceLimit(List<double> listAutocorrelation, int dataSize, List<double> listConfidenceLimit)
        {
            listConfidenceLimit.Clear();
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

        //Tested: OK
        public static double GetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            return listPartialCorrelation[index];
        }

        //Tested: OK
        public static void SetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j, double value)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            listPartialCorrelation[index] = value;
        }

        //Tested: OK
        public static void ComputePartialAutocorrelation(List<double> listAutocorrelation, List<double> listPartialAutocorrelation)
        {
            int lag = listAutocorrelation.Count;
            int numPartialCorrelation = (int)(lag * (lag + 1) / 2);
            listPartialAutocorrelation.Clear();
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

        //Tested: OK
        public static void DrawPartialAutocorrelation(List<double> listAutocorrelation, double confidenceLimit)
        {
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            DrawAutocorrelation(listAutocorrelation, listConfidenceLimit, true);
        }

        //Tested: OK
        public static void DrawAutocorrelation(List<double> listAutocorrelation, List<double> listConfidenceLimit, bool isPACF = false)
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

        //Tested: SOSO
        public static DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, double confidenceLimit)
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

        //Tested: SOSO
        public static DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i])*1.3)
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

        //Tested: SOSO
        public static void RemoveNonstationarity(List<double> series, ref int startIndex, out int regularDifferencingLevel)
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
                ComputeAutocorrelation(series, startIndex, listAutocorrelation);
                ComputeConfidenceLimit(listAutocorrelation, dataSize, listConfidenceLimit);
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

        public static void RemoveSeasonality(List<double> series, ref int startIndex, out int seasonPartern, out int seasonDifferencingLevel)
        {
            seasonPartern = 0;
            seasonDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            List<int> levelLocation = new List<int>();

            while (true)
            {
                listAutocorrelation.Clear();
                ComputeAutocorrelation(series, startIndex, listAutocorrelation);

                //DrawSeriesData(series, startIndex);
                //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);

                int newSeasonPartern;
                ComputeSeason(listAutocorrelation, out newSeasonPartern);
                if (newSeasonPartern == 0 || (seasonPartern!=0 && newSeasonPartern!=seasonPartern))
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

        public static void EstimateRegularARMA(List<double> series, int startIndex, int pCoef, int qCoef, List<double> listArimaCoeff)
        {
            EstimateSeasonARMA(series, startIndex, 1, pCoef, qCoef, listArimaCoeff);
        }

        public static void EstimateSeasonARMA(List<double> series, int startIndex, int season, int pCoef, int qCoef, List<double> listArimaCoeff)
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
                observationVector[i, 0] = series[(i - 1)*season];
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
                    observationVector[j, 0] = series[i - j*season];
                }
                for (int j = 1; j < qCoef + 1; j++)
                {
                    if (i - j*season >= 0)
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

            for (int i = 0; i < 1 + pCoef + qCoef; i++)
            {
                listArimaCoeff.Add(parameterVector[i, 0]);
            }
        }

        public static void ComputeArima(List<double> series, int startIndex, int seasonPartern, out int p, out int q, out int P, out int Q)
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

            ComputeAutocorrelation(series, startIndex, listAutocorrelation);
            ComputePartialAutocorrelation(listAutocorrelation, listPartialAutocorrelation);
            ComputeConfidenceLimit(listAutocorrelation, series.Count - startIndex, listConfidenceLimit);
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
    
    }
}
