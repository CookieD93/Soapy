using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        private const string ConnectionString =
                "Server = tcp:soapydbserver.database.windows.net,1433;Initial Catalog = SoapyDatabase; Persist Security Info=False;User ID = cookied; Password=Daniel1993; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;"
            ;
        public void SetTal(int tal)
        {
            TestTal = tal;
        }
        private int AddResultToDB(double value)
        {
            const string insertResult = "insert into CalcResults (Result) values (@value)";
            using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
            {
                dataConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertResult, dataConnection))
                {
                    insertCommand.Parameters.AddWithValue("@value", value);
                    return insertCommand.ExecuteNonQuery();
                }
            }
        }

        public double GetTal(int id)
        {
            string selectstring = "select* from CalcResults where id = @id";
            using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
            {
                dataConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectstring, dataConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return 0.0;
                        }
                        reader.Read();
                        double hentetTal = reader.GetDouble(1);
                        return hentetTal;
                    }
                }
            }
        }

        public double Add(double a, double b)
        {
            double result = a + b;
            AddResultToDB(result);
            return result;

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

        public void UploadReadingsToDb(int[] measurements, DateTime tid,int count)
        {
            //if (CheckDb())
            //{
            //    Delete500FromDB();
            //}
            const string insertResult = "insert into PiMeasurements (Time,Lys,Temp,Potent,Count) values (@Time,@Lys,@Temp,@Potent,@Count)";
                using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
                {
                    dataConnection.Open();

                    using (SqlCommand insertCommand = new SqlCommand(insertResult, dataConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@Time", tid);
                        insertCommand.Parameters.AddWithValue("@Lys", measurements[0]);
                        insertCommand.Parameters.AddWithValue("@Temp", measurements[1]);
                        insertCommand.Parameters.AddWithValue("@Potent", measurements[2]);
                        insertCommand.Parameters.AddWithValue("@Count", count);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            
        }

        private bool CheckDb()
        {
            const string checkDBCounter = "select Count(Id) from PiMeasurements";

            using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
            {
                dataConnection.Open();
                using (SqlCommand CheckDBCountCommand = new SqlCommand(checkDBCounter, dataConnection))
                {
                    using (SqlDataReader reader = CheckDBCountCommand.ExecuteReader())
                    {
                        if (reader.GetInt32(0) > 20)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Delete500FromDB()
        {
            const string deleter = "Delete * from PiMeasurements";
            using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
            {
                dataConnection.Open();
                using (SqlCommand DeleteEverything = new SqlCommand(deleter,dataConnection))
                {
                    DeleteEverything.ExecuteNonQuery();
                }
            }
        }
        public string GetLatestReading()
        {
            const string selectResult = "select TOP 1 * from PiMeasurements order by Time desc";
            using (SqlConnection dataConnection = new SqlConnection(ConnectionString))
            {
                dataConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectResult, dataConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        string dataToGet = "";
                        while (reader.Read())
                        {
                             dataToGet = $"{reader.GetDateTime(1)}#{reader.GetInt32(2)}#{reader.GetInt32(3)}#{reader.GetInt32(4)}";

                        }
                        return dataToGet;
                    }
                }
            }
        }
    }
}
