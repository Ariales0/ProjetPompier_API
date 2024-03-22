/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Modeles
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class CaserneModel
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant le nom de la caserne.
        /// </summary>
        private string nom;

        /// <summary>
        /// Attribut représentant le adresse de la caserne.
        /// </summary>
        private string adresse;

        /// <summary>
        /// Attribut représentant le ville de la caserne.
        /// </summary>
        private string ville;

        /// <summary>
        /// Attribut représentant le province de la caserne.
        /// </summary>
        private string province;

        /// <summary>
        /// Attribut représentant le telephone de la caserne.
        /// </summary>
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
