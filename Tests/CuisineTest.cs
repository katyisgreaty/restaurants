using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantList
{
    public class CuisineTest : IDisposable
    {
        public CuisineTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void GetAll_CuisinesEmptyAtFirst_true()
        {
            int result = Cuisine.GetAll().Count;

            Assert.Equal(0, result);
        }

        public void Dispose()
        {
            // Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
