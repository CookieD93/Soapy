using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoapyCalcForCiCd
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static int TestTal { get; set; }

        public void SetTal(int tal)
        {
            TestTal = tal;
        }

        public int GetTal()
        {
            return TestTal;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }
        
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("dummy");
            }
            return a / b;
        }

        public void All(double a, double b, out double add, out double subtract, out double divide)
        {
            subtract = a - b;
            divide = a / b;
            add = a + b;
        }

        public double Sum(double[] numbers)
        {
            return numbers.Sum();
        }

        public int MayThrowExeption(int number)
        {
            if (number == 0)
            {
                throw new Exception();
            }
            return number;
        }
    }
}
