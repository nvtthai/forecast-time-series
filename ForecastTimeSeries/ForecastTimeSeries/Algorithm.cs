using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastTimeSeries
{
    class Algorithm
    {
        #region compute error
        
        public static double ComputeMAE(List<double> errorSeries)
        {
            double result = 0;
            foreach (double item in errorSeries)
            {
                result += Math.Abs(item);
            }
            result /= errorSeries.Count;
            return result;
        }

        public static double ComputeMAE(List<double> processSeries, List<double> testSeries)
        {
            double result = 0;
            for (int i = 0; i < processSeries.Count; i++)
            {
                result += Math.Abs(processSeries[i] - testSeries[i]);
            }
            result /= processSeries.Count;
            return result;
        }

        public static double ComputeSSE(List<double> errorSeries)
        {
            double result = 0;
            foreach (double item in errorSeries)
            {
                result += Math.Pow(item, 2);
            }
            return result;
        }

        public static double ComputeSSE(List<double> processSeries, List<double> testSeries)
        {
            double result = 0;
            for (int i = 0; i < processSeries.Count; i++)
            {
                result += Math.Pow(processSeries[i] - testSeries[i], 2);
            }
            return result;
        }

        public static double ComputeMSE(List<double> errorSeries)
        {
            double result = 0;
            foreach (double item in errorSeries)
            {
                result += Math.Pow(item, 2);
            }
            result /= errorSeries.Count;
            return result;
        }

        public static double ComputeMSE(List<double> processSeries, List<double> testSeries)
        {
            double result = 0;
            for (int i = 0; i < processSeries.Count; i++)
            {
                result += Math.Pow(processSeries[i] - testSeries[i], 2);
            }
            result /= processSeries.Count;
            return result;
        }

        public static double ComputeMAPE(List<double> processSeries, List<double> testSeries)
        {
            double result = 0;
            for (int i = 1; i < processSeries.Count; i++)
            {
                double temp = Math.Abs((processSeries[i] - testSeries[i]) / processSeries[i]);
                if (double.IsNaN(temp))
                {
                    temp = 1.0;
                }
                temp = Math.Min(temp, 1.0);
                result += Math.Abs(temp);
            }
            result = result*100/processSeries.Count;
            return result;
        }

        #endregion compute error


        #region statistic library

        public static void ComputeAutocorrelation(List<double> series, out List<double> listAutocorrelation)
        {
            listAutocorrelation = new List<double>();
            int numAutocorrelation = (series.Count / 4 > 50 ? 50 : series.Count / 4);

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

            listAutocorrelation.Add(1);
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


        public static void ComputeAutocorrelation(List<double> series, int startIndex, out List<double> listAutocorrelation)
        {
            List<double> processSeries = new List<double>();
            for (int i = startIndex; i < series.Count; i++)
            {
                processSeries.Add(series[i]);
            }
            ComputeAutocorrelation(processSeries, out listAutocorrelation);
        }

        public static void ComputeConfidenceLimit(List<double> listAutocorrelation, int dataSize, out List<double> listConfidenceLimit)
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

        public static double GetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            return listPartialCorrelation[index];
        }

        public static void SetPartialCorrelationAt(List<double> listPartialCorrelation, int i, int j, double value)
        {
            int index = 0;
            if (i > 1)
            {
                index = (int)(i * (i - 1) / 2 + i - j);
            }
            listPartialCorrelation[index] = value;
        }

        public static void ComputePartialAutocorrelation(List<double> listAutocorrelation, out List<double> listPartialAutocorrelation)
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

        public static void ComputeDifference(ref List<double> series, ref int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
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

        public static void RevertDifference(ref List<double> series, ref int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
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

        #endregion


        #region draw data

        public static void DrawPartialAutocorrelation(List<double> listPartialAutocorrelation, double confidenceLimit)
        {
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listPartialAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            DrawAutocorrelation(listPartialAutocorrelation, listConfidenceLimit, true);
        }

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

        public static void DrawForecastSeriesData(List<double> firstSeries, int firstStartIndex, List<double> secondSeries, int secondStartIndex)
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

            series2.Points.AddXY(firstSeries.Count, firstSeries[firstSeries.Count - 1]);
            for (int i = secondStartIndex; i < secondSeries.Count; i++)
            {
                series2.Points.AddXY(i + 1 + firstSeries.Count - secondStartIndex, secondSeries[i]);
            }
            form.chart1.Series.Add(series2);

            form.ShowDialog();
        }

        public static void DrawTwoSeriesTestData(List<double> dataSeries, int firstStartIndex, List<double> testSeries, int secondStartIndex)
        {
            double MAE = Algorithm.ComputeMAE(dataSeries, testSeries);
            double MSE = Algorithm.ComputeMSE(dataSeries, testSeries);
            double MAPE = Algorithm.ComputeMAPE(dataSeries, testSeries);

            Test_Form form = new Test_Form();
            form.textBox1.AppendText("Mean Absolute Error MAE =  " + MAE + "\n");
            form.textBox1.AppendText("Mean Square Error MSE =  " + MSE + "\n");
            form.textBox1.AppendText("Mean absolute percentage Error MAPE =  " + MAPE + "\n");

            //Mean absolute percentage
            form.textBox1.ReadOnly = true;
            for (int t = firstStartIndex; t < dataSeries.Count; t++)
            {
                form.chart1.Series["Observations"].Points.AddXY(t + 1 - firstStartIndex, dataSeries[t]);
            }
            for (int t = secondStartIndex; t < testSeries.Count; t++)
            {
                form.chart1.Series["Computations"].Points.AddXY(t + 1 - secondStartIndex, testSeries[t]);
            }
            form.ShowDialog();
        }

        #endregion draw data


        #region test

        public static void WriteSeries(List<double> series, string filename)
        {
            StreamWriter file = new StreamWriter(filename, false);
            foreach (double data in series)
            {
                file.WriteLine(data);
            }
            file.Flush();
            file.Close();
        }


        #endregion test
    }
}
