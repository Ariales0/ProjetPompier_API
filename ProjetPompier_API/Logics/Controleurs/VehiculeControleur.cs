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

       public List<VehiculeDTO> ObtenirListeVehicule(string nomCaserne)
        {
            List<VehiculeDTO> listeVehiculeDTO = VehiculeRepository.Instance.ObtenirListeVehicules(nomCaserne);
            List<VehiculeModel> listeVehicule = new List<VehiculeModel>();

            foreach (VehiculeDTO vehiculeDTO in listeVehiculeDTO)
            {
                listeVehicule.Add(new VehiculeModel(vehiculeDTO.Vin, vehiculeDTO.TypeVehicule, vehiculeDTO.Marque, vehiculeDTO.Modele, vehiculeDTO.Annee));
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

        public VehiculeDTO ObtenirVehicule(string nomCaserne, string vinVehicule)
        {
            VehiculeDTO vehiculeDTO = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vinVehicule);
            VehiculeModel vehicule = new VehiculeModel(vehiculeDTO.Vin, vehiculeDTO.TypeVehicule, vehiculeDTO.Marque, vehiculeDTO.Modele, vehiculeDTO.Annee);
            return new VehiculeDTO(vehicule);
        }

        public void AjouterVehicule(string nomCaserne, int codeVehicule ,VehiculeDTO vehicule)
        {
            
            bool OK = false;
            try
            {
                VehiculeRepository.Instance.ObtenirIdVehicule(nomCaserne, vehicule.Vin);
            }
            catch (Exception)
            {
                OK = true;
            }

            if (OK)
            {
                VehiculeRepository.Instance.AjouterVehicule(nomCaserne, codeVehicule, vehicule);
            }
            else
            {
                throw new Exception("Erreur lors de l'ajout du véhicule, le véhicule existe déjà.");
            }
        }

        public void ModifierVehicule(string nomCaserne, int codeVehicule,VehiculeDTO vehicule)
        {
            VehiculeDTO vehiculeDB = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vehicule.Vin);

            if (vehicule.Marque != vehiculeDB.Marque || vehicule.Modele != vehiculeDB.Modele || vehicule.Annee != vehiculeDB.Annee)
            {
                VehiculeRepository.Instance.ModifierVehicule(nomCaserne, codeVehicule, vehicule);
            }
            else
            {
                throw new Exception("Erreur lors de la modification du véhicule, les informations sont identiques.");
            }
        }

        public void SupprimerVehicule(string nomCaserne, string vinVehicule)
        {
            if (VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vinVehicule) == null)
            {
                throw new Exception("Erreur lors de la suppression du véhicule, le véhicule n'existe pas.");
            }
            VehiculeRepository.Instance.SupprimerVehicule(nomCaserne, vinVehicule);
        }

        public void ViderListeVehicules(string nomCaserne)
        {
            if (!VehiculeRepository.Instance.ObtenirListeVehicules(nomCaserne).Any())
            {
                throw new Exception("Erreur lors de la suppression de la liste de véhicules, la liste est déjà vide.");
            }
            VehiculeRepository.Instance.ViderListeVehicules(nomCaserne);
        }
        

        #endregion Methodes

    }
}
