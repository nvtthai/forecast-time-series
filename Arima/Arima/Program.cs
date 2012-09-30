using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32;
using RDotNet;

namespace Arima
{
    class Program
    {
        static void Main(string[] args)
        {
            //require R 2.15, package forecast on R
            var envPath = Environment.GetEnvironmentVariable("PATH");
            var rBinPath = GetRPath(); //C:\Program Files\R\R-2.15.1\bin\i386
            Environment.SetEnvironmentVariable("PATH", envPath + Path.PathSeparator + rBinPath);
            REngine engine = REngine.CreateInstance("RDotNet");
            engine.Initialize();

            string currentPath = Directory.GetCurrentDirectory();
            string dataPath = currentPath + @"\data\paper.dat";
            string readDataCommand = string.Format("predata <- read.table(\"{0}\", header=FALSE)", dataPath).Replace('\\', '/');


            engine.Evaluate("library(forecast)");
            engine.Evaluate(readDataCommand);
            engine.Evaluate("data <- predata[,1]");
            var model = engine.Evaluate("fit <- auto.arima(data)").AsList();
            var coef = model["coef"].AsList();

            int lengthData = engine.Evaluate("data").AsNumeric().Length;
            double[] dataSeries = new double[lengthData];
            double[] errorSeries = new double[lengthData];

            engine.Evaluate("data").AsNumeric().CopyTo(dataSeries, lengthData);
            model["residuals"].AsNumeric().CopyTo(errorSeries, lengthData);
            //residuals

            int arOrder = model["arma"].AsInteger().ElementAt(0);
            int maOrder = model["arma"].AsInteger().ElementAt(1);
            int arSeasonOrder = model["arma"].AsInteger().ElementAt(2);
            int maSeasonOrder = model["arma"].AsInteger().ElementAt(3);
            int seasonOrder = model["arma"].AsInteger().ElementAt(4);
            int diffOrder = model["arma"].AsInteger().ElementAt(5);
            int diffSeasonOrder = model["arma"].AsInteger().ElementAt(6);

            double[] arCoef = new double[arOrder];
            double[] maCoef = new double[maOrder];
            double[] arSeasonCoef = new double[arSeasonOrder];
            double[] maSeasonCoef = new double[maSeasonOrder];
            double intercept = 0;

            int n = model["coef"].AsNumeric().Length;
            int start = 0;
            model["coef"].AsNumeric().CopyTo(arCoef, arOrder, start, 0);
            start += arOrder;
            model["coef"].AsNumeric().CopyTo(maCoef, maOrder, start, 0);
            start += maOrder;
            model["coef"].AsNumeric().CopyTo(arSeasonCoef, arSeasonOrder, start, 0);
            start += arSeasonOrder;
            model["coef"].AsNumeric().CopyTo(maSeasonCoef, maSeasonOrder, start, 0);
            start += maSeasonOrder;
            if (n > start)
            {
                intercept = model["coef"].AsNumeric().ElementAt(start);
            }

            ArimaModel arimaModel = new ArimaModel(arCoef, maCoef, arSeasonCoef, maSeasonCoef, intercept, (uint)seasonOrder, (uint)diffOrder, (uint)diffSeasonOrder);
            Polynomial arModel = arimaModel.ComputeARModel();
            Polynomial maModel = arimaModel.ComputeMAModel();
            double interceptModel = arimaModel.ComputeIntercept();

            double test = arimaModel.ComputeValue(dataSeries, errorSeries, dataSeries.Length);
            
            Console.WriteLine("Forecast");
            Console.WriteLine(test);
            Console.WriteLine("Model");
            Console.WriteLine(interceptModel);
            Console.WriteLine("Ar");
            Console.WriteLine(arModel.ToString());
            Console.WriteLine("Ma");
            Console.WriteLine(maModel.ToString());
            Console.ReadLine();
        }

        static string GetRPath()
        {
            RegistryKey rCore = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\R-core");
            if (rCore == null)
            {
                return string.Empty;
            }
            bool is64Bit = IntPtr.Size == 8;
            RegistryKey r = rCore.OpenSubKey(is64Bit ? "R64" : "R");
            if (r == null)
            {
                return string.Empty;
            }
            Version currentVersion = new Version((string)r.GetValue("Current Version"));
            return (string)r.GetValue("InstallPath") + @"\bin\i386";
        }
    }
}
