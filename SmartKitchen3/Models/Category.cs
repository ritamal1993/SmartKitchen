using System.ComponentModel;

namespace SmartKitchen3.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

    }
}