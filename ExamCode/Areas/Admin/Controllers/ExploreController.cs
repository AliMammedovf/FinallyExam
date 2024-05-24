using ExamCode.Business.Exceptions;
using ExamCode.Business.Services.Abstract;
using ExamCode.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Composition;

namespace ExamCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ExploreController : Controller
    {
        private readonly IExploreService _exploreService;

        public ExploreController(IExploreService exploreService)
        {
            _exploreService = exploreService;
        }
        public IActionResult Index()
        {
            var explores= _exploreService.GetAllExplores();
            return View(explores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Explore explore)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
               await  _exploreService.AddAsyncExplore(explore);
            }
            catch (FileNullException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");   
        }

        public IActionResult Delete(int id)
        {
            var exsist= _exploreService.GetExplore(x=>x.Id==id);

            if(exsist == null)
            {
                return NotFound();
            }

            return View(exsist);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _exploreService.DeleteExplore(id);
            }
            catch(IdNullException ex)
            {
                return NotFound();
            }
            catch(FileNotExsistException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var oldExplore= _exploreService.GetExplore(x=> x.Id==id);

            if(oldExplore == null)
            {
                return NotFound();
            }

            return View(oldExplore);

        }

        [HttpPost]
        public IActionResult Update(Explore newExplore)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _exploreService.UpdateExplore(newExplore.Id, newExplore);
            }
            catch (IdNullException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(FileNotExsistException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
