using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API des grades d'un pompier.
    /// </summary>
    
    public class GradeController : Controller
    {
        /// <summary>
		/// Méthode de service GET ObtenirListeCaserne
		/// </summary>
		/// <returns> La liste des grades pour un pompier</returns>
        [Route("Grade/ObtenirListeGrade")]
        [HttpGet]
        public List<GradeDTO> ObtenirListeGrade()
        {
            return GradeControleur.Instance.ObtenirListeGrade();
        }

		/// <summary>
        /// Méthode de service GET ObtenirGrade
        /// </summary>
        /// <param name="description">Description du grade</param>
        /// <returns>L'objet DTO du grade</returns>
		[Route("Grade/ObtenirGrade")]
        [HttpGet]
        public GradeDTO ObtenirGrade([FromQuery]string description)
        {
            return GradeControleur.Instance.ObtenirGrade(description);
        }

        /// <summary>
        /// Méthode de service POST AjouterGrade
        /// </summary>
        /// <param name="gradeDTO">L'objet DTO du grade à ajouter</param>
        [Route("Grade/AjouterGrade")]
        [HttpPost]
        public void AjouterGrade([FromBody] GradeDTO gradeDTO)
        {
            GradeControleur.Instance.AjouterGrade(gradeDTO);
        }

        /// <summary>
        /// Méthode de service POST ModifierGrade
        /// </summary>
        /// <param name="descriptionAvantChangement">Description du grade avant modification</param>
        /// <param name="descriptionApresChangement">La nouvelle description du grade à modifier</param>
        [Route("Grade/ModifierGrade")]
        [HttpPost]
        public void ModifierGrade([FromQuery] string descriptionAvantChangement, [FromQuery] string descriptionApresChangement)
        {
			GradeControleur.Instance.ModifierGrade(descriptionAvantChangement, descriptionApresChangement);
        }

        /// <summary>
        /// Méthode de service POST SupprimerGrade
        /// </summary>
        /// <param name="description">Description du grade à supprimer</param>

        [Route("Grade/SupprimerGrade")]
        [HttpPost]
        public void SupprimerGrade([FromQuery] string description)
        {
			GradeControleur.Instance.SupprimerGrade(description);
        }

        /// <summary>
		/// Méthode de service POST ViderListeCaserne
		/// </summary>
        [Route("Grade/ViderListeGrade")]
        [HttpPost]
        public void ViderListeGrade()
        {
			GradeControleur.Instance.ViderListeGrade();
        }
    }
}
