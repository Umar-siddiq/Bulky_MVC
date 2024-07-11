using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {

                ModelState.AddModelError("name", "the displayorder must match the name exactly. ");
            }


            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }




        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryfromDb = _db.Categories.Find(id);     //Find() only works with Primary Key
                                                                    //Category? categoryfromDb1 = _db.Categories.FirstorDefault(u=>u.Id=id);
                                                                    //Category? categoryfromDb2 = _db.Categories.Where(u=>u.Id == id).FirstorDefault();

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
				TempData["success"] = "Category Edited Successfully";
				return RedirectToAction("Index", "Category");
            }
            return View();
        }


		//public IActionResult Delete(int? id)
		//{

		//    if (id == null || id == 0)
		//    {

		//        return NotFound();
		//    }
		//    Category? categoryfromdb = _db.Categories.Find(id);

		//    if (categoryfromdb == null)
		//    {

		//        return NotFound();
		//    }
		//    return View(categoryfromdb);
		//}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
        {

            Category? categoryfromDb = _db.Categories.Find(id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryfromDb);
            _db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToAction("Index", "Category");
        }
    }
}
