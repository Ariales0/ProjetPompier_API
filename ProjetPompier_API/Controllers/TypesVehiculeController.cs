    using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

namespace ProjetPompier_API.Controllers
{
    public class TypesVehiculeController : Controller
    {
        /// <summary>
        /// Méthode de service GET ObtenirListeTypesVehicule
        /// </summary>
        /// <returns>Retourne la liste des types de vehicule</returns>
        [Route("TypesVehicule/ObtenirListeTypesVehicule")]
        [HttpGet]
        public List<TypeVehiculeDTO> ObtenirListeTypesVehicule()
        {
            return TypesVehiculeControleur.Instance.ObtenirListeTypesVehicule();
        }

        /// <summary>
        /// Méthode de service GET ObtenirTypesVehicule
        /// </summary>
        /// <param name="code">Le code du type de vehicule</param>
        /// <returns>Retourne le type de vehicule</returns>
        [Route("TypesVehicule/ObtenirTypesVehicule")]
        [HttpGet]
        public TypeVehiculeDTO ObtenirTypesVehicule([FromQuery] int code)
        {
            return TypesVehiculeControleur.Instance.ObtenirTypeVehicule(code);
        }

        /// <summary>
        /// Méthode de service POST AjouterTypesVehicule
        /// </summary>
        /// <param name="typeVehicule">Le DTO du typeVehicule</param>
        [Route("TypesVehicule/AjouterTypesVehicule")]
        [HttpPost]
        public void AjouterTypesVehicule([FromBody] TypeVehiculeDTO typeVehicule)
        {
            TypesVehiculeControleur.Instance.AjouterTypeVehicule(typeVehicule);
        }

        /// <summary>
        /// Methodes de service POST ModifierTypesVehicule
        /// </summary>
        /// <param name="typeVehicule">Le DTO du typeVehicule</param>
        [Route("TypesVehicule/ModifierTypesVehicule")]
        [HttpPost]
        public void ModifierTypesVehicule([FromBody] TypeVehiculeDTO typeVehicule)
        {
            TypesVehiculeControleur.Instance.ModifierTypeVehicule(typeVehicule);
        }

        /// <summary>
        /// Methodes de service POST SupprimerTypesVehicule
        /// </summary>
        /// <param name="code">Le code de type vehicule</param>
        [Route("TypesVehicule/SupprimerTypesVehicule")]
        [HttpPost]
        public void SupprimerTypesVehicule([FromQuery] int code)
        {
            TypesVehiculeControleur.Instance.SupprimerTypeVehicule(code);
        }

        /// <summary>
        /// Methode de service POST ViderListeTypesVehicules
        /// </summary>
        [Route("TypesVehicule/ViderListeTypesVehicules")]
        [HttpPost]
        public void ViderListeTypesVehicules()
        {
            TypesVehiculeControleur.Instance.ViderListeTypesVehicule();
        }

    }
}
