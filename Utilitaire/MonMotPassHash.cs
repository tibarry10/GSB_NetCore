using System;
using System.Security.Cryptography;
using System.Text;

namespace GSB_NetCore.Models.Utilitaires
{
    public class MonMotPassHash
    {
        private const int SaltSize = 32;

        /// <summary>
        /// Génère le sel sous forme d'une clé
        /// </summary>
        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[SaltSize];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        /// <summary>
        /// Hache le mot de passe avec le sel (HMAC-SHA256)
        /// </summary>
        public static byte[] ComputeHMAC_SHA256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(data);
            }
        }

        /// <summary>
        /// Retourne le mot de passe haché
        /// </summary>
        public static byte[] PasswordHashe(string password, byte[] salt)
        {
            return ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(password), salt);
        }

        /// <summary>
        /// Vérifie si le mot de passe clair correspond au haché
        /// </summary>
        public static bool VerifyPassword(byte[] salt, string pwd, byte[] pwdh)
        {
            byte[] pwdHash = PasswordHashe(pwd, salt);
            int i = 0;
            bool egal = true;

            while (i < pwdHash.Length && egal)
            {
                if (pwdHash[i] != pwdh[i])
                    egal = false;
                i++;
            }

            return egal;
        }

        /// <summary>
        /// Convertit une chaîne base64 en tableau de bytes
        /// </summary>
        public static byte[] transformeEnBytes(string maChaine)
        {
            return Convert.FromBase64String(maChaine);
        }

        /// <summary>
        /// Convertit un tableau de bytes en chaîne base64
        /// </summary>
        public static string BytesToString(byte[] monByte)
        {
            return Convert.ToBase64String(monByte);
        }
    }
}
