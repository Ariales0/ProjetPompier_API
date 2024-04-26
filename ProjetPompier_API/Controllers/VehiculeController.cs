using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

namespace ProjetPompier_API.Controllers
{
    public class VehiculeController : Controller
    {

        /// <summary>
        /// Méthode de service GET ObtenirListeVehicule
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <returns>Retourne la listes des vehicules</returns>
        [Route("Vehicule/ObtenirListeVehicule")]
        [HttpGet]
        public List<VehiculeDTO> ObtenirListeVehicule(string nomCaserne)
        {
            return VehiculeControleur.Instance.ObtenirListeVehicule(nomCaserne);
        }

        /// <summary>
        /// Methode de service GET ObtenirVehicule
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <returns>Retourne un vehicule</returns>
        [Route("Vehicule/ObtenirVehicule")]
        [HttpGet]
        public VehiculeDTO ObtenirVehicule([FromQuery] string nomCaserne,[FromQuery] string vinVehicule)
        {
            return VehiculeControleur.Instance.ObtenirVehicule(nomCaserne, vinVehicule);
        }

        /// <summary>
        /// Methode de service POST AjouterVehicule
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehicule">Le DTO du vehicule</param>
        [Route("Vehicule/AjouterVehicule")]
        [HttpPost]
        public void AjouterVehicule([FromQuery] string nomCaserne, [FromBody] VehiculeDTO vehicule)
        {
            VehiculeControleur.Instance.AjouterVehicule(nomCaserne,  vehicule);
        }

        /// <summary>
        /// Methode de service POST ModifierVehicule
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehicule">Le DTO du vehicule</param>
        [Route("Vehicule/ModifierVehicule")]
        [HttpPost]
        public void ModifierVehicule([FromQuery] string nomCaserne, [FromBody] VehiculeDTO vehicule)
        {
            VehiculeControleur.Instance.ModifierVehicule(nomCaserne, vehicule);
        }

        /// <summary>
        /// Methode de service POST SupprimerVehicule
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        [Route("Vehicule/SupprimerVehicule")]
        [HttpPost]
        public void SupprimerVehicule([FromQuery] string nomCaserne, [FromQuery] string vinVehicule)
        {
            VehiculeControleur.Instance.SupprimerVehicule(nomCaserne, vinVehicule);
        }

        /// <summary>
        /// Methode de service POST ViderListeVehicules
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        [Route("Vehicule/ViderListeVehicules")]
        [HttpPost]
        public void ViderListeVehicules([FromQuery] string nomCaserne)
        {
            VehiculeControleur.Instance.ViderListeVehicules(nomCaserne);
        }

    }
}
