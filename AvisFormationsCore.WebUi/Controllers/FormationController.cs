using AvisFormationsCore.WebUi.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace AvisFormationsCore.WebUi.Controllers
{    public class FormationController : Controller
    {
        IFormationRepository _repository;
        public FormationController(IFormationRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var vm = new ToutesLesFormationsViewModel();
            vm.Message = "Ceci un message du controlleur";
            vm.Nombre = 93;
            return View(vm);
        }

        public IActionResult ToutesLesFormations()
        {
            //FormationMemoryRepository repository = new FormationMemoryRepository();
            //var listFormations = repository.GetAllFormations();

            var listFormations = _repository.GetAllFormations();

            return View(listFormations);
        }
        public IActionResult DetailsFormation(string nomSeo)
        {
            //int iIdFormation = -1;
            //if(!Int32.TryParse(idFormation, out iIdFormation))
            //{
            //    return RedirectToAction("ToutesLesFormations");
            //}

            //FormationMemoryRepository repository = new FormationMemoryRepository();
            //var formation = repository.GetFormationById(iIdFormation);

            var formation = _repository.GetFormationByNomSeo(nomSeo);
            if (formation == null)
            {
                return RedirectToAction("ToutesLesFormations");
            }
            var vm = new DetailFormationViewModel();
            vm.Formation = formation;

            if (formation.Avis != null && formation.Avis.Count > 0)     
            vm.NoteMoyenne = Math.Round(formation.Avis.Select(a => a.Note)
                .ToList()
                .Average(), 2);

            //Math.Round(c, 2)
            //return View(formation);
            return View(vm);
        }
    }
}
