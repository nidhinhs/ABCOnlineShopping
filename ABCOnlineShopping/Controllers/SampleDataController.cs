using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ABCOnlineShopping.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        //Hard coding the product details
 
        [HttpGet("[action]")]
        public IEnumerable<Products> GetProducts()
        {
            List<Products> products = new List<Products> {
                new Products(1, 0, 0, 12.49, "Book"),
                new Products(2, 10.0, 0, 14.99, "music CD"),
                new Products(3, 0, 0, 0.85, "chocolate bar"),
                new Products(4, 0, 5.0, 10.00, "imported box of chocolates"),
                new Products(5, 10, 5.0, 47.50, "imported bottle of perfume"),
                new Products(6, 10, 5.0, 27.99, "imported bottle of perfume 2"),
                new Products(7, 10.0, 0, 18.99, "bottle of perfume"),
                new Products(8, 0, 0, 9.75, "packet of headache pills"),
                new Products(9, 0, 5.0, 11.25, "box of imported chocolates")
            };
            return products;
        }

        public class Products
        {
            public int Id { get; set; }
            public double BasicSalesTax { get; set; }
            public double ImportDuty { get; set; }
            public double UnitPrice { get; set; }
            public string ItemName { get; set; }

            public double GrossPrice
            {
                get
                {
                    //return UnitPrice + BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100;
                    return UnitPrice + Math.Ceiling((BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100) * 20) / 20;
                }
            }
            public double SalesTax
            {
                get
                {
                    //return BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100;
                    return Math.Ceiling((BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100)*20)/20;
                }
            }

            public Products(int id, double basicSalesTax, double importDuty, double unitPrice, string itemName)
            {
                Id = id;
                BasicSalesTax = basicSalesTax;
                ImportDuty = importDuty;
                UnitPrice = unitPrice;
                ItemName = itemName;
            }
        }
    }
}
