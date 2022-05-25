using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPanelMVC.Web.ViewModels
{
    public class CreateDishVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }
        public SelectList  Categories { get; set; }
        public string Identifier { get; set; }
        
        public string Type { get; set; }
       

    }
}
