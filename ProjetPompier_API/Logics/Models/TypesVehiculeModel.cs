namespace ProjetPompier_API.Logics.Models
{
    public class TypesVehiculeModel
    {

        #region AttributsProprietes
        private int code;

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

		private string type;

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

		private int nombrePersonne;

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

        public TypesVehiculeModel(int code = 0000, string type = "", int personnes = 0)
        {
            Code = code;
            Type = type;
            NombrePersonne = personnes;
        }

        #endregion Constructeurs
    }
}
