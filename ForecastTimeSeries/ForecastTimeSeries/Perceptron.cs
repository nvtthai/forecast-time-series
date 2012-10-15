using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastTimeSeries
{
    public enum ActionvationFunction
    {
        SIGMOID_FUNCTION = 0x01,
    }

    public enum PerceptionType
    {
        PERCEPTION_INPUT,
        PERCEPTION_HIDDEN,
        PERCEPTION_OUTPUT,
    }

    public class Perceptron
    {
        private double m_dInput;
        private double m_dOutput = 0.0;
        private bool m_bBias = false;
        public PerceptionType m_perceptionType;
        public ActionvationFunction m_activeFuncType;

        public Perceptron(PerceptionType perceptionType, ActionvationFunction activeType)
        {
            m_perceptionType = perceptionType;
            m_activeFuncType = activeType;
        }

        public void SetBiasNode()
        {
            m_dInput = m_dOutput = 1.0;
            m_bBias = true;
        }

        public void SetInput(double input)
        {
            m_dInput = input;
            CalOutput();
        }

        public double GetInput()
        {
            return m_dInput;
        }

        public double GetOutput()
        {
            return CalOutput();
        }

        private double CalOutput()
        {
            if (m_bBias)
            {
                m_dOutput = 1.0;
            }
            else if (m_perceptionType == PerceptionType.PERCEPTION_INPUT)
            {
                m_dOutput = m_dInput;
            }
            else if (m_activeFuncType == ActionvationFunction.SIGMOID_FUNCTION)
            {
                m_dOutput = 1 / (1 + Math.Exp(-m_dInput));
            }
            return m_dOutput;
        }
    }

}
