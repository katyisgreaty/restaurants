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
           Cuisine firstCuisine = new Cuisine("Mexican");
           Cuisine secondCuisine = new Cuisine("Mexican");

           //Assert
           Assert.Equal(firstCuisine, secondCuisine);
        }

        [Fact]
        public void Save_TestIfTypeSaved_true()
        {
            Cuisine testCuisine = new Cuisine("Asian");
            testCuisine.Save();

            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{testCuisine};

            Assert.Equal(testList, result);
        }










        public void Dispose()
        {
            // Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
