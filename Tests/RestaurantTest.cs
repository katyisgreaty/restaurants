using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantList
{
    public class RestaurantListTest : IDisposable
    {
        public RestaurantListTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void GetAll_RestaurantsEmptyAtFirst_true()
        {
            int result = Restaurant.GetAll().Count;

            Assert.Equal(0, result);
        }



        public void Dispose()
        {
            Restaurant.DeleteAll();
        }

        [Fact]
        public void Equals_ReturnsTrueForSameName_true()
        {
            //Arrange, Act
           Restaurant firstRestaurant = new Restaurant("Bob's","$", "fun", 1);
           Restaurant secondRestaurant = new Restaurant("Bob's","$", "fun", 1);

           //Assert
           Assert.Equal(firstRestaurant, secondRestaurant);
        }
    }
}
