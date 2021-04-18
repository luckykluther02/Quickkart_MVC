using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection conQuickkart = new SqlConnection(ConfigurationManager.ConnectionStrings["QuickkartConnectionString"].ConnectionString);
            //try
            //{
            //    conQuickkart.Open();
            //    bool status = Convert.ToBoolean(conQuickkart.State);
            //    if (status)

            //        Console.WriteLine("Success");

            //    else

            //        Console.WriteLine("Fail");

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    conQuickkart.Close();
            //}
            //UserRepository userObj = new UserRepository();
            //SqlDataReader reader = userObj.ViewCart("John078@gmail.com");
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine(reader["ProductName"].ToString()+" "+ reader["Price"]+" "+reader["quantityPurchased"]+ " " +reader["TotalAmount"]+ " " +reader["ProductId"]+ " " +reader["QuantityAvailable"]);
            //    }
            //}
            //reader.Close();
        }
    }
}
