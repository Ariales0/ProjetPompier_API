using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

namespace ProjetPompier_API.Logics.Controleurs
{
    public class TypesInterventionControleur
    {

        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe TypesInterventionControleur.
        /// </summary>
        private static TypesInterventionControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static TypesInterventionControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new TypesInterventionControleur();
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
        private TypesInterventionControleur() { }

        #endregion Controleurs

        #region Methodes

        /// <summary>
        /// Methode permettant d'obtenir la liste des types d'intervention.
        /// </summary>
        /// <returns>Retourne la liste des types d'intervention</returns>
        public List<TypeInterventionDTO> ObtenirListeTypesIntervention()
        {
            List<TypeInterventionDTO> listeTypesInterventionDTO = TypesInterventionRepository.Instance.ObtenirListeTypesIntervention();
            List<TypeInterventionModel> listeTypesInterventionModel = new List<TypeInterventionModel>();

            foreach (TypeInterventionDTO unTypeIntervention in listeTypesInterventionDTO)
            {
                listeTypesInterventionModel.Add(new TypeInterventionModel(unTypeIntervention.Code, unTypeIntervention.Description));
            }
            try
            {
                if (listeTypesInterventionModel.Count != listeTypesInterventionDTO.Count)
                {
                    throw new Exception("Un problème semble s'être produit lors de l'obtention de la liste");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des types d'intervention.", ex);
            }


            return listeTypesInterventionDTO;
        }

        /// <summary>
        /// Methode permettant d'obtenir un type d'intervention.   
        /// </summary>
        /// <param name="code">Le code du type d'intervention</param>
        /// <returns>Retourne le type d'intervention</returns>
        public TypeInterventionDTO ObtenirTypeIntervention(int code)
        {
            TypeInterventionDTO typeInterventionDTO = new TypeInterventionDTO();
            try
            {
                typeInterventionDTO = TypesInterventionRepository.Instance.ObtenirTypeIntervention(code);
            }
            catch (Exception ex)
            {
                throw new Exception("Le type d'intervention n'a pas été trouvé.", ex);
            }

            return typeInterventionDTO;
        }

        /// <summary>
        /// Methodes permettant d'ajouter un type d'intervention.
        /// </summary>
        /// <param name="leTypeInterventionAjout">Le type d'intervention à ajouter dans la base de données</param>
        public void AjouterTypeIntervention(TypeInterventionDTO leTypeInterventionAjout)
        {
            bool OK = false;
            try
            {
                TypesInterventionRepository.Instance.ObtenirTypeIntervention(leTypeInterventionAjout.Code);
            }
            catch (Exception)
            {
                OK = true;
            }
            if (OK)
            {
                try
                {
                    TypesInterventionRepository.Instance.AjouterTypeIntervention(leTypeInterventionAjout);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Un type d'intervention existe déjà pour ce code, modifier la description de ce type d'intervention ou veilliez prendre un autre code.");
            }
        }

        /// <summary>
        /// Methode permettant de modifier un type d'intervention.
        /// </summary>
        /// <param name="leTypeInterventionModification">Le type d'intervemtion à modifier</param>
        /// <exception cref="Exception"></exception>
        public void ModifierTypeIntervention(TypeInterventionDTO leTypeInterventionModification)
        {
            TypeInterventionDTO typeInterventionDTO = TypesInterventionRepository.Instance.ObtenirTypeIntervention(leTypeInterventionModification.Code);
            if (leTypeInterventionModification.Code != typeInterventionDTO.Code || leTypeInterventionModification.Description != typeInterventionDTO.Description)
            {
                try
                {
                    TypesInterventionRepository.Instance.ModifierTypeIntervention(typeInterventionDTO);
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
            else
            {
                throw new Exception("Aucune modification n'a été apportée.");
            }
        }

        /// <summary>
        /// Methode permettant de supprimer un type d'intervention.
        /// </summary>
        /// <param name="code">Le code du type d'intervention</param>
        public void SupprimerTypeIntervention(int code)
        {
            try
            {
                TypesInterventionRepository.Instance.SupprimerTypeIntervention(code);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Methode permettant de vider la liste des types d'intervention.
        /// </summary>
        public void ViderListeTypesIntervention()
        {
            if (TypesInterventionRepository.Instance.ObtenirListeTypesIntervention().Count == 0)
            {
                throw new Exception("La liste des types de véhicule est déjà vide.");
            }
            try
            {
                TypesInterventionRepository.Instance.ViderListeTypesIntervention();
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
            
        }

        #endregion Methodes

    }
}
