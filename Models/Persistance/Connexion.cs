using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using GSB_NetCore.Models.MesExceptions;
using System;

namespace GSB_NetCore.Models.Dao
{
    public class Connexion
    {
        private static MySqlConnection macnx;
        private static Connexion instance;

        /// <summary>
        /// Constructeur privé (singleton)
        /// </summary>
        private Connexion() { }

        /// <summary>
        /// Obtenir la connexion MySQL à partir de appsettings.json
        /// </summary>
        public MySqlConnection getConnexion()
        {
            string strConnexion;
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                IConfiguration configuration = builder.Build();
                strConnexion = configuration.GetConnectionString("bddCourante");

                macnx = new MySqlConnection(strConnexion);
                macnx.Open();
                return macnx;
            }
            catch (MySqlException err)
            {
                throw new MonException("", "Erreur d'accès à la base.", err.Message);
            }
            catch (Exception e)
            {
                throw new MonException("", "Erreur d'accès", e.Message);
            }
        }

        /// <summary>
        /// Obtenir le singleton
        /// </summary>
        public static Connexion getInstance()
        {
            if (instance == null)
                instance = new Connexion();
            return instance;
        }

        /// <summary>
        /// Fermer la connexion
        /// </summary>
        public static void closeConnexion()
        {
            if (instance != null && macnx != null)
                macnx.Close();
        }
    }
}
