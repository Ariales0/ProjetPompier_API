using ProjetPompier_API.Logics.DTOs;

namespace ProjetPompier_API.Logics.Models
{
    public class TypeInterventionModel
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
                string codeStr = value.ToString();
                if (codeStr.Length <= 2)
                    code = value;
                else
                    throw new Exception("Le code du type d'intervention ne doit pas contenir plus de 2 chiffres.");
            }
        }

        /// <summary>
        /// Attribute du type de véhicule.
        /// </summary>
		private string description;
        /// <summary>
        /// Propriete du type de véhicule.
        /// </summary>
		public string Description
        {
            get { return description; }
            set
            {
                if (value.Length <= 150)
                    description = value;
                else
                    throw new Exception("Le type d'intervention ne peut avoir qu'un maximum de 150 caractères.");
            }
        }
        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur par defaut.
        /// </summary>
        /// <param name="codeTypeIntervention">Le code</param>
        /// <param name="descriptionTypeIntervention">Le type</param>
        public TypeInterventionModel(int codeTypeIntervention = 00, string descriptionTypeIntervention = "")
        {
            Code = codeTypeIntervention;
            Description = descriptionTypeIntervention;
        }

        /// <summary>
        /// Constructeur a partir du DTO
        /// </summary>
        /// <param name="typeInterventionDTO">L'objet DTO</param>
        public TypeInterventionModel(TypeInterventionDTO typeInterventionDTO)
        {
            Code = typeInterventionDTO.Code;
            Description = typeInterventionDTO.Description;
        }

        #endregion Constructeurs
    }
}
