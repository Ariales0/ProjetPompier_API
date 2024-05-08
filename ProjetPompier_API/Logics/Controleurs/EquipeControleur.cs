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
    public class EquipeControleur
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe EquipeControleur.
        /// </summary>
        private static EquipeControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static EquipeControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new EquipeControleur();
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
        private EquipeControleur() { }

        #endregion Controleurs

        #region MethodesCaserne

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des equipes d'une intervention.
        /// </summary>
        /// <returns>Liste contenant les equipes.</returns>
        public List<EquipeDTO> ObtenirListeEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention)
        {
            List<EquipeDTO> listeEquipeDTO = EquipeRepository.Instance.ObtenirListeEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention);
            List<EquipeModel> listeEquipe = new List<EquipeModel>();
            List<PompierModel> listePompierModel = new List<PompierModel>();

            foreach (EquipeDTO uneEquipe in listeEquipeDTO)
            {
                listePompierModel.Clear();
                foreach (PompierDTO pompierDTO in uneEquipe.ListePompierEquipe)
                {
                    listePompierModel.Add(new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom));
                }
                

                listeEquipe.Add(new EquipeModel(
                    uneEquipe.Code,
                    listePompierModel, 
                    uneEquipe.VinVehicule
                    ));
            }

            if (listeEquipeDTO.Count == listeEquipe.Count)
                return listeEquipeDTO;
            else
                throw new Exception("Erreur lors du chargement des equipes de l'intervention.");
        }

        #endregion
    }
}
