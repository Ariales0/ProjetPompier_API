using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API des équipes d'intervention.
    /// </summary>

    public class EquipeController : Controller
    {
        /// <summary>
		/// Méthode de service GET ObtenirListeEquipe
		/// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
		/// <returns> La liste des équipes</returns>
        [Route("Equipe/ObtenirListeEquipe")]
        [HttpGet]
        public List<EquipeDTO> ObtenirListeEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention)
        {
            return EquipeControleur.Instance.ObtenirListeEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention);
        }

        /// <summary>
        /// Méthode de service GET ObtenirEquipe
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="codeEquipe">Vin du véhicule utilisé par l'équipe</param>
        /// <returns>Le DTO de l'équipe recherchée</returns>
        [Route("Equipe/ObtenirEquipe")]
        [HttpGet]
        public EquipeDTO ObtenirEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention, [FromQuery] int codeEquipe)
        {
            return EquipeControleur.Instance.ObtenirEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipe);
        }

        /// <summary>
        /// Méthode de service POST AjouterEquipe
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="equipeDTO">L'équipe à ajouter avec code queleconque (On ne regarde pas le code de l'objet ici)</param>
        [Route("Equipe/AjouterEquipe")]
        [HttpPost]
        public void AjouterEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention, [FromBody] EquipeDTO equipeDTO)
        {
            EquipeControleur.Instance.AjouterEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, equipeDTO);
        }

        /// <summary>
        /// Méthode de service POST AjouterPompierEquipe
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="matriculePompier">Le matricule du pompier à ajouter </param>
        /// <param name="vinVehicule">Vin du véhicule de l'équipe</param>
        [Route("Equipe/AjouterPompierEquipe")]
        [HttpPost]
        public void AjouterPompierEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention, [FromQuery] string vinVehicule, [FromQuery] int matriculePompier)
        {
            EquipeControleur.Instance.AjouterPompierEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, vinVehicule, matriculePompier);
        }
    }
}
