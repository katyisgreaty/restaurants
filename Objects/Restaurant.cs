using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantList
{
    public class Restaurant
    {
        private int _id;
        private string _name;
        private string _price;
        private string _vibe;
        private int _cuisineId;

        public Restaurant(string Name, string Price, string Vibe, int CuisineId, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _price = Price;
            _vibe = Vibe;
            _cuisineId = CuisineId;
        }

        public int GetId()
       {
           return _id;
       }

       public string GetName()
       {
           return _name;
       }

       public string GetPrice()
       {
           return _price;
       }

       public string GetVibe()
       {
           return _vibe;
       }

       public int GetCuisineId()
       {
           return _cuisineId;
       }

       public static List<Restaurant> GetAll()
       {
           List<Restaurant> AllRestaurants = new List<Restaurant>{};

           SqlConnection conn = DB.Connection();
           conn.Open();

           SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);
           SqlDataReader rdr = cmd.ExecuteReader();

           while(rdr.Read())
           {
               int restaurantId = rdr.GetInt32(0);
               string restaurantName = rdr.GetString(1);
               string restaurantPrice = rdr.GetString(2);
               string restaurantVibe = rdr.GetString(3);
               int restaurantCuisineId = rdr.GetInt32(4);
               Restaurant newRestaurant = new Restaurant(restaurantName, restaurantPrice, restaurantVibe, restaurantCuisineId, restaurantId);
               AllRestaurants.Add(newRestaurant);
           }
           if (rdr != null)
           {
               rdr.Close();
           }
           if (conn != null)
           {
               conn.Close();
           }
           return AllRestaurants;
       }

       public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM restaurant;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
