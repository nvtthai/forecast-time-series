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

        public MainInterface()
        {
            InitializeComponent();
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
            List<double> sample = new List<double>();
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
                        sample.Add(Double.Parse(words[columnSelected - 1]));
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
                    sample = null;
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                sample = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.IO.IOException io)
            {
                sample = null;
                MessageBox.Show("File does not found", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception excp)
            {
                sample = null;
                MessageBox.Show("Input is wrong format", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (file != null)
                    file.Close();
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
    
    }
}
