﻿namespace GSB_NetCore.Models.MesExceptions
{
    public class Serreurs
    {
        private String _MessageUtilisateur, _MessageApplication;
        public Serreurs(String mu, String ma)
        {
            _MessageUtilisateur = mu;
            _MessageApplication = ma;
        }
        public String MessageUtilisateur()
        {
            return (_MessageUtilisateur);
        }
        public String MessageApplication()
        {
            return (_MessageApplication);
        }
    }
}
