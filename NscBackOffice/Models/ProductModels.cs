using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NscBackOffice.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }        
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Details { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }        
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int CreatedUserID { get; set; }
        public int UOMID { get; set; }
        public int BrandID { get; set; }
    }

    public class CreateProductViewModel
    {       
        [Required(ErrorMessage ="ProductCode: Required.")]
        [RegularExpression("^[a-zA-Z0-9]+$",ErrorMessage = "ProductCode: Only alphabets, numbers allowed.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "ProductName: Required.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "ProductName: Only alphabets, numbers, space allowed.")]
        public string ProductName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "ShortName: Only alphabets, numbers, space allowed.")]
        public string ShortName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "LongName: Only alphabets, numbers, space allowed.")]
        public string LongName { get; set; }

        public string Details { get; set; }

        public string LongDescription { get; set; }

        public string ShortDescription { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UOMID: Only integer greater than 0 allowed.")]
        public int? UOMID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "BrandID: Only integer greater than 0 allowed.")]
        public int? BrandID { get; set; }
    }
}
