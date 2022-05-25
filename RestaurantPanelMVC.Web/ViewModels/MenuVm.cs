using System.Collections.Generic;

namespace RestaurantPanelMVC.Web.ViewModels
{
    public class MenuVm
    {
        //public MenuVm()
        //{
        //    List<DishVm> Dishes = new List<DishVm>();
        //}
        public string Category { get; set; }
        public List<DishVm> Dishes { get; set; }

    }
}
