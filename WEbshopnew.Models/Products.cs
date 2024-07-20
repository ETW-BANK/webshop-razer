using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEbshopnew.Models
{
    public class Products
    {
        [Key] 
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Ingredients { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int StockQuantity { get; set; }

        [Required]
        public string Usage { get; set; }
        public bool IsPrescriptionRequired { get; set; }

 

       
    }
}