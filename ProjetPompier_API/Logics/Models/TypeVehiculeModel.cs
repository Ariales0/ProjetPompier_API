namespace ProjetPompier_API.Logics.Models
{
    public class TypeVehiculeModel
    {

        #region AttributsProprietes

        /// <summary>
        /// Attribute du code du type de véhicule.
        /// </summary>
        private int code;
        /// <summary>
        /// Propriete du code du type de véhicule.
        /// </summary>
		public int Code
		{
			get { return code; }
			set 
			{
				if (value > 0)
                    code = value;
                else
                    throw new Exception("Le code du type de véhicule doit être supérieur à 0.");
			}
		}

        /// <summary>
        /// Attribute du type de véhicule.
        /// </summary>
		private string type;
        /// <summary>
        /// Propriete du type de véhicule.
        /// </summary>
		public string Type
		{
			get { return type; }
			set 
			{
				if (value.Length <= 100)
                    type = value;
                else
                    throw new Exception("Le type de véhicule doit avoir un maximum de 100 caractères.");
			}
		}

        /// <summary>
        /// attribute du nombre de personne du type de véhicule.
        /// </summary>
		private int nombrePersonne;
        /// <summary>
        /// Propriete du nombre de personne du type de véhicule.
        /// </summary>
		public int NombrePersonne
		{
            get { return nombrePersonne; }
            set
			{
                if (value > 0)
                    nombrePersonne = value;
                else
                    throw new Exception("Le nombre de personne du type de véhicule doit être supérieur à 0.");
            }
        }
        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur par defaut.
        /// </summary>
        /// <param name="code">Le code</param>
        /// <param name="type">Le type</param>
        /// <param name="personnes">Le nombre de personnes</param>
        public TypeVehiculeModel(int code = 0000, string type = "", int personnes = 0)
        {
            Code = code;
            Type = type;
            NombrePersonne = personnes;
        }

        #endregion Constructeurs
    }
}
