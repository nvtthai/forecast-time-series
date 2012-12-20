using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ForecastTimeSeries
{
    public enum DecayPartern
    {
        SLOW_DECAY,
        EXPONENTIAL_DECAY,
        ABRUPT_DECAY
    }

    public static class Configuration
    {
        public static int MAX_REGULAR_ARMA_ORDER = 5;
        public static int MAX_SEASON_ARMA_ORDER = 2;

        public static double SIGN_CORRECLATION = 1.0;
        public static double ABRUPT_DECAY_CHANGE = 0.65;
        public static double EXPONENTIAL_DECAY_CHANGE = 0.1;
    }

    public class ARIMA
    {
        public List<double> _originSeries;
        public List<double> _processSeries;
        public List<double> _errorSeries;

        public int _startIndex; 

        public int _pRegular;
        public int _regularDifferencingLevel;
        public int _qRegular;

        public int _pSeason;
        public int _seasonDifferencingLevel;
        public int _qSeason;
        public int _seasonPartern;

        List<double> _listArimaCoef;

        #region ARIMA algorithm

        private DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, double confidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listConfidenceLimit = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                listConfidenceLimit.Add(confidenceLimit);
            }
            decayPartern = ComputeDecayPartern(listAutocorrelation, listConfidenceLimit);
            return decayPartern;
        }

        private DecayPartern ComputeDecayPartern(List<double> listAutocorrelation, List<double> listConfidenceLimit)
        {
            DecayPartern decayPartern;
            List<double> listHighAutocorrelation = new List<double>();
            for (int i = 0; i < listAutocorrelation.Count; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i]) * Configuration.SIGN_CORRECLATION)
                {
                    listHighAutocorrelation.Add(listAutocorrelation[i]);
                }
            }
            if (listHighAutocorrelation.Count <= 1)
            {
                decayPartern = DecayPartern.ABRUPT_DECAY;
                return decayPartern;
            }

            double averageRateExchange = 0;
            for (int i = 0; i < listHighAutocorrelation.Count - 1; i++)
            {
                averageRateExchange += Math.Abs(Math.Abs(listHighAutocorrelation[i]) - Math.Abs(listHighAutocorrelation[i + 1])) / Math.Abs(listHighAutocorrelation[i]);
            }
            averageRateExchange /= (listHighAutocorrelation.Count - 1);

            if (averageRateExchange > Configuration.ABRUPT_DECAY_CHANGE)
            {
                decayPartern = DecayPartern.ABRUPT_DECAY;
            }
            else if (averageRateExchange < Configuration.EXPONENTIAL_DECAY_CHANGE)
            {
                decayPartern = DecayPartern.SLOW_DECAY;
            }
            else
            {
                decayPartern = DecayPartern.EXPONENTIAL_DECAY;
            }
            return decayPartern;
        }

        private void GetHighCorrelationLocation(List<double> listAutocorrelation, int begin, ref List<int> listHighestCorrelationIndex)
        {
            if (begin >= listAutocorrelation.Count)
            {
                return;
            }

            while (begin < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[begin] <= 0)
                {
                    begin++;
                }
                else if (listAutocorrelation[begin + 1] >= listAutocorrelation[begin])
                {
                    begin++;
                }
                else
                {
                    if ((begin + 2) == listAutocorrelation.Count || (listAutocorrelation[begin + 2] < listAutocorrelation[begin]))
                    {
                        listHighestCorrelationIndex.Add(begin);
                        break;
                    }
                    else
                    {
                        begin = begin + 2;
                    }
                }
            }

            while (begin < listAutocorrelation.Count - 1)
            {
                if (listAutocorrelation[begin] <= 0)
                {
                    begin++;
                }
                else if (listAutocorrelation[begin + 1] < listAutocorrelation[begin])
                {
                    begin++;
                }
                else
                {
                    if ((begin + 2) == listAutocorrelation.Count || (listAutocorrelation[begin + 2] > listAutocorrelation[begin]))
                    {
                        break;
                    }
                    else
                    {
                        begin = begin + 2;
                    }
                }
            }

            GetHighCorrelationLocation(listAutocorrelation, begin + 1, ref listHighestCorrelationIndex);

        }

        private void GetLastSignificant(List<double> listAutocorrelation, List<double> listConfidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 0; i < listAutocorrelation.Count && i < Configuration.MAX_REGULAR_ARMA_ORDER; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(listConfidenceLimit[i]) * Configuration.SIGN_CORRECLATION)
                {
                    lag = i;
                }
            }
        }

        private void GetLastSignificant(List<double> listAutocorrelation, double confidenceLimit, out int lag)
        {
            lag = 0;
            for (int i = 0; i < listAutocorrelation.Count && i < Configuration.MAX_REGULAR_ARMA_ORDER; i++)
            {
                if (Math.Abs(listAutocorrelation[i]) > Math.Abs(confidenceLimit)*1.3)
                {
                    lag = i + 1;
                }
            }
        }

        private void EstimateARIMAModel(List<double> series, int startIndex, int seasonPartern, out int pRegular, out int qRegular, out int pSeason, out int qSeason)
        {
            pRegular = qRegular = pSeason = qSeason = 0;

            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();
            List<double> listPartialAutocorrelation = new List<double>();

            List<double> listSeasonAutocorrelation = new List<double>();
            List<double> listSeasonConfidenceLimit = new List<double>();
            List<double> listSeasonPartialCorrelation = new List<double>();

            List<double> listRegularAutocorrelation = new List<double>();
            List<double> listRegularConfidenceLimit = new List<double>();
            List<double> listRegularPartialCorrelation = new List<double>();

            Statistic.ComputeAutocorrelation(series, startIndex, out listAutocorrelation);
            Statistic.ComputePartialAutocorrelation(listAutocorrelation, out listPartialAutocorrelation);
            Statistic.ComputeConfidenceLimit(listAutocorrelation, series.Count - startIndex, out listConfidenceLimit);
            double confidenceLimit = 1.96 / Math.Sqrt(series.Count - startIndex);

            int regularAutocorrelationLengh = seasonPartern;
            if (seasonPartern == 0)
            {
                regularAutocorrelationLengh = listAutocorrelation.Count;
            }
            for (int i = 0; i < regularAutocorrelationLengh; i++)
            {
                listRegularAutocorrelation.Add(listAutocorrelation[i]);
                listRegularConfidenceLimit.Add(listConfidenceLimit[i]);
                listRegularPartialCorrelation.Add(listPartialAutocorrelation[i]);
            }
            //Algorithm.DrawAutocorrelation(listRegularAutocorrelation, listRegularConfidenceLimit);
            //Algorithm.DrawPartialAutocorrelation(listRegularPartialCorrelation, confidenceLimit);

            DecayPartern decayACF = ComputeDecayPartern(listRegularAutocorrelation, listRegularConfidenceLimit);
            DecayPartern decayPACF = ComputeDecayPartern(listRegularPartialCorrelation, confidenceLimit);

            if (decayACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularAutocorrelation, listRegularConfidenceLimit, out qRegular);
            }
            if (decayPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listRegularPartialCorrelation, confidenceLimit, out pRegular);
            }
            if (decayACF != DecayPartern.ABRUPT_DECAY && decayPACF != DecayPartern.ABRUPT_DECAY)
            {
                pRegular = qRegular = 2;
            }

            pRegular = Math.Min(pRegular, Configuration.MAX_REGULAR_ARMA_ORDER);
            qRegular = Math.Min(qRegular, Configuration.MAX_REGULAR_ARMA_ORDER);

            if (seasonPartern == 0)
            {
                return;
            }
            for (int i = 0; i < Math.Floor(1.0 * listAutocorrelation.Count / seasonPartern); i++)
            {
                listSeasonAutocorrelation.Add(listAutocorrelation[i * seasonPartern]);
                listSeasonConfidenceLimit.Add(listConfidenceLimit[i * seasonPartern]);
                listSeasonPartialCorrelation.Add(listPartialAutocorrelation[i * seasonPartern]);
            }

            //Algorithm.DrawAutocorrelation(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            //Algorithm.DrawPartialAutocorrelation(listSeasonPartialCorrelation, confidenceLimit);

            DecayPartern decaySeasonACF = ComputeDecayPartern(listSeasonAutocorrelation, listSeasonConfidenceLimit);
            DecayPartern decaySeasonPACF = ComputeDecayPartern(listSeasonPartialCorrelation, confidenceLimit);

            if (decaySeasonACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonAutocorrelation, listSeasonConfidenceLimit, out qSeason);
            }
            if (decaySeasonPACF == DecayPartern.ABRUPT_DECAY)
            {
                GetLastSignificant(listSeasonPartialCorrelation, confidenceLimit, out pSeason);
            }
            if (decaySeasonACF != DecayPartern.ABRUPT_DECAY && decaySeasonPACF != DecayPartern.ABRUPT_DECAY)
            {
                pSeason = qSeason = 1;
            }

            pSeason = Math.Min(pSeason, Configuration.MAX_SEASON_ARMA_ORDER);
            qSeason = Math.Min(qSeason, Configuration.MAX_SEASON_ARMA_ORDER);
        }

        private void EstimateARIMACoef(List<double> series, int startIndex, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, out List<double> listArimaCoeff)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < series.Count; i++)
            {
                errors.Add(0);
            }

            Matrix observationVector = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix parameterVector = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix gainFactor = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1);
            Matrix invertedCovarianceMatrix = new Matrix(1 + pRegular + qRegular + pSeason + qSeason, 1 + pRegular + qRegular + pSeason + qSeason);
            double prioriPredictionError;
            double posterioriPredictionError;

            //Phase 1 - Set Initial Conditions
            //the observation vector
            observationVector[0, 0] = 1;
            for (int i = 1; i < pRegular + 1; i++)
            {
                observationVector[i, 0] = series[(i - 1)];
            }
            for (int i = 1; i < qRegular + 1; i++)
            {
                observationVector[pRegular + i, 0] = 0;
            }
            for (int i = 1; i < pSeason + 1; i++)
            {
                observationVector[pRegular + qRegular + i, 0] = series[(i - 1) * seasonPartern];
            }
            for (int i = 1; i < qSeason + 1; i++)
            {
                observationVector[pRegular + qRegular + pSeason + i, 0] = 0;
            }

            for (int i = 0; i < 1 + pRegular + qRegular + pSeason + qSeason; i++)
            {
                invertedCovarianceMatrix[i, i] = Math.Pow(10, 6);
            }

            int begin = ComputeMax(pRegular, qRegular, pSeason * seasonPartern, qSeason * seasonPartern, startIndex);
            for (int k = 0; k < 10; k++)
            {
                for (int i = begin; i < series.Count; i++)
                {
                    //Phase 1
                    observationVector[0, 0] = 1;
                    for (int j = 1; j < pRegular + 1; j++)
                    {
                        observationVector[j, 0] = series[i - j];
                    }
                    for (int j = 1; j < qRegular + 1; j++)
                    {
                        observationVector[pRegular + j, 0] = errors[i - j];
                    }
                    for (int j = 1; j < pSeason + 1; j++)
                    {
                        observationVector[j, 0] = series[i - j * seasonPartern];
                    }
                    for (int j = 1; j < qSeason + 1; j++)
                    {
                        observationVector[pRegular + j, 0] = errors[i - j * seasonPartern];
                    }

                    //Phase 2 - Estimate Parameters
                    prioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
                    double temp = 1 + (Matrix.Transpose(observationVector) * invertedCovarianceMatrix * observationVector)[0, 0];
                    gainFactor = (invertedCovarianceMatrix * observationVector) / temp;
                    parameterVector += gainFactor * prioriPredictionError;

                    //Phase 3 - Prepare for Next Estimation 
                    posterioriPredictionError = series[i] - (Matrix.Transpose(observationVector) * parameterVector)[0, 0];
                    invertedCovarianceMatrix = invertedCovarianceMatrix - gainFactor * Matrix.Transpose(observationVector) * invertedCovarianceMatrix;
                    errors[i] = posterioriPredictionError;
                }
            }

            listArimaCoeff = new List<double>();
            for (int i = 0; i < 1 + pRegular + qRegular + pSeason + qSeason; i++)
            {
                listArimaCoeff.Add(parameterVector[i, 0]);
            }
        }

        private void RemoveNonstationarity(ref List<double> series, ref int startIndex, out int regularDifferencingLevel)
        {
            int dataSize = series.Count - startIndex;
            List<double> listAutocorrelation = new List<double>();
            List<double> listConfidenceLimit = new List<double>();

            regularDifferencingLevel = 0;
            while (true)
            {
                listAutocorrelation.Clear();
                listConfidenceLimit.Clear();
                dataSize = series.Count - startIndex;
                Statistic.ComputeAutocorrelation(series, startIndex, out listAutocorrelation);
                Statistic.ComputeConfidenceLimit(listAutocorrelation, dataSize, out listConfidenceLimit);
                //DrawSeriesData(series, startIndex);
                //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);
                DecayPartern decayPartern = ComputeDecayPartern(listAutocorrelation, listConfidenceLimit);
                if (decayPartern != DecayPartern.SLOW_DECAY)
                {
                    break;
                }

                startIndex += 1;
                regularDifferencingLevel++;
                for (int j = series.Count - 1; j >= startIndex; j--)
                {
                    series[j] = series[j] - series[j - 1];
                }
            }
        }

        private void EstimateSeasonPartern(List<double> listAutocorrelation, out int seasonPartern)
        {
            seasonPartern = 0;
            List<int> listHighestCorrelationIndex = new List<int>();
            GetHighCorrelationLocation(listAutocorrelation, 0, ref listHighestCorrelationIndex);
            List<int> levelOneDistance = new List<int>();
            for (int i = 0; i < listHighestCorrelationIndex.Count - 1; i++)
            {
                levelOneDistance.Add(listHighestCorrelationIndex[i + 1] - listHighestCorrelationIndex[i]);
            }

            List<int> listDistinctLocation = levelOneDistance.Distinct().ToList();

            if (listDistinctLocation.Count == 0)
            {
                return;
            }
            int hightFrequencyDistance = 0;
            int frequency = 0;
            for (int i = 0; i < listDistinctLocation.Count; i++)
            {
                int newFrequency = levelOneDistance.Count(item => item == listDistinctLocation[i]);
                if (newFrequency > frequency && listDistinctLocation[i] != 1)
                {
                    hightFrequencyDistance = listDistinctLocation[i];
                    frequency = newFrequency;
                }
            }

            if (1.0 * frequency / levelOneDistance.Count > 0.5)
            {
                seasonPartern = hightFrequencyDistance;
                return;
            }
        }

        private void RemoveSeasonality(ref List<double> processSeries, ref int startIndex, out int seasonPartern, out int seasonDifferencingLevel)
        {
            seasonPartern = 0;
            seasonDifferencingLevel = 0;
            List<double> listAutocorrelation = new List<double>();
            List<int> levelLocation = new List<int>();

            while (true)
            {
                listAutocorrelation.Clear();
                Statistic.ComputeAutocorrelation(processSeries, startIndex, out listAutocorrelation);

                //DrawSeriesData(series, startIndex);
                //DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);

                int newSeasonPartern;
                EstimateSeasonPartern(listAutocorrelation, out newSeasonPartern);
                if (newSeasonPartern == 0 || (seasonPartern != 0 && newSeasonPartern != seasonPartern))
                {
                    break;
                }
                seasonPartern = newSeasonPartern;
                seasonDifferencingLevel++;
                startIndex += seasonPartern;

                for (int j = processSeries.Count - 1; j >= startIndex; j--)
                {
                    processSeries[j] = processSeries[j] - processSeries[j - seasonPartern];
                }
            }
        }

        private int ComputeMax(params int[] data)
        {
            int max = data.Max();
            return max;
        }

        private void ComputError(List<double> processSeries, int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, List<double> listArimaCoeff, out List<double> error)
        {
            error = new List<double>();
            List<double> currentSeries = processSeries.FindAll(item => true);
            List<double> testSeries = new List<double>();

            int begin = ComputeMax(pRegular, qRegular, pSeason * seasonPartern, qSeason * seasonPartern, startIndex);

            for (int i = 0; i < begin; i++)
            {
                testSeries.Add(processSeries[i]);
            }

            for (int i = begin; i < processSeries.Count; i++)
            {
                double temp = listArimaCoeff[0];
                for (int j = 1; j <= pRegular; j++)
                {
                    temp += listArimaCoeff[j] * processSeries[i - j];
                }
                for (int j = 1; j <= pSeason; j++)
                {
                    temp += listArimaCoeff[pRegular + qRegular + j] * processSeries[i - j * seasonPartern];
                }
                testSeries.Add(temp);

            }

            for (int i = 0; i < currentSeries.Count; i++)
            {
                error.Add(testSeries[i] - currentSeries[i]);
            }
        }

        private void RevertDiffTestSeries(ref List<double> series, ref List<double> testSeries, ref int startIndex, int d, int D, int s)
        {
            for (int i = 0; i < D; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - s];
                    testSeries[j] = testSeries[j] + series[j - s];
                }
                startIndex -= s;
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = startIndex; j < series.Count; j++)
                {
                    series[j] = series[j] + series[j - 1];
                    testSeries[j] = testSeries[j] + series[j - 1];
                }
                startIndex -= 1;
            }
        }

        private void ForecastARIMA(List<double> processSeries, List<double> errors, int startIndex, int regularDifferencingLevel, int seasonDifferencingLevel, int pRegular, int qRegular, int seasonPartern, int pSeason, int qSeason, List<double> listArimaCoeff, int nHead, out List<double> forecastSeries)
        {
            List<double> currentSeries = processSeries.FindAll(item => true);
            List<double> currentErrors = errors.FindAll(item => true);
            int begin = processSeries.Count;
            for (int i = 0; i < nHead; i++)
            {
                currentSeries.Add(0);
                currentErrors.Add(0);
            }
            for (int i = begin; i < currentSeries.Count; i++)
            {
                currentSeries[i] = listArimaCoeff[0];

                for (int j = 1; j <= pRegular; j++)
                {
                    currentSeries[i] += currentSeries[i - j] * listArimaCoeff[j];
                }
                for (int j = 1; j <= pSeason; j++)
                {
                    currentSeries[i] += currentSeries[i - j * seasonPartern] * listArimaCoeff[pRegular + qRegular + j];
                }

                // add error
            }
            Statistic.RevertDifference(ref currentSeries, ref startIndex, regularDifferencingLevel, seasonDifferencingLevel, seasonPartern);
            forecastSeries = new List<double>();
            for (int i = begin; i < currentSeries.Count; i++)
            {
                forecastSeries.Add(currentSeries[i]);
            }
        }
       
        #endregion ARIMA algorithm


        public ARIMA()
        {
            _startIndex = 0;
            _seasonDifferencingLevel = _seasonPartern = _pSeason = _qSeason = 0;
            _regularDifferencingLevel = _pRegular = _qRegular = 0;

            _originSeries = new List<double>();
            _processSeries = new List<double>();
            _errorSeries = new List<double>();

            _listArimaCoef = new List<double>();
        }

        public void InitTraining()
        {
            _startIndex = 0;
            _regularDifferencingLevel = _pRegular = _qRegular = 0;
            _seasonDifferencingLevel = _seasonPartern = _pSeason = _qSeason = 0;

            _processSeries.Clear();
            _errorSeries.Clear();
            _listArimaCoef.Clear();

            _processSeries = _originSeries.FindAll(item => true);
        }

        public void SetData(List<double> series)
        {
            _originSeries.Clear();
            _processSeries.Clear();
            _errorSeries.Clear();
            for (int i = 0; i < series.Count; i++)
            {
                _originSeries.Add(series[i]);
                _processSeries.Add(series[i]);
            }
        }

        public void  GetErrorSeries(out List<double> errors)
        {
            errors = new List<double>();
            List<double> testSeries;
            GetTestSeries(out testSeries);
            for(int i= _startIndex; i<_errorSeries.Count; i++)
            {
                errors.Add(_originSeries[i]-testSeries[i]);
            }
            //errors = _errorSeries.FindAll(item => true);
        }

        public void GetTestSeries(out List<double> testSeries)
        {
            testSeries = new List<double>();

            List<double> currentSeries = _processSeries.FindAll(item => true);
            for (int i = 0; i < _processSeries.Count; i++)
            {
                testSeries.Add(_processSeries[i] + _errorSeries[i]);
            }
            //DrawTwoSeriesData(currentSeries, _startIndex, testSeries, _startIndex);

            int startIndex = _startIndex;
            RevertDiffTestSeries(ref currentSeries, ref testSeries, ref startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
            //DrawTwoSeriesData(currentSeries, startIndex, testSeries, startIndex);
        }

        public void GetModel(out string model)
        {
            model = string.Empty;
            StringBuilder result = new StringBuilder();
            result.Append(String.Format("ARIMA({0}, {1}, {2})({3}, {4}, {5}){6}\n", 
                _pRegular, _regularDifferencingLevel, _qRegular, _pSeason, _seasonDifferencingLevel, _qSeason, _seasonPartern));
            result.Append(String.Format("Perception\t|{0}\n", _listArimaCoef[0]));
            result.Append(String.Format("Order\t\t|"));
            for (int i = 0; i < ComputeMax(_pRegular, _qRegular, _pSeason, _qSeason); i++)
            {
                result.Append(String.Format("  {0}\t|", i + 1));
            }
            result.Append("\n");

            result.Append(String.Format("AR Regular coef\t|"));
            for (int i = 0; i < _pRegular; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + 1]));
            }
            result.Append("\n");

            result.Append(String.Format("MA Regular coef\t|"));
            for (int i = 0; i < _qRegular; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular + 1]));
            }
            result.Append("\n");

            result.Append(String.Format("AR Season coef\t|"));
            for (int i = 0; i < _pSeason; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular + _qRegular + 1]));
            }
            result.Append("\n");

            result.Append(String.Format("MA Season coef\t|"));
            for (int i = 0; i < _qSeason; i++)
            {
                result.Append(String.Format("  {0:0.000}\t|", _listArimaCoef[i + _pRegular + _qRegular + _pSeason + 1]));
            }

            model = result.ToString();
        }

        public void GetModel(out List<int> model)
        {
            model = new List<int>();
            model.Add(_pRegular);
            model.Add(_regularDifferencingLevel);
            model.Add(_qRegular);
            model.Add(_pSeason);
            model.Add(_seasonDifferencingLevel);
            model.Add(_qSeason);
            model.Add(_seasonPartern);
        }

        public void AutomaticTraining()
        {
            InitTraining();
            RemoveNonstationarity(ref _processSeries, ref _startIndex, out _regularDifferencingLevel);
            RemoveSeasonality(ref _processSeries, ref _startIndex, out _seasonPartern, out _seasonDifferencingLevel);

            EstimateARIMAModel(_processSeries, _startIndex, _seasonPartern, out _pRegular, out _qRegular, out _pSeason, out _qSeason);
            EstimateARIMACoef(_processSeries, _startIndex, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, out _listArimaCoef);

            ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);
        }

        public void RemoveTrendSeasonality(int regularDifferencingLevel, int seasonDifferencingLevel, int seasonPartern)
        {
            InitTraining();
            _regularDifferencingLevel = regularDifferencingLevel;
            _seasonDifferencingLevel = seasonDifferencingLevel;
            _seasonPartern = seasonPartern;
            Statistic.ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
        }

        public void ManualTraining(int pRegular, int regularDifferencing, int qRegular, int pSeason, int seassonDifferencing, int qSeason, int seasonPartern)
        {
            InitTraining();
            this._regularDifferencingLevel = regularDifferencing;
            this._pRegular = pRegular;
            this._qRegular = qRegular;
            this._pSeason = pSeason;
            this._qSeason = qSeason;
            this._seasonDifferencingLevel = seassonDifferencing;
            this._seasonPartern = seasonPartern;

            Statistic.ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
            EstimateARIMACoef(_processSeries, _startIndex, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, out _listArimaCoef);

            ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);
        }

        public void Forecast(int nHead, out List<double> forecastSeries)
        {
            ForecastARIMA(_processSeries, _errorSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel,
                _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, nHead, out forecastSeries);
        }

        public void DrawSeriesData()
        {
            Statistic.DrawSeriesData(_processSeries, _startIndex);
        }

        public void DrawErrorData()
        {
            Statistic.DrawSeriesData(_errorSeries, _startIndex);
        }

        public void DrawAutocorrelation()
        {
            List<double> listAutocorrelation;
            List<double> listConfidenceLimit;
            Statistic.ComputeAutocorrelation(_processSeries, _startIndex, out listAutocorrelation);
            Statistic.ComputeConfidenceLimit(listAutocorrelation, _processSeries.Count, out listConfidenceLimit);
            Statistic.DrawAutocorrelation(listAutocorrelation, listConfidenceLimit);
            Statistic.WriteSeries(_processSeries, "Series.txt");
        }

        public void DrawPartialAutocorrelation()
        {
            List<double> listAutocorrelation;
            List<double> listPartialAutocorrelation;
            Statistic.ComputeAutocorrelation(_processSeries, _startIndex, out listAutocorrelation);
            Statistic.ComputePartialAutocorrelation(listAutocorrelation, out listPartialAutocorrelation);
            double confidenceLimit = 1.96 / Math.Sqrt(_processSeries.Count);
            Statistic.DrawPartialAutocorrelation(listPartialAutocorrelation, confidenceLimit);
        }

        public bool Export(string pathFile)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("ARIMA");
            doc.AppendChild(root);
            //save number of Input, Hidden, Output Nodes
            XmlElement regularDifference = doc.CreateElement("RegularDifferencing");
            regularDifference.InnerText = Convert.ToString(this._regularDifferencingLevel);
            XmlElement seasonDifference = doc.CreateElement("SeasonDifferencing");
            seasonDifference.InnerText = Convert.ToString(this._seasonDifferencingLevel);
            XmlElement seasonPartern = doc.CreateElement("SeasonPartern");
            seasonPartern.InnerText = Convert.ToString(this._seasonPartern);

            XmlElement pRegular = doc.CreateElement("ARRegular");
            pRegular.InnerText = Convert.ToString(this._pRegular);
            XmlElement qRegular = doc.CreateElement("MARegular");
            qRegular.InnerText = Convert.ToString(this._qRegular);
            XmlElement pSeason = doc.CreateElement("ARSeason");
            pSeason.InnerText = Convert.ToString(this._pSeason);
            XmlElement qSeason = doc.CreateElement("MASeason");
            qSeason.InnerText = Convert.ToString(this._qSeason);


            root.AppendChild(regularDifference);
            root.AppendChild(seasonDifference);
            root.AppendChild(seasonPartern);
            root.AppendChild(pRegular);
            root.AppendChild(qRegular);
            root.AppendChild(pSeason);
            root.AppendChild(qSeason);

            XmlElement ArimaCoef = doc.CreateElement("ARIMACoef");

            string arimaCoefStr = string.Empty;

            foreach (double coef in _listArimaCoef)
            {
                arimaCoefStr += "|" + coef;
            }
            if (arimaCoefStr.Length > 0)
            {
                arimaCoefStr = arimaCoefStr.Substring(1);
            }
            ArimaCoef.InnerText = arimaCoefStr;

            root.AppendChild(ArimaCoef);
            doc.Save(pathFile);
            return true;
        }

        public bool Import(string pathFile)
        {
            InitTraining();
            XmlDocument input = new XmlDocument();
            try
            {
                input.Load(pathFile);
                XmlNode root = input.FirstChild;
                //Get number of input, hidden, output nodes
                this._regularDifferencingLevel = Int32.Parse(root.SelectSingleNode("descendant::RegularDifferencing").InnerText);
                this._seasonDifferencingLevel = Int32.Parse(root.SelectSingleNode("descendant::SeasonDifferencing").InnerText);
                this._seasonPartern = Int32.Parse(root.SelectSingleNode("descendant::SeasonPartern").InnerText);
                this._pRegular = Int32.Parse(root.SelectSingleNode("descendant::ARRegular").InnerText);
                this._qRegular = Int32.Parse(root.SelectSingleNode("descendant::MARegular").InnerText);
                this._pSeason = Int32.Parse(root.SelectSingleNode("descendant::ARSeason").InnerText);
                this._qSeason = Int32.Parse(root.SelectSingleNode("descendant::MASeason").InnerText);

                string arimaCoef = root.SelectSingleNode("descendant::ARIMACoef").InnerText;
                string[] listArimaCoef = arimaCoef.Split('|');
                _listArimaCoef = new List<double>();
                foreach (string coef in listArimaCoef)
                {
                    double temp = Double.Parse(coef);
                    _listArimaCoef.Add(temp);
                }

                Statistic.ComputeDifference(ref _processSeries, ref _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _seasonPartern);
                ComputError(_processSeries, _startIndex, _regularDifferencingLevel, _seasonDifferencingLevel, _pRegular, _qRegular, _seasonPartern, _pSeason, _qSeason, _listArimaCoef, out _errorSeries);

            }
            catch
            {
            }
            return true;
        }
    
    }

}
