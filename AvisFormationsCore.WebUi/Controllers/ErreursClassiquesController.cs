using AvisFormationsCore.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi.Controllers
{
    public class ErreursClassiquesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VueManquante()
        {
            return View();
        }
        public IActionResult ViewModelNonPasse()
        {
            var vm = new ToutesLesFormationsViewModel();
            vm.Message = "Ceci est un message du Controlleur";
            //return View();
            return View(vm);
        }
        public IActionResult MauvaisViewModel()
        {
            var vm = new ToutesLesFormationsViewModel();
            vm.Message = "Ceci est un message du Controlleur";
            return View(vm);
        }
        public IActionResult VueErreurHtml1()
        {
            return View();
        }

        public IActionResult VueErreurHtml2()
        {
            return View();
        }
        public IActionResult NombreDecimaux(String nombre)
        {
            double number;
            String value = nombre;
            Double.TryParse(value, out number);

            return View(number);
        }
       
    }
}
