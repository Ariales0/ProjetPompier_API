using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur des pompiers de l'API.
    /// </summary>
    public class PompierController : Controller
    {
        /// <summary>
        /// Méthode de service GET ObtenirListePompier
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="seulementCapitaine">Argument booleen qui indique si on souhaite uniquement les capitaines</param>
        /// <returns>List<PompierDTO> La liste des pompiers d'une caserne</returns>
        [Route("Pompier/ObtenirListePompier")]
        [HttpGet]
        public List<PompierDTO> ObtenirListePompier([FromQuery] string nomCaserne, [FromQuery] bool seulementCapitaine)
        {
            return PompierControleur.Instance.ObtenirListePompier(nomCaserne, seulementCapitaine);
        }

        /// <summary>
        /// Méthode de service GET ObtenirListePompierDisponible
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <returns>List<PompierDTO> La liste des pompiers disponible d'une caserne</returns>
        [Route("Pompier/ObtenirListePompierDisponible")]
        [HttpGet]
        public List<PompierDTO> ObtenirListePompierDisponible([FromQuery] string nomCaserne)
        {
            return PompierControleur.Instance.ObtenirListePompierDisponible(nomCaserne);
        }

        /// <summary>
        /// Méthode de service GET ObtenirPompier
        /// </summary>
        /// <param name="nomCaserne"></param>
        /// <param name="matriculePompier"></param>
        /// <returns>Le DTO du pompier recherché</returns>
        [Route("Pompier/ObtenirPompier")]
		[HttpGet]
		public PompierDTO ObtenirPompier([FromQuery] string nomCaserne, [FromQuery] int matriculePompier)
		{
			return PompierControleur.Instance.ObtenirPompier(matriculePompier, nomCaserne);
		}

		/// <summary>
		/// Méthode de service POST AjouterPompier
		/// </summary>
		/// <param name="nomCaserne"></param>
		/// <param name="pompierDTO"></param>
		[Route("Pompier/AjouterPompier")]
		[HttpPost]
		public void AjouterPompier([FromQuery] string nomCaserne, [FromBody] PompierDTO pompierDTO)
		{
			PompierControleur.Instance.AjouterPompier(nomCaserne, pompierDTO);
		}

		/// <summary>
		/// Méthode de service POST ModifierPompier
		/// </summary>
		/// <param name="nomCaserne"></param>
		/// <param name="pompierDTO"></param>
		[Route("Pompier/ModifierPompier")]
		[HttpPost]
		public void ModifierPompier([FromQuery] string nomCaserne, [FromBody] PompierDTO pompierDTO)
		{
			PompierControleur.Instance.ModifierPompier(pompierDTO, nomCaserne);
		}

		/// <summary>
		/// Méthode de service POST SupprimerPompier
		/// </summary>
		/// <param name="nomCaserne"></param>
		/// <param name="matriculePompier"></param>
		[Route("Pompier/SupprimerPompier")]
		[HttpPost]
		public void SupprimerPompier([FromQuery] string nomCaserne, [FromQuery] int matriculePompier)
		{
			PompierControleur.Instance.SupprimerPompier(matriculePompier, nomCaserne);
		}

		/// <summary>
		/// Méthode de service POST ViderListePompier
		/// </summary>
		[Route("Pompier/ViderListePompier")]
		[HttpPost]
		public void ViderListePompier([FromQuery] string nomCaserne)
		{
			PompierControleur.Instance.ViderListePompier(nomCaserne);
		}
	}
}
