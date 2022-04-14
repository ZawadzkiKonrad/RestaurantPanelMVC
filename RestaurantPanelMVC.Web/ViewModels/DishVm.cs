namespace RestaurantPanelMVC.Web.ViewModels
{
    public class DishVm
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
