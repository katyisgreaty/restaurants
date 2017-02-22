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

        [Fact]
        public void GetId_GetsIdForRestaurant_true()
        {
            //Arrange
            Restaurant testRestaurant = new Restaurant("Bob's", "$$$", "grim", 1);
            testRestaurant.Save();

            //Act
            Restaurant savedRestaurant = Restaurant.GetAll()[0];

            int result = savedRestaurant.GetId();
            int testId = testRestaurant.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void UpdateProperties_UpdatePropertiesInDatabase_true()
        {
            //Arrange
            string name = "Darlene's";
            string price = "$$$$";
            string vibe = "country chic";

            Restaurant testRestaurant = new Restaurant(name, price, vibe, 1);
            testRestaurant.Save();
            string newName = "Edna's";
            string newPrice = "$$";
            string newVibe = vibe;

            //Act
            testRestaurant.UpdateProperties(newName, newPrice, newVibe);
            Restaurant result = Restaurant.GetAll()[0];

            //Assert
            Assert.Equal(testRestaurant, result);
            Assert.Equal(newName, result.GetName());
        }

        [Fact]
        public void Find_FindsRestaurantInDatabase_true()
        {
           //Arrange
           Restaurant testRestaurant = new Restaurant("Bob's", "$$$", "grim", 1);
           testRestaurant.Save();

           //Act
           Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

           //Assert
           Assert.Equal(testRestaurant, foundRestaurant);
        }

        [Fact]
        public void DeleteRestaurant_DeleteRestaurantInDatabase_true()
        {
            //Arrange
            string name1 = "Bill's";
            string price1 = "$$$";
            string vibe1 = "hauntingly flavorful";
            Restaurant testRestaurant1 = new Restaurant(name1, price1, vibe1, 1);
            testRestaurant1.Save();

            string name2 = "Bob's";
            string price2 = "$$";
            string vibe2 = "rustic";
            Restaurant testRestaurant2 = new Restaurant(name2, price2, vibe2, 2);
            testRestaurant2.Save();

            //Act
            testRestaurant1.Delete();
            List<Restaurant> resultRestaurants = Restaurant.GetAll();
            List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

            //Assert
            Assert.Equal(testRestaurantList, resultRestaurants);

        }




    }
}
