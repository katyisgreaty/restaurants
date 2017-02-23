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
                List<Cuisines> AllCuisines = Cuisine.GetAll();
                return View["cuisines_form.cshtml", AllCuisines];
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
        }
    }
}
