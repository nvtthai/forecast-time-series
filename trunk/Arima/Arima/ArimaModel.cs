using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arima
{
    public class ArimaModel
    {
        public uint arOrder { get; set; }
        public uint maOrder { get; set; }
        public uint arSeasonOrder { get; set; }
        public uint maSeasonOrder { get; set; }
        public uint seasonOrder { get; set; }
        public uint diffOrder { get; set; }
        public uint diffSeasonOrder { get; set; }

        public double intercept { get; set; }
        public double error { get; set; }
        public double[] arCoeff { get; set; }
        public double[] maCoeff { get; set; }
        public double[] arSeasonCoeff { get; set; }
        public double[] maSeasonCoeff { get; set; }

        public Polynomial ARPoly { get; set; }
        public Polynomial MAPoly { get; set; }

        public ArimaModel(double[] ar, double[] ma, double[] arSeason, double[] maSeason, double intercept, uint season, uint diff, uint diffSeason)
        {
            arOrder = (uint)ar.Length;
            maOrder = (uint)ma.Length;
            arSeasonOrder = (uint)arSeason.Length;
            maSeasonOrder = (uint)maSeason.Length;
            this.intercept = intercept;
            seasonOrder = season;
            diffOrder = diff;
            diffSeasonOrder = diffSeason;

            arCoeff = new double[arOrder];
            maCoeff = new double[maOrder];
            arSeasonCoeff = new double[arSeasonOrder];
            maSeasonCoeff = new double[maSeasonOrder];

            ar.CopyTo(arCoeff, 0);
            ma.CopyTo(maCoeff, 0);
            arSeason.CopyTo(arSeasonCoeff, 0);
            maSeason.CopyTo(maSeasonCoeff, 0);

        }

        public Polynomial ComputeARModel()
        {
            Polynomial arPoly = new Polynomial(1);
            Polynomial arSeasonPoly = new Polynomial(1);
            Polynomial diffPoly = new Polynomial(1);
            Polynomial diffSeasonPoly = new Polynomial(1);

            if (arOrder > 0)
            {
                arPoly -= (new Polynomial(arCoeff)) * (new Polynomial(0, 1));
            }
            if (arSeasonOrder > 0)
            {
                arSeasonPoly -= (new Polynomial(arSeasonCoeff)) * (new Polynomial(0, 1));
            }
            if (diffOrder > 0)
            {
                diffPoly = (new Polynomial(1, -1)) ^ diffOrder;
            }
            if (diffSeasonOrder > 0)
            {
                diffSeasonPoly = (new Polynomial(1, -1)) ^ diffSeasonOrder;
            }

            ARPoly = (new Polynomial(1)) - arPoly * arSeasonPoly * diffPoly * diffSeasonPoly;
            return ARPoly;
        }

        public Polynomial ComputeMAModel()
        {
            Polynomial maPoly = new Polynomial(1);
            Polynomial maSeasonPoly = new Polynomial(1);
            if (maOrder > 0)
            {
                maPoly -= (new Polynomial(maCoeff)) * (new Polynomial(0, 1));
            }
            if (maSeasonOrder > 0)
            {
                maSeasonPoly -= (new Polynomial(maSeasonCoeff)) * (new Polynomial(0, 1));
            }
            MAPoly = (new Polynomial(1)) - maPoly * maSeasonPoly;

            return MAPoly;
        }

        public double ComputeIntercept()
        {
            double result = 0;
            for (int i = 1; i < ARPoly.Coefficients.Length; i++)
            {
                result += ARPoly.Coefficients[i];
            }
            intercept = intercept * (1 - result);
            return intercept;
        }

        public double ComputeARValue(double[] dataSeries, int index)
        {
            double result = 0;
            for (int i = 1; i < ARPoly.Coefficients.Length; i++)
            {
                result += ARPoly.Coefficients[i] * dataSeries[index - i];
            }
            return result;
        }

        public double ComputeMAValue(double[] errorSeries, int index)
        {
            double result = 0;
            for (int i = 1; i < MAPoly.Coefficients.Length; i++)
            {
                result += MAPoly.Coefficients[i] * errorSeries[index - i];
            }
            return result;
        }

        public double ComputeValue(double[] dataSeries, double[] errorSeries, int index)
        {
            double result = 0;
            double ARComponent = ComputeARValue(dataSeries, index);
            double MAComponent = ComputeMAValue(errorSeries, index);
            result = intercept + ARComponent + MAComponent;
            return result;
        }

    }
}
