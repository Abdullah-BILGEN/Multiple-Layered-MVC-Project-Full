using BilgeShop.Business.Dtos;
using BilgeShop.Business.Services;
using BilgeShop.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
			;_categoryService = categoryService;
            
        }
        public IActionResult List()
		{

			var categoryDtoList = _categoryService.GetCategories();

			var viewModel = categoryDtoList.Select(x => new CategoryListViewModel()
			{
				Id = x.Id,	
				Name = x.Name,	
				Description = x.Description?.Length > 20 ? x.Description?.Substring(0,20)+ "..." :x.Description
			}).ToList();


			return View(viewModel);
		}

		public IActionResult New() 
		{



			// Eğer ekleme güncelleme işlemleri için aynı formu kullanacaksak bu ayrım Id üzerinden yapılacağından form mutlaka bir model ile açılmalı.

			return View("Form",new CategoryFormViewModel()  );
		}

		public IActionResult Update(int id)
		{ 		
			var categoryDto = _categoryService.GetCategory(id);
			var viewModel = new CategoryFormViewModel()
			{
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				Description = categoryDto.Description,

			};
			return View("Form",viewModel);
		
		}

		[HttpPost]
		public IActionResult Save(CategoryFormViewModel formData)
		{ 
		
			if (!ModelState.IsValid)
			{


				return View("Form", formData);
			}

			if (formData.Id == 0) // Ekleme işlemi 
			{
				var categoryAddDto = new CategoryAddDto()
				{
					Name = formData.Name.Trim(),
					Description=formData.Description?.Trim()
				};

				var result = _categoryService.AddCategory(categoryAddDto);

				if (result)
				{
					return RedirectToAction("List");
				}
				else
				{
					ViewBag.ErrorMessage = "Bu isimde bir kategory zaten mevcuttur";
					return View("Form", formData);
					// View dönüyorsan ViewBag Çalışır.
					// RedirecToAction ile mesaj döneceksem TempData[..] kullanmalısın.

				}
			}

            else // Güncelleme işlemi 
            {
				var categoryUpdateDto = new CategoryUpdateDto()
				{
					Id = formData.Id,
					Name = formData.Name,
					Description = formData.Description
				};
				_categoryService.UpdateCategory(categoryUpdateDto);

				var result = _categoryService.UpdateCategory(categoryUpdateDto);
                
				if (!result) 
				{
					ViewBag.EroorMessage = "Bu isimde bir kategori zaten mevcut olduğundan, güncelleme  yapamazsınız";


						 return View("Form",formData);	
				}
				return RedirectToAction("List");

            }

			return Ok();
        }

		public ActionResult Delete(int id) 
		{

		  var result = _categoryService.DeleteCategory(id);

			if (!result)
			{
				TempData["CategoryErrorMessage"] = "İlgili kategoride ürün bulunmakta be sebeple silme işlemi gerçekleştirilemez.";
			}
			return RedirectToAction("List");
		}
	}
}
