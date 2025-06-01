using GSB_NetCore.Models.MesExceptions;
using GSB_NetCore.Models.Metier;
using System.Data;

namespace GSB_NetCore.Models.Dao
{
    public class ServiceVisiteur
    {
        public static Visiteur getVisiteur(string login)
        {
            DataTable dt;
            Visiteur visiteur = null;

            string requete = $"SELECT id_visiteur, login, mot_de_passe, salt, role, nom_visiteur, prenom_visiteur FROM visiteur WHERE login = '{login.Replace("'", "''")}'";
            Serreurs er = new Serreurs("Erreur lors de la recherche du visiteur", "ServiceVisiteur.getVisiteur");

            try
            {
                dt = DBInterface.Lecture(requete, er);

                if (dt != null && dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    visiteur = new Visiteur
                    {
                        IdVisiteur = row["id_visiteur"].ToString(),
                        Login = row["login"].ToString(),
                        MotDePasse = row["mot_de_passe"].ToString(),
                        Salt = row["salt"].ToString(),
                        Role = row["role"].ToString(),
                        NomVisiteur = row["nom_visiteur"].ToString(),
                        PrenomVisiteur = row["prenom_visiteur"].ToString()
                    };
                }

                return visiteur;
            }
            catch (MonException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }

    }
}
