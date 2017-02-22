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
        public void Save_TestIfCuisineSaved_true()
        {
            Cuisine testCuisine = new Cuisine("Asian");
            testCuisine.Save();

            List<Cuisine> result = Cuisine.GetAll();
            List<Cuisine> testList = new List<Cuisine>{testCuisine};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void GetAll_ReturnListOfAllCuisines_true()
        {
            Cuisine firstCuisine = new Cuisine("Mexican");
            Cuisine secondCuisine = new Cuisine("Asian");
            firstCuisine.Save();
            secondCuisine.Save();

            List<Cuisine> testCuisineList = new List<Cuisine> {firstCuisine, secondCuisine};
            List<Cuisine> resultCuisineList = Cuisine.GetAll();
            Assert.Equal(testCuisineList, resultCuisineList);
        }

        [Fact]
        public void GetId_GetsIdForCuisine_true()
        {
            //Arrange
            Cuisine testCuisine = new Cuisine("Asian");
            testCuisine.Save();

            //Act
            Cuisine savedCuisine = Cuisine.GetAll()[0];

            int result = savedCuisine.GetId();
            int testId = testCuisine.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Find_FindsCuisineInDatabase_true()
        {
           //Arrange
           Cuisine testCuisine = new Cuisine("Italian");
           testCuisine.Save();

           //Act
           Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

           //Assert
           Assert.Equal(testCuisine, foundCuisine);
        }

        [Fact]
        public void Update_UpdateNameInDatabase_true()
        {
            //Arrange
            string name = "Italian";
            Cuisine testCuisine = new Cuisine(name);
            testCuisine.Save();
            string newName = "Ethiopian";

            //Act
            testCuisine.Update(newName);

            string result = testCuisine.GetName();

            //Assert
            Assert.Equal(newName, result);
        }



        public void Dispose()
        {
            // Restaurant.DeleteAll();
            Cuisine.DeleteAll();
        }
    }
}
