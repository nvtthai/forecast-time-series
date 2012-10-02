using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class TimeSeries
    {
        public string fileName { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }

        public List<double> originalSeries;
        public List<double> arimaProcessedSeries;
        public List<double> arimaErrorSeries;
        public List<double> neuralTrainSeries;
        public List<double> neuralValidateSeries;
        public List<double> neuralTestSeries;

    }
}
