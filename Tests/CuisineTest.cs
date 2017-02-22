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

        [Fact]
        public void Equals_ReturnsTrueForSameName_true()
        {
            //Arrange, Act
           Cuisine firstCuisine = new Cuisine("Dog");
           Cuisine secondCuisine = new Cuisine("Dog");

           //Assert
           Assert.Equal(firstCuisine, secondCuisine);
        }












        public void Dispose()
        {
            // Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
