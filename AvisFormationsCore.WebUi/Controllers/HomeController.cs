using AvisFormationsCore.WebUi.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi.Controllers
{
    public class HomeController : Controller
    {
        IFormationRepository _repository;
       public HomeController(IFormationRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            //FormationMemoryRepository repository = new FormationMemoryRepository();
            //var listFormations = repository.GetFormations(4);

            var listFormations = _repository.GetFormations(6);
            //var listFormations = _repository.GetAllFormations();
            var vm = new List<DetailFormationViewModel>();
            foreach(var f in listFormations)
            {
                vm.Add(
                    new DetailFormationViewModel
                    {
                        Formation = f,
                        NoteMoyenne = Math.Round(f.Avis.Select(a => a.Note)
                        .DefaultIfEmpty(0)
                        .Average(), 2)
                    }) ; 
            }

            return View(vm);
        }

    }
}
