using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantList
{
    public class Cuisine
    {
        private int _id;
        private string _name;

        public Cuisine(string name, int Id = 0)
        {
            _id = Id;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisines = new List<Cuisine>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int cuisineId = rdr.GetInt32(0);
                string cuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
                allCuisines.Add(newCuisine);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allCuisines;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cuisine (name) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);

            SqlParameter nameParameter = new SqlParameter("@CuisineName", this.GetName());
            // nameParameter.ParameterName = "@CuisineName";
            // nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine) otherCuisine;
                bool idEquality = this.GetId() == newCuisine.GetId();
                bool nameEquality = this.GetName() == newCuisine.GetName();
                return (idEquality && nameEquality);
            }
        }

        public static Cuisine Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine WHERE id = @CuisineId;", conn);
            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = id.ToString();
            cmd.Parameters.Add(cuisineIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCuisineId = 0;
            string foundCuisineName = null;

            while(rdr.Read())
            {
                foundCuisineId = rdr.GetInt32(0);
                foundCuisineName = rdr.GetString(1);
            }
            Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundCuisine;
        }

        public static Cuisine FindByName(string name)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cuisine WHERE name = @CuisineName;", conn);
            SqlParameter cuisineNameParameter = new SqlParameter();
            cuisineNameParameter.ParameterName = "@CuisineName";
            cuisineNameParameter.Value = name;
            cmd.Parameters.Add(cuisineNameParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCuisineId = 0;
            string foundCuisineName = null;

            while(rdr.Read())
            {
                foundCuisineId = rdr.GetInt32(0);
                foundCuisineName = rdr.GetString(1);

            }
            Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundCuisine;
        }

        public void UpdateName(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE cuisine SET name = @NewName OUTPUT INSERTED.name WHERE id = @CuisineId;", conn);

            SqlParameter newNameParameter = new SqlParameter();
            newNameParameter.ParameterName = "@NewName";
            newNameParameter.Value = newName;
            cmd.Parameters.Add(newNameParameter);

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = this.GetId();
            cmd.Parameters.Add(cuisineIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
            this._name = rdr.GetString(0);
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

            SqlCommand cmd = new SqlCommand("DELETE FROM cuisine WHERE id = @CuisineId; DELETE FROM restaurant WHERE cuisine_id = @CuisineId;", conn);

            SqlParameter cuisineIdParameter = new SqlParameter();
            cuisineIdParameter.ParameterName = "@CuisineId";
            cuisineIdParameter.Value = this.GetId();

            cmd.Parameters.Add(cuisineIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
            conn.Close();
            }
        }
    }
}
