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

        [Fact]
        public void Save_TestIfRestaurantSaved_true()
        {
            Restaurant testRestaurant = new Restaurant("Bill's", "$$", "Romantic", 1);
            testRestaurant.Save();

            List<Restaurant> allRestaurantList = new List<Restaurant>{testRestaurant};
            //FOREACH LOOP TO CONSOLE wRITElINE THE RESTAURANTS IN LIST
            // foreach (Restaurant restaurant in allRestaurantList)
            // {
            //     Console.WriteLine(restaurant.GetName());
            // }

            List<Restaurant> result = Restaurant.GetAll();
            List<Restaurant> testList = new List<Restaurant>{testRestaurant};

            // foreach (Restaurant restaurant in result)
            // {
            //     Console.WriteLine(restaurant.GetName());
            // }

            Assert.Equal(testList, result);
        }

        [Fact]
        public void GetAll_ReturnListOfAllRestaurants_true()
        {
            Restaurant firstRestaurant = new Restaurant("Bob's", "$$$", "grim", 1);
            Restaurant secondRestaurant = new Restaurant("Bill's", "$$", "happy", 2);
            firstRestaurant.Save();
            secondRestaurant.Save();

            List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
            List<Restaurant> resultRestaurantList = Restaurant.GetAll();
            Assert.Equal(testRestaurantList, resultRestaurantList);
        }
    }
}
