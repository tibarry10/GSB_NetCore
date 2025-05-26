using MySql.Data.MySqlClient;
using System.Data;
using GSB_NetCore.Models.MesExceptions;

namespace GSB_NetCore.Models.Dao
{
    public class DBInterface
    {
        /// <summary>
        /// Exécute une requête SELECT et retourne les résultats dans un DataTable.
        /// </summary>
        public static DataTable Lecture(string requete, Serreurs er)
        {
            MySqlConnection cnx = null;
            try
            {
                cnx = Connexion.getInstance().getConnexion();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = cnx,
                    CommandText = requete
                };

                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = cmd
                };

                DataSet ds = new DataSet();
                da.Fill(ds, "resultat");

                return ds.Tables["resultat"];
            }
            catch (MonException me)
            {
                throw me;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            finally
            {
                if (cnx != null)
                    cnx.Close();
            }
        }

        /// <summary>
        /// Exécute une requête d'écriture (INSERT, UPDATE, DELETE) dans une transaction.
        /// </summary>
        public static void Execute_Transaction(string requete)
        {
            MySqlConnection cnx = null;
            try
            {
                cnx = Connexion.getInstance().getConnexion();
                MySqlTransaction trans = cnx.BeginTransaction();
                MySqlCommand cmd = cnx.CreateCommand();
                cmd.Transaction = trans;
                cmd.CommandText = requete;
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (MySqlException e)
            {
                throw new MonException(e.Message, "Erreur d'insertion ou mise à jour", "SQL");
            }
            finally
            {
                if (cnx != null)
                    cnx.Close();
            }
        }
    }
}
