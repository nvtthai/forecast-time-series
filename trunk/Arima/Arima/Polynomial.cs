using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace Arima
{
    public class Polynomial
    {
        #region Fields

        /// <summary>
        /// Coefficients a_0,...,a_n of a polynomial p, such that
        /// p(x) = a_0 + a_1*x + a_2*x^2 + ... + a_n*x^n.
        /// </summary>
        public double[] Coefficients;

        #endregion

        #region constructors

        /// <summary>
        /// Inits zero polynomial p = 0.
        /// </summary>
        public Polynomial()
        {
            Coefficients = new double[1];
            Coefficients[0] = 0;
        }


        /// <summary>
        /// Inits polynomial from given real coefficient array.
        /// </summary>
        /// <param name="coeffs"></param>
        public Polynomial(params double[] coeffs)
        {
            if (coeffs == null || coeffs.Length < 1)
            {
                Coefficients = new double[1];
                Coefficients[0] = 0;
            }
            else
            {
                Coefficients = new double[coeffs.Length];
                for (int i = 0; i < coeffs.Length; i++)
                    Coefficients[i] = coeffs[i];
            }
        }

        /// <summary>
        /// Inits constant polynomial.
        /// </summary>
        /// <param name="coeffs"></param>
        public Polynomial(double c)
        {
            Coefficients = new double[1];
            Coefficients[0] = c;
        }

        #endregion

        #region dynamics

        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Differentiate(double x)
        {
            double[] buf = new double[Degree];

            for (int i = 0; i < buf.Length; i++)
                buf[i] = (i + 1) * Coefficients[i + 1];

            return (new Polynomial(buf)).Evaluate(x);
        }

        /// <summary>
        /// Computes the definite integral within the borders a and b.
        /// </summary>
        /// <param name="a">Left integration border.</param>
        /// <param name="b">Right integration border.</param>
        /// <returns></returns>
        public double Integrate(double a, double b)
        {
            double[] buf = new double[Degree + 2];
            buf[0] = 0; // this value can be arbitrary, in fact

            for (int i = 1; i < buf.Length; i++)
                buf[i] = Coefficients[i - 1] / i;

            Polynomial p = new Polynomial(buf);

            return (p.Evaluate(b) - p.Evaluate(a));
        }

        /// <summary>
        /// Degree of the polynomial.
        /// </summary>
        public int Degree
        {
            get
            {
                return Coefficients.Length - 1;
            }
        }

        /// <summary>
        /// Checks if given polynomial is zero.
        /// </summary>
        /// <returns></returns>
        public bool IsZero()
        {
            for (int i = 0; i < Coefficients.Length; i++)
                if (Coefficients[i] != 0) return false;

            return true;
        }

        /// <summary>
        /// Evaluates polynomial by using the horner scheme.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Evaluate(double x)
        {
            double buf = Coefficients[Degree];

            for (int i = Degree - 1; i >= 0; i--)
            {
                buf = Coefficients[i] + x * buf;
            }

            return buf;
        }

        /// <summary>
        /// Normalizes the polynomial, e.i. divides each coefficient by the
        /// coefficient of a_n the greatest term if a_n != 1.
        /// </summary>
        public void Normalize()
        {
            this.Clean();

            if (Coefficients[Degree] != 1)
                for (int k = 0; k <= Degree; k++)
                    Coefficients[k] /= Coefficients[Degree];
        }

        /// <summary>
        /// Removes unnecessary zero terms.
        /// </summary>
        public void Clean()
        {
            int i;

            for (i = Degree; i >= 0 && Coefficients[i] == 0; i--) ;

            double[] coeffs = new double[i + 1];

            for (int k = 0; k <= i; k++)
                coeffs[k] = Coefficients[k];

            Coefficients = (double[])coeffs.Clone();
        }

        /// <summary>
        /// Factorizes polynomial to its linear factors.
        /// </summary>
        /// <returns></returns>
        public FactorizedPolynomial Factorize()
        {
            // this is to be returned
            FactorizedPolynomial p = new FactorizedPolynomial();

            // cannot factorize polynomial of degree 0 or 1
            if (this.Degree <= 1)
            {
                p.Factor = new Polynomial[] { this };
                p.Power = new int[] { 1 };

                return p;
            }

            double[] roots = Roots(this);

            Polynomial[] factor = new Polynomial[roots.Length];
            int[] power = new int[roots.Length];

            power[0] = 1;
            factor[0] = new Polynomial(new double[] { -Coefficients[Degree] * (double)roots[0], Coefficients[Degree] });

            for (int i = 1; i < roots.Length; i++)
            {
                power[i] = 1;
                factor[i] = new Polynomial(new double[] { -(double)roots[i], 1 });
            }

            p.Factor = factor;
            p.Power = power;

            return p;
        }

        /// <summary>
        /// Computes the roots of polynomial via Weierstrass iteration.
        /// </summary>        
        /// <returns></returns>
        public double[] Roots()
        {
            double tolerance = 1e-12;
            int max_iterations = 30;

            Polynomial q = Normalize(this);
            //Polynomial q = p;

            double[] z = new double[q.Degree]; // approx. for roots
            double[] w = new double[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Math.Exp(2 * Math.PI * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
            {
                z[k] = Math.Round(z[k], 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p">Polynomial to compute the roots of.</param>
        /// <param name="tolerance">Computation precision; e.g. 1e-12 denotes 12 exact digits.</param>
        /// <param name="max_iterations">Maximum number of iterations; this value is used to bound
        /// the computation effort if desired pecision is hard to achieve.</param>
        /// <returns></returns>
        public double[] Roots(double tolerance, int max_iterations)
        {
            Polynomial q = Normalize(this);

            double[] z = new double[q.Degree]; // approx. for roots
            double[] w = new double[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Math.Exp(2 * Math.PI * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
            {
                z[k] = Math.Round(z[k], 12);
            }

            return z;
        }

        #endregion

        #region statics

        /// <summary>
        /// Expands factorized polynomial p_1(x)^(k_1)*...*p_r(x)^(k_r) to its normal form a_0 + a_1 x + ... + a_n x^n.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Expand(FactorizedPolynomial p)
        {
            Polynomial q = new Polynomial(1);

            for (int i = 0; i < p.Factor.Length; i++)
            {
                for (int j = 0; j < p.Power[i]; j++)
                    q *= p.Factor[i];

                q.Clean();
            }

            return q;
        }

        /// <summary>
        /// Evaluates factorized polynomial p at point x.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double Evaluate(FactorizedPolynomial p, double x)
        {
            double z = 1;

            for (int i = 0; i < p.Factor.Length; i++)
            {
                z *= Math.Pow(p.Factor[i].Evaluate(x), p.Power[i]);
            }

            return z;
        }

        /// <summary>
        /// Removes unncessary leading zeros.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Clean(Polynomial p)
        {
            int i;

            for (i = p.Degree; i >= 0 && p.Coefficients[i] == 0; i--) ;

            double[] coeffs = new double[i + 1];

            for (int k = 0; k <= i; k++)
                coeffs[k] = p.Coefficients[k];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// Normalizes the polynomial, e.i. divides each coefficient by the
        /// coefficient of a_n the greatest term if a_n != 1.
        /// </summary>
        public static Polynomial Normalize(Polynomial p)
        {
            Polynomial q = Clean(p);

            if (q.Coefficients[q.Degree] != 1)
                for (int k = 0; k <= q.Degree; k++)
                    q.Coefficients[k] /= q.Coefficients[q.Degree];

            return q;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double[] Roots(Polynomial p)
        {
            double tolerance = 1e-12;
            int max_iterations = 30;

            Polynomial q = Normalize(p);
            //Polynomial q = p;

            double[] z = new double[q.Degree]; // approx. for roots
            double[] w = new double[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Math.Exp(2 * Math.PI * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
            {
                z[k] = Math.Round(z[k], 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p">Polynomial to compute the roots of.</param>
        /// <param name="tolerance">Computation precision; e.g. 1e-12 denotes 12 exact digits.</param>
        /// <param name="max_iterations">Maximum number of iterations; this value is used to bound
        /// the computation effort if desired pecision is hard to achieve.</param>
        /// <returns></returns>
        public static double[] Roots(Polynomial p, double tolerance, int max_iterations)
        {
            Polynomial q = Normalize(p);

            double[] z = new double[q.Degree]; // approx. for roots
            double[] w = new double[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Math.Exp(2 * Math.PI * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
            {
                z[k] = Math.Round(z[k], 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the greatest value |p(z_k)|.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double MaxValue(Polynomial p, double[] z)
        {
            double buf = 0;
            for (int i = 0; i < z.Length; i++)
            {
                if (Math.Abs(p.Evaluate(z[i])) > buf)
                    buf = Math.Abs(p.Evaluate(z[i]));
            }

            return buf;
        }

        /// <summary>
        /// For g(x) = (x-z_0)*...*(x-z_n), this method returns
        /// g'(z_k) = \prod_{j != k} (z_k - z_j).
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        private static double WeierNull(double[] z, int k)
        {
            if (k < 0 || k >= z.Length)
                throw new ArgumentOutOfRangeException();

            double buf = 1;

            for (int j = 0; j < z.Length; j++)
                if (j != k) buf *= (z[k] - z[j]);

            return buf;
        }

        /// <summary>
        /// Differentiates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Derivative(Polynomial p)
        {
            double[] buf = new double[p.Degree];

            for (int i = 0; i < buf.Length; i++)
                buf[i] = (i + 1) * p.Coefficients[i + 1];

            return new Polynomial(buf);
        }

        /// <summary>
        /// Integrates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Integral(Polynomial p)
        {
            double[] buf = new double[p.Degree + 2];
            buf[0] = 0; // this value can be arbitrary, in fact

            for (int i = 1; i < buf.Length; i++)
                buf[i] = p.Coefficients[i - 1] / i;

            return new Polynomial(buf);
        }

        /// <summary>
        /// Computes the monomial x^degree.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Polynomial Monomial(int degree)
        {
            if (degree == 0) return new Polynomial(1);

            double[] coeffs = new double[degree + 1];

            for (int i = 0; i < degree; i++)
                coeffs[i] = 0;

            coeffs[degree] = 1;

            return new Polynomial(coeffs);
        }

        public static Polynomial[] GetStandardBase(int dim)
        {
            if (dim < 1)
                throw new ArgumentException("Dimension expected to be greater than zero.");

            Polynomial[] buf = new Polynomial[dim];

            for (int i = 0; i < dim; i++)
                buf[i] = Monomial(i);

            return buf;
        }

        #endregion

        #region overrides & operators

        public static Polynomial operator +(Polynomial p, Polynomial q)
        {

            int degree = Math.Max(p.Degree, q.Degree);

            double[] coeffs = new double[degree + 1];

            for (int i = 0; i <= degree; i++)
            {
                if (i > p.Degree) coeffs[i] = q.Coefficients[i];
                else if (i > q.Degree) coeffs[i] = p.Coefficients[i];
                else coeffs[i] = p.Coefficients[i] + q.Coefficients[i];
            }

            return new Polynomial(coeffs);
        }

        public static Polynomial operator -(Polynomial p, Polynomial q)
        {
            return p + (-q);
        }

        public static Polynomial operator -(Polynomial p)
        {
            double[] coeffs = new double[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = -p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        public static Polynomial operator *(double d, Polynomial p)
        {
            double[] coeffs = new double[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        public static Polynomial operator *(Polynomial p, double d)
        {
            double[] coeffs = new double[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        public static Polynomial operator /(Polynomial p, double d)
        {
            double[] coeffs = new double[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = p.Coefficients[i] / d;

            return new Polynomial(coeffs);
        }

        public static Polynomial operator *(Polynomial p, Polynomial q)
        {
            int degree = p.Degree + q.Degree;

            Polynomial r = new Polynomial();

            for (int i = 0; i <= p.Degree; i++)
                for (int j = 0; j <= q.Degree; j++)
                    r += (p.Coefficients[i] * q.Coefficients[j]) * Monomial(i + j);

            return r;
        }

        public static Polynomial operator ^(Polynomial p, uint k)
        {
            if (k == 0)
                return Monomial(0);
            else if (k == 1)
                return p;
            else
                return p * (p ^ (k - 1));
        }

        public override string ToString()
        {
            if (this.IsZero()) return "0";
            else
            {
                string s = "";

                for (int i = 0; i < Degree + 1; i++)
                {
                    if (Coefficients[i] != 0)
                    {
                        if (Coefficients[i] != 1)
                        {
                            s += Coefficients[i].ToString();
                        }
                        else if (/*Coefficients[i] == Complex.One && */i == 0)
                            s += 1;

                        if (i == 1)
                            s += "x";
                        else if (i > 1)
                            s += "x^" + i.ToString();
                    }

                    if (i < Degree && Coefficients[i + 1] != 0 && s.Length > 0)
                        s += " + ";
                }

                return s;
            }
        }

        public string ToString(string format)
        {
            if (this.IsZero()) return "0";
            else
            {
                string s = "";

                for (int i = 0; i < Degree + 1; i++)
                {
                    if (Coefficients[i] != 0)
                    {
                        if (Coefficients[i] != 1)
                        {
                            s += Coefficients[i].ToString(format);

                        }
                        else if (/*Coefficients[i] == Complex.One && */i == 0)
                            s += 1;

                        if (i == 1)
                            s += "x";
                        else if (i > 1)
                            s += "x^" + i.ToString(format);
                    }

                    if (i < Degree && Coefficients[i + 1] != 0 && s.Length > 0)
                        s += " + ";
                }

                return s;
            }
        }

        public override bool Equals(object obj)
        {
            return (this.ToString() == ((Polynomial)obj).ToString());
        }

        #endregion

        #region structs

        /// <summary>
        /// Factorized polynomial p := set of polynomials p_1,...,p_k and their corresponding
        /// powers n_1,...,n_k, such that p = (p_1)^(n_1)*...*(p_k)^(n_k).
        /// </summary>
        public struct FactorizedPolynomial
        {
            /// <summary>
            /// Set of factors the polynomial  consists of.
            /// </summary>
            public Polynomial[] Factor;

            /// <summary>
            /// Set of powers, where Factor[i] is lifted
            /// to Power[i].
            /// </summary>
            public int[] Power;
        }

        #endregion
    }

}
