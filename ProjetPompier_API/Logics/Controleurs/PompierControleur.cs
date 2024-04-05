using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classes de type Controleur.
/// </summary>
namespace ProjetPompier_API.Logics.Controleurs
{
    /// <summary>
    /// Classe représentant le controleur de l'application.
    /// </summary>
    public class PompierControleur 
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierControleur.
        /// </summary>
        private static PompierControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static PompierControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new PompierControleur();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Controleurs

        /// <summary>
        /// Constructeur par défaut de la classe.
        /// </summary>
        private PompierControleur() { }

        #endregion Controleurs

        #region MethodesCaserne

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des pompiers.
        /// </summary>
        /// <returns>Liste contenant les pompiers d'une caserne.</returns>
        public List<PompierDTO> ObtenirListePompier(string nomCaserne, bool seulementCapitaine)
        {
            List<PompierDTO> listePompierDTO = PompierRepository.Instance.ObtenirListePompier(nomCaserne);
            if (seulementCapitaine)
            {
                List<PompierDTO> listePompierCapitaineDTO = new List<PompierDTO>();
                List<PompierModel> listePompierCapitaine = new List<PompierModel>();

                foreach (PompierDTO pompier in listePompierDTO)
                {
                    if(pompier.Grade=="Capitaine")
                    {
                        listePompierCapitaine.Add(new PompierModel(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
                        listePompierCapitaineDTO.Add(new PompierDTO(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
                    }
                }
                if (listePompierCapitaine.Count == listePompierCapitaineDTO.Count)
                    return listePompierCapitaineDTO;
                else
                    throw new Exception("Erreur lors du chargement des capitaines de la caserne, problème avec l'intégrité des données de la base de données.");
            }
            else 
            {
                List<PompierModel> listePompier = new List<PompierModel>();

                foreach (PompierDTO pompier in listePompierDTO)
                {
                    listePompier.Add(new PompierModel(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
                }
                if (listePompier.Count == listePompierDTO.Count)
                    return listePompierDTO;
                else
                    throw new Exception("Erreur lors du chargement des pompiers de la caserne, problème avec l'intégrité des données de la base de données.");
            }   

            
        }


        #endregion MethodesCaserne
    }
}
