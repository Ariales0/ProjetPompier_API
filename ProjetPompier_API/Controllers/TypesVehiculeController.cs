    using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

namespace ProjetPompier_API.Controllers
{
    public class TypesVehiculeController : Controller
    {
        [Route("TypesVehicule/ObtenirListeTypesVehicule")]
        [HttpGet]
        public List<TypesVehiculeDTO> ObtenirListeTypesVehicule()
        {
            return TypesVehiculeControleur.Instance.ObtenirListeTypesVehicule();
        }

        [Route("TypesVehicule/ObtenirTypesVehicule")]
        [HttpGet]
        public TypesVehiculeDTO ObtenirTypesVehicule([FromQuery] int code)
        {
            return TypesVehiculeControleur.Instance.ObtenirTypeVehicule(code);
        }

        [Route("TypesVehicule/AjouterTypesVehicule")]
        [HttpPost]
        public void AjouterTypesVehicule([FromBody] TypesVehiculeDTO typeVehicule)
        {
            TypesVehiculeControleur.Instance.AjouterTypeVehicule(typeVehicule);
        }

        [Route("TypesVehicule/ModifierTypesVehicule")]
        [HttpPost]
        public void ModifierTypesVehicule([FromBody] TypesVehiculeDTO typeVehicule)
        {
            TypesVehiculeControleur.Instance.ModifierTypeVehicule(typeVehicule);
        }

        [Route("TypesVehicule/SupprimerTypesVehicule")]
        [HttpPost]
        public void SupprimerTypesVehicule([FromQuery] int code)
        {
            TypesVehiculeControleur.Instance.SupprimerTypeVehicule(code);
        }

        [Route("TypesVehicule/ViderListeTypesVehicules")]
        [HttpPost]
        public void ViderListeTypesVehicules()
        {
            TypesVehiculeControleur.Instance.ViderListeTypesVehicule();
        }

    }
}
