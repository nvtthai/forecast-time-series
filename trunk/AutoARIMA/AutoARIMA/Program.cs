using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoARIMA
{

    public enum DecayPartern
    {
        SLOW_DECAY,
        EXPONENTIAL_DECAY,
        ABRUPT_DECAY
    }

    public static class Configuration
    {
        public static int MAX_ARMA_ORDER = 25;
    }

    class Program
    {

        static void Main(string[] args)
        {
            string fileName = @"E:\PROJECT\FINAL PROJECT\Other\Test\airpass.dat";
            List<double> originSeries = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            try
            {
                file = new System.IO.StreamReader(fileName);
                while ((line = file.ReadLine()) != null)
                {
                    originSeries.Add(Double.Parse(line));
                }
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                originSeries = null;
            }

            ARIMA arima = new ARIMA();
            arima.SetData(originSeries);
            arima.Run();
        }
    }
}
