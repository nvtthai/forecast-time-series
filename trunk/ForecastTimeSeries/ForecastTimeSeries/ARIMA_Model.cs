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
    public partial class ARIMA_Model : Form
    {
        public ARIMA_Model()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public void SetResult(string result)
        {
            richTextBoxResult.Text = result;
        }
    }
}
