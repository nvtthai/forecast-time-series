using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace ForecastTimeSeries
{
    public class Neural
    {
        private List<double> _originSeries;
        private List<double> _processSeries;

        public int m_iNumInputNodes;
        public int m_iNumHiddenNodes;
        public int m_iNumOutputNodes;

        private Perceptron[] m_arInputNodes;
        private Perceptron[] m_arHiddenNodes;
        private Perceptron[] m_arOutputNodes;

        private double[,] m_arInputHiddenConn;
        private double[,] m_arHiddenOutputConn;

        private double[,] Backup_m_arInputHiddenConn;
        private double[,] Backup_m_arHiddenOutputConn;

        public void DrawSeriesData()
        {
            Statistic.DrawSeriesData(_originSeries, 0);
        }

        public Neural()
        {
            _originSeries = new List<double>();
            _processSeries = new List<double>();

            m_iNumInputNodes = 0;
            m_iNumHiddenNodes = 0;
            m_iNumOutputNodes = 0;
        }

        public Neural(int inputNodes, int hiddenNodes, int outputNodes)
        {
            _originSeries = new List<double>();
            _processSeries = new List<double>();

            m_iNumInputNodes = inputNodes;
            m_iNumHiddenNodes = hiddenNodes;
            m_iNumOutputNodes = outputNodes;

            int i, j, k;

            m_arInputNodes = new Perceptron[m_iNumInputNodes + 1];
            for (i = 0; i <= m_iNumInputNodes; i++)
            {
                m_arInputNodes[i] = new Perceptron(PerceptionType.PERCEPTION_INPUT, ActionvationFunction.SIGMOID_FUNCTION);
            }
            m_arInputNodes[m_iNumInputNodes].SetBiasNode();

            m_arHiddenNodes = new Perceptron[m_iNumHiddenNodes + 1];
            for (j = 0; j <= m_iNumHiddenNodes; j++)
            {
                m_arHiddenNodes[j] = new Perceptron(PerceptionType.PERCEPTION_HIDDEN, ActionvationFunction.SIGMOID_FUNCTION);
            }
            m_arHiddenNodes[m_iNumHiddenNodes].SetBiasNode();

            m_arOutputNodes = new Perceptron[m_iNumOutputNodes];
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                m_arOutputNodes[k] = new Perceptron(PerceptionType.PERCEPTION_OUTPUT, ActionvationFunction.SIGMOID_FUNCTION);
            }

            m_arInputHiddenConn = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            m_arHiddenOutputConn = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];

            Random rand = new Random();
            for (i = 0; i <= m_iNumInputNodes; i++)
            {
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    m_arInputHiddenConn[i, j] = rand.NextDouble();
                }
            }
            for (j = 0; j <= m_iNumHiddenNodes; j++)
            {
                for (k = 0; k < m_iNumOutputNodes; k++)
                {
                    m_arHiddenOutputConn[j, k] = rand.NextDouble();
                }
            }
        }

        private void InitForTrain()
        {
            Backup_m_arInputHiddenConn = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            Backup_m_arHiddenOutputConn = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];
            BackUp();
        }

        private void RollBack()
        {
            int i, j, k;
            for (j = 0; j < m_iNumHiddenNodes; j++)
            {
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    m_arInputHiddenConn[i, j] = Backup_m_arInputHiddenConn[i, j];
                }
            }
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    m_arHiddenOutputConn[j, k] = Backup_m_arHiddenOutputConn[j, k];
                }
            }
        }

        private void BackUp()
        {
            int i, j, k;

            for (j = 0; j < m_iNumHiddenNodes; j++)
            {
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    Backup_m_arInputHiddenConn[i, j] = m_arInputHiddenConn[i, j];
                }
            }
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    Backup_m_arHiddenOutputConn[j, k] = m_arHiddenOutputConn[j, k];
                }
            }
        }

        private void CalculateOutput(double[] input)
        {
            int i, j, k;
            double temp = 0;
            for (i = 0; i < m_iNumInputNodes; i++)
                m_arInputNodes[i].SetInput(input[i]);
            for (j = 0; j < m_iNumHiddenNodes; j++)
            {
                temp = 0;
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    temp += m_arInputHiddenConn[i, j] * m_arInputNodes[i].GetOutput();
                }
                m_arHiddenNodes[j].SetInput(temp);
            }
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                temp = 0;
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    temp += m_arHiddenOutputConn[j, k] * m_arHiddenNodes[j].GetOutput();
                }
                m_arOutputNodes[k].SetInput(temp);
            }
        }

        public void SetData(List<double> series)
        {
            _originSeries.Clear();
            _processSeries.Clear();
            for (int i = 0; i < series.Count; i++)
            {
                _originSeries.Add(series[i]);
            }

            double max = _originSeries.Max();
            double min = _originSeries.Min();
            int count = _originSeries.Count;
            for (int i = 0; i < count; i++)
            {
                double a = _originSeries.ElementAt(i);
                double b = (a - min) / (max - min) * (0.99 - 0.01) + 0.01;
                _processSeries.Add(b);
            }
        }

        public static Neural Import(string pathFile)
        {
            XmlDocument input = new XmlDocument();
            Neural loadedNetwork = null;
            try
            {
                input.Load(pathFile);
                XmlNode root = input.FirstChild;
                //Get number of input, hidden, output nodes
                int numInputNodes = Int32.Parse(root.SelectSingleNode("descendant::numInputNodes").InnerText);
                int numHiddenNodes = Int32.Parse(root.SelectSingleNode("descendant::numHiddenNodes").InnerText);
                int numOutputNodes = Int32.Parse(root.SelectSingleNode("descendant::numOutputNodes").InnerText);
                //create a network
                loadedNetwork = new Neural(numInputNodes, numHiddenNodes, numOutputNodes);
                //Get Input Nodes
                for (int i = 0; i <= loadedNetwork.m_iNumInputNodes; i++)
                {
                    //get a input node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Input" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arInputNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }
                    //get weight
                    for (int j = 0; j < loadedNetwork.m_iNumHiddenNodes; j++)
                    {
                        loadedNetwork.m_arInputHiddenConn[i, j] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::InHid" + Convert.ToString(i + 1) + Convert.ToString(j + 1)).InnerText);
                    }
                }
                //Get Hidden Nodes
                for (int i = 0; i <= loadedNetwork.m_iNumHiddenNodes; i++)
                {
                    //get a hidden node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Hidden" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arHiddenNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }

                    for (int j = 0; j < loadedNetwork.m_iNumOutputNodes; j++)
                    {
                        loadedNetwork.m_arHiddenOutputConn[i, j] = Convert.ToDouble(tempNode.SelectSingleNode("descendant::HidOut" + Convert.ToString(i + 1) + Convert.ToString(j + 1)).InnerText);
                    }
                }
                //Get Output Nodes
                for (int i = 0; i < loadedNetwork.m_iNumOutputNodes; i++)
                {
                    //get a output node
                    XmlNode tempNode = root.SelectSingleNode("descendant::Output" + Convert.ToString(i + 1));
                    //get activation function type
                    string activationFunc = tempNode.SelectSingleNode("descendant::activateFunc").InnerText;
                    if (activationFunc.Equals("SIGMOID_FUNCTION"))
                    {
                        loadedNetwork.m_arOutputNodes[i].m_activeFuncType = ActionvationFunction.SIGMOID_FUNCTION;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return loadedNetwork;
        }

        public static bool Export(Neural network, string pathFile)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Network");
            doc.AppendChild(root);
            //save number of Input, Hidden, Output Nodes
            XmlElement numInput = doc.CreateElement("numInputNodes");
            numInput.InnerText = Convert.ToString(network.m_iNumInputNodes);
            XmlElement numHidden = doc.CreateElement("numHiddenNodes");
            numHidden.InnerText = Convert.ToString(network.m_iNumHiddenNodes);
            XmlElement numOutput = doc.CreateElement("numOutputNodes");
            numOutput.InnerText = Convert.ToString(network.m_iNumOutputNodes);
            root.AppendChild(numInput);
            root.AppendChild(numHidden);
            root.AppendChild(numOutput);
            //save input nodes
            XmlElement InputNodes = doc.CreateElement("InputNodes");
            for (int i = 0; i <= network.m_iNumInputNodes; i++)
            {
                XmlElement aInputNode = doc.CreateElement("Input" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arInputNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aInputNode.AppendChild(actFunc);
                }

                //save weight for in-hid connection
                for (int j = 0; j < network.m_iNumHiddenNodes; j++)
                {
                    XmlElement aWeight = doc.CreateElement("InHid" + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                    aWeight.InnerText = Convert.ToString(network.m_arInputHiddenConn[i, j]);
                    aInputNode.AppendChild(aWeight);
                }
                InputNodes.AppendChild(aInputNode);
            }
            root.AppendChild(InputNodes);

            //save hidden nodes
            XmlElement HiddenNodes = doc.CreateElement("HiddenNodes");
            for (int i = 0; i <= network.m_iNumHiddenNodes; i++)
            {
                XmlElement aHiddenNode = doc.CreateElement("Hidden" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arHiddenNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aHiddenNode.AppendChild(actFunc);
                }

                //save weight for hid-out connection
                for (int j = 0; j < network.m_iNumOutputNodes; j++)
                {
                    XmlElement aWeight = doc.CreateElement("HidOut" + Convert.ToString(i + 1) + Convert.ToString(j + 1));
                    aWeight.InnerText = Convert.ToString(network.m_arHiddenOutputConn[i, j]);
                    aHiddenNode.AppendChild(aWeight);
                }
                HiddenNodes.AppendChild(aHiddenNode);
            }
            root.AppendChild(HiddenNodes);

            //save output nodes
            XmlElement OutputNodes = doc.CreateElement("OutputNodes");
            for (int i = 0; i < network.m_iNumOutputNodes; i++)
            {
                XmlElement aOutputNode = doc.CreateElement("Output" + Convert.ToString(i + 1));
                //save activation func
                if (network.m_arOutputNodes[i].m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
                {
                    XmlElement actFunc = doc.CreateElement("activateFunc");
                    actFunc.InnerText = "SIGMOID_FUNCTION";
                    aOutputNode.AppendChild(actFunc);
                }

                OutputNodes.AppendChild(aOutputNode);
            }
            root.AppendChild(OutputNodes);
            doc.Save(pathFile);
            return true;
        }

        public void Bp_Run(double learnRate, double momentum, double theEpoches = 10000, double residual = 1.0E-5)
        {
            InitForTrain();

            int i, j, k, n;
            int epoch = 0;
            double MAE = Double.MaxValue;
            double LastError = Double.MaxValue;
            List<double> MAError = new List<double>();

            double[,] deltaInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            double[,] deltaHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];
            double[,] lagDeltaInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            double[,] lagDeltaHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];

            for (j = 0; j < m_iNumHiddenNodes; j++)    // initialize weight-step of Input Hidden connection
            {
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    deltaInputHidden[i, j] = 0.0;
                    lagDeltaInputHidden[i, j] = 0.0;
                }
            }
            for (k = 0; k < m_iNumOutputNodes; k++)   // initialize weight-step of Hidden Output connection
            {
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    deltaHiddenOutput[j, k] = 0.0;
                    lagDeltaHiddenOutput[j, k] = 0.0;
                }
            }

            while (epoch < theEpoches)
            {
                MAE = 0.0;
                for (n = m_iNumInputNodes; n < _processSeries.Count; n++)
                {
                    // forward
                    double[] lstTemp = new double[m_iNumInputNodes];
                    for (i = m_iNumInputNodes; i > 0; i--)
                    {
                        lstTemp[m_iNumInputNodes - i] = _processSeries[n - i];
                    }
                    CalculateOutput(lstTemp);
                    for (k = 0; k < m_iNumOutputNodes; k++)
                    {
                        MAE += Math.Abs(_processSeries.ElementAt(n + k) - m_arOutputNodes[k].GetOutput());
                    }

                    // backward
                    /*calculate weight-step for weights connecting from hidden nodes to output nodes*/
                    for (k = 0; k < m_iNumOutputNodes; k++)
                    {
                        for (j = 0; j <= m_iNumHiddenNodes; j++)
                        {
                            double parDerv = -m_arOutputNodes[k].GetOutput() * (1 - m_arOutputNodes[k].GetOutput()) * m_arHiddenNodes[j].GetOutput() * (_processSeries.ElementAt(n) - m_arOutputNodes[k].GetOutput());
                            deltaHiddenOutput[j, k] = -learnRate * parDerv + momentum * lagDeltaHiddenOutput[j, k];
                            lagDeltaHiddenOutput[j, k] = deltaHiddenOutput[j, k];
                        }
                    }
                    /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double temp = 0.0;
                        for (k = 0; k < m_iNumOutputNodes; k++)
                        {
                            temp += -(_processSeries.ElementAt(n) - m_arOutputNodes[k].GetOutput()) * m_arOutputNodes[k].GetOutput() * (1 - m_arOutputNodes[k].GetOutput()) * m_arHiddenOutputConn[j, k];
                        }
                        for (i = 0; i <= m_iNumInputNodes; i++)
                        {
                            double parDerv = m_arHiddenNodes[j].GetOutput() * (1 - m_arHiddenNodes[j].GetOutput()) * m_arInputNodes[i].GetInput() * temp;
                            deltaInputHidden[i, j] = -learnRate * parDerv + momentum * lagDeltaInputHidden[i, j];
                            lagDeltaInputHidden[i, j] = deltaInputHidden[i, j];
                        }
                    }
                    /*updating weight from Input to Hidden*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        for (i = 0; i <= m_iNumInputNodes; i++)
                        {
                            m_arInputHiddenConn[i, j] += deltaInputHidden[i, j];
                        }
                    }
                    /*updating weight from Hidden to Output*/
                    for (k = 0; k < m_iNumOutputNodes; k++)
                    {
                        for (j = 0; j <= m_iNumHiddenNodes; j++)
                        {
                            m_arHiddenOutputConn[j, k] += deltaHiddenOutput[j, k];
                        }
                    }

                } // end outer for
                MAE = MAE / (_processSeries.Count - m_iNumInputNodes); // caculate mean square error
                if (Math.Abs(MAE - LastError) < residual) // if the Error is not improved significantly, halt training process and rollback
                {
                    RollBack();
                    break;

                }
                else
                { //else backup the current configuration and continue training
                    LastError = MAE;
                    BackUp();
                    MAError.Add(MAE);
                    epoch++;
                }
            }
            /* output training result */

            Train_Result result = new Train_Result();
            result.trainResult.AppendText("Maximum Epochs: " + theEpoches + "\n");
            result.trainResult.AppendText("Training Epoches: " + epoch + "\n");
            result.trainResult.AppendText("Training MAE: " + MAE + "\n");
            result.trainResult.AppendText("Terminated Condition: residual of Error is less than " + residual + "\n");
            result.trainResult.AppendText("Learning Rate: " + learnRate + "\n");
            result.trainResult.AppendText("Momentum Term: " + momentum + "\n");
            result.trainResult.ReadOnly = true;
            result.chart1.Series["MAE"].Color = System.Drawing.Color.Red;
            for (int t = 0; t < MAError.Count; t++)
            {
                result.chart1.Series["MAE"].Points.AddXY(t + 1, MAError.ElementAt(t));
            }
            result.ShowDialog();
        }

        public void Rprop_Run(double defaultDeltaValue = 0.0001, double maxDelta = 50.0, double theEpoches = 10000, double residual = 1.0E-5)
        {
            InitForTrain();

            int n, i, j, k;
            int epoch = 0;
            double MAE = Double.MaxValue;
            double defaultWeightChange = 0.0;
            double defaultGradientValue = 0.0;
            double minDelta = 1.0E-6;
            double maxStep = 1.2;
            double minStep = 0.5;
            double LastError = Double.MaxValue;
            List<double> MAError = new List<double>();

            double[,] weightChangeInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            double[,] deltaInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            double[,] gradientInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];
            double[,] newGradientInputHidden = new double[m_iNumInputNodes + 1, m_iNumHiddenNodes];

            double[,] weightChangeHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];
            double[,] deltaHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];
            double[,] gradientHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];
            double[,] newGradientHiddenOutput = new double[m_iNumHiddenNodes + 1, m_iNumOutputNodes];


            // initialize Input Hidden connection
            for (j = 0; j < m_iNumHiddenNodes; j++)
            {
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    weightChangeInputHidden[i, j] = defaultWeightChange;
                    deltaInputHidden[i, j] = defaultDeltaValue;
                    gradientInputHidden[i, j] = defaultGradientValue;
                    newGradientInputHidden[i, j] = defaultGradientValue;
                }
            }

            // initialize Hidden Output connection
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    weightChangeHiddenOutput[j, k] = defaultWeightChange;
                    deltaHiddenOutput[j, k] = defaultDeltaValue;
                    gradientHiddenOutput[j, k] = defaultGradientValue;
                    newGradientHiddenOutput[j, k] = defaultGradientValue;
                }
            }

            while (epoch < theEpoches)
            {
                MAE = 0.0;
                //training for each epoch
                for (n = m_iNumInputNodes; n < _processSeries.Count; n++)
                {
                    //forward
                    double[] lstTemp = new double[m_iNumInputNodes];
                    for (i = m_iNumInputNodes; i > 0; i--)
                    {
                        lstTemp[m_iNumInputNodes - i] = _processSeries[n - i];
                    }
                    CalculateOutput(lstTemp);

                    /*calculate abs error*/
                    for (k = 0; k < m_iNumOutputNodes; k++)
                    {
                        MAE += Math.Abs(_processSeries.ElementAt(n) - m_arOutputNodes[k].GetOutput());
                    }
                    // backward
                    /*calculate weight-step for weights connecting from hidden nodes to output nodes*/
                    for (k = 0; k < m_iNumOutputNodes; k++)
                    {
                        for (j = 0; j <= m_iNumHiddenNodes; j++)
                        {
                            newGradientHiddenOutput[j, k] += -m_arOutputNodes[k].GetOutput() * (1 - m_arOutputNodes[k].GetOutput()) * m_arHiddenNodes[j].GetOutput() * (_processSeries[n] - m_arOutputNodes[k].GetOutput());
                        }
                    }
                    /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                    for (j = 0; j < m_iNumHiddenNodes; j++)
                    {
                        double temp = 0.0;
                        for (k = 0; k < m_iNumOutputNodes; k++)
                        {
                            temp += -(_processSeries.ElementAt(n) - m_arOutputNodes[k].GetOutput()) * m_arOutputNodes[k].GetOutput() * (1 - m_arOutputNodes[k].GetOutput()) * m_arHiddenOutputConn[j, k];
                        }
                        for (i = 0; i <= m_iNumInputNodes; i++)
                        {
                            newGradientInputHidden[i, j] += m_arHiddenNodes[j].GetOutput() * (1 - m_arHiddenNodes[j].GetOutput()) * m_arInputNodes[i].GetOutput() * temp;
                        }
                    }

                } // end outer for

                int sign;
                for (k = 0; k < m_iNumOutputNodes; k++)
                {
                    for (j = 0; j <= m_iNumHiddenNodes; j++)
                    {
                        sign = Math.Sign(newGradientHiddenOutput[j, k] * gradientHiddenOutput[j, k]);
                        if (sign > 0)
                        {
                            deltaHiddenOutput[j, k] = Math.Min(deltaHiddenOutput[j, k] * maxStep, maxDelta);
                            weightChangeHiddenOutput[j, k] = -Math.Sign(newGradientHiddenOutput[j, k]) * deltaHiddenOutput[j, k];
                            m_arHiddenOutputConn[j, k] += weightChangeHiddenOutput[j, k];
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];
                        }
                        else if (sign < 0)
                        {
                            deltaHiddenOutput[j, k] = Math.Max(deltaHiddenOutput[j, k] * minStep, minDelta);
                            m_arHiddenOutputConn[j, k] -= weightChangeHiddenOutput[j, k]; //restore old value
                            newGradientHiddenOutput[j, k] = 0.0;
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];
                        }
                        else
                        {
                            weightChangeHiddenOutput[j, k] = -Math.Sign(newGradientHiddenOutput[j, k]) * deltaHiddenOutput[j, k];
                            m_arHiddenOutputConn[j, k] += weightChangeHiddenOutput[j, k];
                            gradientHiddenOutput[j, k] = newGradientHiddenOutput[j, k];
                        }
                        newGradientHiddenOutput[j, k] = 0.0;
                    }
                }

                /*calculate weight-step for weights connecting from input nodes to hidden nodes*/
                for (j = 0; j < m_iNumHiddenNodes; j++)
                {
                    for (i = 0; i <= m_iNumInputNodes; i++)
                    {
                        sign = Math.Sign(newGradientInputHidden[i, j] * gradientInputHidden[i, j]);
                        if (sign > 0)
                        {
                            deltaInputHidden[i, j] = Math.Min(deltaInputHidden[i, j] * maxStep, maxDelta);
                            weightChangeInputHidden[i, j] = -Math.Sign(newGradientInputHidden[i, j]) * deltaInputHidden[i, j];
                            m_arInputHiddenConn[i, j] += weightChangeInputHidden[i, j];
                            gradientInputHidden[i, j] = newGradientInputHidden[i, j];
                        }
                        else if (sign < 0)
                        {
                            deltaInputHidden[i, j] = Math.Max(deltaInputHidden[i, j] * minStep, minDelta);
                            m_arInputHiddenConn[i, j] -= weightChangeInputHidden[i, j];
                            newGradientInputHidden[i, j] = 0.0;
                            gradientInputHidden[i, j] = 0.0;
                        }
                        else
                        {
                            weightChangeInputHidden[i, j] = -Math.Sign(newGradientInputHidden[i, j]) * deltaInputHidden[i, j];
                            m_arInputHiddenConn[i, j] += weightChangeInputHidden[i, j];
                            gradientInputHidden[i, j] = newGradientInputHidden[i, j];
                        }
                        newGradientInputHidden[i, j] = 0.0;
                    }
                }
                MAE = MAE / (_processSeries.Count); // caculate mean square error
                if (Math.Abs(MAE - LastError) < residual) // if the Error is not improved significantly, halt training process and rollback
                {
                    RollBack();
                    break;

                }
                else
                { //else backup the current configuration and continue training
                    LastError = MAE;
                    BackUp();
                    MAError.Add(MAE);
                    epoch++;
                }
            }
            /* output training result */
            Train_Result result = new Train_Result();
            result.trainResult.AppendText("Maximum Epochs: " + theEpoches + "\n");
            result.trainResult.AppendText("Training Epoches: " + epoch + "\n");
            result.trainResult.AppendText("Training MAE: " + MAE + "\n");
            result.trainResult.AppendText("Terminated Condition: residual of Error is less than " + residual + "\n");
            result.trainResult.AppendText("Maximum update value: " + maxDelta + "\n");
            result.trainResult.AppendText("default update value: " + defaultDeltaValue + "\n");
            result.trainResult.ReadOnly = true;
            result.chart1.Series["MAE"].Color = System.Drawing.Color.Red;
            for (int t = 0; t < MAError.Count; t++)
            {
                result.chart1.Series["MAE"].Points.AddXY(t + 1, MAError.ElementAt(t));
            }
            result.label1.Text = "Algorithm: RPROP";
            result.ShowDialog();
        }

        public void Forecast(int nHead, out List<double> forecastSeries)
        {
            forecastSeries = new List<double>();
            double max = _originSeries.Max();
            double min = _originSeries.Min();
            List<double> currentSeries = _processSeries.FindAll(item => true);
            for (int i = 0; i < nHead; i++)
            {
                double[] input = new double[m_iNumInputNodes];
                for (int j = m_iNumInputNodes; j > 0; j--)
                {
                    input[m_iNumInputNodes - j] = currentSeries[currentSeries.Count - j];
                }
                CalculateOutput(input);
                double temp = m_arOutputNodes[0].GetOutput();
                currentSeries.Add(temp);
                temp = (temp - 0.01) / (0.99 - 0.01) * (max - min) + min;
                forecastSeries.Add(temp);
            }
        }

        public void GetTestSeries(out List<double> testSeries)
        {
            testSeries = new List<double>();
            for (int i = 0; i < m_iNumInputNodes; i++)
            {
                testSeries.Add(_originSeries[i]);
            }
            double min = _originSeries.Min();
            double max = _originSeries.Max();
            for (int i = m_iNumInputNodes; i < _processSeries.Count; i++)
            {
                double[] input = new double[m_iNumInputNodes];
                for (int j = m_iNumInputNodes; j > 0; j--)
                {
                    input[m_iNumInputNodes - j] = _processSeries[i - j];
                }
                CalculateOutput(input);
                double temp = m_arOutputNodes[0].GetOutput();
                temp = (temp - 0.01) / (0.99 - 0.01) * (max - min) + min;
                testSeries.Add(temp);
            }
        }
        
    }

}
