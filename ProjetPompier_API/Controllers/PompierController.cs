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
    /// Classe représentant le controleur de l'API'.
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


		[Route("Pompier/ObtenirPompier")]
		[HttpGet]
		public PompierDTO ObtenirPompier([FromQuery] int matriculePompier, [FromQuery] string nomCaserne)
		{
			return PompierControleur.Instance.ObtenirPompier(matriculePompier, nomCaserne);
		}

		
		[Route("Pompier/AjouterPompier")]
		[HttpPost]
		public void AjouterPompier([FromQuery] string nomCaserne, [FromBody] PompierDTO pompierDTO)
		{
			PompierControleur.Instance.AjouterPompier(nomCaserne, pompierDTO);
		}


		[Route("Pompier/ModifierPompier")]
		[HttpPost]
		public void ModifierPompier([FromBody] PompierDTO pompierDTO, [FromQuery] string nomCaserne)
		{
			PompierControleur.Instance.ModifierPompier(pompierDTO, nomCaserne);
		}



		[Route("Pompier/SupprimerPompier")]
		[HttpPost]
		public void SupprimerPompier([FromQuery] int matriculePompier, [FromQuery] string nomCaserne)
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
