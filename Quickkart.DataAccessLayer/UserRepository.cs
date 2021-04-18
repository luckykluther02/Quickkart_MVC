using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.DataAccessLayer
{
    public class UserRepository
    {
        SqlConnection conQuickkart;
        SqlCommand cmdQuickkart;
        //SqlDataAdapter daquickkart;
        public UserRepository()
        {
            conQuickkart = new SqlConnection(ConfigurationManager.ConnectionStrings["QuickkartConnectionString"].ConnectionString);
        }


        public int RegisterUser(string emailId, string password, string firstName, string lastName, string gender, string mobile, DateTime dateOfBirth, string address)
        {
            int res = 0;
            

            cmdQuickkart = new SqlCommand("usp_RegisterUser", conQuickkart);
            cmdQuickkart.CommandType = CommandType.StoredProcedure;
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            cmdQuickkart.Parameters.AddWithValue("@password", password);
            cmdQuickkart.Parameters.AddWithValue("@firstname", firstName);
            cmdQuickkart.Parameters.AddWithValue("@lastname", String.IsNullOrEmpty(lastName) ? DBNull.Value : (Object)lastName);
            cmdQuickkart.Parameters.AddWithValue("@gender", gender);
            cmdQuickkart.Parameters.AddWithValue("@mobile", mobile);
            cmdQuickkart.Parameters.AddWithValue("@dateofbirth", dateOfBirth);
            cmdQuickkart.Parameters.AddWithValue("@address", address);

            SqlParameter prmRetValue = new SqlParameter("@returnValue", System.Data.SqlDbType.Int);
            prmRetValue.Direction = System.Data.ParameterDirection.ReturnValue;
            cmdQuickkart.Parameters.Add(prmRetValue);

            try
            {
                conQuickkart.Open();
                cmdQuickkart.ExecuteNonQuery();
                res = Convert.ToInt32(prmRetValue.Value);
            }
            catch(Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            return res;
        }
        public int UserLogin(string emailId, string password)
        {
            int res = 0;
            cmdQuickkart = new SqlCommand("Select [dbo].ufn_Login(@email, @pwd)", conQuickkart);
            cmdQuickkart.Parameters.AddWithValue("@pwd", password);
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            try
            {
                conQuickkart.Open();
                res = Convert.ToInt32(cmdQuickkart.ExecuteScalar());
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            return res;
        }

        public SqlDataReader ViewCart(string emailId)
        {
            SqlDataReader drCart = null;
            SqlCommand cmdQuickkart = new SqlCommand("Select * from ufn_ViewCart(@email)", conQuickkart);
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            try
            {
                conQuickkart.Open();
                drCart = cmdQuickkart.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception obj)
            {
                drCart = null;
            }
            return drCart;
        }

        public SqlDataReader ViewProducts()
        {
            //DataSet dsProduct = new DataSet();
            SqlDataReader drProduct = null;
            cmdQuickkart = new SqlCommand("Select * from ufn_ViewProducts()", conQuickkart);
            //daquickkart = new SqlDataAdapter(cmdQuickkart);
            try
            {
                conQuickkart.Open();
                drProduct = cmdQuickkart.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                //daquickkart.Fill(dsProduct);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
                drProduct = null;
            }
            return drProduct;
        }

        public int AddToCart(string emailId, string productId, int qtyPurchased)
        {
            int res = 0;
            cmdQuickkart = new SqlCommand("usp_AddToCart", conQuickkart);
            cmdQuickkart.CommandType = CommandType.StoredProcedure;
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            cmdQuickkart.Parameters.AddWithValue("@prodId", productId);
            cmdQuickkart.Parameters.AddWithValue("@qtyPurchased", qtyPurchased);

            SqlParameter prmRet = new SqlParameter("@RetVal", System.Data.SqlDbType.Int);
            prmRet.Direction = System.Data.ParameterDirection.ReturnValue;
            cmdQuickkart.Parameters.Add(prmRet);

            try
            {
                conQuickkart.Open();
                cmdQuickkart.ExecuteNonQuery();
                res = Convert.ToInt32(prmRet.Value);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            return res;
        }

        public int UpdateCart(string emailId, string productId, int qtyPurchased)
        {
            int res = 0;
            cmdQuickkart = new SqlCommand("usp_UpdateCart", conQuickkart);
            cmdQuickkart.CommandType = CommandType.StoredProcedure;
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            cmdQuickkart.Parameters.AddWithValue("@prodId", productId);
            cmdQuickkart.Parameters.AddWithValue("@qtyPurchased", qtyPurchased);

            SqlParameter prmRet = new SqlParameter("@RetVal", System.Data.SqlDbType.Int);
            prmRet.Direction = System.Data.ParameterDirection.ReturnValue;
            cmdQuickkart.Parameters.Add(prmRet);

            try
            {
                conQuickkart.Open();
                cmdQuickkart.ExecuteNonQuery();
                res = Convert.ToInt32(prmRet.Value);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            return res;
        }

        public int DeleteFromCart(string emailId, String productId)
        {
            int res = 0;
            cmdQuickkart = new SqlCommand("usp_RemoveFromCart", conQuickkart);
            cmdQuickkart.CommandType = CommandType.StoredProcedure;
            cmdQuickkart.Parameters.AddWithValue("@email", emailId);
            cmdQuickkart.Parameters.AddWithValue("@prodId", productId);

            SqlParameter prmRet = new SqlParameter("@ReturnVal", System.Data.SqlDbType.Int);
            prmRet.Direction = System.Data.ParameterDirection.ReturnValue;
            cmdQuickkart.Parameters.Add(prmRet);

            try
            {
                conQuickkart.Open();
                cmdQuickkart.ExecuteNonQuery();
                res = Convert.ToInt32(prmRet.Value);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            return res;
        }

        public int ProductOrder(string email, out string output)
        {
            int res = 0;
            string outParam = null;

            cmdQuickkart = new SqlCommand("usp_ProductOrder", conQuickkart);
            cmdQuickkart.CommandType = CommandType.StoredProcedure;
            cmdQuickkart.Parameters.AddWithValue("@email", email);

            SqlParameter prmRet = new SqlParameter("@ReturnVal", System.Data.SqlDbType.Int);
            prmRet.Direction = System.Data.ParameterDirection.ReturnValue;
            cmdQuickkart.Parameters.Add(prmRet);

            SqlParameter prmOut = new SqlParameter("@orderId", System.Data.SqlDbType.VarChar, 10);
            prmOut.Direction = System.Data.ParameterDirection.Output;
            cmdQuickkart.Parameters.Add(prmOut);

            try
            {
                conQuickkart.Open();
                cmdQuickkart.ExecuteNonQuery();
                res = Convert.ToInt32(prmRet.Value);
                outParam = prmOut.Value.ToString();
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
            }
            finally
            {
                conQuickkart.Close();
            }
            output = outParam;
            return res;
        }

        public SqlDataReader ViewOrders(string email)
        {
            cmdQuickkart = new SqlCommand("Select * from ufn_ViewOrders(@emailId)", conQuickkart);
            cmdQuickkart.Parameters.AddWithValue("@emailId", email);
            SqlDataReader drOrders = null;
            try
            {
                conQuickkart.Open();
                drOrders = cmdQuickkart.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception obj)
            {
                Console.WriteLine(obj.Message);
                drOrders = null;
            }
            return drOrders;
        }
    }
}
