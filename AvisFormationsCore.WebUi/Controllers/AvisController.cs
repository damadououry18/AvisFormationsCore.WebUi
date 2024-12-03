using AvisFormationsCore.WebUi.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AvisFormationsCore.WebUi.Controllers
{
    public class AvisController : Controller
    {
        
        IFormationRepository _repository;
        IAvisRepository _avisRepository;
        UserManager<IdentityUser> _userManager;
        public AvisController(IFormationRepository repository, 
            IAvisRepository avisRepository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _avisRepository = avisRepository;
            _userManager = userManager;
        }
     
        public IActionResult Index(string SortBy, string Order, string Search)
        {

            var model1 = _avisRepository.GetAllAvis();
            var model2 = _repository.GetAllFormations();
            var model3 = from l1 in model1
                        join l2 in model2
                        on l1.FormationId equals l2.Id
                        select new { l1.Id, l1.NomUtilisateur, l1.Commentaire, l1.Note, l2.NomSeo };

            var model = new List<TouteslesAvisViewModel>();
            foreach(var f in model3)
            {
                model.Add(
                          new TouteslesAvisViewModel
                          {
                              Id = f.Id,
                              NomUtilisateur = f.NomUtilisateur,
                              Commentaire = f.Commentaire,
                              Note = f.Note,
                              NomSeo = f.NomSeo           
                          });
            }
            

            if (Search != null)
            {
                model = model.Where(c => c.NomUtilisateur.ToLower().StartsWith(Search.ToLower())).ToList();
            }
            if (SortBy != null && Order != null)
            {
                switch (SortBy)
                {
                    case "NomUtilisateur":
                        model = (Order == "desc") ? model.OrderByDescending(c => c.NomUtilisateur).ToList() : model.OrderBy(c => c.NomUtilisateur).ToList();
                        break;
                    case "Commentaire":
                        model = (Order == "desc") ? model.OrderByDescending(c => c.Commentaire).ToList() : model.OrderBy(c => c.Commentaire).ToList();
                        break;
                    case "Note":
                        model = (Order == "desc") ? model.OrderByDescending(c => c.Note).ToList() : model.OrderBy(c => c.Note).ToList();
                        break;
                    case "NomSeo":
                        model = (Order == "desc") ? model.OrderByDescending(c => c.NomSeo).ToList() : model.OrderBy(c => c.NomSeo).ToList();
                        break;

                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult LaisserUnAvis(string nomSeo)
        {
            //int iIdFormation = -1;
            //if (!Int32.TryParse(idFormation, out iIdFormation))
            //{
            //    return RedirectToAction("ToutesLesFormations","Formation");
            //}

            //FormationMemoryRepository repository = new FormationMemoryRepository();
            //var formation = repository.GetFormationById(iIdFormation);

            //var formation = _repository.GetFormationById(iIdFormation);

            var formation = _repository.GetFormationByNomSeo(nomSeo);
            if (formation == null)
            {
                return RedirectToAction("ToutesLesFormations","Formation");
            }

            var vm = new LaisserUnAvisViewModel();
            vm.NomFormation = formation.Nom;
            vm.IdFormation = formation.Id.ToString();
            
            
            return View(vm);
        }

        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(int id, string nomSeo)
        {
            var formation = _repository.GetFormationByNomSeo(nomSeo);
            var avis = _avisRepository.GetAvisById(id);

            var model = new EditAvisViewModel()
            {
                Id = avis.Id,
                NomUtilisateur = avis.NomUtilisateur,
                Commentaire = avis.Commentaire,
                Note = avis.Note,
                FormationId = avis.FormationId,
                NomSeo = formation.NomSeo
            };
            var model2 = _repository.GetAllFormations().ToList();
            var nomseo = from l1 in model2
                         select new { l1.NomSeo };

            var statusList = new List<SelectListItem>();
            foreach (var item in nomseo)
            {
                var newStatus = new SelectListItem() { Text = item.NomSeo};
                statusList.Add(newStatus);
            }
            model.StatusList = statusList;

            return View(model);
        }
        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(EditAvisViewModel model)
        {
            
            var model3 = _repository.GetFormationByNomSeo(model.NomSeo);
            int model4 = model3.Id;
            
            Avis avis = new Avis();
            avis.Id = model.Id;
            avis.Commentaire = model.Commentaire;
            avis.NomUtilisateur = model.NomUtilisateur;
            avis.Note = model.Note;
            avis.FormationId = model4;
            
            _avisRepository.UpdateAvis(avis);
            return RedirectToAction("Index");
        }
        // GET: /Avis/Edit/
        public IActionResult Delete(int id)
        {
            var model1 = _avisRepository.GetAvisById(id);
            var formation = _repository.GetFormationById(model1.FormationId);
            var model = new DeleteAvisViewModel()
            {
                Id = model1.Id,
                Commentaire = model1.Commentaire,
                Note = model1.Note,
                NomUtilisateur = model1.NomUtilisateur,
                NomFormation = formation.Nom
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _avisRepository.DeleteAvisById(id);
            return View();
        }

        [Authorize]
       // [HttpPost]
        public IActionResult SaveComment(LaisserUnAvisViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("LaisserUnAvis", viewModel); //new { idFormation = viewModel.IdFormation });
                //return RedirectToAction("LaisserUnAvis", new { idFormation = viewModel.IdFormation });
            }

            //if (String.IsNullOrEmpty(viewModel.Nom) || String.IsNullOrEmpty(viewModel.Note))
            if (String.IsNullOrEmpty(viewModel.Note))
            {
                return RedirectToAction("LaisserUnAvis", new { idFormation = viewModel.IdFormation });
            }



            var currentUser = this.User;
            var UserName = _userManager.GetUserName(currentUser);

            // limiter à un seul avis par formation

            var userId = _userManager.GetUserId(currentUser);
            //var mgerUnicite = new UniqueAvisVerification();
            int IdFormation1 = Int32.Parse(viewModel.IdFormation);

            //if (!mgerUnicite.EstAutoriseACommenter(userId, IdFormation1))

            if (_avisRepository.EstAutoriseACommenter(userId, IdFormation1))
            {
                TempData["Message"] = "Désolé un seul avis par formation par compte utilisateur";
                int iIdFormation1 = Int32.Parse(viewModel.IdFormation);
                var formation1 = _repository.GetFormationById(iIdFormation1);
                return RedirectToAction("DetailsFormation", "Formation", new { nomSeo = formation1.NomSeo });
            }

            // fin limiter un seul avis par formation

            //AvisRepository repository = new AvisRepository();
            _avisRepository.SaveAvis(viewModel.Commentaire, 
                UserName, viewModel.IdFormation, viewModel.Note, userId);


            //get nomseo from formation

            int iIdFormation = Int32.Parse(viewModel.IdFormation);
            var formation = _repository.GetFormationById(iIdFormation);

            return RedirectToAction("DetailsFormation", "Formation", new { nomSeo = formation.NomSeo });

            //return RedirectToAction("DetailsFormation","Formation",new {idFormation= viewModel.IdFormation});
            //return View();
        }

    }
}
