using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

namespace ProjetPompier_API.Controllers
{
    public class VehiculeController : Controller
    {
        [Route("Vehicule/ObtenirListeVehicule")]
        [HttpGet]
        public List<VehiculeDTO> ObtenirListeVehicule(string nomCaserne)
        {
            return VehiculeControleur.Instance.ObtenirListeVehicule(nomCaserne);
        }

        [Route("Vehicule/ObtenirVehicule")]
        [HttpGet]
        public VehiculeDTO ObtenirVehicule([FromQuery] string nomCaserne,[FromQuery] string vinVehicule)
        {
            return VehiculeControleur.Instance.ObtenirVehicule(nomCaserne, vinVehicule);
        }

        [Route("Vehicule/AjouterVehicule")]
        [HttpPost]
        public void AjouterVehicule([FromQuery] string nomCaserne, [FromQuery] int codeVehicule,[FromBody] VehiculeDTO vehicule)
        {
            VehiculeControleur.Instance.AjouterVehicule(nomCaserne, codeVehicule, vehicule);
        }

        [Route("Vehicule/ModifierVehicule")]
        [HttpPost]
        public void ModifierVehicule([FromQuery] string nomCaserne, [FromQuery] int codeVehicule , [FromBody] VehiculeDTO vehicule)
        {
            VehiculeControleur.Instance.ModifierVehicule(nomCaserne, codeVehicule, vehicule);
        }

        [Route("Vehicule/SupprimerVehicule")]
        [HttpPost]
        public void SupprimerVehicule([FromQuery] string nomCaserne, [FromQuery] string vinVehicule)
        {
            VehiculeControleur.Instance.SupprimerVehicule(nomCaserne, vinVehicule);
        }

        [Route("Vehicule/ViderListeVehicules")]
        [HttpPost]
        public void ViderListeVehicules([FromQuery] string nomCaserne)
        {
            VehiculeControleur.Instance.ViderListeVehicules(nomCaserne);
        }

    }
}
