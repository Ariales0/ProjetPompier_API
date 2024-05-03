using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Models
{
    /// <summary>
    /// Classe représentant un model d'une équipe.
    /// </summary>
    public class EquipeModel
    {
        #region AttributsProprietes
        /// <summary>
        /// Attribut représentant le code de l'équipe.
        /// </summary>
        private int code;
        /// <summary>
        /// Propriété représentant le code de l'équipe.
        /// </summary>
        public int Code
        {
            get { return code; }
            set
            {
                string codeStr = value.ToString();
                if (codeStr.Length <= 4)
                    code = value;
                else
                    throw new Exception("Le code de l'équipe est incorrect.");
            }
        }

        /// <summary>
        /// Attribut représentant la liste des pompiers de l'équipe.
        /// </summary>
        private List<PompierDTO> listePompierEquipe;
        /// <summary>
        /// Propriété représentant la liste des pompiers de l'équipe.
        /// </summary>
        public List<PompierDTO> ListePompierEquipe
        {
            get { return listePompierEquipe; }
            set
            {
                if (value != null && value.Count > 0)
                    listePompierEquipe = value;
                else
                    throw new Exception("La liste de l'équipe est vide ou inexistante.");
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur pour le model d'une équipe.
        /// </summary>
        /// <param name="codeEquipe">Le code de l'équipe</param>
        /// <param name="listePompier">La liste des pompiers de l'équipe</param>
        public EquipeModel(int codeEquipe = 0,List<PompierDTO> listePompier = null)
        {
			Code = codeEquipe;
            ListePompierEquipe = listePompier;
		}

        #endregion Constructeurs
    }
}
