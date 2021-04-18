using Quickkart.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.Business
{
    public class UserBL
    {
        private UserRepository userObj;
        public UserRepository UserObj { get { return userObj; } }

        public UserBL()
        {
            userObj = new UserRepository();
        }

        public UserBL(UserRepository userRepository)
        {
            userObj = userRepository;
        }

        public int RegisterUser(Common.Models.User user)
        {
            DateTime dateOfBirth = Convert.ToDateTime(user.DateOfBirth);
            int result = 0;
            try
            {
                result = UserObj.RegisterUser(user.EmailId, user.Password, user.FirstName, user.LastName, user.Gender, user.Mobile, dateOfBirth, user.Address);
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public int UserLogin(string email, string password)
        {
            int result = 0;
            try
            {
                result = userObj.UserLogin(email, password);
            }
            catch (Exception ob)
            {
                //Console.WriteLine(ob.Message);
                result = 0;
            }
            return result;
        }

        public List<Common.Models.Cart> ViewCart(string email)
        {
            List<Common.Models.Cart> lstCart = new List<Common.Models.Cart>();
            try
            {
                SqlDataReader reader = userObj.ViewCart(email);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Common.Models.Cart cart = new Common.Models.Cart();
                        cart.ProductName = reader["ProductName"].ToString();
                        cart.Price = Convert.ToSingle(reader["Price"]);
                        cart.QuantityPurchased = Convert.ToInt32(reader["quantityPurchased"]);
                        cart.TotalAmount = Convert.ToSingle(reader["TotalAmount"]);
                        cart.ProductId = reader["ProductId"].ToString();
                        cart.QuantityAvailable = Convert.ToInt32(reader["QuantityAvailable"]);
                        lstCart.Add(cart);
                    }
                }
                reader.Close();
            }
            catch
            {
                lstCart = null;
            }
            return lstCart;
        }

        public List<Common.Models.Product> ViewProduct()
        {
            List<Common.Models.Product> lstProduct = new List<Common.Models.Product>();
            try
            {
                SqlDataReader reader = userObj.ViewProducts();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Common.Models.Product product = new Common.Models.Product();
                        product.ProductId = reader["ProductId"].ToString();
                        product.ProductName = reader["ProductName"].ToString();
                        product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                        product.Price = Convert.ToSingle(reader["Price"]);
                        product.QuantityAvailable = Convert.ToInt32(reader["QuantityAvailable"]);
                        lstProduct.Add(product);
                    }
                }
                reader.Close();
            }
            catch
            {
                lstProduct = null;
            }
            return lstProduct;
        }

        public int AddToCart(Common.Models.Cart cart)
        {
            int result = 0;
            try
            {
                result = userObj.AddToCart(cart.EmailId, cart.ProductId, cart.QuantityPurchased);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public int UpdateCart(Common.Models.Cart cart)
        {
            int result = 0;
            try
            {
                result = userObj.UpdateCart(cart.EmailId, cart.ProductId, cart.QuantityPurchased);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public int DeleteCart(string email, string prodId)
        {
            int result = 0;
            try
            {
                result = userObj.DeleteFromCart(email, prodId);
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public string Order(string emailId)
        {
            int result = 0;
            string orderId = null;
            try
            {
                result = userObj.ProductOrder(emailId, out orderId);
            }
            catch
            {
                result = 0;
            }
            return orderId;
        }

        public List<Common.Models.Cart> ViewOrders(string emailId)
        {
            List<Common.Models.Cart> lstOrder = new List<Common.Models.Cart>();
            try
            {
                SqlDataReader reader = userObj.ViewOrders(emailId);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Common.Models.Cart cart = new Common.Models.Cart();
                        cart.ProductName = reader["ProductName"].ToString();
                        cart.QuantityPurchased = Convert.ToInt32(reader["QuantityPurchased"]);
                        cart.TotalAmount = Convert.ToSingle(reader["TotalAmount"]);
                        cart.OrderId = reader["OrderID"].ToString();
                        cart.OrderDate = reader["OrderDate"].ToString();
                        lstOrder.Add(cart);
                    }
                }
                reader.Close();
            }
            catch
            {
                lstOrder = null;
            }
            return lstOrder;
        }

    }
}
