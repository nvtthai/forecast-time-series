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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnManualARIMA = new System.Windows.Forms.RadioButton();
            this.btnSaveARIMA = new System.Windows.Forms.Button();
            this.btnLoadARIMA = new System.Windows.Forms.Button();
            this.labelTrainDataNumRows = new System.Windows.Forms.Label();
            this.labelTrainDataNumColumns = new System.Windows.Forms.Label();
            this.btnTrainARIMA = new System.Windows.Forms.Button();
            this.groupBoxARIMAParameter = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAutomaticARIMA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnPartialCorrelation = new System.Windows.Forms.Button();
            this.btnCorrelogram = new System.Windows.Forms.Button();
            this.btnPlotData = new System.Windows.Forms.Button();
            this.richTextProcessData = new System.Windows.Forms.RichTextBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioRPROP = new System.Windows.Forms.RadioButton();
            this.radioBackPropagation = new System.Windows.Forms.RadioButton();
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
            this.buttonForecast = new System.Windows.Forms.Button();
            this.textBoxNHead = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chartForecast = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxARIMAParameter.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxAlgorithmConfig.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 482);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage1.Controls.Add(this.btnManualARIMA);
            this.tabPage1.Controls.Add(this.btnSaveARIMA);
            this.tabPage1.Controls.Add(this.btnLoadARIMA);
            this.tabPage1.Controls.Add(this.labelTrainDataNumRows);
            this.tabPage1.Controls.Add(this.labelTrainDataNumColumns);
            this.tabPage1.Controls.Add(this.btnTrainARIMA);
            this.tabPage1.Controls.Add(this.groupBoxARIMAParameter);
            this.tabPage1.Controls.Add(this.btnAutomaticARIMA);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.btnPartialCorrelation);
            this.tabPage1.Controls.Add(this.btnCorrelogram);
            this.tabPage1.Controls.Add(this.btnPlotData);
            this.tabPage1.Controls.Add(this.richTextProcessData);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(909, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ARIMA";
            // 
            // btnManualARIMA
            // 
            this.btnManualARIMA.AutoSize = true;
            this.btnManualARIMA.ForeColor = System.Drawing.Color.Red;
            this.btnManualARIMA.Location = new System.Drawing.Point(499, 9);
            this.btnManualARIMA.Name = "btnManualARIMA";
            this.btnManualARIMA.Size = new System.Drawing.Size(89, 17);
            this.btnManualARIMA.TabIndex = 48;
            this.btnManualARIMA.TabStop = true;
            this.btnManualARIMA.Text = "Manual mode";
            this.btnManualARIMA.UseVisualStyleBackColor = true;
            this.btnManualARIMA.CheckedChanged += new System.EventHandler(this.btnManualARIMA_CheckedChanged);
            // 
            // btnSaveARIMA
            // 
            this.btnSaveARIMA.ForeColor = System.Drawing.Color.Red;
            this.btnSaveARIMA.Location = new System.Drawing.Point(748, 241);
            this.btnSaveARIMA.Name = "btnSaveARIMA";
            this.btnSaveARIMA.Size = new System.Drawing.Size(112, 23);
            this.btnSaveARIMA.TabIndex = 47;
            this.btnSaveARIMA.Text = "Save ARIMA";
            this.btnSaveARIMA.UseVisualStyleBackColor = true;
            // 
            // btnLoadARIMA
            // 
            this.btnLoadARIMA.ForeColor = System.Drawing.Color.Red;
            this.btnLoadARIMA.Location = new System.Drawing.Point(562, 241);
            this.btnLoadARIMA.Name = "btnLoadARIMA";
            this.btnLoadARIMA.Size = new System.Drawing.Size(113, 23);
            this.btnLoadARIMA.TabIndex = 46;
            this.btnLoadARIMA.Text = "Load ARIMA";
            this.btnLoadARIMA.UseVisualStyleBackColor = true;
            // 
            // labelTrainDataNumRows
            // 
            this.labelTrainDataNumRows.AutoSize = true;
            this.labelTrainDataNumRows.Location = new System.Drawing.Point(141, 52);
            this.labelTrainDataNumRows.Name = "labelTrainDataNumRows";
            this.labelTrainDataNumRows.Size = new System.Drawing.Size(0, 13);
            this.labelTrainDataNumRows.TabIndex = 45;
            // 
            // labelTrainDataNumColumns
            // 
            this.labelTrainDataNumColumns.AutoSize = true;
            this.labelTrainDataNumColumns.Location = new System.Drawing.Point(141, 30);
            this.labelTrainDataNumColumns.Name = "labelTrainDataNumColumns";
            this.labelTrainDataNumColumns.Size = new System.Drawing.Size(0, 13);
            this.labelTrainDataNumColumns.TabIndex = 44;
            // 
            // btnTrainARIMA
            // 
            this.btnTrainARIMA.Location = new System.Drawing.Point(391, 241);
            this.btnTrainARIMA.Name = "btnTrainARIMA";
            this.btnTrainARIMA.Size = new System.Drawing.Size(123, 23);
            this.btnTrainARIMA.TabIndex = 43;
            this.btnTrainARIMA.Text = "Training ARIMA";
            this.btnTrainARIMA.UseVisualStyleBackColor = true;
            this.btnTrainARIMA.Click += new System.EventHandler(this.btnTrainARIMA_Click);
            // 
            // groupBoxARIMAParameter
            // 
            this.groupBoxARIMAParameter.Controls.Add(this.button4);
            this.groupBoxARIMAParameter.Controls.Add(this.button3);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox7);
            this.groupBoxARIMAParameter.Controls.Add(this.label9);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox6);
            this.groupBoxARIMAParameter.Controls.Add(this.label8);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox5);
            this.groupBoxARIMAParameter.Controls.Add(this.label7);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox4);
            this.groupBoxARIMAParameter.Controls.Add(this.label6);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox3);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox2);
            this.groupBoxARIMAParameter.Controls.Add(this.textBox1);
            this.groupBoxARIMAParameter.Controls.Add(this.label5);
            this.groupBoxARIMAParameter.Controls.Add(this.label4);
            this.groupBoxARIMAParameter.Controls.Add(this.label3);
            this.groupBoxARIMAParameter.Enabled = false;
            this.groupBoxARIMAParameter.Location = new System.Drawing.Point(370, 32);
            this.groupBoxARIMAParameter.Name = "groupBoxARIMAParameter";
            this.groupBoxARIMAParameter.Size = new System.Drawing.Size(533, 187);
            this.groupBoxARIMAParameter.TabIndex = 42;
            this.groupBoxARIMAParameter.TabStop = false;
            this.groupBoxARIMAParameter.Text = "ARIMA Coef";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(386, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Restore";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(386, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Process";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(390, 159);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(283, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "MA season order";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(129, 159);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "AR season order";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(390, 117);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(283, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "MA order";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(129, 117);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "AR  order";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(205, 79);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(205, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(205, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Season Partern";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Season differencing";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Regular differencing";
            // 
            // btnAutomaticARIMA
            // 
            this.btnAutomaticARIMA.AutoSize = true;
            this.btnAutomaticARIMA.Checked = true;
            this.btnAutomaticARIMA.Location = new System.Drawing.Point(367, 9);
            this.btnAutomaticARIMA.Name = "btnAutomaticARIMA";
            this.btnAutomaticARIMA.Size = new System.Drawing.Size(101, 17);
            this.btnAutomaticARIMA.TabIndex = 41;
            this.btnAutomaticARIMA.TabStop = true;
            this.btnAutomaticARIMA.Text = "Automatic mode";
            this.btnAutomaticARIMA.UseVisualStyleBackColor = true;
            this.btnAutomaticARIMA.CheckedChanged += new System.EventHandler(this.btnAutomaticARIMA_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "ARIMA model";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(367, 293);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(533, 96);
            this.richTextBox1.TabIndex = 39;
            this.richTextBox1.Text = "";
            // 
            // btnPartialCorrelation
            // 
            this.btnPartialCorrelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPartialCorrelation.Location = new System.Drawing.Point(691, 398);
            this.btnPartialCorrelation.Name = "btnPartialCorrelation";
            this.btnPartialCorrelation.Size = new System.Drawing.Size(204, 51);
            this.btnPartialCorrelation.TabIndex = 38;
            this.btnPartialCorrelation.Text = "Partial Correlogram";
            this.btnPartialCorrelation.UseVisualStyleBackColor = true;
            this.btnPartialCorrelation.Click += new System.EventHandler(this.btnPartialCorrelation_Click);
            // 
            // btnCorrelogram
            // 
            this.btnCorrelogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorrelogram.Location = new System.Drawing.Point(542, 398);
            this.btnCorrelogram.Name = "btnCorrelogram";
            this.btnCorrelogram.Size = new System.Drawing.Size(113, 51);
            this.btnCorrelogram.TabIndex = 35;
            this.btnCorrelogram.Text = "Correlogram";
            this.btnCorrelogram.UseVisualStyleBackColor = true;
            this.btnCorrelogram.Click += new System.EventHandler(this.btnCorrelogram_Click);
            // 
            // btnPlotData
            // 
            this.btnPlotData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlotData.Location = new System.Drawing.Point(367, 398);
            this.btnPlotData.Name = "btnPlotData";
            this.btnPlotData.Size = new System.Drawing.Size(138, 51);
            this.btnPlotData.TabIndex = 34;
            this.btnPlotData.Text = "Plot";
            this.btnPlotData.UseVisualStyleBackColor = true;
            this.btnPlotData.Click += new System.EventHandler(this.btnPlotData_Click);
            // 
            // richTextProcessData
            // 
            this.richTextProcessData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextProcessData.Location = new System.Drawing.Point(6, 166);
            this.richTextProcessData.Name = "richTextProcessData";
            this.richTextProcessData.ReadOnly = true;
            this.richTextProcessData.Size = new System.Drawing.Size(347, 284);
            this.richTextProcessData.TabIndex = 33;
            this.richTextProcessData.Text = "";
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(117, 124);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(95, 27);
            this.btnGetData.TabIndex = 32;
            this.btnGetData.Text = "Get";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // txtTrainDataColumn
            // 
            this.txtTrainDataColumn.Location = new System.Drawing.Point(77, 98);
            this.txtTrainDataColumn.Name = "txtTrainDataColumn";
            this.txtTrainDataColumn.Size = new System.Drawing.Size(80, 20);
            this.txtTrainDataColumn.TabIndex = 31;
            // 
            // txtTrainDataToRow
            // 
            this.txtTrainDataToRow.Location = new System.Drawing.Point(205, 75);
            this.txtTrainDataToRow.Name = "txtTrainDataToRow";
            this.txtTrainDataToRow.Size = new System.Drawing.Size(100, 20);
            this.txtTrainDataToRow.TabIndex = 30;
            // 
            // txtTrainDataFromRow
            // 
            this.txtTrainDataFromRow.Location = new System.Drawing.Point(77, 74);
            this.txtTrainDataFromRow.Name = "txtTrainDataFromRow";
            this.txtTrainDataFromRow.Size = new System.Drawing.Size(80, 20);
            this.txtTrainDataFromRow.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 101);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Column";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Row";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Num Columns:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Num Rows:";
            // 
            // btnChooseData
            // 
            this.btnChooseData.BackColor = System.Drawing.Color.Gainsboro;
            this.btnChooseData.Location = new System.Drawing.Point(208, 8);
            this.btnChooseData.Name = "btnChooseData";
            this.btnChooseData.Size = new System.Drawing.Size(75, 23);
            this.btnChooseData.TabIndex = 23;
            this.btnChooseData.Text = "Browse";
            this.btnChooseData.UseVisualStyleBackColor = false;
            this.btnChooseData.Click += new System.EventHandler(this.btnChooseData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "Time Series Data";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage2.Controls.Add(this.btnTrainNeural);
            this.tabPage2.Controls.Add(this.groupBoxAlgorithmConfig);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBoxNetworkConfig);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(909, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Neural Network";
            // 
            // btnTrainNeural
            // 
            this.btnTrainNeural.Location = new System.Drawing.Point(419, 276);
            this.btnTrainNeural.Name = "btnTrainNeural";
            this.btnTrainNeural.Size = new System.Drawing.Size(75, 23);
            this.btnTrainNeural.TabIndex = 5;
            this.btnTrainNeural.Text = "Train Neural";
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
            this.groupBoxAlgorithmConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAlgorithmConfig.Location = new System.Drawing.Point(504, 186);
            this.groupBoxAlgorithmConfig.Name = "groupBoxAlgorithmConfig";
            this.groupBoxAlgorithmConfig.Size = new System.Drawing.Size(390, 226);
            this.groupBoxAlgorithmConfig.TabIndex = 4;
            this.groupBoxAlgorithmConfig.TabStop = false;
            this.groupBoxAlgorithmConfig.Text = "RPROP Config";
            // 
            // txtConfigErrors
            // 
            this.txtConfigErrors.Location = new System.Drawing.Point(176, 155);
            this.txtConfigErrors.Name = "txtConfigErrors";
            this.txtConfigErrors.Size = new System.Drawing.Size(100, 23);
            this.txtConfigErrors.TabIndex = 7;
            // 
            // txtConfig2
            // 
            this.txtConfig2.Location = new System.Drawing.Point(174, 114);
            this.txtConfig2.Name = "txtConfig2";
            this.txtConfig2.Size = new System.Drawing.Size(100, 23);
            this.txtConfig2.TabIndex = 6;
            // 
            // txtConfigEpoches
            // 
            this.txtConfigEpoches.Location = new System.Drawing.Point(174, 71);
            this.txtConfigEpoches.Name = "txtConfigEpoches";
            this.txtConfigEpoches.Size = new System.Drawing.Size(100, 23);
            this.txtConfigEpoches.TabIndex = 5;
            // 
            // txtConfig1
            // 
            this.txtConfig1.Location = new System.Drawing.Point(174, 33);
            this.txtConfig1.Name = "txtConfig1";
            this.txtConfig1.Size = new System.Drawing.Size(100, 23);
            this.txtConfig1.TabIndex = 4;
            // 
            // labelConfigResidual
            // 
            this.labelConfigResidual.AutoSize = true;
            this.labelConfigResidual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfigResidual.Location = new System.Drawing.Point(9, 155);
            this.labelConfigResidual.Name = "labelConfigResidual";
            this.labelConfigResidual.Size = new System.Drawing.Size(122, 17);
            this.labelConfigResidual.TabIndex = 3;
            this.labelConfigResidual.Text = "Residual of Errors";
            // 
            // labelConfigEpoches
            // 
            this.labelConfigEpoches.AutoSize = true;
            this.labelConfigEpoches.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfigEpoches.Location = new System.Drawing.Point(9, 74);
            this.labelConfigEpoches.Name = "labelConfigEpoches";
            this.labelConfigEpoches.Size = new System.Drawing.Size(162, 17);
            this.labelConfigEpoches.TabIndex = 2;
            this.labelConfigEpoches.Text = "Max Number of Epoches";
            // 
            // labelConfig2
            // 
            this.labelConfig2.AutoSize = true;
            this.labelConfig2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfig2.Location = new System.Drawing.Point(9, 119);
            this.labelConfig2.Name = "labelConfig2";
            this.labelConfig2.Size = new System.Drawing.Size(123, 17);
            this.labelConfig2.TabIndex = 1;
            this.labelConfig2.Text = "Max Update Value";
            // 
            // labelConfig1
            // 
            this.labelConfig1.AutoSize = true;
            this.labelConfig1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfig1.Location = new System.Drawing.Point(9, 36);
            this.labelConfig1.Name = "labelConfig1";
            this.labelConfig1.Size = new System.Drawing.Size(143, 17);
            this.labelConfig1.TabIndex = 0;
            this.labelConfig1.Text = "Default Update Value";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(28, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 226);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training Config";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(9, 100);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(348, 109);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Validate";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioRPROP);
            this.groupBox3.Controls.Add(this.radioBackPropagation);
            this.groupBox3.Location = new System.Drawing.Point(9, 19);
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
            this.radioRPROP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioRPROP.Location = new System.Drawing.Point(62, 39);
            this.radioRPROP.Name = "radioRPROP";
            this.radioRPROP.Size = new System.Drawing.Size(75, 21);
            this.radioRPROP.TabIndex = 16;
            this.radioRPROP.TabStop = true;
            this.radioRPROP.Text = "RPROP";
            this.radioRPROP.UseVisualStyleBackColor = true;
            this.radioRPROP.CheckedChanged += new System.EventHandler(this.radioRPROP_CheckedChanged);
            // 
            // radioBackPropagation
            // 
            this.radioBackPropagation.AutoSize = true;
            this.radioBackPropagation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBackPropagation.Location = new System.Drawing.Point(62, 15);
            this.radioBackPropagation.Name = "radioBackPropagation";
            this.radioBackPropagation.Size = new System.Drawing.Size(138, 21);
            this.radioBackPropagation.TabIndex = 15;
            this.radioBackPropagation.Text = "Back Propagation";
            this.radioBackPropagation.UseVisualStyleBackColor = true;
            this.radioBackPropagation.CheckedChanged += new System.EventHandler(this.radioBackPropagation_CheckedChanged);
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
            this.groupBoxNetworkConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnNetworkClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkClear.ForeColor = System.Drawing.Color.Red;
            this.btnNetworkClear.Location = new System.Drawing.Point(755, 25);
            this.btnNetworkClear.Name = "btnNetworkClear";
            this.btnNetworkClear.Size = new System.Drawing.Size(71, 38);
            this.btnNetworkClear.TabIndex = 10;
            this.btnNetworkClear.Text = "Clear";
            this.btnNetworkClear.UseVisualStyleBackColor = true;
            // 
            // btnNetworkSave
            // 
            this.btnNetworkSave.Enabled = false;
            this.btnNetworkSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkSave.ForeColor = System.Drawing.Color.Red;
            this.btnNetworkSave.Location = new System.Drawing.Point(628, 25);
            this.btnNetworkSave.Name = "btnNetworkSave";
            this.btnNetworkSave.Size = new System.Drawing.Size(85, 38);
            this.btnNetworkSave.TabIndex = 9;
            this.btnNetworkSave.Text = "Save";
            this.btnNetworkSave.UseVisualStyleBackColor = true;
            // 
            // btnNetworkLoad
            // 
            this.btnNetworkLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkLoad.ForeColor = System.Drawing.Color.Red;
            this.btnNetworkLoad.Location = new System.Drawing.Point(476, 25);
            this.btnNetworkLoad.Name = "btnNetworkLoad";
            this.btnNetworkLoad.Size = new System.Drawing.Size(81, 38);
            this.btnNetworkLoad.TabIndex = 8;
            this.btnNetworkLoad.Text = "Load";
            this.btnNetworkLoad.UseVisualStyleBackColor = true;
            // 
            // btnNetworkNew
            // 
            this.btnNetworkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkNew.Location = new System.Drawing.Point(339, 25);
            this.btnNetworkNew.Name = "btnNetworkNew";
            this.btnNetworkNew.Size = new System.Drawing.Size(81, 38);
            this.btnNetworkNew.TabIndex = 7;
            this.btnNetworkNew.Text = "New";
            this.btnNetworkNew.UseVisualStyleBackColor = true;
            this.btnNetworkNew.Click += new System.EventHandler(this.btnNetworkNew_Click);
            // 
            // txtNumOutput
            // 
            this.txtNumOutput.Enabled = false;
            this.txtNumOutput.Location = new System.Drawing.Point(262, 19);
            this.txtNumOutput.Name = "txtNumOutput";
            this.txtNumOutput.Size = new System.Drawing.Size(43, 23);
            this.txtNumOutput.TabIndex = 4;
            this.txtNumOutput.Text = "1";
            // 
            // txtNumHidden
            // 
            this.txtNumHidden.Location = new System.Drawing.Point(209, 19);
            this.txtNumHidden.Name = "txtNumHidden";
            this.txtNumHidden.Size = new System.Drawing.Size(43, 23);
            this.txtNumHidden.TabIndex = 3;
            // 
            // txtNumInput
            // 
            this.txtNumInput.Location = new System.Drawing.Point(156, 19);
            this.txtNumInput.Name = "txtNumInput";
            this.txtNumInput.Size = new System.Drawing.Size(43, 23);
            this.txtNumInput.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "Input-Hidden-Output";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.SeaGreen;
            this.tabPage3.Controls.Add(this.buttonForecast);
            this.tabPage3.Controls.Add(this.textBoxNHead);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.chartForecast);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(909, 456);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Forecast";
            // 
            // buttonForecast
            // 
            this.buttonForecast.Location = new System.Drawing.Point(287, 23);
            this.buttonForecast.Name = "buttonForecast";
            this.buttonForecast.Size = new System.Drawing.Size(75, 23);
            this.buttonForecast.TabIndex = 3;
            this.buttonForecast.Text = "Forecast";
            this.buttonForecast.UseVisualStyleBackColor = true;
            this.buttonForecast.Click += new System.EventHandler(this.buttonForecast_Click);
            // 
            // textBoxNHead
            // 
            this.textBoxNHead.Location = new System.Drawing.Point(135, 26);
            this.textBoxNHead.Name = "textBoxNHead";
            this.textBoxNHead.Size = new System.Drawing.Size(100, 20);
            this.textBoxNHead.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "N ahead";
            // 
            // chartForecast
            // 
            chartArea3.Name = "ChartArea1";
            this.chartForecast.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartForecast.Legends.Add(legend3);
            this.chartForecast.Location = new System.Drawing.Point(6, 82);
            this.chartForecast.Name = "chartForecast";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series";
            this.chartForecast.Series.Add(series3);
            this.chartForecast.Size = new System.Drawing.Size(894, 368);
            this.chartForecast.TabIndex = 0;
            this.chartForecast.Text = "chart1";
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 487);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainInterface";
            this.Text = "Forecast Time Series Model";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxARIMAParameter.ResumeLayout(false);
            this.groupBoxARIMAParameter.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBoxAlgorithmConfig.ResumeLayout(false);
            this.groupBoxAlgorithmConfig.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.RichTextBox richTextProcessData;
        private System.Windows.Forms.Button btnPartialCorrelation;
        private System.Windows.Forms.Button btnCorrelogram;
        private System.Windows.Forms.Button btnPlotData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RadioButton btnAutomaticARIMA;
        private System.Windows.Forms.GroupBox groupBoxARIMAParameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSaveARIMA;
        private System.Windows.Forms.Button btnLoadARIMA;
        private System.Windows.Forms.RadioButton btnManualARIMA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxAlgorithmConfig;
        private System.Windows.Forms.TextBox txtConfigErrors;
        private System.Windows.Forms.TextBox txtConfig2;
        private System.Windows.Forms.TextBox txtConfigEpoches;
        private System.Windows.Forms.TextBox txtConfig1;
        private System.Windows.Forms.Label labelConfigResidual;
        private System.Windows.Forms.Label labelConfigEpoches;
        private System.Windows.Forms.Label labelConfig2;
        private System.Windows.Forms.Label labelConfig1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioRPROP;
        private System.Windows.Forms.RadioButton radioBackPropagation;
        private System.Windows.Forms.Button btnTrainNeural;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartForecast;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxNHead;
        private System.Windows.Forms.Button buttonForecast;
    }
}