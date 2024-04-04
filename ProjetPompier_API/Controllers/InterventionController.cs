using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API'.
    /// </summary>
    [ApiController]
    public class InterventionController : Controller
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierController.
        /// </summary>
        private static InterventionController instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static InterventionController Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new InterventionController();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        /// <summary>
		/// Méthode de service GET ObtenirListeFicheIntervention
		/// </summary>
		/// <returns>List<PompierDTO> La liste des fiches d'intervention d'une caserne</returns>
        [Route("Intervention/ObtenirListeFicheIntervention")]
        [HttpGet]
        public List<FicheInterventionDTO> ObtenirListeFicheIntervention(string nomCaserne, int matriceCapitaine)
        {
            return InterventionControleur.Instance.ObtenirListeFicheIntervention(nomCaserne, matriceCapitaine);
        }

        /// <summary>
        /// Méthode de service permettant d'ouvrir une fiche d'intervention'.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="dateTemps">Date et heure le l'intervention</param>
        /// <param name="typeIntervention">Type d'intervention</param>
        /// <param name="adresse">Adresse de l'intervention</param>
        /// <param name="resume">Resumé de l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du Capitaine de l'intervention</param>
        /// [Route("Intervention/ObtenirListeFicheIntervention")]
        [HttpPost]
        public void OuvrirFicheIntervention(string nomCaserne, string dateTemps, string typeIntervention, string adresse, string resume, int matriculeCapitaine)
        {
            InterventionControleur.Instance.OuvrirFicheIntervention(nomCaserne, dateTemps, typeIntervention, adresse, resume, matriculeCapitaine);
        }

    }
}
