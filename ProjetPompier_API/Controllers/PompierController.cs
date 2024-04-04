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
    [ApiController]
    public class PompierController : Controller
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierController.
        /// </summary>
        private static PompierController instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static PompierController Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new PompierController();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

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
