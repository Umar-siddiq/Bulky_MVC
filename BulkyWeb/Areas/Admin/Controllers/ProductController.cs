using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Bulky.DataAccess.Data;
using System.Collections.Generic;   



namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_User_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = db;
			_webHostEnvironment = webHostEnvironment;

		}
		public IActionResult Index()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll().ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }

            else
            {
                productVM.Product = _unitofwork.Product.Get(u => u.Id == id);     //Find() only works with Primary Key
                return View(productVM);
            }
        }



        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create)) { 
                    
                        file.CopyTo(fileStream); 
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + filename;
                }

                _unitofwork.Product.Add(productVM.Product);
                _unitofwork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index", "Product");
            }
            
            return View();
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? categoryfromDb = _unitofwork.Product.Get(u => u.Id == id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            _unitofwork.Product.Remove(categoryfromDb);
            _unitofwork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");
        }
    }


}