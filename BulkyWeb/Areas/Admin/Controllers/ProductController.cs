using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork db)
        {
            _unitofwork = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitofwork.Product.GetAll().ToList();

			return View(objProductList);
        }

        public IActionResult Create()
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
			return View(productVM);
        }
        [HttpPost]
        public IActionResult Create( ProductVM obj)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Add(obj.Product);
                _unitofwork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }




        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productfromDb = _unitofwork.Product.Get(u => u.Id == id);     //Find() only works with Primary Key
                                                                                      //Category? categoryfromDb1 = _db.Categories.FirstorDefault(u=>u.Id=id);
                                                                                      //Category? categoryfromDb2 = _db.Categories.Where(u=>u.Id == id).FirstorDefault();

            if (productfromDb == null)
            {
                return NotFound();
            }

            return View(productfromDb);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM obj)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Update(obj.Product);
                _unitofwork.Save();
                TempData["success"] = "Product Edited Successfully";
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
