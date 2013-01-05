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
    public partial class AHead_Form : Form
    {
        public AHead_Form()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public int GetAHead()
        {
            int aHead = -1;
            try
            {
                aHead = Int16.Parse(this.textBox1.Text);
            }
            catch
            {
            }
            return aHead;
        }

    }
}
