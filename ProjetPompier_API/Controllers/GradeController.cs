using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.DTOs;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API'.
    /// </summary>
    
    public class GradeController : Controller
    {

        /// <summary>
		/// Méthode de service GET ObtenirListeCaserne
		/// </summary>
		/// <returns>List<CaserneDTO> La liste des casernes</returns>
        [Route("Grade/ObtenirListeGrade")]
        [HttpGet]
        public List<GradeDTO> ObtenirListeGrade()
        {
            return GradeControleur.Instance.ObtenirListeGrade();
        }

		/// <summary>
        /// Méthode de service GET ObtenirGrade
        /// </summary>
        /// <param name="idGrade"></param>
        /// <returns></returns>
		[Route("Grade/ObtenirGrade")]
        [HttpGet]
        public GradeDTO ObtenirGrade([FromQuery]int idGrade)
        {
            return GradeControleur.Instance.ObtenirGrade(idGrade);
        }

        /// <summary>
        /// Méthode de service POST AjouterGrade
        /// </summary>
        /// <param name="gradeDTO"></param>
        [Route("Grade/AjouterGrade")]
        [HttpPost]
        public void AjouterGrade([FromBody] GradeDTO gradeDTO)
        {
            GradeControleur.Instance.AjouterGrade(gradeDTO);
        }



        /// <summary>
        /// Méthode de service POST ModifierGrade
        /// </summary>
        /// <param name="descriptionAvantChangement"></param>
        /// <param name="descriptionApresChangement"></param>
        [Route("Grade/ModifierGrade")]
        [HttpPost]
        public void ModifierGrade([FromQuery] string descriptionAvantChangement, [FromQuery] string descriptionApresChangement)
        {
			GradeControleur.Instance.ModifierGrade(descriptionAvantChangement, descriptionApresChangement);
        }

        /// <summary>
        /// Méthode de service POST SupprimerGrade
        /// </summary>
        /// <param name="idGrade"></param>

        [Route("Grade/SupprimerGrade")]
        [HttpPost]
        public void SupprimerGrade([FromQuery] int idGrade)
        {
			GradeControleur.Instance.SupprimerGrade(idGrade);
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
