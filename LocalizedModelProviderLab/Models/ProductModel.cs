using LocalizedModelProviderLab.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalizedModelProviderLab.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime CreateDate { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

    }
}