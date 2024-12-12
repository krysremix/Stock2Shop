using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2Shop.Core.Models
{
    public class Product
    {
        [Key]
        public string Sku { get; set; } = string.Empty;
        public Dictionary<string, string> Attributes { get; set; } = new();
    }
}
