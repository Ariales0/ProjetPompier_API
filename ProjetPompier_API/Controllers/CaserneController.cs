using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
    public class CaserneController : Controller
    {

        /// <summary>
		/// Méthode de service GET ObtenirListeCaserne
		/// </summary>
		/// <returns>List<CaserneDTO> La liste des casernes</returns>
        [Route("Caserne/ObtenirListeCaserne")]
        [HttpGet]
        public List<CaserneDTO> ObtenirListeCaserne()
        {
            return CaserneRepository.Instance.ObtenirListeCaserne();
        }

        /// <summary>
		/// Méthode de service GET ObtenirCaserne
		/// </summary>
		/// <param name="nomCaserne">Le nom de la caserne.</param>
		/// <returns>Le DTO de la caserne</returns>
        [Route("Caserne/ObtenirCaserne")]
        [HttpGet]
        public CaserneDTO ObtenirCaserne([FromQuery]string nomCaserne)
        {
            return CaserneRepository.Instance.ObtenirCaserne(nomCaserne);
        }

        /// <summary>
		/// Méthode de service POST AjouterCaserne
		/// </summary>
		/// <param name="caserneDTO">Le DTO de la caserne.</param>
        [Route("Caserne/AjouterCaserne")]
        [HttpPost]
        public void AjouterCaserne ([FromBody]CaserneDTO caserneDTO) 
        { 
            CaserneRepository.Instance.AjouterCaserne(caserneDTO);
        }

        /// <summary>
		/// Méthode de service POST ModifierCaserne
		/// </summary>
		/// <param name="caserneDTO">Le DTO de la caserne.</param>
        [Route("Caserne/ModifierCaserne")]
        [HttpPost]
        public void ModifierCaserne([FromBody] CaserneDTO caserneDTO)
        {
            CaserneRepository.Instance.ModifierCaserne(caserneDTO);
        }

        /// <summary>
		/// Méthode de service POST SupprimerCaserne
		/// </summary>
		/// <param name="nomCaserne">Le nom de la caserne.</param>
        [Route("Caserne/SupprimerCaserne")]
        [HttpPost]
        public void SupprimerCaserne([FromQuery] string nomCaserne)
        {
            CaserneRepository.Instance.SupprimerCaserne(nomCaserne);
        }

        /// <summary>
		/// Méthode de service POST ViderListeCaserne
		/// </summary>
        [Route("Caserne/ViderListeCaserne")]
        [HttpPost]
        public void ViderListeCaserne()
        {
            CaserneRepository.Instance.ViderListeCaserne();
        }
    }
}
