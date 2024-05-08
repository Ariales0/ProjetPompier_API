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

    public class EquipeController : Controller
    {
        /// <summary>
		/// Méthode de service GET ObtenirListeEquipe
		/// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
		/// <returns>List<CaserneDTO> La liste des equipes</returns>
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
        /// <param name="vinVehiculeEquipe">Vin du véhicule utilisé par l'équipe</param>
        /// <returns></returns>
        //[Route("Equipe/ObtenirEquipe")]
        //[HttpGet]
        //public EquipeDTO ObtenirEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention, [FromQuery] string vinVehiculeEquipe)
        //{
        //    //return EquipeControleur.Instance.ObtenirEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, vinVehiculeEquipe);
        //}

        /// <summary>
        /// Méthode de service POST AjouterEquipe
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="equipeDTO">L'équipe à ajouter</param>
        //[Route("Equipe/AjouterEquipe")]
        //[HttpPost]
        //public void AjouterEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention,[FromBody] EquipeDTO equipeDTO)
        //{
        //    //return EquipeControleur.Instance.AjouterEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, equipeDTO);
        //}
    }
}
