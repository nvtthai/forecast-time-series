using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForecastTimeSeries
{
    public partial class MainInterface : Form
    {
        private void WriteSeries(List<double> series, string filename)
        {
            StreamWriter file = new StreamWriter(filename, false);
            foreach (double data in series)
            {
                file.WriteLine(data);
            }
            file.Flush();
            file.Close();
        }

        private string m_DataFile;
        private ARIMA ARIMAModel;
        private Neural NeuralModel;
        List<double> _dataSeries;
        List<double> _errorSeries;

        public MainInterface()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            _dataSeries = new List<double>();
            _errorSeries = new List<double>();
            ARIMAModel = new ARIMA();
            NeuralModel = new Neural();
        }

        private void btnChooseData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open File";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_DataFile = openDialog.FileName;
            }
            else
            {
                return;
            }

            if (m_DataFile == null || m_DataFile.Equals(""))
            {
                MessageBox.Show("Please choose validate data file before training", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.IO.StreamReader fileInput = null;
                string line = null;
                int numRows = 0;
                int numColumns = 0;
                try
                {
                    fileInput = new System.IO.StreamReader(m_DataFile);
                    while ((line = fileInput.ReadLine()) != null)
                    {
                        if (numRows == 0)
                        {
                            char[] delimiterChars = { ' ', ',' };
                            string[] words = line.Split(delimiterChars);
                            numColumns = words.Length;
                        }
                        numRows++;
                    }
                    this.labelTrainDataNumColumns.Text = Convert.ToString(numColumns);
                    this.labelTrainDataNumRows.Text = Convert.ToString(numRows);

                    this.txtTrainDataColumn.Text = "1";
                    this.txtTrainDataFromRow.Text = "1";
                    this.txtTrainDataToRow.Text = Convert.ToString(numRows);

                    if (numColumns == 1)
                    {
                        this.txtTrainDataColumn.Enabled = false;
                    }
                    else
                        this.txtTrainDataColumn.Enabled = true;
                }
                catch (System.OutOfMemoryException outOfMemory)
                {
                    MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.IO.IOException io)
                {
                    MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.Exception excp)
                {
                    MessageBox.Show("Input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (fileInput != null)
                        fileInput.Close();
                }
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            _dataSeries.Clear();
            System.IO.StreamReader file = null;
            string line = null;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.txtTrainDataFromRow.Text);
            int endRow = Convert.ToInt32(this.txtTrainDataToRow.Text);
            int columnSelected = Convert.ToInt32(this.txtTrainDataColumn.Text);
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(m_DataFile);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    string[] words = line.Split(delimiterChars);
                    if (columnSelected <= words.Length)
                    {
                        _dataSeries.Add(Double.Parse(words[columnSelected - 1]));
                    }
                    else
                    {
                        isFormatFileRight = false;
                        break;
                    }
                }
                if (!isFormatFileRight)
                {
                    MessageBox.Show("Input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _dataSeries = null;
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                _dataSeries = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.IO.IOException io)
            {
                _dataSeries = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception excp)
            {
                _dataSeries = null;
                MessageBox.Show("Input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            if (_dataSeries != null)
            {
                //m_DemoDataSeries.loadData(sample);
                ARIMAModel.SetData(_dataSeries);
                showData();
                //this.m_DemoDataSeries.preProcessList.Clear(); //clear for new data
            }
            else
            {
                MessageBox.Show("Load data fail", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showData()
        {
            this.richTextProcessData.Clear();
            this.richTextProcessData.AppendText("Time Series\n");
            this.richTextProcessData.AppendText(String.Format("{0,10}", "TIME"));
            this.richTextProcessData.AppendText(String.Format("{0,20}", "VALUE\n\n"));
            for (int t = 0; t < _dataSeries.Count; t++)
            {
                this.richTextProcessData.AppendText(String.Format("{0,10}", "[" + (t + 1) + "]"));
                this.richTextProcessData.AppendText(String.Format("{0,20}", _dataSeries.ElementAt(t)) + "\n");
            }
        }

        private void showARIMAModel()
        {
            string model;
            ARIMAModel.GetModel(out model);
            this.richARIMAModel.Text = model.ToString();
            this.richARIMAModel.ScrollToCaret();
        }

        private void radioBackPropagation_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAlgorithmConfig.Enabled = true;
            groupBoxAlgorithmConfig.Text = "Back Propagation Config";
            labelConfig1.Text = "Learning Rate";
            labelConfig2.Text = "Momemtum";

            txtConfig1.Text = 0.4.ToString();
            txtConfigEpoches.Text = 1000.ToString();
            txtConfig2.Text = 0.2.ToString();
            txtConfigErrors.Text = 0.000001.ToString();
        }

        private void radioRPROP_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAlgorithmConfig.Enabled = true;
            groupBoxAlgorithmConfig.Text = "Resilient Propagation Config";
            labelConfig1.Text = "Default Update Value";
            labelConfig2.Text = "Max Update Value";

            txtConfig1.Text = 0.1.ToString();
            txtConfigEpoches.Text = 1000.ToString();
            txtConfig2.Text = 10.ToString();
            txtConfigErrors.Text = 0.000001.ToString();
        }

        private void btnAutomaticARIMA_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxARIMAParameter.Enabled = false;
        }

        private void btnManualARIMA_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxARIMAParameter.Enabled = true;
            txtRegularDifferencing.Text = "0";
            txtSeasonDifferencing.Text = "0";
            txtSeasonPartern.Text = "0";
            txtARRegular.Text = "0";
            txtMARegular.Text = "0";
            txtARSeason.Text = "0";
            txtMASeason.Text = "0";
        }

        private void btnRemoveSeason_Click(object sender, EventArgs e)
        {
            try
            {
                int regularDifferencingLevel = Int32.Parse(txtRegularDifferencing.Text);
                int seasonDifferencingLevel = Int32.Parse(txtSeasonDifferencing.Text);
                int seasonPartern = Int32.Parse(txtSeasonPartern.Text);
                ARIMAModel.RemoveTrendSeasonality(regularDifferencingLevel, seasonDifferencingLevel, seasonPartern);
            }
            catch
            {
            }
        }

        private void btnManualTrainingARIMA_Click(object sender, EventArgs e)
        {
            int regularDifferencing, pRegular, qRegular, pSeason, qSeason, seasonDifferencing, seasonPartern;
            try
            {
                pRegular = Int32.Parse(txtARRegular.Text);
                regularDifferencing = Int32.Parse(txtRegularDifferencing.Text);
                qRegular = Int32.Parse(txtMARegular.Text);

                pSeason = Int32.Parse(this.txtARSeason.Text);
                seasonDifferencing = Int32.Parse(this.txtSeasonDifferencing.Text);
                qSeason = Int32.Parse(this.txtMASeason.Text);

                seasonPartern = Int32.Parse(this.txtSeasonPartern.Text);
                ARIMAModel.ManualTraining(pRegular, regularDifferencing, qRegular, pSeason, seasonDifferencing, qSeason, seasonPartern);
                showARIMAModel();
                ARIMAModel.GetErrorSeries(out _errorSeries);
                WriteSeries(_errorSeries, "ErrorSeries.txt");
            }
            catch
            {
            }
        }

        private void btnManualRestoreARIMA_Click(object sender, EventArgs e)
        {
            txtRegularDifferencing.Text = "0";
            txtSeasonDifferencing.Text = "0";
            txtSeasonPartern.Text = "0";
            txtARRegular.Text = "0";
            txtMARegular.Text = "0";
            txtARSeason.Text = "0";
            txtMASeason.Text = "0";
            ARIMAModel.InitTraining();
        }

        private void btnAutomaticTrainingARIMA_Click(object sender, EventArgs e)
        {
            if (_dataSeries == null || _dataSeries.Count == 0)
            {
                MessageBox.Show("Please set data for training", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ARIMAModel.SetData(_dataSeries);
            ARIMAModel.AutomaticTraining();
            showARIMAModel();
            ARIMAModel.GetErrorSeries(out _errorSeries);
            WriteSeries(_errorSeries, "ErrorSeries.txt");
        }

        private void btnForecastARIMA_Click(object sender, EventArgs e)
        {
            List<double> forecastSeries;
            ARIMAModel.Forecast(30, out forecastSeries);
            Algorithm.DrawForecastSeriesData(_dataSeries, 0, forecastSeries, 0);
        }

        private void btnTestArima_Click(object sender, EventArgs e)
        {
            List<double> testSeries;
            ARIMAModel.GetTestSeries(out testSeries);
            Algorithm.DrawTwoSeriesTestData(_dataSeries, 0, testSeries, 0);
        }

        private void btnPlotDataARIMA_Click(object sender, EventArgs e)
        {
            ARIMAModel.DrawSeriesData();
        }

        private void btnCorrelogram_Click(object sender, EventArgs e)
        {
            ARIMAModel.DrawAutocorrelation();
        }

        private void btnPartialCorrelation_Click(object sender, EventArgs e)
        {
            ARIMAModel.DrawPartialAutocorrelation();
        }

        private void btnSaveARIMA_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save Arima Config File";
            saveDialog.DefaultExt = "xml";
            DialogResult result = saveDialog.ShowDialog();
            string dataFile = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dataFile = saveDialog.FileName;
                bool exportResult = ARIMAModel.Export(dataFile);
            }
            else
            {
                return;
            }
        }

        private void btnLoadARIMA_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Load Network Config File";
            fileDialog.DefaultExt = "xml";
            DialogResult result = fileDialog.ShowDialog();
            string dataFile = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dataFile = fileDialog.FileName;
                bool importResult = ARIMAModel.Import(dataFile);
                if (importResult)
                {
                    List<int> model;
                    ARIMAModel.GetModel(out model);
                    this.txtARRegular.Text = model[0].ToString();
                    this.txtRegularDifferencing.Text = model[1].ToString();
                    this.txtMARegular.Text = model[2].ToString();
                    this.txtARSeason.Text = model[3].ToString();
                    this.txtSeasonDifferencing.Text = model[4].ToString();
                    this.txtMASeason.Text = model[5].ToString();
                    this.txtSeasonPartern.Text = model[6].ToString();

                    this.groupBoxARIMAParameter.Enabled = true;
                }
            }
            else
            {
                return;
            }
        }


        private void btnNetworkNew_Click(object sender, EventArgs e)
        {
            string numInputs = this.txtNumInput.Text;
            string numHiddens = this.txtNumHidden.Text;
            string numOutputs = this.txtNumOutput.Text;

            if (numInputs == "" || numHiddens == "" || numOutputs == "")
            {
                System.Windows.Forms.MessageBox.Show("Please insert the number of Input Nodes, Hidden Nodes, Output Nodes", null, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }
            try
            {
                NeuralModel = new Neural(Int32.Parse(numInputs), Int32.Parse(numHiddens), Int32.Parse(numOutputs));
                NeuralModel.SetData(_errorSeries);
                System.Windows.Forms.MessageBox.Show("NetWork configuration successfull, You can train it");

                this.txtNumOutput.Enabled = false;
                this.txtNumHidden.Enabled = false;
                this.txtNumInput.Enabled = false;
                this.btnNetworkNew.Enabled = false;
                this.btnNetworkLoad.Enabled = false;
                this.btnNetworkSave.Enabled = true;
                this.btnNetworkClear.Enabled = true;
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
            }
        }

        private void btnNetworkLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Load Network Config File";
            fileDialog.DefaultExt = "xml";
            DialogResult result = fileDialog.ShowDialog();
            string dataFile = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dataFile = fileDialog.FileName;
                Neural temp = Neural.Import(dataFile);
                if (temp == null)
                {
                    System.Windows.Forms.MessageBox.Show("The Input file is not correct format !!!", null, System.Windows.Forms.MessageBoxButtons.OK);
                }
                else
                {
                    NeuralModel = temp;
                    NeuralModel.SetData(_errorSeries);

                    this.txtNumInput.Text = NeuralModel.m_iNumInputNodes.ToString();
                    this.txtNumHidden.Text = NeuralModel.m_iNumHiddenNodes.ToString();
                    this.btnNetworkNew.Enabled = false;
                    this.btnNetworkLoad.Enabled = false;
                    this.btnNetworkSave.Enabled = true;
                    this.btnNetworkClear.Enabled = true;
                }
            }
            else
                return;
        }

        private void btnNetworkSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save Network Config File";
            saveDialog.DefaultExt = "xml";
            DialogResult result = saveDialog.ShowDialog();
            string dataFile = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                dataFile = saveDialog.FileName;
                bool exportResult = Neural.Export(NeuralModel, dataFile);
            }
            else
            {
                return;
            }
        }

        private void btnNetworkClear_Click(object sender, EventArgs e)
        {
            NeuralModel = null;
            this.txtNumInput.Text = "";
            this.txtNumHidden.Text = "";
            this.txtNumInput.Enabled = true;
            this.txtNumHidden.Enabled = true;

            this.btnNetworkNew.Enabled = true;
            this.btnNetworkLoad.Enabled = true;
            this.btnNetworkSave.Enabled = false;
            this.btnNetworkClear.Enabled = false;
        }

        private void btnTrainNeural_Click(object sender, EventArgs e)
        {
            if (radioBackPropagation.Checked)
            {
                double epoch, learningRate, momemtum, residual;
                try
                {
                    epoch = Double.Parse(txtConfigEpoches.Text);
                    learningRate = Double.Parse(txtConfig1.Text);
                    momemtum = Double.Parse(txtConfig2.Text);
                    residual = Double.Parse(txtConfigErrors.Text);

                    NeuralModel.Bp_Run(learningRate, momemtum, epoch, residual);
                }
                catch
                {
                }

            }
            else if (radioRPROP.Checked)
            {
                double defaultValue, maxValue, epoch, residual;
                try
                {
                    epoch = Double.Parse(txtConfigEpoches.Text);
                    defaultValue = Double.Parse(txtConfig1.Text);
                    maxValue = Double.Parse(txtConfig2.Text);
                    residual = Double.Parse(txtConfigErrors.Text);

                    NeuralModel.Rprop_Run(defaultValue, maxValue, epoch, residual);
                }
                catch
                {
                }
            }
        }

        private void btnPlotNeural_Click(object sender, EventArgs e)
        {
            NeuralModel.DrawSeriesData();
        }

        private void btnTestNeural_Click(object sender, EventArgs e)
        {
            //neuralModel.Test();
            List<double> testSeries;
            NeuralModel.GetTestSeries(out testSeries);
            Algorithm.DrawTwoSeriesTestData(_errorSeries, 0, testSeries, 0);
        }

        private void btnForecastNeural_Click(object sender, EventArgs e)
        {
            //NeuralModel.Forecast(30);
            List<double> forecastSeries;
            NeuralModel.Forecast(30, out forecastSeries);
            Algorithm.DrawForecastSeriesData(_errorSeries, 0, forecastSeries, 0);
        }

        private void MainInterface_Load(object sender, EventArgs e)
        {
            txtConfig1.Text = 0.1.ToString();
            txtConfigEpoches.Text = 1000.ToString();
            txtConfig2.Text = 10.ToString();
            txtConfigErrors.Text = 0.000001.ToString();
        }

        private void buttonForecast_Click(object sender, EventArgs e)
        {
            int nHead = Int16.Parse(textBoxNHead.Text);
            List<double> forecastSeries;
            List<double> forecastErrorSeries;
            ARIMAModel.Forecast(nHead, out forecastSeries);
            NeuralModel.Forecast(nHead, out forecastErrorSeries);
            for (int i = 0; i < nHead; i++)
            {
                forecastSeries[i] += forecastErrorSeries[i];
            }

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

            for (int i = 0; i < _dataSeries.Count; i++)
            {
                series1.Points.AddXY(i + 1, _dataSeries[i]);
            }
            chartForecast.Series.Add(series1);

            series2.Points.AddXY(_dataSeries.Count, _dataSeries[_dataSeries.Count - 1]);
            for (int i = 0; i < forecastSeries.Count; i++)
            {
                series2.Points.AddXY(_dataSeries.Count + i + 1, forecastSeries[i]);
            }
            chartForecast.Series.Add(series2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> testSeries;
            List<double> errorSeries;

            ARIMAModel.GetTestSeries(out testSeries);
            NeuralModel.GetTestSeries(out errorSeries);
            for (int i = 0; i < testSeries.Count; i++)
            {
                testSeries[i] += errorSeries[i];
            }

            double MAE = Algorithm.ComputeMAE(_dataSeries, testSeries);
            double SSE = Algorithm.ComputeSSE(_dataSeries, testSeries);
            double MSE = Algorithm.ComputeMSE(_dataSeries, testSeries);

            Test_Form form = new Test_Form();
            form.textBox1.AppendText("Mean Absolute Error MAE =  " + MAE + "\n");
            form.textBox1.AppendText("Sum Square Error SSE =  " + SSE + "\n");
            form.textBox1.AppendText("Mean Square Error MSE =  " + MSE + "\n");
            form.textBox1.ReadOnly = true;
            for (int t = 0; t < _dataSeries.Count; t++)
            {
                form.chart1.Series["Observations"].Points.AddXY(t + 1, _dataSeries.ElementAt(t));
                form.chart1.Series["Computations"].Points.AddXY(t + 1, testSeries[t]);
            }
            form.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> listCorrelation;
            List<double> listCorrelationLimit;
            List<double> listPartialCorrelation;
            Algorithm.ComputeAutocorrelation(_dataSeries, 0, out listCorrelation);
            Algorithm.ComputeConfidenceLimit(listCorrelation, _dataSeries.Count, out listCorrelationLimit);
            Algorithm.ComputePartialAutocorrelation(listCorrelation, out listPartialCorrelation);
            double confidenceLimit = 1.96 / Math.Sqrt(_dataSeries.Count);

            Algorithm.DrawAutocorrelation(listCorrelation, listCorrelationLimit);
            Algorithm.DrawPartialAutocorrelation(listPartialCorrelation, confidenceLimit);

        }
    
    }
}
