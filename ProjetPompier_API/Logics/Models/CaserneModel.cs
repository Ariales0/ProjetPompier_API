/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Models
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
        /// Propriété représentant le nom de la Caserne.
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set
            {
                if (value.Length <= 100)
                    nom = value;
                else
                    throw new Exception("Le nom de la Caserne doit avoir un maximum de 100 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le adresse de la caserne.
        /// </summary>
        private string adresse;

        /// <summary>
        /// Propriété représentant l'adresse de la Caserne.
        /// </summary>
        public string Adresse
        {
            get { return adresse; }
            set
            {
                if (value.Length <= 200)
                    adresse = value;
                else
                    throw new Exception("L'adresse de la Caserne doit avoir un maximum de 200 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le ville de la caserne.
        /// </summary>
        private string ville;

        /// <summary>
        /// Propriété représentant la ville de la Caserne.
        /// </summary>
        public string Ville
        {
            get { return ville; }
            set
            {
                if (value.Length <= 100)
                    ville = value;
                else
                    throw new Exception("La ville de la Caserne doit avoir un maximum de 100 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le province de la caserne.
        /// </summary>
        private string province;

        /// <summary>
        /// Propriété représentant la province de la Caserne.
        /// </summary>
        public string Province
        {
            get { return province; }
            set
            {
                if (value.Length <= 50)
                    province = value;
                else
                    throw new Exception("La province de la Caserne doit avoir un maximum de 50 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le telephone de la caserne.
        /// </summary>
        private string telephone;

        /// <summary>
        /// Propriété représentant le telephone de la Caserne.
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set
            {
                if (value.Length <= 12)
                    telephone = value;
                else
                    throw new Exception("Le telephone de la Caserne doit avoir un maximum de 12 caractères.");
            }
        }

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
            Nom = unNom;
            Adresse = uneAdresse;
            Ville = uneVille;
            Province = uneProvince;
            Telephone = unTelephone;
        }

        #endregion Constructeurs
    }
}
