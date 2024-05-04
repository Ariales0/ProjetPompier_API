using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API des types d'intervention.
    /// </summary>
    public class TypesInterventionController : Controller
    {
        /// <summary>
        /// Méthode de service GET ObtenirListeTypesIntervention
        /// </summary>
        /// <returns>Retourne la liste des types d'intervention</returns>
        [Route("TypesIntervention/ObtenirListeTypesIntervention")]
        [HttpGet]
        public List<TypeInterventionDTO> ObtenirListeTypesIntervention()
        {
            return TypesInterventionRepository.Instance.ObtenirListeTypesIntervention();
        }

        /// <summary>
        /// Méthode de service GET ObtenirTypeIntervention
        /// </summary>
        /// <param name="code">Le code du type d'intervention</param>
        /// <returns>Retourne le type d'intervention</returns>
        [Route("TypesIntervention/ObtenirTypeIntervention")]
        [HttpGet]
        public TypeInterventionDTO ObtenirTypeIntervention([FromQuery] int code)
        {
            return TypesInterventionRepository.Instance.ObtenirTypeIntervention(code);
        }

        /// <summary>
        /// Méthode de service POST AjouterTypeIntervention
        /// </summary>
        /// <param name="typeInterventionAjout">Le DTO du type d'intervention à ajouter</param>
        [Route("TypesIntervention/AjouterTypeIntervention")]
        [HttpPost]
        public void AjouterTypeIntervention([FromBody] TypeInterventionDTO typeInterventionAjout)
        {
            TypesInterventionRepository.Instance.AjouterTypeIntervention(typeInterventionAjout);
        }

        /// <summary>
        /// Methodes de service POST ModifierTypeIntervention
        /// </summary>
        /// <param name="typeInterventionModification">Le DTO du type d'intervention à modifier</param>
        [Route("TypesIntervention/ModifierTypeIntervention")]
        [HttpPost]
        public void ModifierTypeIntervention([FromBody] TypeInterventionDTO typeInterventionModification)
        {
            TypesInterventionRepository.Instance.ModifierTypeIntervention(typeInterventionModification);
        }

        /// <summary>
        /// Methodes de service POST SupprimerTypeIntervention
        /// </summary>
        /// <param name="code">Le code de type d'intervention</param>
        [Route("TypesIntervention/SupprimerTypeIntervention")]
        [HttpPost]
        public void SupprimerTypeIntervention([FromQuery] int code)
        {
            TypesInterventionRepository.Instance.SupprimerTypeIntervention(code);
        }

        /// <summary>
        /// Methode de service POST ViderListeTypesIntervention
        /// </summary>
        [Route("TypesIntervention/ViderListeTypesIntervention")]
        [HttpPost]
        public void ViderListeTypesIntervention()
        {
            TypesInterventionRepository.Instance.ViderListeTypesIntervention();
        }
    }
}
