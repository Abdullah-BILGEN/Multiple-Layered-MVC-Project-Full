using System.ComponentModel.DataAnnotations;

namespace BilgeShop.WebUI.Models
{
	public class RegisterViewModel
	{

        [Display(Name ="Ad")]
        [MaxLength(25,ErrorMessage ="Ad En Fazla 25 karakter olablir")]
        [Required(ErrorMessage ="Ad alanı boş bırakılamaz")]
        public string FirstName { get; set; }


		[Display(Name = "SoyAd")]
		[MaxLength(25, ErrorMessage = "SoyAd En Fazla 25 karakter uzunluğunda olablir")]
		[Required(ErrorMessage = "SoyAd alanı boş bırakılamaz")]
		public string LastName { get; set; }

        [Display(Name ="Eposta")]
        [Required(ErrorMessage ="Eposta alanı boş bırakılamaz")]
        public string EMail { get; set; }

        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="Şifre Alanı boş bırakılamaz.")]
        public string Password { get; set; }

		[Display(Name = "Şifre tekrarı")]
		[Required(ErrorMessage = "Şifre tekrarı Alanı boş bırakılamaz.")]
        [Compare(nameof(Password),ErrorMessage ="Şifreler Eşleşmiyor.")]
		public string PasswordConfirm { get; set; }


    }
}
