using Doorang_mvc.DAL;
using Doorang_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Composition;

namespace Doorang_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExploreController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _environment;

        public ExploreController(AppDbContext dbcontext,IWebHostEnvironment webHostEnvironment)
        {
            _context = dbcontext;
            _environment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Explore> explores = _context.Explories.ToList();
            return View(explores);
        }
        public IActionResult Update(int id)
        {
            var explore = _context.Explories.FirstOrDefault(x => x.Id == id);
            if (explore == null)
            {
                return RedirectToAction("Index");
            }
            return View(explore);
        }
        [HttpPost]
        public IActionResult Update(Explore newexp)
        {
            var oldexp = _context.Explories.FirstOrDefault(x => x.Id == newexp.Id);
            if (oldexp == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (newexp.imgFile != null)
            {
                if (newexp.imgFile.ContentType.Contains("image/"))
                {
                    if (newexp.imgFile.Length < 2097152)
                    {
                        string path = _environment.WebRootPath + @"\uploads\explores\" + newexp.imgFile.FileName;

                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            newexp.imgFile.CopyTo(stream);
                        }
                        oldexp.ImgUrl = newexp.imgFile.FileName;


                    }
                }
            }
            oldexp.Title = newexp.Title;
            oldexp.Description = newexp.Description;
            oldexp.Name = newexp.Name;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var exp = _context.Explories.FirstOrDefault(x => x.Id == id);
            _context.Explories.Remove(exp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Explore explore)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (explore.imgFile == null) throw new NullReferenceException("Null Photo");
            if (explore.imgFile.ContentType.Contains("image/"))
            {
                if (explore.imgFile.Length < 2097152)
                {
                    string path = _environment.WebRootPath + @"\uploads\explores\" + explore.imgFile.FileName;

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        explore.imgFile.CopyTo(stream);
                    }
                    explore.ImgUrl = explore.imgFile.FileName;

                    _context.Explories.Add(explore);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
            

        }
    }
}
