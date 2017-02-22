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

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool idEquality = (this.GetId() == newRestaurant.GetId());
                bool nameEquality = (this.GetName() == newRestaurant.GetName());
                bool priceEquality = this.GetPrice() == newRestaurant.GetPrice();
                bool vibeEquality = (this.GetVibe() == newRestaurant.GetVibe());
                bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
                return (idEquality && nameEquality && priceEquality && vibeEquality && cuisineIdEquality);
            }
        }

        public static Restaurant Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant WHERE id = @RestaurantId;", conn);
            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = id.ToString();
            cmd.Parameters.Add(restaurantIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundRestaurantId = 0;
            string foundRestaurantName = null;
            string foundRestaurantPrice = null;
            string foundRestaurantVibe = null;
            int foundRestaurantCuisineId = 0;

            while(rdr.Read())
            {
                foundRestaurantId = rdr.GetInt32(0);
                foundRestaurantName = rdr.GetString(1);
                foundRestaurantPrice = rdr.GetString(2);
                foundRestaurantVibe = rdr.GetString(3);
                foundRestaurantCuisineId = rdr.GetInt32(4);
            }
            Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantPrice, foundRestaurantVibe, foundRestaurantCuisineId, foundRestaurantId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundRestaurant;
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> AllRestaurants = new List<Restaurant>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurant;", conn);
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurant (name, price, vibe, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantPrice, @RestaurantVibe, @RestaurantCuisineId);", conn);

            SqlParameter nameParameter = new SqlParameter("@RestaurantName", this.GetName());
            // nameParameter.ParameterName = "@RestaurantName";
            // nameParameter.Value = this.GetName();

            SqlParameter priceParameter = new SqlParameter();
            priceParameter.ParameterName = "@RestaurantPrice";
            priceParameter.Value = this.GetPrice();

            SqlParameter vibeParameter = new SqlParameter();
            vibeParameter.ParameterName = "@RestaurantVibe";
            vibeParameter.Value = this.GetVibe();

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
            cuisineIdParameter.Value = this.GetCuisineId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(priceParameter);
            cmd.Parameters.Add(vibeParameter);
            cmd.Parameters.Add(cuisineIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public void UpdateProperties(string newName, string newPrice, string newVibe)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE restaurant SET name = @NewName, price = @NewPrice, vibe = @NewVibe OUTPUT INSERTED.* WHERE id = @RestaurantId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter newPriceParameter = new SqlParameter();
            newPriceParameter.ParameterName = "@NewPrice";
            newPriceParameter.Value = newPrice;
            cmd.Parameters.Add(newPriceParameter);

            SqlParameter newVibeParameter = new SqlParameter();
            newVibeParameter.ParameterName = "@NewVibe";
            newVibeParameter.Value = newVibe;
            cmd.Parameters.Add(newVibeParameter);

            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = this.GetId();
            cmd.Parameters.Add(restaurantIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(1);
                this._price = rdr.GetString(2);
                this._vibe = rdr.GetString(3);
            }

            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM restaurant WHERE id = @RestaurantId;", conn);

            SqlParameter restaurantIdParameter = new SqlParameter();
            restaurantIdParameter.ParameterName = "@RestaurantId";
            restaurantIdParameter.Value = this.GetId();

            cmd.Parameters.Add(restaurantIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
            conn.Close();
            }
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
