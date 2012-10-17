using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;


namespace NeuralNetwork
{
    public class Neural
    {
        //use hommology, the last perception in each level is Bias 
        public int m_iNumInputNodes;
        public int m_iNumHiddenNodes;
        public int m_iNumOutputNodes;

        public Perceptron[] m_arInputNodes; 
        public Perceptron[] m_arHiddenNodes; 
        public Perceptron[] m_arOutputNodes; 

        public double[,] m_arInputHiddenConn; 
        public double[,] m_arHiddenOutputConn; 

        public double[,] Backup_m_arInputHiddenConn; 
        public double[,] Backup_m_arHiddenOutputConn; 

        private Neural()
        {
            m_iNumInputNodes = 0;
            m_iNumHiddenNodes = 0;
            m_iNumOutputNodes = 0;
        }

        public Neural(int inputNodes, int hiddenNodes, int outputNodes)
        {
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

        public void CalculateOutput(double[] input)
        {
            int i, j, k;
            for (i = 0; i < m_iNumInputNodes; i++)
                m_arInputNodes[i].SetInput(input[i]);
            for (j = 0; j < m_iNumHiddenNodes; j++)
            {
                double temp = 0;
                for (i = 0; i <= m_iNumInputNodes; i++)
                {
                    temp += m_arInputHiddenConn[i, j] * m_arInputNodes[i].GetOutput();
                }
                m_arHiddenNodes[j].SetInput(temp);
            }
            for (k = 0; k < m_iNumOutputNodes; k++)
            {
                double temp = 0;
                for (j = 0; j <= m_iNumHiddenNodes; j++)
                {
                    temp += m_arHiddenOutputConn[j, k] * m_arHiddenNodes[j].GetOutput();
                }
                m_arOutputNodes[k].SetInput(temp);
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
                string lags = root.SelectSingleNode("descendant::Lag").InnerText;
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
                    aWeight.InnerText = Convert.ToString(network.m_arInputHiddenConn[j, i]);
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
                    aWeight.InnerText = Convert.ToString(network.m_arHiddenOutputConn[j, i]);
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

    }

}
