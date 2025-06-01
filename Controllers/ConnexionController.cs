using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GSB_NetCore.Models.Dao;
using GSB_NetCore.Models.MesExceptions;
using GSB_NetCore.Models.Metier;
using GSB_NetCore.Models.Utilitaires;

namespace GSB_NetCore.Controllers
{
    public class ConnexionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Controle()
        {
            try
            {
                string login = Request.Form["login"];
                string mdp = Request.Form["pwd"];

                Visiteur unVisiteur = ServiceVisiteur.getVisiteur(login);

                if (unVisiteur != null)
                {
                    if (string.IsNullOrEmpty(unVisiteur.Salt) || string.IsNullOrEmpty(unVisiteur.MotDePasse))
                    {
                        ModelState.AddModelError("Erreur", "Salt ou mot de passe manquant.");
                        return View("Index");
                    }

                    byte[] salt = MonMotPassHash.transformeEnBytes(unVisiteur.Salt);
                    byte[] hash = MonMotPassHash.transformeEnBytes(unVisiteur.MotDePasse);

                    byte[] hashCalcule = MonMotPassHash.PasswordHashe(mdp, salt);
                    string hashCalculeStr = MonMotPassHash.BytesToString(hashCalcule);

                    Console.WriteLine("Hash attendu : " + unVisiteur.MotDePasse);
                    Console.WriteLine("Hash calculé : " + hashCalculeStr);


                    if (MonMotPassHash.VerifyPassword(salt, mdp, hash))
                    {
                        ModelState.AddModelError("Erreur", "Mot de passe incorrect.");
                        return View("Index");
                    }

                    // Connexion réussie
                    HttpContext.Session.SetString("login", unVisiteur.Login);
                    HttpContext.Session.SetString("nom", unVisiteur.NomVisiteur);
                    HttpContext.Session.SetString("role", unVisiteur.Role);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Erreur", "Visiteur introuvable.");
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Erreur", "Erreur : " + e.Message);
                return View("Index");
            }
        }

        public IActionResult Session()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Connexion");
        }
    }

}
