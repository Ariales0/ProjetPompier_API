/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Models
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class GradeModel
    {
        #region AttributsProprietes
        /// <summary>
        /// Attribut représentant le matricule du pompier.
        /// </summary>
        private string description;
        /// <summary>
        /// Propriété représentant le matricule du pompier.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (value.Length <= 200)
                    description = value;
                else
                    throw new Exception("La description d'un grade doit faire 200 caractères ou moins");
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur pour le model d'un grade.
        /// </summary>
        /// <param name="uneDescription"></param>
        public GradeModel(string uneDescription = "")
        {
			Description = uneDescription;
		}

        #endregion Constructeurs
    }
}
