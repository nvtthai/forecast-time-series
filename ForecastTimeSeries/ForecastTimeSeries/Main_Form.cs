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
    public partial class Main_Form : Form
    {
        private string m_TrainingDataFile;
        private string m_TestingDataFile;
        private ARIMA ARIMAModel;
        private Neural NeuralModel;
        List<double> _dataSeries;
        List<double> _testSeries;

        public Main_Form()
        {
            InitializeComponent();
            this.CenterToScreen();
            InitData();
            SettingGUIBeforeChooseData();
        }

        private void InitData()
        {
            _dataSeries = new List<double>();
            _testSeries = new List<double>();
            ARIMAModel = new ARIMA();
            NeuralModel = new Neural();
        }

        private void SettingGUIBeforeChooseData()
        {
            radioBtnAutomaticARIMA.Enabled = false;
            radioBtnManualARIMA.Enabled = false;
            groupBoxARIMAParameter.Enabled = false;

            btnTrainARIMA.Enabled = false;
            btnTestArima.Enabled = false;
            btnForecastARIMA.Enabled = false;
            btnLoadARIMA.Enabled = false;
            btnSaveARIMA.Enabled = false;

            btnPlotDataARIMA.Enabled = false;
            btnPlotErrorARIMA.Enabled = false;
            btnCorrelogram.Enabled = false;
            btnPartialCorrelation.Enabled = false;

            groupBoxNetworkConfig.Enabled = false;
            groupBoxAlgorithmConfig.Enabled = false;
            groupBoxNetworkAlgorithm.Enabled = false;

            btnTrainNeural.Enabled = false;
            btnPlotNeural.Enabled = false;
            btnTestNeural.Enabled = false;
            btnForecastNeural.Enabled = false;

            btnForecast.Enabled = false;
            btnTest.Enabled = false;
        }

        private void SettingGUIBeforeARIMAModel()
        {
            radioBtnAutomaticARIMA.Enabled = true;
            radioBtnManualARIMA.Enabled = true;
            groupBoxARIMAParameter.Enabled = false;

            btnTrainARIMA.Enabled = true;
            btnTestArima.Enabled = false;
            btnForecastARIMA.Enabled = false;
            btnLoadARIMA.Enabled = true;
            btnSaveARIMA.Enabled = false;

            btnPlotDataARIMA.Enabled = true;
            btnPlotErrorARIMA.Enabled = false;
            btnCorrelogram.Enabled = true;
            btnPartialCorrelation.Enabled = true;
        }

        private void SettingGUIBeforeNeuralNetwork()
        {
            radioBtnAutomaticARIMA.Enabled = true;
            radioBtnManualARIMA.Enabled = true;

            btnTrainARIMA.Enabled = true;
            btnTestArima.Enabled = true;
            btnForecastARIMA.Enabled = true;
            btnLoadARIMA.Enabled = true;
            btnSaveARIMA.Enabled = true;

            btnPlotDataARIMA.Enabled = true;
            btnPlotErrorARIMA.Enabled = true;
            btnCorrelogram.Enabled = true;
            btnPartialCorrelation.Enabled = true;

            groupBoxNetworkConfig.Enabled = true;
            groupBoxAlgorithmConfig.Enabled = true;
            groupBoxNetworkAlgorithm.Enabled = true;

            btnTrainNeural.Enabled = true;
            btnPlotNeural.Enabled = true;
            btnTestNeural.Enabled = true;
            btnForecastNeural.Enabled = true;
        }

        #region choose data

        private void btnChooseTrainingData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open File";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_TrainingDataFile = openDialog.FileName;
                m_TestingDataFile = openDialog.FileName;
            }
            else
            {
                return;
            }

            System.IO.StreamReader fileInput = null;
            string line = null;
            int numRows = 0;
            int numColumns = 0;
            try
            {
                fileInput = new System.IO.StreamReader(m_TrainingDataFile);
                while ((line = fileInput.ReadLine()) != null)
                {
                    if (numRows == 0)
                    {
                        char[] delimiterChars = { ' ', ',' };
                        List<String> words = new List<string>();
                        words.AddRange(line.Split(delimiterChars));
                        words.RemoveAll(item => "" == item);
                        numColumns = words.Count;
                    }
                    numRows++;
                }
                this.textDataFileTraining.Text = m_TrainingDataFile;
                this.labelNumColumnDataTraining.Text = Convert.ToString(numColumns);
                this.labelNumRowDataTraining.Text = Convert.ToString(numRows);
                this.txtTrainDataColumn.Text = "1";
                this.txtTrainDataFromRow.Text = "1";
                this.txtTrainDataToRow.Text = Convert.ToString((int)(numRows*0.9));

                if (numColumns == 1)
                {
                    this.txtTrainDataColumn.Enabled = false;
                    this.txtTestDataColumn.Enabled = false;
                }
                else
                {
                    this.txtTrainDataColumn.Enabled = true;
                    this.txtTestDataColumn.Enabled = true;
                }
                
                this.textDataFileTesting.Text = m_TrainingDataFile;
                this.labelNumColumnDataTesting.Text = Convert.ToString(numColumns);
                this.labelNumRowDataTesting.Text = Convert.ToString(numRows); ;
                this.txtTestDataColumn.Text = "1";
                this.txtTestDataFromRow.Text = Convert.ToString((int)(numRows * 0.9) + 1);
                this.txtTestDataToRow.Text = Convert.ToString(numRows);
            }
            catch
            {
                MessageBox.Show("File does not found or input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (fileInput != null)
                    fileInput.Close();
            }
        }

        private void btnChooseTestingData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Open File";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                m_TestingDataFile = openDialog.FileName;
            }
            else
            {
                return;
            }

            System.IO.StreamReader fileInput = null;
            string line = null;
            int numRows = 0;
            int numColumns = 0;
            try
            {
                fileInput = new System.IO.StreamReader(m_TestingDataFile);
                while ((line = fileInput.ReadLine()) != null)
                {
                    if (numRows == 0)
                    {
                        char[] delimiterChars = { ' ', ',' };
                        List<String> words = new List<string>();
                        words.AddRange(line.Split(delimiterChars));
                        words.RemoveAll(item => "" == item);
                        numColumns = words.Count;
                    }
                    numRows++;
                }

                this.textDataFileTesting.Text = m_TrainingDataFile;
                this.labelNumColumnDataTesting.Text = Convert.ToString(numColumns);
                this.labelNumRowDataTesting.Text = Convert.ToString(numRows);
                this.txtTestDataColumn.Text = "1";
                this.txtTestDataFromRow.Text = "1";
                this.txtTestDataToRow.Text = Convert.ToString(numRows);

                if (numColumns == 1)
                {
                    this.txtTestDataColumn.Enabled = false;
                }
                else
                {
                    this.txtTestDataColumn.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("File does not found or input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (fileInput != null)
                    fileInput.Close();
            }
        }

        private void txtTrainDataFromRow_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTrainDataToRow_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTrainDataColumn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTestDataFromRow_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTestDataToRow_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTestDataColumn_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChooseData_Click(object sender, EventArgs e)
        {
            _dataSeries = new List<double>();
            _testSeries = new List<double>();
            System.IO.StreamReader trainingFile = null;
            System.IO.StreamReader testingFile = null;
            string lineTrainingFile = null;
            int beginTrainingDataRow = Convert.ToInt32(this.txtTrainDataFromRow.Text);
            int endTrainingDataRow = Convert.ToInt32(this.txtTrainDataToRow.Text);
            int columnTrainingDataSelected = Convert.ToInt32(this.txtTrainDataColumn.Text);
            int idxRowTrainingFile = 0;

            string lineTestingFile = null;
            int beginTestingDataRow = Convert.ToInt32(this.txtTestDataFromRow.Text);
            int endTestingDataRow = Convert.ToInt32(this.txtTestDataToRow.Text);
            int columnTestingDataSelected = Convert.ToInt32(this.txtTestDataColumn.Text);
            int idxRowTestingFile = 0;

            try
            {
                trainingFile = new System.IO.StreamReader(m_TrainingDataFile);
                while ((lineTrainingFile = trainingFile.ReadLine()) != null)
                {
                    idxRowTrainingFile++;
                    if (idxRowTrainingFile < beginTrainingDataRow || idxRowTrainingFile > endTrainingDataRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    List<String> words = new List<string>();
                    words.AddRange(lineTrainingFile.Split(delimiterChars));
                    words.RemoveAll(item => "" == item);

                    if (columnTrainingDataSelected <= words.Count)
                    {
                        _dataSeries.Add(Double.Parse(words[columnTrainingDataSelected - 1]));
                    }
                    else
                    {
                        throw new DataException();
                    }
                }
            }
            catch
            {
                _dataSeries = null;
                MessageBox.Show("Training data file does not found or input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (trainingFile != null)
                    trainingFile.Close();
            }

            try
            {
                testingFile = new System.IO.StreamReader(m_TestingDataFile);
                while ((lineTestingFile = testingFile.ReadLine()) != null)
                {
                    idxRowTestingFile++;
                    if (idxRowTestingFile < beginTestingDataRow || idxRowTestingFile > endTestingDataRow)
                        continue;

                    char[] delimiterChars = { ' ', ',' };
                    List<String> words = new List<string>();
                    words.AddRange(lineTestingFile.Split(delimiterChars));
                    words.RemoveAll(item => "" == item);

                    if (columnTestingDataSelected <= words.Count)
                    {
                        _testSeries.Add(Double.Parse(words[columnTestingDataSelected - 1]));
                    }
                    else
                    {
                        throw new DataException();
                    }
                }
            }
            catch (Exception ex)
            {
                _testSeries = null;
                MessageBox.Show("Testing data file does not found or input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (testingFile != null)
                    testingFile.Close();
            }


            if (_dataSeries != null && _testSeries != null)
            {
                ARIMAModel = new ARIMA();
                NeuralModel = new Neural();
                ARIMAModel.SetData(_dataSeries);
                SettingGUIBeforeARIMAModel();
            }
            else
            {
                MessageBox.Show("Load data fail", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion choose data

        #region SARIMA model

        private void radioBtnAutomaticARIMA_Click(object sender, EventArgs e)
        {
            groupBoxARIMAParameter.Enabled = false;
            txtRegularDifferencing.Text = "";
            txtSeasonDifferencing.Text = "";
            txtSeasonPartern.Text = "";
            txtARRegular.Text = "";
            txtMARegular.Text = "";
            txtARSeason.Text = "";
            txtMASeason.Text = "";
        }

        private void radioBtnManualARIMA_CheckedChanged(object sender, EventArgs e)
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

        private void btnPlotErrorARIMA_Click(object sender, EventArgs e)
        {
            ARIMAModel.DrawErrorData();
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
                List<double> errorARIMASeries;
                ARIMAModel.GetErrorSeries(out errorARIMASeries);
                NeuralModel.SetData(errorARIMASeries);

                SettingGUIBeforeNeuralNetwork();

                string model;
                ARIMAModel.GetModel(out model);
                ARIMA_Model ARIMAResult = new ARIMA_Model();
                ARIMAResult.SetResult(model);
                ARIMAResult.Show();
            }
            catch
            {
            }
        }

        private void btnManualRestoreARIMA_Click(object sender, EventArgs e)
        {
            SettingGUIBeforeARIMAModel();
            groupBoxARIMAParameter.Enabled = true;
            txtRegularDifferencing.Text = "0";
            txtSeasonDifferencing.Text = "0";
            txtSeasonPartern.Text = "0";
            txtARRegular.Text = "0";
            txtMARegular.Text = "0";
            txtARSeason.Text = "0";
            txtMASeason.Text = "0";
            ARIMAModel.InitTraining();        
        }

        private void btnTrainARIMA_Click(object sender, EventArgs e)
        {
            if (_dataSeries == null || _dataSeries.Count == 0)
            {
                MessageBox.Show("Please set data for training", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ARIMAModel.SetData(_dataSeries);
            ARIMAModel.AutomaticTraining();
            List<double> errorARIMASeries;
            ARIMAModel.GetErrorSeries(out errorARIMASeries);
            NeuralModel.SetData(errorARIMASeries);
            radioBtnAutomaticARIMA.Checked = true;
            SettingGUIBeforeNeuralNetwork();

            string model;
            ARIMAModel.GetModel(out model);
            ARIMA_Model ARIMAResult = new ARIMA_Model();
            ARIMAResult.SetResult(model);
            ARIMAResult.Show();

        }

        private void btnTestArima_Click(object sender, EventArgs e)
        {
            List<double> testARIMASeries;
            ARIMAModel.GetTestSeries(out testARIMASeries);
            Statistic.DrawTwoSeriesTestData(_dataSeries, 0, testARIMASeries, 0);
        }

        private void btnForecastARIMA_Click(object sender, EventArgs e)
        {
            List<double> forecastARIMASeries;
            int aHead = 0;
            AHead_Form aHeadDialog = new AHead_Form();

            if (aHeadDialog.ShowDialog() == DialogResult.OK)
            {
                aHead = aHeadDialog.GetAHead();
            }
            aHeadDialog.Dispose();

            if (aHead > 0)
            {
                ARIMAModel.Forecast(aHead, out forecastARIMASeries);
                Statistic.DrawForecastSeriesData(_dataSeries, 0, forecastARIMASeries, 0);
            }
            else
            {
                MessageBox.Show(this, "Please enter input in correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    btnTestArima.Enabled = true;
                    btnForecastARIMA.Enabled = true;
                    btnLoadARIMA.Enabled = true;
                    btnSaveARIMA.Enabled = true;

                    btnNetworkNew.Enabled = true;
                    btnNetworkLoad.Enabled = true;
                    btnPlotNeural.Enabled = true;

                    List<double> errorARIMASeries;
                    ARIMAModel.GetErrorSeries(out errorARIMASeries);
                    NeuralModel.SetData(errorARIMASeries);
                }
            }
            else
            {
                return;
            }
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

        #endregion SARIMA model

        #region Neural network

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
                NeuralModel.SettingNeuralNetwork(Int32.Parse(numInputs), Int32.Parse(numHiddens), Int32.Parse(numOutputs));
                System.Windows.Forms.MessageBox.Show("NetWork configuration successfull, You can train it");

                this.txtNumOutput.Enabled = false;
                this.txtNumHidden.Enabled = false;
                this.txtNumInput.Enabled = false;
                this.btnNetworkNew.Enabled = false;
                this.btnNetworkLoad.Enabled = false;
                this.btnNetworkSave.Enabled = true;
                this.btnNetworkClear.Enabled = true;

                this.btnTrainNeural.Enabled = true;
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
                    NeuralModel.SettingNeuralNetwork(temp.m_iNumInputNodes, temp.m_iNumOutputNodes, temp.m_iNumOutputNodes);

                    this.txtNumInput.Text = NeuralModel.m_iNumInputNodes.ToString();
                    this.txtNumHidden.Text = NeuralModel.m_iNumHiddenNodes.ToString();
                    this.btnNetworkNew.Enabled = false;
                    this.btnNetworkLoad.Enabled = false;
                    this.btnNetworkSave.Enabled = true;
                    this.btnNetworkClear.Enabled = true;

                    this.btnTrainNeural.Enabled = true;
                    this.btnForecastNeural.Enabled = true;
                    this.btnTestNeural.Enabled = true;

                    this.btnForecast.Enabled = true;
                    this.btnTest.Enabled = true;
                }
            }
            else
            {
                return;
            }
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
            NeuralModel.SettingNeuralNetwork(0, 0, 0);

            this.txtNumInput.Text = "";
            this.txtNumHidden.Text = "";
            this.txtNumInput.Enabled = true;
            this.txtNumHidden.Enabled = true;

            this.btnNetworkNew.Enabled = true;
            this.btnNetworkLoad.Enabled = true;
            this.btnNetworkSave.Enabled = false;
            this.btnNetworkClear.Enabled = false;

            this.btnTrainNeural.Enabled = false;
            this.btnTestNeural.Enabled = false;
            this.btnForecastNeural.Enabled = false;
        }

        private void radioBackPropagation_Click(object sender, EventArgs e)
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

        private void radioRPROP_Click(object sender, EventArgs e)
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
                    this.btnForecastNeural.Enabled = true;
                    this.btnTestNeural.Enabled = true;

                    this.btnForecast.Enabled = true;
                    this.btnTest.Enabled = true;
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
                    this.btnForecastNeural.Enabled = true;
                    this.btnTestNeural.Enabled = true;

                    this.btnForecast.Enabled = true;
                    this.btnTest.Enabled = true;
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
            List<double> testSeries;
            NeuralModel.GetTestSeries(out testSeries);
            //Statistic.DrawTwoSeriesTestData(_errorSeries, 0, testSeries, 0);
        }

        private void btnForecastNeural_Click(object sender, EventArgs e)
        {
            List<double> forecastSeries;
            int aHead = 0;
            AHead_Form aHeadDialog = new AHead_Form();

            if (aHeadDialog.ShowDialog() == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                aHead = aHeadDialog.GetAHead();
            }
            aHeadDialog.Dispose();
            if (aHead > 0)
            {
                NeuralModel.Forecast(aHead, out forecastSeries);
                //Statistic.DrawForecastSeriesData(_errorSeries, 0, forecastSeries, 0);
            }
            else
            {
                MessageBox.Show(this, "Please enter input in correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Neural network

        private void btnForecast_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }
    }
}
