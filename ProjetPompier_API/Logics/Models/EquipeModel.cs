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
        private List<PompierModel> listePompierEquipe;
        /// <summary>
        /// Propriété représentant la liste des pompiers de l'équipe.
        /// </summary>
        public List<PompierModel> ListePompierEquipe
        {
            get { return listePompierEquipe; }
            set
            {
                if (value != null && value.Count >= 0)
                    listePompierEquipe = value;
                else
                    throw new Exception("Erreur en rapport avec la liste des pompiers");
            }
        }

        /// <summary>
        /// Attribut représentant le vin du  véhicule.
        /// </summary>
        private string vinVehicule;
        /// <summary>
        /// Propriété représentant le vin du véhicule.
        /// </summary>
        public string VinVehicule
        {
            get { return vinVehicule; }
            set
            {
                if (value.Length <= 17)
                    vinVehicule = value;
                else
                    throw new Exception("Le vin du véhicule ne peut contenir plus de 17 caractères.");
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur pour le model d'une équipe.
        /// </summary>
        /// <param name="codeEquipe">Le code de l'équipe</param>
        /// <param name="listePompier">La liste des pompiers de l'équipe</param>
        /// <param name="vinVehiculeEquipe">Le vin du véhicule</param>
        public EquipeModel(int codeEquipe = 0,List<PompierModel> listePompier = null, string vinVehiculeEquipe = "")
        {
			Code = codeEquipe;
            ListePompierEquipe = listePompier;
            VinVehicule = vinVehiculeEquipe;
		}

        #endregion Constructeurs
    }
}
