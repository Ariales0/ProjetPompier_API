using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

namespace ProjetPompier_API.Logics.Controleurs
{
    public class VehiculeControleur
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe VehiculeControleur.
        /// </summary>
        private static VehiculeControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static VehiculeControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new VehiculeControleur();
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
        private VehiculeControleur() { }

        #endregion Controleurs

        #region Methodes

        /// <summary>
        /// Obtenir la liste des véhicules d'une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <returns>Retourne la liste des vehicules</returns>
        /// <exception cref="Exception"></exception>
       public List<VehiculeDTO> ObtenirListeVehicule(string nomCaserne, bool disponibleSeulement)
        {
            List<VehiculeDTO> listeVehiculeDTO = VehiculeRepository.Instance.ObtenirListeVehicules(nomCaserne, disponibleSeulement);
            List<VehiculeModel> listeVehicule = new List<VehiculeModel>();

            foreach (VehiculeDTO vehiculeDTO in listeVehiculeDTO)
            {
                listeVehicule.Add(new VehiculeModel(vehiculeDTO.Vin, vehiculeDTO.Code , vehiculeDTO.Marque, vehiculeDTO.Modele, vehiculeDTO.Annee));
            }
            
            if (listeVehiculeDTO.Count == listeVehicule.Count)
            {
                return listeVehiculeDTO;
            }
            else
            {
                throw new Exception("Erreur lors de la conversion de la liste de DTO en liste de Model.");
            }
       }

        /// <summary>
        /// Obtenir un véhicule d'une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <returns>Retourne le vehicule</returns>
        public VehiculeDTO ObtenirVehicule(string nomCaserne, string vinVehicule)
        {
            VehiculeDTO vehiculeDTO = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vinVehicule);
            VehiculeModel vehicule = new VehiculeModel(vehiculeDTO.Vin, vehiculeDTO.Code, vehiculeDTO.Marque, vehiculeDTO.Modele, vehiculeDTO.Annee);
            return new VehiculeDTO(vehicule);
        }

        /// <summary>
        /// Methode permettant d'ajouter un vehicule à une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehicule">Le DTO du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void AjouterVehicule(string nomCaserne, VehiculeDTO vehicule)
        {
            try
            {
                VehiculeRepository.Instance.AjouterVehicule(nomCaserne, vehicule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Methodes permettant de modifier un vehicule d'une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehicule">Le DTO du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void ModifierVehicule(string nomCaserne, VehiculeDTO vehicule)
        {
            VehiculeDTO vehiculeDB = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vehicule.Vin);

            if (vehicule.Marque != vehiculeDB.Marque || vehicule.Code != vehiculeDB.Code || vehicule.Modele != vehiculeDB.Modele || vehicule.Annee != vehiculeDB.Annee)
            {
                VehiculeRepository.Instance.ModifierVehicule(nomCaserne, vehicule);
            }
            else
            {
                throw new Exception("Erreur lors de la modification du véhicule, les informations sont identiques.");
            }
        }

        /// <summary>
        /// methode permettant de supprimer un vehicule d'une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void SupprimerVehicule(string nomCaserne, string vinVehicule)
        {
            if (VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vinVehicule) == null)
            {
                throw new Exception("Erreur lors de la suppression du véhicule, le véhicule n'existe pas.");
            }
            VehiculeRepository.Instance.SupprimerVehicule(nomCaserne, vinVehicule);
        }

        /// <summary>
        /// Methode permettant de vider la liste des vehicules d'une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <exception cref="Exception"></exception>
        public void ViderListeVehicules(string nomCaserne)
        {
            if (!VehiculeRepository.Instance.ObtenirListeVehicules(nomCaserne, false).Any())
            {
                throw new Exception("Erreur lors de la suppression de la liste de véhicules, la liste est déjà vide.");
            }
            VehiculeRepository.Instance.ViderListeVehicules(nomCaserne);
        }
        

        #endregion Methodes

    }
}
