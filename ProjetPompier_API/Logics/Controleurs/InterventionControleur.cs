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
    public class InterventionControleur 
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierControleur.
        /// </summary>
        private static InterventionControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static InterventionControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new InterventionControleur();
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
        private InterventionControleur() { }

        #endregion Controleurs

        #region MethodesCaserne

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des pompiers.
        /// </summary>
        /// <returns>Liste contenant les pompiers d'une caserne.</returns>
        public List<FicheInterventionDTO> ObtenirListeFicheIntervention(string nomCaserne, int matriculeCapitaine)
        {
            List<FicheInterventionDTO> listeFicheInterventionDTO = InterventionRepository.Instance.ObtenirListeFicheIntervention(nomCaserne, matriculeCapitaine);
            List<FicheInterventionModel> listeFicheIntervention = new List<FicheInterventionModel>();

            foreach (FicheInterventionDTO ficheIntervention in listeFicheInterventionDTO)
            {
                listeFicheIntervention.Add(new FicheInterventionModel(ficheIntervention.DateDebut, ficheIntervention.DateFin, ficheIntervention.Adresse, ficheIntervention.TypeIntervention, ficheIntervention.Resume, ficheIntervention.MatriculeCapitaine));
            }

            if (listeFicheIntervention.Count == listeFicheInterventionDTO.Count)
                return listeFicheInterventionDTO;
            else
                throw new Exception("Erreur lors du chargement des fiches d'intevention de la caserne et du capitaine, problème avec l'intégrité des données de la base de données.");
        }

        public FicheInterventionDTO ObtenirFicheIntevention(string nomCaserne, int matriculeCapitaine, string dateIntervention)
        {
            FicheInterventionDTO ficheInterventionDTO = InterventionRepository.Instance.ObtenirFicheIntervention(nomCaserne, matriculeCapitaine, dateIntervention);
            FicheInterventionModel ficheIntervention = new FicheInterventionModel(ficheInterventionDTO.DateDebut, ficheInterventionDTO.DateFin, ficheInterventionDTO.Adresse, ficheInterventionDTO.TypeIntervention, ficheInterventionDTO.Resume, ficheInterventionDTO.MatriculeCapitaine);
            return new FicheInterventionDTO(ficheIntervention);
        }

        /// <summary>
        /// Méthode de service permettant d'ouvrir une fiche d'intervention dans une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="fiche">DTO de l'intervention</param>
        public void OuvrirFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            try
            {
                FicheInterventionModel uneFicheIntervention = new FicheInterventionModel(fiche.DateDebut, fiche.DateFin, fiche.Adresse, fiche.TypeIntervention, fiche.Resume, fiche.MatriculeCapitaine);
                InterventionRepository.Instance.OuvrirFicheIntervention(nomCaserne, fiche);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur - Une fiche d'intervention existe deja a cette date.");
            }
        }

        public void ModifierFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            try
            {
                FicheInterventionModel uneFicheIntervention = new FicheInterventionModel(fiche.DateDebut, fiche.DateFin, fiche.Adresse, fiche.TypeIntervention, fiche.Resume, fiche.MatriculeCapitaine);
                InterventionRepository.Instance.ModifierIntervention(nomCaserne, fiche);
            }
            catch (Exception e)
            {
                throw new Exception("Erreur - Une fiche d'intervention existe deja a cette date.");
            }
        }

        public void FermerFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            if (fiche.DateFin == null)
            {
                throw new Exception("Erreur - La date de fin de l'intervention est obligatoire.");
            }
            else
            {
                try
                {
                    FicheInterventionModel uneFicheIntervention = new FicheInterventionModel(fiche.DateDebut, fiche.DateFin, fiche.Adresse, fiche.TypeIntervention, fiche.Resume, fiche.MatriculeCapitaine);
                    InterventionRepository.Instance.FermerFicheIntervention(nomCaserne, fiche);
                }
                catch (Exception e)
                {
                    throw new Exception("Erreur - La fiche est déja fermer.");
                }
            }

        }

        #endregion MethodesCaserne
    }
}
