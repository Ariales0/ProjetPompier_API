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
    
    public class InterventionController : Controller
    {


        /// <summary>
        /// Méthode de service GET ObtenirListeFicheIntervention
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="matriculeCapitaine">Le matricule du capitaine</param>
        /// <returns>List<PompierDTO> La liste des fiches d'intervention d'une caserne</returns>
        [Route("Intervention/ObtenirListeFicheIntervention")]
        [HttpGet]
        public List<FicheInterventionDTO> ObtenirListeFicheIntervention(string nomCaserne, int matriculeCapitaine)
        {
            return InterventionControleur.Instance.ObtenirListeFicheIntervention(nomCaserne, matriculeCapitaine);
        }

        /// <summary>
        /// Méthode de service permettant d'ouvrir une fiche d'intervention'.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="fiche">DTO de l'intervention</param>
        [Route("Intervention/OuvrirFicheIntervention")]
        [HttpPost]
        public void OuvrirFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            InterventionControleur.Instance.OuvrirFicheIntervention(nomCaserne, fiche);
        }

    }
}
