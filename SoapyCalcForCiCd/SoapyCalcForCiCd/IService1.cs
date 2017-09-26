using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoapyCalcForCiCd
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void SetTal(int tal);

        [OperationContract]
        int GetTal();
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Subtract(double a, double b);

        [OperationContract]
        double Divide(double a, double b);

        [OperationContract]
        void All(double a, double b, out double add, out double substact, out double divide);
        [OperationContract]
        double Sum(double[] numbers);

        [OperationContract]
        int MayThrowExeption(int number);
    }



}
