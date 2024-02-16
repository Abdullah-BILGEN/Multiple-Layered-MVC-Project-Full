using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BilgeShop.WebUI.Areas.Admin.Models
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }
        [Display(Name ="İsim")]
        [Required(ErrorMessage ="Ürün ismi girmek zorunludur.")]
        public string Name { get; set; }


        [Display(Name ="Açıklama")]
        public string Description { get; set; }
       
        
        [Display(Name ="Ürün Fiyatı")]
        public decimal? UnitPrice { get; set; }

        [Display(Name="Stok Miktarı")]
        public int UnitsInStock { get; set; }


        [Display(Name ="Kategori")]
        [Required(ErrorMessage ="Bir Kategori seçmek zorunludur.")]
        public int CategoryId { get; set; }


        public IFormFile? File { get; set; }

        // Herhangi bir dosya taşıyorsanız -> Tipi : IFormFile 


    }
}
