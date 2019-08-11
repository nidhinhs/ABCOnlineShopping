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
        //These values are suppose to be comming from a database
        private static string[] ItemNames = new[]
        {
            "Book", "music CD", "chocolate bar", "imported box of chocolates", "imported bottle of perfume","imported bottle of perfume 2", "bottle of perfume", "packet of headache pills", "box of imported chocolates"
        };
        private static double[] UnitPrices = new[]
        {
            12.49, 14.99, 0.85, 10.00 ,47.50, 27.99, 18.99, 9.75,11.25
        };
        private static double[] BasicSalesTaxs = new[]//Values in percentage
        {
            10.0, 10.0, 0, 0 ,0, 0, 10.0,0,10.0
        };
        private static double[] ImportDutys = new[]//Values in percentage
        {
            0, 0, 0, 5.0 ,5.0, 5.0, 0, 0,5.0
        };

        [HttpGet("[action]")]
        public IEnumerable<Products> GetProducts()
        {
            List<Products> products = new List<Products> {
                new Products(1, 10.0, 0, 12.49, "Book"),
                new Products(2, 10.0, 0, 14.99, "music CD"),
                new Products(3, 0, 0, 0.85, "chocolate bar"),
                new Products(4, 0, 5.0, 10.00, "imported box of chocolates"),
                new Products(5, 0, 5.0, 47.50, "imported bottle of perfume"),
                new Products(6, 0, 5.0, 27.99, "imported bottle of perfume 2"),
                new Products(7, 10.0, 0, 18.99, "bottle of perfume"),
                new Products(8, 0, 0, 9.75, "packet of headache pills"),
                new Products(9, 10.0, 5.0, 11.25, "box of imported chocolates")
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
                    return UnitPrice + BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100;
                }
            }
            public double SalesTax
            {
                get
                {
                    return BasicSalesTax * UnitPrice / 100 + ImportDuty * UnitPrice / 100;
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
