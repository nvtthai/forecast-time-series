namespace ForecastTimeSeries
{
    partial class MainInterface
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnPlotErrorARIMA = new System.Windows.Forms.Button();
            this.btnTestArima = new System.Windows.Forms.Button();
            this.btnForecastARIMA = new System.Windows.Forms.Button();
            this.radioBtnManualARIMA = new System.Windows.Forms.RadioButton();
            this.btnSaveARIMA = new System.Windows.Forms.Button();
            this.btnLoadARIMA = new System.Windows.Forms.Button();
            this.labelTrainDataNumRows = new System.Windows.Forms.Label();
            this.labelTrainDataNumColumns = new System.Windows.Forms.Label();
            this.btnTrainARIMA = new System.Windows.Forms.Button();
            this.groupBoxARIMAParameter = new System.Windows.Forms.GroupBox();
            this.txtSeasonPartern = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRemoveSeason = new System.Windows.Forms.Button();
            this.txtMASeason = new System.Windows.Forms.TextBox();
            this.txtARSeason = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSeasonDifferencing = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnManualRestoreARIMA = new System.Windows.Forms.Button();
            this.btnManualTrainingARIMA = new System.Windows.Forms.Button();
            this.txtMARegular = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtARRegular = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRegularDifferencing = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioBtnAutomaticARIMA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.richARIMAModel = new System.Windows.Forms.RichTextBox();
            this.btnPartialCorrelation = new System.Windows.Forms.Button();
            this.btnCorrelogram = new System.Windows.Forms.Button();
            this.btnPlotDataARIMA = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.txtTrainDataColumn = new System.Windows.Forms.TextBox();
            this.txtTrainDataToRow = new System.Windows.Forms.TextBox();
            this.txtTrainDataFromRow = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnChooseData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.btnForecastNeural = new System.Windows.Forms.Button();
            this.btnTestNeural = new System.Windows.Forms.Button();
            this.btnPlotNeural = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioRPROP = new System.Windows.Forms.RadioButton();
            this.radioBackPropagation = new System.Windows.Forms.RadioButton();
            this.btnTrainNeural = new System.Windows.Forms.Button();
            this.groupBoxAlgorithmConfig = new System.Windows.Forms.GroupBox();
            this.txtConfigErrors = new System.Windows.Forms.TextBox();
            this.txtConfig2 = new System.Windows.Forms.TextBox();
            this.txtConfigEpoches = new System.Windows.Forms.TextBox();
            this.txtConfig1 = new System.Windows.Forms.TextBox();
            this.labelConfigResidual = new System.Windows.Forms.Label();
            this.labelConfigEpoches = new System.Windows.Forms.Label();
            this.labelConfig2 = new System.Windows.Forms.Label();
            this.labelConfig1 = new System.Windows.Forms.Label();
            this.groupBoxNetworkConfig = new System.Windows.Forms.GroupBox();
            this.btnNetworkClear = new System.Windows.Forms.Button();
            this.btnNetworkSave = new System.Windows.Forms.Button();
            this.btnNetworkLoad = new System.Windows.Forms.Button();
            this.btnNetworkNew = new System.Windows.Forms.Button();
            this.txtNumOutput = new System.Windows.Forms.TextBox();
            this.txtNumHidden = new System.Windows.Forms.TextBox();
            this.txtNumInput = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextForecast = new System.Windows.Forms.RichTextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnForecast = new System.Windows.Forms.Button();
            this.textBoxNHead = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chartForecast = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxARIMAParameter.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAlgorithmConfig.SuspendLayout();
            this.groupBoxNetworkConfig.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 482);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage1.Controls.Add(this.btnPlotErrorARIMA);
            this.tabPage1.Controls.Add(this.btnTestArima);
            this.tabPage1.Controls.Add(this.btnForecastARIMA);
            this.tabPage1.Controls.Add(this.radioBtnManualARIMA);
            this.tabPage1.Controls.Add(this.btnSaveARIMA);
            this.tabPage1.Controls.Add(this.btnLoadARIMA);
            this.tabPage1.Controls.Add(this.labelTrainDataNumRows);
            this.tabPage1.Controls.Add(this.labelTrainDataNumColumns);
            this.tabPage1.Controls.Add(this.btnTrainARIMA);
            this.tabPage1.Controls.Add(this.groupBoxARIMAParameter);
            this.tabPage1.Controls.Add(this.radioBtnAutomaticARIMA);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.richARIMAModel);
            this.tabPage1.Controls.Add(this.btnPartialCorrelation);
            this.tabPage1.Controls.Add(this.btnCorrelogram);
            this.tabPage1.Controls.Add(this.btnPlotDataARIMA);
            this.tabPage1.Controls.Add(this.btnGetData);
            this.tabPage1.Controls.Add(this.txtTrainDataColumn);
            this.tabPage1.Controls.Add(this.txtTrainDataToRow);
            this.tabPage1.Controls.Add(this.txtTrainDataFromRow);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.btnChooseData);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(909, 451);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SARIMA";
            // 
            // btnPlotErrorARIMA
            // 
            this.btnPlotErrorARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlotErrorARIMA.Location = new System.Drawing.Point(178, 284);
            this.btnPlotErrorARIMA.Name = "btnPlotErrorARIMA";
            this.btnPlotErrorARIMA.Size = new System.Drawing.Size(170, 51);
            this.btnPlotErrorARIMA.TabIndex = 51;
            this.btnPlotErrorARIMA.Text = "Plot Error";
            this.btnPlotErrorARIMA.UseVisualStyleBackColor = true;
            this.btnPlotErrorARIMA.Click += new System.EventHandler(this.btnPlotErrorARIMA_Click);
            // 
            // btnTestArima
            // 
            this.btnTestArima.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestArima.Location = new System.Drawing.Point(495, 247);
            this.btnTestArima.Name = "btnTestArima";
            this.btnTestArima.Size = new System.Drawing.Size(90, 36);
            this.btnTestArima.TabIndex = 50;
            this.btnTestArima.Text = "Test";
            this.btnTestArima.UseVisualStyleBackColor = true;
            this.btnTestArima.Click += new System.EventHandler(this.btnTestArima_Click);
            // 
            // btnForecastARIMA
            // 
            this.btnForecastARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForecastARIMA.Location = new System.Drawing.Point(600, 247);
            this.btnForecastARIMA.Name = "btnForecastARIMA";
            this.btnForecastARIMA.Size = new System.Drawing.Size(90, 36);
            this.btnForecastARIMA.TabIndex = 49;
            this.btnForecastARIMA.Text = "ForeCast";
            this.btnForecastARIMA.UseVisualStyleBackColor = true;
            this.btnForecastARIMA.Click += new System.EventHandler(this.btnForecastARIMA_Click);
            // 
            // radioBtnManualARIMA
            // 
            this.radioBtnManualARIMA.AutoSize = true;
            this.radioBtnManualARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnManualARIMA.ForeColor = System.Drawing.Color.Black;
            this.radioBtnManualARIMA.Location = new System.Drawing.Point(499, 9);
            this.radioBtnManualARIMA.Name = "radioBtnManualARIMA";
            this.radioBtnManualARIMA.Size = new System.Drawing.Size(116, 22);
            this.radioBtnManualARIMA.TabIndex = 48;
            this.radioBtnManualARIMA.TabStop = true;
            this.radioBtnManualARIMA.Text = "Manual mode";
            this.radioBtnManualARIMA.UseVisualStyleBackColor = true;
            this.radioBtnManualARIMA.CheckedChanged += new System.EventHandler(this.btnManualARIMA_CheckedChanged);
            // 
            // btnSaveARIMA
            // 
            this.btnSaveARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveARIMA.ForeColor = System.Drawing.Color.Black;
            this.btnSaveARIMA.Location = new System.Drawing.Point(810, 247);
            this.btnSaveARIMA.Name = "btnSaveARIMA";
            this.btnSaveARIMA.Size = new System.Drawing.Size(90, 36);
            this.btnSaveARIMA.TabIndex = 47;
            this.btnSaveARIMA.Text = "Save";
            this.btnSaveARIMA.UseVisualStyleBackColor = true;
            this.btnSaveARIMA.Click += new System.EventHandler(this.btnSaveARIMA_Click);
            // 
            // btnLoadARIMA
            // 
            this.btnLoadARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadARIMA.ForeColor = System.Drawing.Color.Black;
            this.btnLoadARIMA.Location = new System.Drawing.Point(705, 247);
            this.btnLoadARIMA.Name = "btnLoadARIMA";
            this.btnLoadARIMA.Size = new System.Drawing.Size(90, 36);
            this.btnLoadARIMA.TabIndex = 46;
            this.btnLoadARIMA.Text = "Load";
            this.btnLoadARIMA.UseVisualStyleBackColor = true;
            this.btnLoadARIMA.Click += new System.EventHandler(this.btnLoadARIMA_Click);
            // 
            // labelTrainDataNumRows
            // 
            this.labelTrainDataNumRows.AutoSize = true;
            this.labelTrainDataNumRows.Location = new System.Drawing.Point(141, 52);
            this.labelTrainDataNumRows.Name = "labelTrainDataNumRows";
            this.labelTrainDataNumRows.Size = new System.Drawing.Size(0, 18);
            this.labelTrainDataNumRows.TabIndex = 45;
            // 
            // labelTrainDataNumColumns
            // 
            this.labelTrainDataNumColumns.AutoSize = true;
            this.labelTrainDataNumColumns.Location = new System.Drawing.Point(141, 30);
            this.labelTrainDataNumColumns.Name = "labelTrainDataNumColumns";
            this.labelTrainDataNumColumns.Size = new System.Drawing.Size(0, 18);
            this.labelTrainDataNumColumns.TabIndex = 44;
            // 
            // btnTrainARIMA
            // 
            this.btnTrainARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrainARIMA.Location = new System.Drawing.Point(378, 247);
            this.btnTrainARIMA.Name = "btnTrainARIMA";
            this.btnTrainARIMA.Size = new System.Drawing.Size(102, 36);
            this.btnTrainARIMA.TabIndex = 43;
            this.btnTrainARIMA.Text = "Auto Training";
            this.btnTrainARIMA.UseVisualStyleBackColor = true;
            this.btnTrainARIMA.Click += new System.EventHandler(this.btnAutomaticTrainingARIMA_Click);
            // 
            // groupBoxARIMAParameter
            // 
            this.groupBoxARIMAParameter.Controls.Add(this.txtSeasonPartern);
            this.groupBoxARIMAParameter.Controls.Add(this.label9);
            this.groupBoxARIMAParameter.Controls.Add(this.btnRemoveSeason);
            this.groupBoxARIMAParameter.Controls.Add(this.txtMASeason);
            this.groupBoxARIMAParameter.Controls.Add(this.txtARSeason);
            this.groupBoxARIMAParameter.Controls.Add(this.label8);
            this.groupBoxARIMAParameter.Controls.Add(this.label5);
            this.groupBoxARIMAParameter.Controls.Add(this.txtSeasonDifferencing);
            this.groupBoxARIMAParameter.Controls.Add(this.label4);
            this.groupBoxARIMAParameter.Controls.Add(this.btnManualRestoreARIMA);
            this.groupBoxARIMAParameter.Controls.Add(this.btnManualTrainingARIMA);
            this.groupBoxARIMAParameter.Controls.Add(this.txtMARegular);
            this.groupBoxARIMAParameter.Controls.Add(this.label7);
            this.groupBoxARIMAParameter.Controls.Add(this.txtARRegular);
            this.groupBoxARIMAParameter.Controls.Add(this.label6);
            this.groupBoxARIMAParameter.Controls.Add(this.txtRegularDifferencing);
            this.groupBoxARIMAParameter.Controls.Add(this.label3);
            this.groupBoxARIMAParameter.Enabled = false;
            this.groupBoxARIMAParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxARIMAParameter.Location = new System.Drawing.Point(370, 32);
            this.groupBoxARIMAParameter.Name = "groupBoxARIMAParameter";
            this.groupBoxARIMAParameter.Size = new System.Drawing.Size(533, 203);
            this.groupBoxARIMAParameter.TabIndex = 42;
            this.groupBoxARIMAParameter.TabStop = false;
            this.groupBoxARIMAParameter.Text = "ARIMA Coef";
            // 
            // txtSeasonPartern
            // 
            this.txtSeasonPartern.Location = new System.Drawing.Point(220, 75);
            this.txtSeasonPartern.Name = "txtSeasonPartern";
            this.txtSeasonPartern.Size = new System.Drawing.Size(100, 24);
            this.txtSeasonPartern.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 18);
            this.label9.TabIndex = 24;
            this.label9.Text = "Season Partern";
            // 
            // btnRemoveSeason
            // 
            this.btnRemoveSeason.Location = new System.Drawing.Point(361, 44);
            this.btnRemoveSeason.Name = "btnRemoveSeason";
            this.btnRemoveSeason.Size = new System.Drawing.Size(128, 35);
            this.btnRemoveSeason.TabIndex = 23;
            this.btnRemoveSeason.Text = "Remove Season";
            this.btnRemoveSeason.UseVisualStyleBackColor = true;
            this.btnRemoveSeason.Click += new System.EventHandler(this.btnRemoveSeason_Click);
            // 
            // txtMASeason
            // 
            this.txtMASeason.Location = new System.Drawing.Point(386, 140);
            this.txtMASeason.Name = "txtMASeason";
            this.txtMASeason.Size = new System.Drawing.Size(100, 24);
            this.txtMASeason.TabIndex = 21;
            // 
            // txtARSeason
            // 
            this.txtARSeason.Location = new System.Drawing.Point(145, 140);
            this.txtARSeason.Name = "txtARSeason";
            this.txtARSeason.Size = new System.Drawing.Size(100, 24);
            this.txtARSeason.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(286, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 18);
            this.label8.TabIndex = 19;
            this.label8.Text = "MA Season";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "AR Season";
            // 
            // txtSeasonDifferencing
            // 
            this.txtSeasonDifferencing.Location = new System.Drawing.Point(220, 46);
            this.txtSeasonDifferencing.Name = "txtSeasonDifferencing";
            this.txtSeasonDifferencing.Size = new System.Drawing.Size(100, 24);
            this.txtSeasonDifferencing.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Season differencing";
            // 
            // btnManualRestoreARIMA
            // 
            this.btnManualRestoreARIMA.Location = new System.Drawing.Point(289, 168);
            this.btnManualRestoreARIMA.Name = "btnManualRestoreARIMA";
            this.btnManualRestoreARIMA.Size = new System.Drawing.Size(75, 29);
            this.btnManualRestoreARIMA.TabIndex = 15;
            this.btnManualRestoreARIMA.Text = "Restore";
            this.btnManualRestoreARIMA.UseVisualStyleBackColor = true;
            this.btnManualRestoreARIMA.Click += new System.EventHandler(this.btnManualRestoreARIMA_Click);
            // 
            // btnManualTrainingARIMA
            // 
            this.btnManualTrainingARIMA.Location = new System.Drawing.Point(129, 166);
            this.btnManualTrainingARIMA.Name = "btnManualTrainingARIMA";
            this.btnManualTrainingARIMA.Size = new System.Drawing.Size(75, 31);
            this.btnManualTrainingARIMA.TabIndex = 14;
            this.btnManualTrainingARIMA.Text = "Process";
            this.btnManualTrainingARIMA.UseVisualStyleBackColor = true;
            this.btnManualTrainingARIMA.Click += new System.EventHandler(this.btnManualTrainingARIMA_Click);
            // 
            // txtMARegular
            // 
            this.txtMARegular.Location = new System.Drawing.Point(386, 110);
            this.txtMARegular.Name = "txtMARegular";
            this.txtMARegular.Size = new System.Drawing.Size(100, 24);
            this.txtMARegular.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "MA Regular";
            // 
            // txtARRegular
            // 
            this.txtARRegular.Location = new System.Drawing.Point(145, 106);
            this.txtARRegular.Name = "txtARRegular";
            this.txtARRegular.Size = new System.Drawing.Size(100, 24);
            this.txtARRegular.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "AR  Regular";
            // 
            // txtRegularDifferencing
            // 
            this.txtRegularDifferencing.Location = new System.Drawing.Point(220, 17);
            this.txtRegularDifferencing.Name = "txtRegularDifferencing";
            this.txtRegularDifferencing.Size = new System.Drawing.Size(100, 24);
            this.txtRegularDifferencing.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Regular differencing";
            // 
            // radioBtnAutomaticARIMA
            // 
            this.radioBtnAutomaticARIMA.AutoSize = true;
            this.radioBtnAutomaticARIMA.Checked = true;
            this.radioBtnAutomaticARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnAutomaticARIMA.Location = new System.Drawing.Point(367, 9);
            this.radioBtnAutomaticARIMA.Name = "radioBtnAutomaticARIMA";
            this.radioBtnAutomaticARIMA.Size = new System.Drawing.Size(134, 22);
            this.radioBtnAutomaticARIMA.TabIndex = 41;
            this.radioBtnAutomaticARIMA.TabStop = true;
            this.radioBtnAutomaticARIMA.Text = "Automatic mode";
            this.radioBtnAutomaticARIMA.UseVisualStyleBackColor = true;
            this.radioBtnAutomaticARIMA.CheckedChanged += new System.EventHandler(this.btnAutomaticARIMA_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(364, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 40;
            this.label2.Text = "ARIMA model";
            // 
            // richARIMAModel
            // 
            this.richARIMAModel.Location = new System.Drawing.Point(367, 321);
            this.richARIMAModel.Name = "richARIMAModel";
            this.richARIMAModel.ReadOnly = true;
            this.richARIMAModel.Size = new System.Drawing.Size(533, 112);
            this.richARIMAModel.TabIndex = 39;
            this.richARIMAModel.Text = "";
            this.richARIMAModel.WordWrap = false;
            // 
            // btnPartialCorrelation
            // 
            this.btnPartialCorrelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPartialCorrelation.Location = new System.Drawing.Point(178, 366);
            this.btnPartialCorrelation.Name = "btnPartialCorrelation";
            this.btnPartialCorrelation.Size = new System.Drawing.Size(170, 51);
            this.btnPartialCorrelation.TabIndex = 38;
            this.btnPartialCorrelation.Text = "Partial Correlogram";
            this.btnPartialCorrelation.UseVisualStyleBackColor = true;
            this.btnPartialCorrelation.Click += new System.EventHandler(this.btnPartialCorrelation_Click);
            // 
            // btnCorrelogram
            // 
            this.btnCorrelogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorrelogram.Location = new System.Drawing.Point(18, 366);
            this.btnCorrelogram.Name = "btnCorrelogram";
            this.btnCorrelogram.Size = new System.Drawing.Size(139, 51);
            this.btnCorrelogram.TabIndex = 35;
            this.btnCorrelogram.Text = "Correlogram";
            this.btnCorrelogram.UseVisualStyleBackColor = true;
            this.btnCorrelogram.Click += new System.EventHandler(this.btnCorrelogram_Click);
            // 
            // btnPlotDataARIMA
            // 
            this.btnPlotDataARIMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlotDataARIMA.Location = new System.Drawing.Point(18, 284);
            this.btnPlotDataARIMA.Name = "btnPlotDataARIMA";
            this.btnPlotDataARIMA.Size = new System.Drawing.Size(138, 51);
            this.btnPlotDataARIMA.TabIndex = 34;
            this.btnPlotDataARIMA.Text = "Plot Data";
            this.btnPlotDataARIMA.UseVisualStyleBackColor = true;
            this.btnPlotDataARIMA.Click += new System.EventHandler(this.btnPlotDataARIMA_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetData.Location = new System.Drawing.Point(122, 198);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(95, 37);
            this.btnGetData.TabIndex = 32;
            this.btnGetData.Text = "Get";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // txtTrainDataColumn
            // 
            this.txtTrainDataColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrainDataColumn.Location = new System.Drawing.Point(77, 149);
            this.txtTrainDataColumn.Name = "txtTrainDataColumn";
            this.txtTrainDataColumn.Size = new System.Drawing.Size(94, 24);
            this.txtTrainDataColumn.TabIndex = 31;
            // 
            // txtTrainDataToRow
            // 
            this.txtTrainDataToRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrainDataToRow.Location = new System.Drawing.Point(205, 103);
            this.txtTrainDataToRow.Name = "txtTrainDataToRow";
            this.txtTrainDataToRow.Size = new System.Drawing.Size(100, 24);
            this.txtTrainDataToRow.TabIndex = 30;
            // 
            // txtTrainDataFromRow
            // 
            this.txtTrainDataFromRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrainDataFromRow.Location = new System.Drawing.Point(77, 102);
            this.txtTrainDataFromRow.Name = "txtTrainDataFromRow";
            this.txtTrainDataFromRow.Size = new System.Drawing.Size(94, 24);
            this.txtTrainDataFromRow.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 152);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 18);
            this.label15.TabIndex = 28;
            this.label15.Text = "Column";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(16, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 18);
            this.label14.TabIndex = 27;
            this.label14.Text = "Row";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 18);
            this.label13.TabIndex = 26;
            this.label13.Text = "Num Columns:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 18);
            this.label12.TabIndex = 25;
            this.label12.Text = "Num Rows:";
            // 
            // btnChooseData
            // 
            this.btnChooseData.BackColor = System.Drawing.Color.Gainsboro;
            this.btnChooseData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseData.Location = new System.Drawing.Point(208, 8);
            this.btnChooseData.Name = "btnChooseData";
            this.btnChooseData.Size = new System.Drawing.Size(75, 35);
            this.btnChooseData.TabIndex = 23;
            this.btnChooseData.Text = "Browse";
            this.btnChooseData.UseVisualStyleBackColor = false;
            this.btnChooseData.Click += new System.EventHandler(this.btnChooseData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Time Series Data";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnForecastNeural);
            this.tabPage2.Controls.Add(this.btnTestNeural);
            this.tabPage2.Controls.Add(this.btnPlotNeural);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.btnTrainNeural);
            this.tabPage2.Controls.Add(this.groupBoxAlgorithmConfig);
            this.tabPage2.Controls.Add(this.groupBoxNetworkConfig);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(909, 451);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Neural Network";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "ChooseData";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnForecastNeural
            // 
            this.btnForecastNeural.Location = new System.Drawing.Point(384, 341);
            this.btnForecastNeural.Name = "btnForecastNeural";
            this.btnForecastNeural.Size = new System.Drawing.Size(90, 39);
            this.btnForecastNeural.TabIndex = 8;
            this.btnForecastNeural.Text = "Forecast";
            this.btnForecastNeural.UseVisualStyleBackColor = true;
            this.btnForecastNeural.Click += new System.EventHandler(this.btnForecastNeural_Click);
            // 
            // btnTestNeural
            // 
            this.btnTestNeural.Location = new System.Drawing.Point(267, 341);
            this.btnTestNeural.Name = "btnTestNeural";
            this.btnTestNeural.Size = new System.Drawing.Size(90, 39);
            this.btnTestNeural.TabIndex = 7;
            this.btnTestNeural.Text = "Test";
            this.btnTestNeural.UseVisualStyleBackColor = true;
            this.btnTestNeural.Click += new System.EventHandler(this.btnTestNeural_Click);
            // 
            // btnPlotNeural
            // 
            this.btnPlotNeural.Location = new System.Drawing.Point(150, 341);
            this.btnPlotNeural.Name = "btnPlotNeural";
            this.btnPlotNeural.Size = new System.Drawing.Size(90, 39);
            this.btnPlotNeural.TabIndex = 6;
            this.btnPlotNeural.Text = "Plot";
            this.btnPlotNeural.UseVisualStyleBackColor = true;
            this.btnPlotNeural.Click += new System.EventHandler(this.btnPlotNeural_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioRPROP);
            this.groupBox3.Controls.Add(this.radioBackPropagation);
            this.groupBox3.Location = new System.Drawing.Point(37, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 75);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Algorithm";
            // 
            // radioRPROP
            // 
            this.radioRPROP.AutoSize = true;
            this.radioRPROP.Checked = true;
            this.radioRPROP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioRPROP.Location = new System.Drawing.Point(62, 39);
            this.radioRPROP.Name = "radioRPROP";
            this.radioRPROP.Size = new System.Drawing.Size(80, 22);
            this.radioRPROP.TabIndex = 16;
            this.radioRPROP.TabStop = true;
            this.radioRPROP.Text = "RPROP";
            this.radioRPROP.UseVisualStyleBackColor = true;
            this.radioRPROP.CheckedChanged += new System.EventHandler(this.radioRPROP_CheckedChanged);
            // 
            // radioBackPropagation
            // 
            this.radioBackPropagation.AutoSize = true;
            this.radioBackPropagation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBackPropagation.Location = new System.Drawing.Point(62, 19);
            this.radioBackPropagation.Name = "radioBackPropagation";
            this.radioBackPropagation.Size = new System.Drawing.Size(144, 22);
            this.radioBackPropagation.TabIndex = 15;
            this.radioBackPropagation.Text = "Back Propagation";
            this.radioBackPropagation.UseVisualStyleBackColor = true;
            this.radioBackPropagation.CheckedChanged += new System.EventHandler(this.radioBackPropagation_CheckedChanged);
            // 
            // btnTrainNeural
            // 
            this.btnTrainNeural.Location = new System.Drawing.Point(33, 341);
            this.btnTrainNeural.Name = "btnTrainNeural";
            this.btnTrainNeural.Size = new System.Drawing.Size(90, 38);
            this.btnTrainNeural.TabIndex = 5;
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
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfigResidual);
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfigEpoches);
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfig2);
            this.groupBoxAlgorithmConfig.Controls.Add(this.labelConfig1);
            this.groupBoxAlgorithmConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAlgorithmConfig.Location = new System.Drawing.Point(504, 186);
            this.groupBoxAlgorithmConfig.Name = "groupBoxAlgorithmConfig";
            this.groupBoxAlgorithmConfig.Size = new System.Drawing.Size(390, 226);
            this.groupBoxAlgorithmConfig.TabIndex = 4;
            this.groupBoxAlgorithmConfig.TabStop = false;
            this.groupBoxAlgorithmConfig.Text = "RPROP Config";
            // 
            // txtConfigErrors
            // 
            this.txtConfigErrors.Location = new System.Drawing.Point(199, 155);
            this.txtConfigErrors.Name = "txtConfigErrors";
            this.txtConfigErrors.Size = new System.Drawing.Size(100, 24);
            this.txtConfigErrors.TabIndex = 7;
            // 
            // txtConfig2
            // 
            this.txtConfig2.Location = new System.Drawing.Point(197, 114);
            this.txtConfig2.Name = "txtConfig2";
            this.txtConfig2.Size = new System.Drawing.Size(100, 24);
            this.txtConfig2.TabIndex = 6;
            // 
            // txtConfigEpoches
            // 
            this.txtConfigEpoches.Location = new System.Drawing.Point(197, 71);
            this.txtConfigEpoches.Name = "txtConfigEpoches";
            this.txtConfigEpoches.Size = new System.Drawing.Size(100, 24);
            this.txtConfigEpoches.TabIndex = 5;
            // 
            // txtConfig1
            // 
            this.txtConfig1.Location = new System.Drawing.Point(197, 33);
            this.txtConfig1.Name = "txtConfig1";
            this.txtConfig1.Size = new System.Drawing.Size(100, 24);
            this.txtConfig1.TabIndex = 4;
            // 
            // labelConfigResidual
            // 
            this.labelConfigResidual.AutoSize = true;
            this.labelConfigResidual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfigResidual.Location = new System.Drawing.Point(9, 155);
            this.labelConfigResidual.Name = "labelConfigResidual";
            this.labelConfigResidual.Size = new System.Drawing.Size(128, 18);
            this.labelConfigResidual.TabIndex = 3;
            this.labelConfigResidual.Text = "Residual of Errors";
            // 
            // labelConfigEpoches
            // 
            this.labelConfigEpoches.AutoSize = true;
            this.labelConfigEpoches.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfigEpoches.Location = new System.Drawing.Point(9, 74);
            this.labelConfigEpoches.Name = "labelConfigEpoches";
            this.labelConfigEpoches.Size = new System.Drawing.Size(173, 18);
            this.labelConfigEpoches.TabIndex = 2;
            this.labelConfigEpoches.Text = "Max Number of Epoches";
            // 
            // labelConfig2
            // 
            this.labelConfig2.AutoSize = true;
            this.labelConfig2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfig2.Location = new System.Drawing.Point(9, 119);
            this.labelConfig2.Name = "labelConfig2";
            this.labelConfig2.Size = new System.Drawing.Size(127, 18);
            this.labelConfig2.TabIndex = 1;
            this.labelConfig2.Text = "Max Update Value";
            // 
            // labelConfig1
            // 
            this.labelConfig1.AutoSize = true;
            this.labelConfig1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfig1.Location = new System.Drawing.Point(9, 36);
            this.labelConfig1.Name = "labelConfig1";
            this.labelConfig1.Size = new System.Drawing.Size(145, 18);
            this.labelConfig1.TabIndex = 0;
            this.labelConfig1.Text = "Default Update Value";
            // 
            // groupBoxNetworkConfig
            // 
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkClear);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkSave);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkLoad);
            this.groupBoxNetworkConfig.Controls.Add(this.btnNetworkNew);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumOutput);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumHidden);
            this.groupBoxNetworkConfig.Controls.Add(this.txtNumInput);
            this.groupBoxNetworkConfig.Controls.Add(this.label11);
            this.groupBoxNetworkConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNetworkConfig.Location = new System.Drawing.Point(28, 36);
            this.groupBoxNetworkConfig.Name = "groupBoxNetworkConfig";
            this.groupBoxNetworkConfig.Size = new System.Drawing.Size(866, 87);
            this.groupBoxNetworkConfig.TabIndex = 1;
            this.groupBoxNetworkConfig.TabStop = false;
            this.groupBoxNetworkConfig.Text = "Network Model";
            // 
            // btnNetworkClear
            // 
            this.btnNetworkClear.Enabled = false;
            this.btnNetworkClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkClear.ForeColor = System.Drawing.Color.Black;
            this.btnNetworkClear.Location = new System.Drawing.Point(754, 25);
            this.btnNetworkClear.Name = "btnNetworkClear";
            this.btnNetworkClear.Size = new System.Drawing.Size(85, 38);
            this.btnNetworkClear.TabIndex = 10;
            this.btnNetworkClear.Text = "Clear";
            this.btnNetworkClear.UseVisualStyleBackColor = true;
            this.btnNetworkClear.Click += new System.EventHandler(this.btnNetworkClear_Click);
            // 
            // btnNetworkSave
            // 
            this.btnNetworkSave.Enabled = false;
            this.btnNetworkSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkSave.ForeColor = System.Drawing.Color.Black;
            this.btnNetworkSave.Location = new System.Drawing.Point(613, 25);
            this.btnNetworkSave.Name = "btnNetworkSave";
            this.btnNetworkSave.Size = new System.Drawing.Size(99, 38);
            this.btnNetworkSave.TabIndex = 9;
            this.btnNetworkSave.Text = "Save";
            this.btnNetworkSave.UseVisualStyleBackColor = true;
            this.btnNetworkSave.Click += new System.EventHandler(this.btnNetworkSave_Click);
            // 
            // btnNetworkLoad
            // 
            this.btnNetworkLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkLoad.ForeColor = System.Drawing.Color.Black;
            this.btnNetworkLoad.Location = new System.Drawing.Point(476, 25);
            this.btnNetworkLoad.Name = "btnNetworkLoad";
            this.btnNetworkLoad.Size = new System.Drawing.Size(95, 38);
            this.btnNetworkLoad.TabIndex = 8;
            this.btnNetworkLoad.Text = "Load";
            this.btnNetworkLoad.UseVisualStyleBackColor = true;
            this.btnNetworkLoad.Click += new System.EventHandler(this.btnNetworkLoad_Click);
            // 
            // btnNetworkNew
            // 
            this.btnNetworkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkNew.Location = new System.Drawing.Point(339, 25);
            this.btnNetworkNew.Name = "btnNetworkNew";
            this.btnNetworkNew.Size = new System.Drawing.Size(95, 38);
            this.btnNetworkNew.TabIndex = 7;
            this.btnNetworkNew.Text = "New";
            this.btnNetworkNew.UseVisualStyleBackColor = true;
            this.btnNetworkNew.Click += new System.EventHandler(this.btnNetworkNew_Click);
            // 
            // txtNumOutput
            // 
            this.txtNumOutput.Enabled = false;
            this.txtNumOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumOutput.Location = new System.Drawing.Point(262, 37);
            this.txtNumOutput.Name = "txtNumOutput";
            this.txtNumOutput.Size = new System.Drawing.Size(43, 24);
            this.txtNumOutput.TabIndex = 4;
            this.txtNumOutput.Text = "1";
            // 
            // txtNumHidden
            // 
            this.txtNumHidden.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumHidden.Location = new System.Drawing.Point(209, 38);
            this.txtNumHidden.Name = "txtNumHidden";
            this.txtNumHidden.Size = new System.Drawing.Size(43, 24);
            this.txtNumHidden.TabIndex = 3;
            // 
            // txtNumInput
            // 
            this.txtNumInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumInput.Location = new System.Drawing.Point(156, 37);
            this.txtNumInput.Name = "txtNumInput";
            this.txtNumInput.Size = new System.Drawing.Size(43, 24);
            this.txtNumInput.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 18);
            this.label11.TabIndex = 1;
            this.label11.Text = "Input-Hidden-Output";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage3.Controls.Add(this.richTextForecast);
            this.tabPage3.Controls.Add(this.btnTest);
            this.tabPage3.Controls.Add(this.btnForecast);
            this.tabPage3.Controls.Add(this.textBoxNHead);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.chartForecast);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(909, 451);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Forecast";
            // 
            // richTextForecast
            // 
            this.richTextForecast.Location = new System.Drawing.Point(668, 68);
            this.richTextForecast.Name = "richTextForecast";
            this.richTextForecast.Size = new System.Drawing.Size(228, 376);
            this.richTextForecast.TabIndex = 5;
            this.richTextForecast.Text = "";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(425, 23);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 27);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // btnForecast
            // 
            this.btnForecast.Location = new System.Drawing.Point(287, 23);
            this.btnForecast.Name = "btnForecast";
            this.btnForecast.Size = new System.Drawing.Size(88, 27);
            this.btnForecast.TabIndex = 3;
            this.btnForecast.Text = "Forecast";
            this.btnForecast.UseVisualStyleBackColor = true;
            this.btnForecast.Click += new System.EventHandler(this.buttonForecast_Click);
            // 
            // textBoxNHead
            // 
            this.textBoxNHead.Location = new System.Drawing.Point(135, 26);
            this.textBoxNHead.Name = "textBoxNHead";
            this.textBoxNHead.Size = new System.Drawing.Size(100, 24);
            this.textBoxNHead.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "N ahead";
            // 
            // chartForecast
            // 
            chartArea1.Name = "ChartArea1";
            this.chartForecast.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartForecast.Legends.Add(legend1);
            this.chartForecast.Location = new System.Drawing.Point(6, 66);
            this.chartForecast.Name = "chartForecast";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series";
            this.chartForecast.Series.Add(series1);
            this.chartForecast.Size = new System.Drawing.Size(649, 379);
            this.chartForecast.TabIndex = 0;
            this.chartForecast.Text = "chart1";
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 487);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainInterface";
            this.Text = "Forecast Time Series Model";
            this.Load += new System.EventHandler(this.MainInterface_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxARIMAParameter.ResumeLayout(false);
            this.groupBoxARIMAParameter.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxAlgorithmConfig.ResumeLayout(false);
            this.groupBoxAlgorithmConfig.PerformLayout();
            this.groupBoxNetworkConfig.ResumeLayout(false);
            this.groupBoxNetworkConfig.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartForecast)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox txtTrainDataColumn;
        private System.Windows.Forms.TextBox txtTrainDataToRow;
        private System.Windows.Forms.TextBox txtTrainDataFromRow;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnChooseData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPartialCorrelation;
        private System.Windows.Forms.Button btnCorrelogram;
        private System.Windows.Forms.Button btnPlotDataARIMA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richARIMAModel;
        private System.Windows.Forms.RadioButton radioBtnAutomaticARIMA;
        private System.Windows.Forms.GroupBox groupBoxARIMAParameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegularDifferencing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtARRegular;
        private System.Windows.Forms.TextBox txtMARegular;
        private System.Windows.Forms.Button btnTrainARIMA;
        private System.Windows.Forms.GroupBox groupBoxNetworkConfig;
        private System.Windows.Forms.Button btnNetworkClear;
        private System.Windows.Forms.Button btnNetworkSave;
        private System.Windows.Forms.Button btnNetworkLoad;
        private System.Windows.Forms.Button btnNetworkNew;
        private System.Windows.Forms.TextBox txtNumOutput;
        private System.Windows.Forms.TextBox txtNumHidden;
        private System.Windows.Forms.TextBox txtNumInput;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelTrainDataNumRows;
        private System.Windows.Forms.Label labelTrainDataNumColumns;
        private System.Windows.Forms.Button btnManualRestoreARIMA;
        private System.Windows.Forms.Button btnManualTrainingARIMA;
        private System.Windows.Forms.Button btnSaveARIMA;
        private System.Windows.Forms.Button btnLoadARIMA;
        private System.Windows.Forms.RadioButton radioBtnManualARIMA;
        private System.Windows.Forms.GroupBox groupBoxAlgorithmConfig;
        private System.Windows.Forms.TextBox txtConfigErrors;
        private System.Windows.Forms.TextBox txtConfig2;
        private System.Windows.Forms.TextBox txtConfigEpoches;
        private System.Windows.Forms.TextBox txtConfig1;
        private System.Windows.Forms.Label labelConfigResidual;
        private System.Windows.Forms.Label labelConfigEpoches;
        private System.Windows.Forms.Label labelConfig2;
        private System.Windows.Forms.Label labelConfig1;
        private System.Windows.Forms.Button btnTrainNeural;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartForecast;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxNHead;
        private System.Windows.Forms.Button btnForecast;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioRPROP;
        private System.Windows.Forms.RadioButton radioBackPropagation;
        private System.Windows.Forms.Button btnForecastARIMA;
        private System.Windows.Forms.TextBox txtSeasonDifferencing;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtARSeason;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMASeason;
        private System.Windows.Forms.Button btnRemoveSeason;
        private System.Windows.Forms.TextBox txtSeasonPartern;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTestArima;
        private System.Windows.Forms.Button btnPlotNeural;
        private System.Windows.Forms.Button btnTestNeural;
        private System.Windows.Forms.Button btnForecastNeural;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnPlotErrorARIMA;
        private System.Windows.Forms.RichTextBox richTextForecast;
        private System.Windows.Forms.Button button1;
    }
}