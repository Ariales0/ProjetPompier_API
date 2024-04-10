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
		/// <returns>List<PompierDTO> La liste des pompiers d'une caserne</returns>
        [Route("Pompier/ObtenirListePompier")]
        [HttpGet]
        public List<PompierDTO> ObtenirListePompier(string nomCaserne, bool seulementCapitaine)
        {
            return PompierControleur.Instance.ObtenirListePompier(nomCaserne, seulementCapitaine);
        }

    }
}
