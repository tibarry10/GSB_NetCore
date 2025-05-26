using Microsoft.AspNetCore.Mvc;
using GSB_NetCore.Models.Dao;
using GSB_NetCore.Models.MesExceptions;
using System.Data;

namespace GSB_NetCore.Controllers
{
    public class PraticienController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                DataTable dt = ServicePraticien.GetTousLesPraticiensAvecSpecialites();
                return View(dt);
            }
            catch (MonException e)
            {
                ViewBag.Message = e.MessageUtilisateur();
                return View("Error");
            }
        }
    }
}
