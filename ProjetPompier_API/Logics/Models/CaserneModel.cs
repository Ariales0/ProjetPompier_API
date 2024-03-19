namespace ProjetPompier_API.Logics.Modeles
{
    public class CaserneModel
    {
        #region AttributsProprietes

        private string nom;
        private string adresse;
        private string ville;
        private string province;
        private string telephone;

        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="unNom">Le nom d'une caserne</param>
        /// <param name="uneAdresse">L'adresse d'une caserne</param>
        /// <param name="uneVille">La ville d'une caserne</param>
        /// <param name="uneProvince">La province d'une caserne</param>
        /// <param name="unTelephone">Le téléphone d'une caserne</param>
        public CaserneModel(string unNom = "", string uneAdresse = "", string uneVille = "", string uneProvince = "", string unTelephone = "")
        {
            
        }

        #endregion Constructeurs
    }
}
