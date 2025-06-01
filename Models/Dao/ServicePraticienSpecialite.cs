using System.Data;
using GSB_NetCore.Models.MesExceptions;

namespace GSB_NetCore.Models.Dao
{
    public class ServicePraticienSpecialite
    {
        public static DataTable GetTousLesPraticiensAvecSpecialites()
        {
            DataTable dt;
            Serreurs er = new Serreurs("Erreur lors de la lecture des praticiens.", "ServicePraticien.GetTousLesPraticiensAvecSpecialites()");

            try
            {
                string requete = @"
                    SELECT 
                        praticien.id_praticien,
                        CONCAT(prenom_praticien, ' ', nom_praticien) AS nom_complet,
                        ville_praticien,
                        GROUP_CONCAT(specialite.lib_specialite SEPARATOR ', ') AS specialites
                    FROM praticien
                    LEFT JOIN posseder ON praticien.id_praticien = posseder.id_praticien
                    LEFT JOIN specialite ON posseder.id_specialite = specialite.id_specialite
                    GROUP BY praticien.id_praticien, nom_praticien, prenom_praticien, ville_praticien
                    ORDER BY nom_praticien, prenom_praticien
                ";

                dt = DBInterface.Lecture(requete, er);
                return dt;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
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

        public static DataTable GetSpecialiteDuPraticien(int idPraticien, int idSpecialite)
        {
            string sql = $@"
        SELECT s.id_specialite, s.lib_specialite, p.diplome, p.coef_prescription
        FROM posseder p
        JOIN specialite s ON p.id_specialite = s.id_specialite
        WHERE p.id_praticien = {idPraticien} AND p.id_specialite = {idSpecialite}";

            Serreurs er = new Serreurs("Erreur lecture spécialité unique", "GetSpecialiteDuPraticien");
            return DBInterface.Lecture(sql, er);
        }

    }
}
