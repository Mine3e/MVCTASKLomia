using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace MVCTASK.Areas.Adminn.Controllers
{
    [Area("Adminn")]
    [Authorize(Roles ="Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _service;
        public TeamController(ITeamService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var doctors=_service.GetAllTeams();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Team team )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _service.AddTeam(team);
            }
            catch(FileSizeException ex) 
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ImageFileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(TeamNotFoundException ex) 
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }
            catch (Exception ex)
            {
              BadRequest(ex.Message);
            }
           
            return RedirectToAction(nameof(Index ));
        }
        public IActionResult Delete(int id)
        {
            var eixstteam=_service.GetTeam(x=>x.Id == id);
            return View(eixstteam);
        }
        [HttpPost]
        public IActionResult DeleteTeam(int id)
        {
            try
            {
                _service.DeleteTeam(id);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
               BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index ));
        }
        public IActionResult Update(int id)
        {
            var existteam=_service.GetTeam(x=>x.Id == id);
            if (existteam == null)
            {
                return NotFound();
            }
            return View(existteam);
        }
        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _service.UpdateTeam(team.Id, team);
            }
            catch(EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(TeamNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return RedirectToAction(nameof (Index ));
        }


    }
}
