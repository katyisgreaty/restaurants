using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace RestaurantList
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Cuisine> AllCuisines = Cuisine.GetAll();
                return View["index.cshtml", AllCuisines];
            };

            Get["/restaurants"] = _ => {
                List<Restaurant> AllRestaurants = Restaurant.GetAll();
                return View["restaurants.cshtml", AllRestaurants];
            };

            Get["/cuisines"] = _ => {
                List<Cuisine> AllCuisines = Cuisine.GetAll();
                return View["cuisines.cshtml", AllCuisines];
            };

            Get["/cuisines/new"] = _ => {
                return View["cuisines_form.cshtml"];
            };
            Post["/cuisines/new"] = _ => {
                Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
                newCuisine.Save();
                return View["success.cshtml"];
            };

            Get["/restaurants/new"] = _ => {
                List<Cuisine> AllCuisines = Cuisine.GetAll();
                return View["restaurants_form.cshtml", AllCuisines];
            };

            Post["/restaurants/new"] = _ => {
                Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-price"], Request.Form["restaurant-vibe"], Request.Form["cuisine-id"]);
                newRestaurant.Save();
                return View["success.cshtml"];
            };

            Post["/restaurants/delete"] = _ => {
                Restaurant.DeleteAll();
                return View["cleared.cshtml"];
            };

            Get["/cuisines/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                var SelectedCuisine = Cuisine.Find(parameters.id);
                var CuisineRestaurants = SelectedCuisine.GetRestaurants();
                model.Add("cuisine", SelectedCuisine);
                model.Add("restaurants", CuisineRestaurants);
                return View["cuisine.cshtml", model];
            };

            Get["/restaurant/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                return View["restaurant.cshtml", SelectedRestaurant];
            };

            Get["restaurant/edit/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                return View["update_restaurant.cshtml", SelectedRestaurant];
            };

            Patch["restaurant/edit/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                SelectedRestaurant.UpdateProperties(Request.Form["restaurant-name"], Request.Form["restaurant-price"], Request.Form["restaurant-vibe"]);
                return View["restaurant_updated.cshtml", SelectedRestaurant];
            };

            Get["cuisine/edit/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                return View["update_cuisine.cshtml", SelectedCuisine];
            };

            Patch["cuisine/edit/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                SelectedCuisine.UpdateName(Request.Form["cuisine-name"]);
                return View["cuisine_updated.cshtml", SelectedCuisine];
            };

            Delete["restaurant/delete/{id}"] = parameters => {
                Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
                SelectedRestaurant.Delete();
                return View["restaurant_deleted.cshtml"];
            };

            Delete["cuisine/delete/{id}"] = parameters => {
                Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
                SelectedCuisine.Delete();
                return View["cuisine_deleted.cshtml"];
            };

            Post["/restaurant/search/results"] = _ => {
                List<Restaurant> FoundList = new List<Restaurant>{};
                Restaurant foundRestaurant = Restaurant.FindByName(Request.Form["restaurant-search"]);
                FoundList.Add(foundRestaurant);
                return View["search_restaurant_results.cshtml", FoundList];
            };

            Post["/cuisine/search/results"] = _ => {
                List<Cuisine> FoundList = new List<Cuisine>{};
                Cuisine foundCuisine = Cuisine.FindByName(Request.Form["cuisine-search"]);
                FoundList.Add(foundCuisine);
                return View["search_cuisine_results.cshtml", FoundList];
            };
        }
    }
}
