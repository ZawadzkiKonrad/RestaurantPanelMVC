using System.Collections.Generic;

namespace RestaurantPanelMVC.Web.ViewModels
{
    public class MenuVm
    {
        public string Category { get; set; }
        public List<DishVm> Dishes { get; set; }

    }
}
