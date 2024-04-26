namespace ProjetPompier_API.Logics.Models
{
    public class VehiculeModel
    {

        #region AttributsProprietes

        /// <summary>
        /// Attribut du vin du véhicule.
        /// </summary>
        private string vin;
        /// <summary>
        /// Propriete du vin du véhicule.
        /// </summary>
        public string Vin
        {
            get { return vin; }
            set 
            {
                if (value.Length <= 17)
                    vin = value;
                else
                    throw new Exception("Le VIN doit avoir un maximum de 17 caractères.");
            }
        }

        /// <summary>
        /// Attribut du code du véhicule.
        /// </summary>
        private int code;
        /// <summary>
        /// Propriete du code du véhicule.
        /// </summary>
        public int Code
        {
            get { return code; }
            set { code = value; }
        }


        /// <summary>
        /// Attribut de la marque du véhicule.
        /// </summary>
        private string marque;
        /// <summary>
        /// Propriete de la marque du véhicule.
        /// </summary>
        public string Marque
        {
            get { return marque; }
            set
            {
                if (value.Length <= 30)
                    marque = value;
                else
                    throw new Exception("La marque du véhicule doit avoir un maximum de 100 caractères.");
            }
        }

        /// <summary>
        /// Attribute du modèle du véhicule.
        /// </summary>
        private string modele;
        /// <summary>
        /// Propriete du modèle du véhicule.
        /// </summary>
        public string Modele
        {
            get { return modele; }
            set
            {
                if (value.Length <= 50)
                    modele = value;
                else
                    throw new Exception("Le modèle du véhicule doit avoir un maximum de 100 caractères.");
            }
        }

        /// <summary>
        /// Attribute de l'année du véhicule.
        /// </summary>
        private int annee;
        /// <summary>
        /// Propriete de l'année du véhicule.
        /// </summary>
        public int Annee
        {
            get { return annee; }
            set
            {
                if (value >= 1900 && value <= DateTime.Now.Year)
                    annee = value;
                else
                    throw new Exception("L'année du véhicule doit être entre 1900 et la date auctuelle.");
            }
        }

        #endregion 

        #region Constructeurs

        /// <summary>
        /// Constructeur par defaut.
        /// </summary>
        /// <param name="vin">Le vin</param>
        /// <param name="code">Le code de vehicule</param>
        /// <param name="marque">La marque</param>
        /// <param name="modele">Le modele</param>
        /// <param name="annee">L'annee</param>
        public VehiculeModel(string vin = "", int code = 0000, string marque = "", string modele = "", int annee = 0000)
        {
            Vin = vin;
            Code = code;
            Marque = marque;
            Modele = modele;
            Annee = annee;
        }

        #endregion 
    }
}
