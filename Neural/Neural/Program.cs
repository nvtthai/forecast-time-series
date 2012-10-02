using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Neural neural = new Neural(1, 2, 1);
            double learningRate = 0.7;
            double moment = 0.4;
            double maxEpouch = 10000;
            double expectError = 0.00001;

            string fileName = @"E:\PROJECT\FINAL PROJECT\Other\Test\fuel.txt";
            List<double> sample = new List<double>();
            System.IO.StreamReader file = null;
            string line = null;
            int counter = 0;
            bool isFormatFileRight = true;
            int beginRow = 1;
            int endRow = 71;
            int columnSelected = 1;
            int idxRow = 0;
            try
            {
                file = new System.IO.StreamReader(fileName);
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
            }
            catch (System.OutOfMemoryException outOfMemory)
            {
                sample = null;
            }


            double max = sample.Max();
            double min = sample.Min();
            int count = sample.Count;
            double[] series = new double[count];
            List<double> sample2 = new List<double>();
            for (int i = 0; i < count; i++)
            {
                double a = sample.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                series[i] = b;
                sample2.Add(b);
            }

            NeuralTraining training = new NeuralTraining();
            training.s_Network = neural;
            //training.Rprop_Run(sample2, null);
            training.Bp_Run(sample2, null, 0.7, 0.4);
            int x = 0;
        }
    }
}
