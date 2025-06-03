using Microsoft.AspNetCore.Mvc;
using GSB_NetCore.Models.Dao;
using GSB_NetCore.Models.MesExceptions;
using System.Data;
using GSB_NetCore.Models.Metier;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GSB_NetCore.Controllers
{
    public class PraticienController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                DataTable dt = ServicePraticienSpecialite.GetTousLesPraticiensAvecSpecialites();
                return View(dt);
            }
            catch (MonException e)
            {
                ViewBag.Message = e.MessageUtilisateur();
                return View("Error");
            }
        }

        // GET : Ajout
        public IActionResult AjouterSpecialite(int idPraticien)
        {
            var dt = ServicePraticienSpecialite.GetToutesLesSpecialites();
            var liste = new List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {
                liste.Add(new SelectListItem
                {
                    Value = row["id_specialite"].ToString(),
                    Text = row["lib_specialite"].ToString()
                });
            }

            ViewBag.LesSpecialites = liste;

            ViewBag.IdPraticien = idPraticien;
            return View();
        }

        // POST : Ajout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AjouterSpecialite(int idPraticien, int idSpecialite, string diplome, double coef)
        {
            // Vérification simple (facultative)
            if (idPraticien <= 0)
            {
                ModelState.AddModelError("Erreur", "Praticien invalide.");
                return RedirectToAction("Index");
            }

            ServicePraticienSpecialite.AjouterSpecialite(idPraticien, idSpecialite, diplome, coef);
            return RedirectToAction("Index");
        }

        public IActionResult ModifierSpecialite(int idPraticien, int idSpecialite)
        {
            var dt = ServicePraticienSpecialite.GetSpecialiteDuPraticien(idPraticien, idSpecialite);

            if (dt.Rows.Count == 0) return NotFound();

            var row = dt.Rows[0];
            var specialite = new Specialite
            {
                IdSpecialite = idSpecialite,
                LibSpecialite = row["lib_specialite"].ToString(),
                Diplome = row["diplome"].ToString(),
                CoefPrescription = Convert.ToDouble(row["coef_prescription"]),
            };

            ViewBag.IdPraticien = idPraticien;
            return View(specialite);
        }

        [HttpPost]
        public IActionResult ModifierSpecialite(int idPraticien, Specialite specialite)
        {
            ServicePraticienSpecialite.ModifierSpecialite(idPraticien, specialite.IdSpecialite, specialite.Diplome, specialite.CoefPrescription);
            return RedirectToAction("Index");
        }

        public IActionResult SelectionnerSpecialitePourModification(int idPraticien)
        {
            ViewBag.IdPraticien = idPraticien;
            ViewBag.LesSpecialites = ServicePraticienSpecialite.GetSpecialitesPourPraticienSelectList(idPraticien);
            return View();
        }

        [HttpPost]
        public IActionResult SelectionnerSpecialitePourModification(int idPraticien, int idSpecialite)
        {
            return RedirectToAction("ModifierSpecialite", new { idPraticien, idSpecialite });
        }

        public IActionResult SelectionnerSpecialitePourSuppression(int idPraticien)
        {
            ViewBag.IdPraticien = idPraticien;
            ViewBag.LesSpecialites = ServicePraticienSpecialite.GetSpecialitesPourPraticienSelectList(idPraticien);
            return View();
        }

        [HttpPost]
        public IActionResult SelectionnerSpecialitePourSuppression(int idPraticien, int idSpecialite)
        {
            return RedirectToAction("SupprimerSpecialite", new { idPraticien, idSpecialite });
        }



        // Suppression
        // GET
        public IActionResult SupprimerSpecialite(int idPraticien, int idSpecialite)
        {
            var dt = ServicePraticienSpecialite.GetSpecialiteDuPraticien(idPraticien, idSpecialite);
            if (dt.Rows.Count == 0) return NotFound();

            var row = dt.Rows[0];
            var specialite = new Specialite
            {
                IdSpecialite = idSpecialite,
                LibSpecialite = row["lib_specialite"].ToString(),
                Diplome = row["diplome"].ToString(),
                CoefPrescription = Convert.ToDouble(row["coef_prescription"]),
            };

            ViewBag.IdPraticien = idPraticien;
            return View(specialite);
        }

        // POST
        [HttpPost]
        public IActionResult ConfirmerSuppressionSpecialite(int idPraticien, int idSpecialite)
        {
            ServicePraticienSpecialite.SupprimerSpecialite(idPraticien, idSpecialite);
            return RedirectToAction("Index");
        }

    }
}
