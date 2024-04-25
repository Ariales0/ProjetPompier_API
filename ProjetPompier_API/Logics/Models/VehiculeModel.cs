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

        private int annee;
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

        #endregion 
    }
}
