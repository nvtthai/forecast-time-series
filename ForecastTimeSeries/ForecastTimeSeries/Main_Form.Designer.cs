namespace ForecastTimeSeries
{
    partial class Main_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabCreateModel = new System.Windows.Forms.TabControl();
            this.tabChooseData = new System.Windows.Forms.TabPage();
            this.txtTestDataToRow = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtTestDataFromRow = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.labelNumRowDataTesting = new System.Windows.Forms.Label();
            this.txtTrainDataToRow = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTrainDataFromRow = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.labelNumRowDataTraining = new System.Windows.Forms.Label();
            this.btnChooseData = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnChooseTestingData = new System.Windows.Forms.Button();
            this.textDataFileTesting = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChooseTrainingData = new System.Windows.Forms.Button();
            this.textDataFileTraining = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxTrainingData = new System.Windows.Forms.GroupBox();
            this.txtTrainDataColumn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelNumColumnDataTraining = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxTestingData = new System.Windows.Forms.GroupBox();
            this.txtTestDataColumn = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.labelNumColumnDataTesting = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tabSARIMAModel = new System.Windows.Forms.TabPage();
            this.btnSaveARIMA = new System.Windows.Forms.Button();
            this.btnLoadARIMA = new System.Windows.Forms.Button();
            this.btnForecastARIMA = new System.Windows.Forms.Button();
            this.btnTestArima = new System.Windows.Forms.Button();
            this.btnTrainARIMA = new System.Windows.Forms.Button();
            this.btnPlotErrorARIMA = new System.Windows.Forms.Button();
            this.btnPartialCorrelation = new System.Windows.Forms.Button();
            this.btnCorrelogram = new System.Windows.Forms.Button();
            this.btnPlotDataARIMA = new System.Windows.Forms.Button();
            this.groupBoxARIMAParameter = new System.Windows.Forms.GroupBox();
            this.btnManualRestoreARIMA = new System.Windows.Forms.Button();
            this.btnManualTrainingARIMA = new System.Windows.Forms.Button();
            this.btnRemoveSeason = new System.Windows.Forms.Button();
            this.txtMASeason = new System.Windows.Forms.TextBox();
            this.txtARSeason = new System.Windows.Forms.TextBox();
            this.txtMARegular = new System.Windows.Forms.TextBox();
            this.txtARRegular = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSeasonPartern = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSeasonDifferencing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRegularDifferencing = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioBtnManualARIMA = new System.Windows.Forms.RadioButton();
            this.radioBtnAutomaticARIMA = new System.Windows.Forms.RadioButton();
            this.tabNeuralNetwork = new System.Windows.Forms.TabPage();
            this.groupBoxNetworkAlgorithm = new System.Windows.Forms.GroupBox();
            this.radioRPROP = new System.Windows.Forms.RadioButton();
            this.radioBackPropagation = new System.Windows.Forms.RadioButton();
            this.btnForecastNeural = new System.Windows.Forms.Button();
            this.btnTestNeural = new System.Windows.Forms.Button();
            this.btnPlotNeural = new System.Windows.Forms.Button();
            this.btnTrainNeural = new System.Windows.Forms.Button();
            this.groupBoxAlgorithmConfig = new System.Windows.Forms.GroupBox();
            this.txtConfigErrors = new System.Windows.Forms.TextBox();
            this.txtConfig2 = new System.Windows.Forms.TextBox();
            this.txtConfigEpoches = new System.Windows.Forms.TextBox();
            this.txtConfig1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.labelConfig2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelConfig1 = new System.Windows.Forms.Label();
            this.groupBoxNetworkConfig = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnNetworkClear = new System.Windows.Forms.Button();
            this.btnNetworkSave = new System.Windows.Forms.Button();
            this.btnNetworkLoad = new System.Windows.Forms.Button();
            this.btnNetworkNew = new System.Windows.Forms.Button();
            this.txtNumOutput = new System.Windows.Forms.TextBox();
            this.txtNumInput = new System.Windows.Forms.TextBox();
            this.txtNumHidden = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabForecast = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnForecast = new System.Windows.Forms.Button();
            this.richTextForecast = new System.Windows.Forms.RichTextBox();
            this.chartForecast = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.tabCreateModel.SuspendLayout();
            this.tabChooseData.SuspendLayout();
            this.groupBoxTrainingData.SuspendLayout();
            this.groupBoxTestingData.SuspendLayout();
            this.tabSARIMAModel.SuspendLayout();
            this.groupBoxARIMAParameter.SuspendLayout();
            this.tabNeuralNetwork.SuspendLayout();
            this.groupBoxNetworkAlgorithm.SuspendLayout();
            this.groupBoxAlgorithmConfig.SuspendLayout();
            this.groupBoxNetworkConfig.SuspendLayout();
            this.tabForecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCreateModel
            // 
            this.tabCreateModel.Controls.Add(this.tabChooseData);
            this.tabCreateModel.Controls.Add(this.tabSARIMAModel);
            this.tabCreateModel.Controls.Add(this.tabNeuralNetwork);
            this.tabCreateModel.Controls.Add(this.tabForecast);
            this.tabCreateModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCreateModel.Location = new System.Drawing.Point(4, 5);
            this.tabCreateModel.Name = "tabCreateModel";
            this.tabCreateModel.SelectedIndex = 0;
            this.tabCreateModel.Size = new System.Drawing.Size(664, 397);
            this.tabCreateModel.TabIndex = 0;
            // 
            // tabChooseData
            // 
            this.tabChooseData.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabChooseData.Controls.Add(this.comboBoxModel);
            this.tabChooseData.Controls.Add(this.label15);
            this.tabChooseData.Controls.Add(this.txtTestDataToRow);
            this.tabChooseData.Controls.Add(this.label24);
            this.tabChooseData.Controls.Add(this.txtTestDataFromRow);
            this.tabChooseData.Controls.Add(this.label23);
            this.tabChooseData.Controls.Add(this.labelNumRowDataTesting);
            this.tabChooseData.Controls.Add(this.txtTrainDataToRow);
            this.tabChooseData.Controls.Add(this.label22);
            this.tabChooseData.Controls.Add(this.txtTrainDataFromRow);
            this.tabChooseData.Controls.Add(this.label21);
            this.tabChooseData.Controls.Add(this.labelNumRowDataTraining);
            this.tabChooseData.Controls.Add(this.btnChooseData);
            this.tabChooseData.Controls.Add(this.label5);
            this.tabChooseData.Controls.Add(this.btnChooseTestingData);
            this.tabChooseData.Controls.Add(this.textDataFileTesting);
            this.tabChooseData.Controls.Add(this.label4);
            this.tabChooseData.Controls.Add(this.label3);
            this.tabChooseData.Controls.Add(this.btnChooseTrainingData);
            this.tabChooseData.Controls.Add(this.textDataFileTraining);
            this.tabChooseData.Controls.Add(this.label1);
            this.tabChooseData.Controls.Add(this.groupBoxTrainingData);
            this.tabChooseData.Controls.Add(this.groupBoxTestingData);
            this.tabChooseData.Location = new System.Drawing.Point(4, 25);
            this.tabChooseData.Name = "tabChooseData";
            this.tabChooseData.Padding = new System.Windows.Forms.Padding(3);
            this.tabChooseData.Size = new System.Drawing.Size(656, 368);
            this.tabChooseData.TabIndex = 0;
            this.tabChooseData.Text = "Data";
            // 
            // txtTestDataToRow
            // 
            this.txtTestDataToRow.Location = new System.Drawing.Point(415, 222);
            this.txtTestDataToRow.Name = "txtTestDataToRow";
            this.txtTestDataToRow.Size = new System.Drawing.Size(100, 22);
            this.txtTestDataToRow.TabIndex = 20;
            this.txtTestDataToRow.TextChanged += new System.EventHandler(this.txtTestDataToRow_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(356, 227);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(25, 16);
            this.label24.TabIndex = 19;
            this.label24.Text = "To";
            // 
            // txtTestDataFromRow
            // 
            this.txtTestDataFromRow.Location = new System.Drawing.Point(122, 223);
            this.txtTestDataFromRow.Name = "txtTestDataFromRow";
            this.txtTestDataFromRow.Size = new System.Drawing.Size(100, 22);
            this.txtTestDataFromRow.TabIndex = 18;
            this.txtTestDataFromRow.TextChanged += new System.EventHandler(this.txtTestDataFromRow_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(23, 225);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 16);
            this.label23.TabIndex = 17;
            this.label23.Text = "Row";
            // 
            // labelNumRowDataTesting
            // 
            this.labelNumRowDataTesting.AutoSize = true;
            this.labelNumRowDataTesting.Location = new System.Drawing.Point(119, 197);
            this.labelNumRowDataTesting.Name = "labelNumRowDataTesting";
            this.labelNumRowDataTesting.Size = new System.Drawing.Size(0, 16);
            this.labelNumRowDataTesting.TabIndex = 16;
            // 
            // txtTrainDataToRow
            // 
            this.txtTrainDataToRow.Location = new System.Drawing.Point(415, 78);
            this.txtTrainDataToRow.Name = "txtTrainDataToRow";
            this.txtTrainDataToRow.Size = new System.Drawing.Size(100, 22);
            this.txtTrainDataToRow.TabIndex = 15;
            this.txtTrainDataToRow.TextChanged += new System.EventHandler(this.txtTrainDataToRow_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(331, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(25, 16);
            this.label22.TabIndex = 14;
            this.label22.Text = "To";
            // 
            // txtTrainDataFromRow
            // 
            this.txtTrainDataFromRow.Location = new System.Drawing.Point(122, 81);
            this.txtTrainDataFromRow.Name = "txtTrainDataFromRow";
            this.txtTrainDataFromRow.Size = new System.Drawing.Size(100, 22);
            this.txtTrainDataFromRow.TabIndex = 13;
            this.txtTrainDataFromRow.TextChanged += new System.EventHandler(this.txtTrainDataFromRow_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(24, 84);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 16);
            this.label21.TabIndex = 12;
            this.label21.Text = "Row";
            // 
            // labelNumRowDataTraining
            // 
            this.labelNumRowDataTraining.AutoSize = true;
            this.labelNumRowDataTraining.Location = new System.Drawing.Point(119, 55);
            this.labelNumRowDataTraining.Name = "labelNumRowDataTraining";
            this.labelNumRowDataTraining.Size = new System.Drawing.Size(15, 16);
            this.labelNumRowDataTraining.TabIndex = 11;
            this.labelNumRowDataTraining.Text = "0";
            // 
            // btnChooseData
            // 
            this.btnChooseData.Location = new System.Drawing.Point(259, 288);
            this.btnChooseData.Name = "btnChooseData";
            this.btnChooseData.Size = new System.Drawing.Size(138, 23);
            this.btnChooseData.TabIndex = 10;
            this.btnChooseData.Text = "Choose data";
            this.btnChooseData.UseVisualStyleBackColor = true;
            this.btnChooseData.Click += new System.EventHandler(this.btnChooseData_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Num rows";
            // 
            // btnChooseTestingData
            // 
            this.btnChooseTestingData.Location = new System.Drawing.Point(550, 160);
            this.btnChooseTestingData.Name = "btnChooseTestingData";
            this.btnChooseTestingData.Size = new System.Drawing.Size(75, 23);
            this.btnChooseTestingData.TabIndex = 7;
            this.btnChooseTestingData.Text = "Browse";
            this.btnChooseTestingData.UseVisualStyleBackColor = true;
            this.btnChooseTestingData.Click += new System.EventHandler(this.btnChooseTestingData_Click);
            // 
            // textDataFileTesting
            // 
            this.textDataFileTesting.Location = new System.Drawing.Point(122, 162);
            this.textDataFileTesting.Name = "textDataFileTesting";
            this.textDataFileTesting.ReadOnly = true;
            this.textDataFileTesting.Size = new System.Drawing.Size(393, 22);
            this.textDataFileTesting.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Data file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Num rows";
            // 
            // btnChooseTrainingData
            // 
            this.btnChooseTrainingData.Location = new System.Drawing.Point(550, 22);
            this.btnChooseTrainingData.Name = "btnChooseTrainingData";
            this.btnChooseTrainingData.Size = new System.Drawing.Size(75, 23);
            this.btnChooseTrainingData.TabIndex = 2;
            this.btnChooseTrainingData.Text = "Browse";
            this.btnChooseTrainingData.UseVisualStyleBackColor = true;
            this.btnChooseTrainingData.Click += new System.EventHandler(this.btnChooseTrainingData_Click);
            // 
            // textDataFileTraining
            // 
            this.textDataFileTraining.Location = new System.Drawing.Point(122, 24);
            this.textDataFileTraining.Name = "textDataFileTraining";
            this.textDataFileTraining.ReadOnly = true;
            this.textDataFileTraining.Size = new System.Drawing.Size(393, 22);
            this.textDataFileTraining.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data file";
            // 
            // groupBoxTrainingData
            // 
            this.groupBoxTrainingData.Controls.Add(this.txtTrainDataColumn);
            this.groupBoxTrainingData.Controls.Add(this.label6);
            this.groupBoxTrainingData.Controls.Add(this.labelNumColumnDataTraining);
            this.groupBoxTrainingData.Controls.Add(this.label2);
            this.groupBoxTrainingData.Location = new System.Drawing.Point(17, 3);
            this.groupBoxTrainingData.Name = "groupBoxTrainingData";
            this.groupBoxTrainingData.Size = new System.Drawing.Size(618, 134);
            this.groupBoxTrainingData.TabIndex = 21;
            this.groupBoxTrainingData.TabStop = false;
            this.groupBoxTrainingData.Text = "Training data";
            // 
            // txtTrainDataColumn
            // 
            this.txtTrainDataColumn.Location = new System.Drawing.Point(105, 108);
            this.txtTrainDataColumn.Name = "txtTrainDataColumn";
            this.txtTrainDataColumn.Size = new System.Drawing.Size(100, 22);
            this.txtTrainDataColumn.TabIndex = 14;
            this.txtTrainDataColumn.TextChanged += new System.EventHandler(this.txtTrainDataColumn_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Column";
            // 
            // labelNumColumnDataTraining
            // 
            this.labelNumColumnDataTraining.AutoSize = true;
            this.labelNumColumnDataTraining.Location = new System.Drawing.Point(403, 55);
            this.labelNumColumnDataTraining.Name = "labelNumColumnDataTraining";
            this.labelNumColumnDataTraining.Size = new System.Drawing.Size(15, 16);
            this.labelNumColumnDataTraining.TabIndex = 12;
            this.labelNumColumnDataTraining.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Num columns";
            // 
            // groupBoxTestingData
            // 
            this.groupBoxTestingData.Controls.Add(this.txtTestDataColumn);
            this.groupBoxTestingData.Controls.Add(this.label26);
            this.groupBoxTestingData.Controls.Add(this.labelNumColumnDataTesting);
            this.groupBoxTestingData.Controls.Add(this.label25);
            this.groupBoxTestingData.Location = new System.Drawing.Point(17, 141);
            this.groupBoxTestingData.Name = "groupBoxTestingData";
            this.groupBoxTestingData.Size = new System.Drawing.Size(618, 145);
            this.groupBoxTestingData.TabIndex = 22;
            this.groupBoxTestingData.TabStop = false;
            this.groupBoxTestingData.Text = "Testing data";
            // 
            // txtTestDataColumn
            // 
            this.txtTestDataColumn.Location = new System.Drawing.Point(105, 113);
            this.txtTestDataColumn.Name = "txtTestDataColumn";
            this.txtTestDataColumn.Size = new System.Drawing.Size(100, 22);
            this.txtTestDataColumn.TabIndex = 15;
            this.txtTestDataColumn.TextChanged += new System.EventHandler(this.txtTestDataColumn_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 116);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 16);
            this.label26.TabIndex = 14;
            this.label26.Text = "Column";
            // 
            // labelNumColumnDataTesting
            // 
            this.labelNumColumnDataTesting.AutoSize = true;
            this.labelNumColumnDataTesting.Location = new System.Drawing.Point(403, 60);
            this.labelNumColumnDataTesting.Name = "labelNumColumnDataTesting";
            this.labelNumColumnDataTesting.Size = new System.Drawing.Size(15, 16);
            this.labelNumColumnDataTesting.TabIndex = 13;
            this.labelNumColumnDataTesting.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(314, 60);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 16);
            this.label25.TabIndex = 0;
            this.label25.Text = "Num columns";
            // 
            // tabSARIMAModel
            // 
            this.tabSARIMAModel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabSARIMAModel.Controls.Add(this.btnSaveARIMA);
            this.tabSARIMAModel.Controls.Add(this.btnLoadARIMA);
            this.tabSARIMAModel.Controls.Add(this.btnForecastARIMA);
            this.tabSARIMAModel.Controls.Add(this.btnTestArima);
            this.tabSARIMAModel.Controls.Add(this.btnTrainARIMA);
            this.tabSARIMAModel.Controls.Add(this.btnPlotErrorARIMA);
            this.tabSARIMAModel.Controls.Add(this.btnPartialCorrelation);
            this.tabSARIMAModel.Controls.Add(this.btnCorrelogram);
            this.tabSARIMAModel.Controls.Add(this.btnPlotDataARIMA);
            this.tabSARIMAModel.Controls.Add(this.groupBoxARIMAParameter);
            this.tabSARIMAModel.Controls.Add(this.radioBtnManualARIMA);
            this.tabSARIMAModel.Controls.Add(this.radioBtnAutomaticARIMA);
            this.tabSARIMAModel.Location = new System.Drawing.Point(4, 25);
            this.tabSARIMAModel.Name = "tabSARIMAModel";
            this.tabSARIMAModel.Padding = new System.Windows.Forms.Padding(3);
            this.tabSARIMAModel.Size = new System.Drawing.Size(656, 368);
            this.tabSARIMAModel.TabIndex = 1;
            this.tabSARIMAModel.Text = "SARIMA";
            // 
            // btnSaveARIMA
            // 
            this.btnSaveARIMA.Location = new System.Drawing.Point(542, 322);
            this.btnSaveARIMA.Name = "btnSaveARIMA";
            this.btnSaveARIMA.Size = new System.Drawing.Size(101, 23);
            this.btnSaveARIMA.TabIndex = 11;
            this.btnSaveARIMA.Text = "Save";
            this.btnSaveARIMA.UseVisualStyleBackColor = true;
            this.btnSaveARIMA.Click += new System.EventHandler(this.btnSaveARIMA_Click);
            // 
            // btnLoadARIMA
            // 
            this.btnLoadARIMA.Location = new System.Drawing.Point(412, 322);
            this.btnLoadARIMA.Name = "btnLoadARIMA";
            this.btnLoadARIMA.Size = new System.Drawing.Size(101, 23);
            this.btnLoadARIMA.TabIndex = 10;
            this.btnLoadARIMA.Text = "Load";
            this.btnLoadARIMA.UseVisualStyleBackColor = true;
            this.btnLoadARIMA.Click += new System.EventHandler(this.btnLoadARIMA_Click);
            // 
            // btnForecastARIMA
            // 
            this.btnForecastARIMA.Location = new System.Drawing.Point(282, 322);
            this.btnForecastARIMA.Name = "btnForecastARIMA";
            this.btnForecastARIMA.Size = new System.Drawing.Size(101, 23);
            this.btnForecastARIMA.TabIndex = 9;
            this.btnForecastARIMA.Text = "Forecast";
            this.btnForecastARIMA.UseVisualStyleBackColor = true;
            this.btnForecastARIMA.Click += new System.EventHandler(this.btnForecastARIMA_Click);
            // 
            // btnTestArima
            // 
            this.btnTestArima.Location = new System.Drawing.Point(152, 322);
            this.btnTestArima.Name = "btnTestArima";
            this.btnTestArima.Size = new System.Drawing.Size(101, 23);
            this.btnTestArima.TabIndex = 8;
            this.btnTestArima.Text = "Test";
            this.btnTestArima.UseVisualStyleBackColor = true;
            this.btnTestArima.Click += new System.EventHandler(this.btnTestArima_Click);
            // 
            // btnTrainARIMA
            // 
            this.btnTrainARIMA.Location = new System.Drawing.Point(22, 322);
            this.btnTrainARIMA.Name = "btnTrainARIMA";
            this.btnTrainARIMA.Size = new System.Drawing.Size(101, 23);
            this.btnTrainARIMA.TabIndex = 7;
            this.btnTrainARIMA.Text = "Training";
            this.btnTrainARIMA.UseVisualStyleBackColor = true;
            this.btnTrainARIMA.Click += new System.EventHandler(this.btnTrainARIMA_Click);
            // 
            // btnPlotErrorARIMA
            // 
            this.btnPlotErrorARIMA.Location = new System.Drawing.Point(534, 275);
            this.btnPlotErrorARIMA.Name = "btnPlotErrorARIMA";
            this.btnPlotErrorARIMA.Size = new System.Drawing.Size(111, 23);
            this.btnPlotErrorARIMA.TabIndex = 6;
            this.btnPlotErrorARIMA.Text = "Plot error";
            this.btnPlotErrorARIMA.UseVisualStyleBackColor = true;
            this.btnPlotErrorARIMA.Click += new System.EventHandler(this.btnPlotErrorARIMA_Click);
            // 
            // btnPartialCorrelation
            // 
            this.btnPartialCorrelation.Location = new System.Drawing.Point(334, 275);
            this.btnPartialCorrelation.Name = "btnPartialCorrelation";
            this.btnPartialCorrelation.Size = new System.Drawing.Size(157, 23);
            this.btnPartialCorrelation.TabIndex = 5;
            this.btnPartialCorrelation.Text = "Partial autocorrelation";
            this.btnPartialCorrelation.UseVisualStyleBackColor = true;
            this.btnPartialCorrelation.Click += new System.EventHandler(this.btnPartialCorrelation_Click);
            // 
            // btnCorrelogram
            // 
            this.btnCorrelogram.Location = new System.Drawing.Point(173, 275);
            this.btnCorrelogram.Name = "btnCorrelogram";
            this.btnCorrelogram.Size = new System.Drawing.Size(111, 23);
            this.btnCorrelogram.TabIndex = 4;
            this.btnCorrelogram.Text = "Autocorrelation";
            this.btnCorrelogram.UseVisualStyleBackColor = true;
            this.btnCorrelogram.Click += new System.EventHandler(this.btnCorrelogram_Click);
            // 
            // btnPlotDataARIMA
            // 
            this.btnPlotDataARIMA.Location = new System.Drawing.Point(22, 275);
            this.btnPlotDataARIMA.Name = "btnPlotDataARIMA";
            this.btnPlotDataARIMA.Size = new System.Drawing.Size(111, 23);
            this.btnPlotDataARIMA.TabIndex = 3;
            this.btnPlotDataARIMA.Text = "Plot data";
            this.btnPlotDataARIMA.UseVisualStyleBackColor = true;
            this.btnPlotDataARIMA.Click += new System.EventHandler(this.btnPlotDataARIMA_Click);
            // 
            // groupBoxARIMAParameter
            // 
            this.groupBoxARIMAParameter.Controls.Add(this.btnManualRestoreARIMA);
            this.groupBoxARIMAParameter.Controls.Add(this.btnManualTrainingARIMA);
            this.groupBoxARIMAParameter.Controls.Add(this.btnRemoveSeason);
            this.groupBoxARIMAParameter.Controls.Add(this.txtMASeason);
            this.groupBoxARIMAParameter.Controls.Add(this.txtARSeason);
            this.groupBoxARIMAParameter.Controls.Add(this.txtMARegular);
            this.groupBoxARIMAParameter.Controls.Add(this.txtARRegular);
            this.groupBoxARIMAParameter.Controls.Add(this.label13);
            this.groupBoxARIMAParameter.Controls.Add(this.label12);
            this.groupBoxARIMAParameter.Controls.Add(this.label11);
            this.groupBoxARIMAParameter.Controls.Add(this.label10);
            this.groupBoxARIMAParameter.Controls.Add(this.txtSeasonPartern);
            this.groupBoxARIMAParameter.Controls.Add(this.label9);
            this.groupBoxARIMAParameter.Controls.Add(this.txtSeasonDifferencing);
            this.groupBoxARIMAParameter.Controls.Add(this.label8);
            this.groupBoxARIMAParameter.Controls.Add(this.txtRegularDifferencing);
            this.groupBoxARIMAParameter.Controls.Add(this.label7);
            this.groupBoxARIMAParameter.Location = new System.Drawing.Point(26, 39);
            this.groupBoxARIMAParameter.Name = "groupBoxARIMAParameter";
            this.groupBoxARIMAParameter.Size = new System.Drawing.Size(613, 219);
            this.groupBoxARIMAParameter.TabIndex = 2;
            this.groupBoxARIMAParameter.TabStop = false;
            this.groupBoxARIMAParameter.Text = "SARIMA model";
            // 
            // btnManualRestoreARIMA
            // 
            this.btnManualRestoreARIMA.Location = new System.Drawing.Point(407, 180);
            this.btnManualRestoreARIMA.Name = "btnManualRestoreARIMA";
            this.btnManualRestoreARIMA.Size = new System.Drawing.Size(120, 23);
            this.btnManualRestoreARIMA.TabIndex = 11;
            this.btnManualRestoreARIMA.Text = "Restore";
            this.btnManualRestoreARIMA.UseVisualStyleBackColor = true;
            this.btnManualRestoreARIMA.Click += new System.EventHandler(this.btnManualRestoreARIMA_Click);
            // 
            // btnManualTrainingARIMA
            // 
            this.btnManualTrainingARIMA.Location = new System.Drawing.Point(240, 180);
            this.btnManualTrainingARIMA.Name = "btnManualTrainingARIMA";
            this.btnManualTrainingARIMA.Size = new System.Drawing.Size(120, 23);
            this.btnManualTrainingARIMA.TabIndex = 11;
            this.btnManualTrainingARIMA.Text = "Process";
            this.btnManualTrainingARIMA.UseVisualStyleBackColor = true;
            this.btnManualTrainingARIMA.Click += new System.EventHandler(this.btnManualTrainingARIMA_Click);
            // 
            // btnRemoveSeason
            // 
            this.btnRemoveSeason.Location = new System.Drawing.Point(73, 180);
            this.btnRemoveSeason.Name = "btnRemoveSeason";
            this.btnRemoveSeason.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveSeason.TabIndex = 11;
            this.btnRemoveSeason.Text = "Remove season";
            this.btnRemoveSeason.UseVisualStyleBackColor = true;
            this.btnRemoveSeason.Click += new System.EventHandler(this.btnRemoveSeason_Click);
            // 
            // txtMASeason
            // 
            this.txtMASeason.Location = new System.Drawing.Point(471, 144);
            this.txtMASeason.Name = "txtMASeason";
            this.txtMASeason.Size = new System.Drawing.Size(100, 22);
            this.txtMASeason.TabIndex = 10;
            // 
            // txtARSeason
            // 
            this.txtARSeason.Location = new System.Drawing.Point(181, 144);
            this.txtARSeason.Name = "txtARSeason";
            this.txtARSeason.Size = new System.Drawing.Size(100, 22);
            this.txtARSeason.TabIndex = 9;
            // 
            // txtMARegular
            // 
            this.txtMARegular.Location = new System.Drawing.Point(471, 107);
            this.txtMARegular.Name = "txtMARegular";
            this.txtMARegular.Size = new System.Drawing.Size(100, 22);
            this.txtMARegular.TabIndex = 8;
            // 
            // txtARRegular
            // 
            this.txtARRegular.Location = new System.Drawing.Point(181, 107);
            this.txtARRegular.Name = "txtARRegular";
            this.txtARRegular.Size = new System.Drawing.Size(100, 22);
            this.txtARRegular.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(365, 144);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "MA Season";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 16);
            this.label12.TabIndex = 6;
            this.label12.Text = "AR  Season";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(365, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "MA  Regular";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 6;
            this.label10.Text = "AR  Regular";
            // 
            // txtSeasonPartern
            // 
            this.txtSeasonPartern.Location = new System.Drawing.Point(471, 67);
            this.txtSeasonPartern.Name = "txtSeasonPartern";
            this.txtSeasonPartern.Size = new System.Drawing.Size(100, 22);
            this.txtSeasonPartern.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(365, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Season partern";
            // 
            // txtSeasonDifferencing
            // 
            this.txtSeasonDifferencing.Location = new System.Drawing.Point(181, 67);
            this.txtSeasonDifferencing.Name = "txtSeasonDifferencing";
            this.txtSeasonDifferencing.Size = new System.Drawing.Size(100, 22);
            this.txtSeasonDifferencing.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Season differencing";
            // 
            // txtRegularDifferencing
            // 
            this.txtRegularDifferencing.Location = new System.Drawing.Point(181, 24);
            this.txtRegularDifferencing.Name = "txtRegularDifferencing";
            this.txtRegularDifferencing.Size = new System.Drawing.Size(100, 22);
            this.txtRegularDifferencing.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Regular differencing";
            // 
            // radioBtnManualARIMA
            // 
            this.radioBtnManualARIMA.AutoSize = true;
            this.radioBtnManualARIMA.Location = new System.Drawing.Point(176, 13);
            this.radioBtnManualARIMA.Name = "radioBtnManualARIMA";
            this.radioBtnManualARIMA.Size = new System.Drawing.Size(108, 20);
            this.radioBtnManualARIMA.TabIndex = 1;
            this.radioBtnManualARIMA.Text = "Manual mode";
            this.radioBtnManualARIMA.UseVisualStyleBackColor = true;
            this.radioBtnManualARIMA.CheckedChanged += new System.EventHandler(this.radioBtnManualARIMA_CheckedChanged);
            // 
            // radioBtnAutomaticARIMA
            // 
            this.radioBtnAutomaticARIMA.AutoSize = true;
            this.radioBtnAutomaticARIMA.Checked = true;
            this.radioBtnAutomaticARIMA.Location = new System.Drawing.Point(26, 13);
            this.radioBtnAutomaticARIMA.Name = "radioBtnAutomaticARIMA";
            this.radioBtnAutomaticARIMA.Size = new System.Drawing.Size(123, 20);
            this.radioBtnAutomaticARIMA.TabIndex = 0;
            this.radioBtnAutomaticARIMA.TabStop = true;
            this.radioBtnAutomaticARIMA.Text = "Automatic mode";
            this.radioBtnAutomaticARIMA.UseVisualStyleBackColor = true;
            this.radioBtnAutomaticARIMA.Click += new System.EventHandler(this.radioBtnAutomaticARIMA_Click);
            // 
            // tabNeuralNetwork
            // 
            this.tabNeuralNetwork.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabNeuralNetwork.Controls.Add(this.groupBoxNetworkAlgorithm);
            this.tabNeuralNetwork.Controls.Add(this.btnForecastNeural);
            this.tabNeuralNetwork.Controls.Add(this.btnTestNeural);
            this.tabNeuralNetwork.Controls.Add(this.btnPlotNeural);
            this.tabNeuralNetwork.Controls.Add(this.btnTrainNeural);
            this.tabNeuralNetwork.Controls.Add(this.groupBoxAlgorithmConfig);
            this.tabNeuralNetwork.Controls.Add(this.groupBoxNetworkConfig);
            this.tabNeuralNetwork.Location = new System.Drawing.Point(4, 25);
            this.tabNeuralNetwork.Name = "tabNeuralNetwork";
            this.tabNeuralNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabNeuralNetwork.Size = new System.Drawing.Size(656, 368);
            this.tabNeuralNetwork.TabIndex = 2;
            this.tabNeuralNetwork.Text = "Neural network";
            // 
            // groupBoxNetworkAlgorithm
            // 
            this.groupBoxNetworkAlgorithm.Controls.Add(this.radioRPROP);
            this.groupBoxNetworkAlgorithm.Controls.Add(this.radioBackPropagation);
            this.groupBoxNetworkAlgorithm.Location = new System.Drawing.Point(15, 140);
            this.groupBoxNetworkAlgorithm.Name = "groupBoxNetworkAlgorithm";
            this.groupBoxNetworkAlgorithm.Size = new System.Drawing.Size(329, 93);
            this.groupBoxNetworkAlgorithm.TabIndex = 4;
            this.groupBoxNetworkAlgorithm.TabStop = false;
            this.groupBoxNetworkAlgorithm.Text = "Algorithm";
            // 
            // radioRPROP
            // 
            this.radioRPROP.AutoSize = true;
            this.radioRPROP.Checked = true;
            this.radioRPROP.Location = new System.Drawing.Point(193, 35);
            this.radioRPROP.Name = "radioRPROP";
            this.radioRPROP.Size = new System.Drawing.Size(74, 20);
            this.radioRPROP.TabIndex = 1;
            this.radioRPROP.TabStop = true;
            this.radioRPROP.Text = "RPROP";
            this.radioRPROP.UseVisualStyleBackColor = true;
            this.radioRPROP.Click += new System.EventHandler(this.radioRPROP_Click);
            // 
            // radioBackPropagation
            // 
            this.radioBackPropagation.AutoSize = true;
            this.radioBackPropagation.Location = new System.Drawing.Point(23, 35);
            this.radioBackPropagation.Name = "radioBackPropagation";
            this.radioBackPropagation.Size = new System.Drawing.Size(134, 20);
            this.radioBackPropagation.TabIndex = 0;
            this.radioBackPropagation.Text = "Back Propagation";
            this.radioBackPropagation.UseVisualStyleBackColor = true;
            this.radioBackPropagation.Click += new System.EventHandler(this.radioBackPropagation_Click);
            // 
            // btnForecastNeural
            // 
            this.btnForecastNeural.Location = new System.Drawing.Point(208, 311);
            this.btnForecastNeural.Name = "btnForecastNeural";
            this.btnForecastNeural.Size = new System.Drawing.Size(106, 23);
            this.btnForecastNeural.TabIndex = 3;
            this.btnForecastNeural.Text = "Forecast";
            this.btnForecastNeural.UseVisualStyleBackColor = true;
            this.btnForecastNeural.Click += new System.EventHandler(this.btnForecastNeural_Click);
            // 
            // btnTestNeural
            // 
            this.btnTestNeural.Location = new System.Drawing.Point(33, 314);
            this.btnTestNeural.Name = "btnTestNeural";
            this.btnTestNeural.Size = new System.Drawing.Size(106, 23);
            this.btnTestNeural.TabIndex = 3;
            this.btnTestNeural.Text = "Test";
            this.btnTestNeural.UseVisualStyleBackColor = true;
            this.btnTestNeural.Click += new System.EventHandler(this.btnTestNeural_Click);
            // 
            // btnPlotNeural
            // 
            this.btnPlotNeural.Location = new System.Drawing.Point(208, 262);
            this.btnPlotNeural.Name = "btnPlotNeural";
            this.btnPlotNeural.Size = new System.Drawing.Size(106, 23);
            this.btnPlotNeural.TabIndex = 3;
            this.btnPlotNeural.Text = "Plot data";
            this.btnPlotNeural.UseVisualStyleBackColor = true;
            this.btnPlotNeural.Click += new System.EventHandler(this.btnPlotNeural_Click);
            // 
            // btnTrainNeural
            // 
            this.btnTrainNeural.Location = new System.Drawing.Point(33, 263);
            this.btnTrainNeural.Name = "btnTrainNeural";
            this.btnTrainNeural.Size = new System.Drawing.Size(106, 23);
            this.btnTrainNeural.TabIndex = 3;
            this.btnTrainNeural.Text = "Training";
            this.btnTrainNeural.UseVisualStyleBackColor = true;
            this.btnTrainNeural.Click += new System.EventHandler(this.btnTrainNeural_Click);
            // 
            // groupBoxAlgorithmConfig
            // 
            this.groupBoxAlgorithmConfig.Controls.Add(this.txtConfigErrors);
            this.groupBoxAlgorithmConfig.Controls.Add(this.txtConfig2);
            this.groupBoxAlgorithmConfig.Controls.Add(this.txtConfigEpoches);
            this.groupBoxAlgorithmConfig.Controls.Add(this.txtConfig1);
            this.groupBoxAlgorithmConfig.Controls.Add(this.label18);
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfig2);
            this.groupBoxAlgorithmConfig.Controls.Add(this.label16);
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfig1);
            this.groupBoxAlgorithmConfig.Location = new System.Drawing.Point(366, 140);
            this.groupBoxAlgorithmConfig.Name = "groupBoxAlgorithmConfig";
            this.groupBoxAlgorithmConfig.Size = new System.Drawing.Size(275, 211);
            this.groupBoxAlgorithmConfig.TabIndex = 1;
            this.groupBoxAlgorithmConfig.TabStop = false;
            this.groupBoxAlgorithmConfig.Text = "RPROP Config";
            // 
            // txtConfigErrors
            // 
            this.txtConfigErrors.Location = new System.Drawing.Point(169, 168);
            this.txtConfigErrors.Name = "txtConfigErrors";
            this.txtConfigErrors.Size = new System.Drawing.Size(100, 22);
            this.txtConfigErrors.TabIndex = 7;
            // 
            // txtConfig2
            // 
            this.txtConfig2.Location = new System.Drawing.Point(169, 123);
            this.txtConfig2.Name = "txtConfig2";
            this.txtConfig2.Size = new System.Drawing.Size(100, 22);
            this.txtConfig2.TabIndex = 6;
            // 
            // txtConfigEpoches
            // 
            this.txtConfigEpoches.Location = new System.Drawing.Point(169, 78);
            this.txtConfigEpoches.Name = "txtConfigEpoches";
            this.txtConfigEpoches.Size = new System.Drawing.Size(100, 22);
            this.txtConfigEpoches.TabIndex = 5;
            // 
            // txtConfig1
            // 
            this.txtConfig1.Location = new System.Drawing.Point(169, 33);
            this.txtConfig1.Name = "txtConfig1";
            this.txtConfig1.Size = new System.Drawing.Size(100, 22);
            this.txtConfig1.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 174);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(115, 16);
            this.label18.TabIndex = 3;
            this.label18.Text = "Residual of Errors";
            // 
            // labelConfig2
            // 
            this.labelConfig2.AutoSize = true;
            this.labelConfig2.Location = new System.Drawing.Point(18, 127);
            this.labelConfig2.Name = "labelConfig2";
            this.labelConfig2.Size = new System.Drawing.Size(119, 16);
            this.labelConfig2.TabIndex = 2;
            this.labelConfig2.Text = "Max Update Value";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 16);
            this.label16.TabIndex = 1;
            this.label16.Text = "Max Number of Epoches";
            // 
            // labelConfig1
            // 
            this.labelConfig1.AutoSize = true;
            this.labelConfig1.Location = new System.Drawing.Point(18, 33);
            this.labelConfig1.Name = "labelConfig1";
            this.labelConfig1.Size = new System.Drawing.Size(136, 16);
            this.labelConfig1.TabIndex = 0;
            this.labelConfig1.Text = "Default Update Value";
            // 
            // groupBoxNetworkConfig
            // 
            this.groupBoxNetworkConfig.Controls.Add(this.label20);
            this.groupBoxNetworkConfig.Controls.Add(this.label19);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkClear);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkSave);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkLoad);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkNew);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumOutput);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumInput);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumHidden);
            this.groupBoxNetworkConfig.Controls.Add(this.label14);
            this.groupBoxNetworkConfig.Location = new System.Drawing.Point(15, 30);
            this.groupBoxNetworkConfig.Name = "groupBoxNetworkConfig";
            this.groupBoxNetworkConfig.Size = new System.Drawing.Size(626, 91);
            this.groupBoxNetworkConfig.TabIndex = 0;
            this.groupBoxNetworkConfig.TabStop = false;
            this.groupBoxNetworkConfig.Text = "Neural model";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(438, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(46, 16);
            this.label20.TabIndex = 9;
            this.label20.Text = "Output";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(231, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 16);
            this.label19.TabIndex = 8;
            this.label19.Text = "Hidden";
            // 
            // btnNetworkClear
            // 
            this.btnNetworkClear.Location = new System.Drawing.Point(495, 58);
            this.btnNetworkClear.Name = "btnNetworkClear";
            this.btnNetworkClear.Size = new System.Drawing.Size(106, 23);
            this.btnNetworkClear.TabIndex = 7;
            this.btnNetworkClear.Text = "Clear";
            this.btnNetworkClear.UseVisualStyleBackColor = true;
            this.btnNetworkClear.Click += new System.EventHandler(this.btnNetworkClear_Click);
            // 
            // btnNetworkSave
            // 
            this.btnNetworkSave.Location = new System.Drawing.Point(336, 58);
            this.btnNetworkSave.Name = "btnNetworkSave";
            this.btnNetworkSave.Size = new System.Drawing.Size(106, 23);
            this.btnNetworkSave.TabIndex = 6;
            this.btnNetworkSave.Text = "Save";
            this.btnNetworkSave.UseVisualStyleBackColor = true;
            this.btnNetworkSave.Click += new System.EventHandler(this.btnNetworkSave_Click);
            // 
            // btnNetworkLoad
            // 
            this.btnNetworkLoad.Location = new System.Drawing.Point(177, 58);
            this.btnNetworkLoad.Name = "btnNetworkLoad";
            this.btnNetworkLoad.Size = new System.Drawing.Size(106, 23);
            this.btnNetworkLoad.TabIndex = 5;
            this.btnNetworkLoad.Text = "Load";
            this.btnNetworkLoad.UseVisualStyleBackColor = true;
            this.btnNetworkLoad.Click += new System.EventHandler(this.btnNetworkLoad_Click);
            // 
            // btnNetworkNew
            // 
            this.btnNetworkNew.Location = new System.Drawing.Point(18, 58);
            this.btnNetworkNew.Name = "btnNetworkNew";
            this.btnNetworkNew.Size = new System.Drawing.Size(106, 23);
            this.btnNetworkNew.TabIndex = 4;
            this.btnNetworkNew.Text = "New";
            this.btnNetworkNew.UseVisualStyleBackColor = true;
            this.btnNetworkNew.Click += new System.EventHandler(this.btnNetworkNew_Click);
            // 
            // txtNumOutput
            // 
            this.txtNumOutput.Location = new System.Drawing.Point(509, 23);
            this.txtNumOutput.Name = "txtNumOutput";
            this.txtNumOutput.ReadOnly = true;
            this.txtNumOutput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNumOutput.Size = new System.Drawing.Size(96, 22);
            this.txtNumOutput.TabIndex = 2;
            this.txtNumOutput.Text = "1";
            // 
            // txtNumInput
            // 
            this.txtNumInput.Location = new System.Drawing.Point(62, 26);
            this.txtNumInput.Name = "txtNumInput";
            this.txtNumInput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNumInput.Size = new System.Drawing.Size(96, 22);
            this.txtNumInput.TabIndex = 1;
            // 
            // txtNumHidden
            // 
            this.txtNumHidden.Location = new System.Drawing.Point(289, 26);
            this.txtNumHidden.Name = "txtNumHidden";
            this.txtNumHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNumHidden.Size = new System.Drawing.Size(96, 22);
            this.txtNumHidden.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "Input";
            // 
            // tabForecast
            // 
            this.tabForecast.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabForecast.Controls.Add(this.btnTest);
            this.tabForecast.Controls.Add(this.btnForecast);
            this.tabForecast.Controls.Add(this.richTextForecast);
            this.tabForecast.Controls.Add(this.chartForecast);
            this.tabForecast.Location = new System.Drawing.Point(4, 25);
            this.tabForecast.Name = "tabForecast";
            this.tabForecast.Padding = new System.Windows.Forms.Padding(3);
            this.tabForecast.Size = new System.Drawing.Size(656, 368);
            this.tabForecast.TabIndex = 3;
            this.tabForecast.Text = "Forecast";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(341, 17);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnForecast
            // 
            this.btnForecast.Location = new System.Drawing.Point(165, 17);
            this.btnForecast.Name = "btnForecast";
            this.btnForecast.Size = new System.Drawing.Size(75, 23);
            this.btnForecast.TabIndex = 2;
            this.btnForecast.Text = "Forecast";
            this.btnForecast.UseVisualStyleBackColor = true;
            this.btnForecast.Click += new System.EventHandler(this.btnForecast_Click);
            // 
            // richTextForecast
            // 
            this.richTextForecast.Location = new System.Drawing.Point(519, 61);
            this.richTextForecast.Name = "richTextForecast";
            this.richTextForecast.ReadOnly = true;
            this.richTextForecast.Size = new System.Drawing.Size(127, 301);
            this.richTextForecast.TabIndex = 1;
            this.richTextForecast.Text = "";
            // 
            // chartForecast
            // 
            chartArea2.Name = "ChartArea1";
            this.chartForecast.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartForecast.Legends.Add(legend2);
            this.chartForecast.Location = new System.Drawing.Point(6, 61);
            this.chartForecast.Name = "chartForecast";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Data";
            this.chartForecast.Series.Add(series2);
            this.chartForecast.Size = new System.Drawing.Size(501, 300);
            this.chartForecast.TabIndex = 0;
            this.chartForecast.Text = "chart1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 325);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 16);
            this.label15.TabIndex = 23;
            this.label15.Text = "Choose model";
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Items.AddRange(new object[] {
            "SARIMA",
            "ANN",
            "SARIMA-ANN"});
            this.comboBoxModel.Location = new System.Drawing.Point(122, 317);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(121, 24);
            this.comboBoxModel.TabIndex = 24;
            this.comboBoxModel.Tag = "";
            this.comboBoxModel.Text = "SARIMA-ANN";
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 403);
            this.Controls.Add(this.tabCreateModel);
            this.Name = "Main_Form";
            this.Text = "Forecast Time Series Model";
            this.tabCreateModel.ResumeLayout(false);
            this.tabChooseData.ResumeLayout(false);
            this.tabChooseData.PerformLayout();
            this.groupBoxTrainingData.ResumeLayout(false);
            this.groupBoxTrainingData.PerformLayout();
            this.groupBoxTestingData.ResumeLayout(false);
            this.groupBoxTestingData.PerformLayout();
            this.tabSARIMAModel.ResumeLayout(false);
            this.tabSARIMAModel.PerformLayout();
            this.groupBoxARIMAParameter.ResumeLayout(false);
            this.groupBoxARIMAParameter.PerformLayout();
            this.tabNeuralNetwork.ResumeLayout(false);
            this.groupBoxNetworkAlgorithm.ResumeLayout(false);
            this.groupBoxNetworkAlgorithm.PerformLayout();
            this.groupBoxAlgorithmConfig.ResumeLayout(false);
            this.groupBoxAlgorithmConfig.PerformLayout();
            this.groupBoxNetworkConfig.ResumeLayout(false);
            this.groupBoxNetworkConfig.PerformLayout();
            this.tabForecast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartForecast)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCreateModel;
        private System.Windows.Forms.TabPage tabChooseData;
        private System.Windows.Forms.TabPage tabSARIMAModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChooseTrainingData;
        private System.Windows.Forms.TextBox textDataFileTraining;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabNeuralNetwork;
        private System.Windows.Forms.TabPage tabForecast;
        private System.Windows.Forms.RadioButton radioBtnManualARIMA;
        private System.Windows.Forms.RadioButton radioBtnAutomaticARIMA;
        private System.Windows.Forms.GroupBox groupBoxARIMAParameter;
        private System.Windows.Forms.Button btnPlotErrorARIMA;
        private System.Windows.Forms.Button btnPartialCorrelation;
        private System.Windows.Forms.Button btnCorrelogram;
        private System.Windows.Forms.Button btnPlotDataARIMA;
        private System.Windows.Forms.Button btnLoadARIMA;
        private System.Windows.Forms.Button btnForecastARIMA;
        private System.Windows.Forms.Button btnTestArima;
        private System.Windows.Forms.Button btnTrainARIMA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnChooseTestingData;
        private System.Windows.Forms.TextBox textDataFileTesting;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRegularDifferencing;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSeasonDifferencing;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSeasonPartern;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnManualRestoreARIMA;
        private System.Windows.Forms.Button btnManualTrainingARIMA;
        private System.Windows.Forms.Button btnRemoveSeason;
        private System.Windows.Forms.TextBox txtMASeason;
        private System.Windows.Forms.TextBox txtARSeason;
        private System.Windows.Forms.TextBox txtMARegular;
        private System.Windows.Forms.TextBox txtARRegular;
        private System.Windows.Forms.Button btnSaveARIMA;
        private System.Windows.Forms.GroupBox groupBoxNetworkConfig;
        private System.Windows.Forms.TextBox txtNumHidden;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNumOutput;
        private System.Windows.Forms.TextBox txtNumInput;
        private System.Windows.Forms.GroupBox groupBoxAlgorithmConfig;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelConfig2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelConfig1;
        private System.Windows.Forms.TextBox txtConfigErrors;
        private System.Windows.Forms.TextBox txtConfig2;
        private System.Windows.Forms.TextBox txtConfigEpoches;
        private System.Windows.Forms.TextBox txtConfig1;
        private System.Windows.Forms.Button btnForecastNeural;
        private System.Windows.Forms.Button btnTestNeural;
        private System.Windows.Forms.Button btnPlotNeural;
        private System.Windows.Forms.Button btnTrainNeural;
        private System.Windows.Forms.GroupBox groupBoxNetworkAlgorithm;
        private System.Windows.Forms.Button btnNetworkClear;
        private System.Windows.Forms.Button btnNetworkSave;
        private System.Windows.Forms.Button btnNetworkLoad;
        private System.Windows.Forms.Button btnNetworkNew;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartForecast;
        private System.Windows.Forms.RichTextBox richTextForecast;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnForecast;
        private System.Windows.Forms.RadioButton radioBackPropagation;
        private System.Windows.Forms.RadioButton radioRPROP;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnChooseData;
        private System.Windows.Forms.Label labelNumRowDataTraining;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label labelNumRowDataTesting;
        private System.Windows.Forms.TextBox txtTrainDataToRow;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTrainDataFromRow;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTestDataToRow;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTestDataFromRow;
        private System.Windows.Forms.GroupBox groupBoxTrainingData;
        private System.Windows.Forms.GroupBox groupBoxTestingData;
        private System.Windows.Forms.Label labelNumColumnDataTraining;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrainDataColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelNumColumnDataTesting;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtTestDataColumn;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.Label label15;
    }
}