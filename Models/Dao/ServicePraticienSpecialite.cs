using System.Data;
using GSB_NetCore.Models.MesExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GSB_NetCore.Models.Dao
{
    public class ServicePraticienSpecialite
    {
        public static DataTable GetTousLesPraticiensAvecSpecialites()
        {
            string sql = @"
            SELECT 
    p.id_praticien,
    CONCAT(p.nom_praticien, ' ', p.prenom_praticien) AS nom_complet,
    p.ville_praticien,
    GROUP_CONCAT(s.lib_specialite SEPARATOR ', ') AS specialites
FROM praticien p
LEFT JOIN posseder ps ON p.id_praticien = ps.id_praticien
LEFT JOIN specialite s ON ps.id_specialite = s.id_specialite
GROUP BY p.id_praticien, p.nom_praticien, p.prenom_praticien, p.ville_praticien
ORDER BY p.nom_praticien";

            Serreurs er = new Serreurs("Erreur chargement praticiens avec spécialités", "GetTousLesPraticiensAvecSpecialites()");
            return DBInterface.Lecture(sql, er);
        }


        public static DataTable GetToutesLesSpecialites()
        {
            string sql = "SELECT id_specialite, lib_specialite FROM specialite ORDER BY lib_specialite";
            Serreurs er = new Serreurs("Erreur chargement spécialités", "ServicePraticienSpecialite.GetToutesLesSpecialites");
            return DBInterface.Lecture(sql, er);
        }


        public static void AjouterSpecialite(int idPraticien, int idSpecialite, string diplome, double coefPrescription)
        {
            string requete = $@"
                INSERT INTO posseder (id_praticien, id_specialite, diplome, coef_prescription)
                VALUES ({idPraticien}, {idSpecialite}, '{diplome}', {coefPrescription.ToString().Replace(",", ".")})
            ";

            Serreurs er = new Serreurs("Erreur ajout spécialité", "ServicePraticien.AjouterSpecialite()");
            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException e)
            {
                throw e;

            }
            Console.WriteLine("SQL utilisée : " + requete);

        }

        public static DataTable GetSpecialiteDuPraticien(int idPraticien, int idSpecialite)
        {
            string sql = $@"
        SELECT p.id_specialite, s.lib_specialite, p.diplome, p.coef_prescription
        FROM posseder p
        JOIN specialite s ON p.id_specialite = s.id_specialite
        WHERE p.id_praticien = {idPraticien} AND p.id_specialite = {idSpecialite}";

            Serreurs er = new Serreurs("Erreur chargement spécialité", "GetSpecialiteDuPraticien()");
            return DBInterface.Lecture(sql, er);
        }


        public static void SupprimerSpecialite(int idPraticien, int idSpecialite)
        {
            string requete = $@"
                DELETE FROM posseder
                WHERE id_praticien = {idPraticien} AND id_specialite = {idSpecialite}
            ";

            Serreurs er = new Serreurs("Erreur suppression spécialité", "ServicePraticien.SupprimerSpecialite()");
            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException e)
            {
                throw e;
            }
        }

        public static void ModifierSpecialite(int idPraticien, int idSpecialite, string diplome, double coefPrescription)
        {
            string requete = $@"
                UPDATE posseder
                SET diplome = '{diplome}', coef_prescription = {coefPrescription.ToString().Replace(",", ".")}
                WHERE id_praticien = {idPraticien} AND id_specialite = {idSpecialite}
            ";

            Serreurs er = new Serreurs("Erreur modification spécialité", "ServicePraticien.ModifierSpecialite()");
            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException e)
            {
                throw e;
            }
        }

        public static List<SelectListItem> GetSpecialitesPourPraticienSelectList(int idPraticien)
        {
            List<SelectListItem> liste = new List<SelectListItem>();
            string sql = $@"
        SELECT s.id_specialite, s.lib_specialite
        FROM posseder p
        JOIN specialite s ON p.id_specialite = s.id_specialite
        WHERE p.id_praticien = {idPraticien}";

            DataTable dt = DBInterface.Lecture(sql, new Serreurs("Erreur chargement spécialités", "Service"));

            foreach (DataRow row in dt.Rows)
            {
                liste.Add(new SelectListItem
                {
                    Value = row["id_specialite"].ToString(),
                    Text = row["lib_specialite"].ToString()
                });
            }

            return liste;
        }



    }
}
