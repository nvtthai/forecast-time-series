using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForecastTimeSeries
{
    public partial class MainInterface : Form
    {
        private string m_DemoFile;
        private ARIMA ARIMAModel;
        private Neural neuralModel;
        List<double> dataSeries;
        List<double> errorSeries;

        public MainInterface()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            dataSeries = new List<double>();
            ARIMAModel = new ARIMA();
        }

        private void btnChooseData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open File";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_DemoFile = openDialog.FileName;
            }
            else
            {
                return;
            }

            if (m_DemoFile == null || m_DemoFile.Equals(""))
            {
                MessageBox.Show("Please choose validate test data file before training", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.IO.StreamReader fileInput = null;
                string line = null;
                int numRows = 0;
                int numColumns = 0;
                try
                {
                    fileInput = new System.IO.StreamReader(m_DemoFile);
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
            dataSeries.Clear();
            System.IO.StreamReader file = null;
            string line = null;
            int counter = 0;
            bool isFormatFileRight = true;
            int beginRow = Convert.ToInt32(this.txtTrainDataFromRow.Text);
            int endRow = Convert.ToInt32(this.txtTrainDataToRow.Text);
            int columnSelected = Convert.ToInt32(this.txtTrainDataColumn.Text);
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(m_DemoFile);
                while ((line = file.ReadLine()) != null)
                {
                    idxRow++;
                    if (idxRow < beginRow || idxRow > endRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    string[] words = line.Split(delimiterChars);
                    if (columnSelected <= words.Length)
                    {
                        dataSeries.Add(Double.Parse(words[columnSelected - 1]));
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
                    dataSeries = null;
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                dataSeries = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.IO.IOException io)
            {
                dataSeries = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception excp)
            {
                dataSeries = null;
                MessageBox.Show("Input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            if (dataSeries != null)
            {
                //m_DemoDataSeries.loadData(sample);
                ARIMAModel.SetData(dataSeries);
                showing();
                //this.m_DemoDataSeries.preProcessList.Clear(); //clear for new data
            }
            else
            {
                MessageBox.Show("Load data fail", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showing()
        {
            this.richTextProcessData.Clear();
            //List<double> data = this.m_DemoDataSeries.getProcessedSeries();
            this.richTextProcessData.AppendText("Time Series\n");
            //this.richTextProcessData.AppendText("Start = " + this.m_DemoDataSeries.getStart() + "\n");
            //this.richTextProcessData.AppendText("End = " + this.m_DemoDataSeries.getEnd() + "\n\n");
            this.richTextProcessData.AppendText(String.Format("{0,10}", "TIME"));
            this.richTextProcessData.AppendText(String.Format("{0,20}", "VALUE\n\n"));
            for (int t = 0; t < dataSeries.Count; t++)
            {
                this.richTextProcessData.AppendText(String.Format("{0,10}", "[" + (t + 1) + "]"));
                this.richTextProcessData.AppendText(String.Format("{0,20}", dataSeries.ElementAt(t)) + "\n");
            }
        }

        private void radioBackPropagation_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAlgorithmConfig.Enabled = true;
            groupBoxAlgorithmConfig.Text = "Back Propagation Config";
            labelConfig1.Text = "Learning Rate";
            labelConfig2.Text = "Momemtum";
        }

        private void radioRPROP_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAlgorithmConfig.Enabled = true;
            groupBoxAlgorithmConfig.Text = "Resilient Propagation Config";
            labelConfig1.Text = "Default Update Value";
            labelConfig2.Text = "Max Update Value";
        }

        private void btnAutomaticARIMA_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxARIMAParameter.Enabled = false;
        }

        private void btnManualARIMA_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxARIMAParameter.Enabled = true;
        }

        private void btnTrainARIMA_Click(object sender, EventArgs e)
        {
            ARIMAModel.SetData(dataSeries);
            ARIMAModel.Run();
            ARIMAModel.GetError(out errorSeries);
        }

        private void btnPlotData_Click(object sender, EventArgs e)
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
                neuralModel = new Neural(Int32.Parse(numInputs), Int32.Parse(numHiddens), Int32.Parse(numOutputs));
                neuralModel.SetData(errorSeries);
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

                    neuralModel.Bp_Run(learningRate, momemtum, epoch, residual);
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

                    neuralModel.Rprop_Run(defaultValue, maxValue, epoch, residual);
                }
                catch
                {
                }
            }
        }

        private void buttonForecast_Click(object sender, EventArgs e)
        {
            int nHead = Int16.Parse(textBoxNHead.Text);
            List<double> forecastSeries;
            List<double> forecastErrorSeries;
            ARIMAModel.Forecast(nHead, out forecastSeries);
            neuralModel.Forecast(nHead, out forecastErrorSeries);
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

            for (int i = 0; i < dataSeries.Count; i++)
            {
                series1.Points.AddXY(i + 1, dataSeries[i]);
            }
            chartForecast.Series.Add(series1);

            for (int i = 0; i < forecastSeries.Count; i++)
            {
                series2.Points.AddXY(dataSeries.Count + i +1, forecastSeries[i]);
            }
            chartForecast.Series.Add(series2);
        }
    
    }
}
