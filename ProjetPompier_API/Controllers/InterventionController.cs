﻿using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classes de type Controller.
/// </summary>
namespace ProjetPompier_API.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de l'API des Fiches d'intervention.
    /// </summary>
    public class InterventionController : Controller
    {
        /// <summary>
		/// Méthode de service GET ObtenirListeFicheIntervention
		/// </summary>
		/// <returns>La liste des fiches d'intervention d'une caserne</returns>
        [Route("Intervention/ObtenirListeFicheIntervention")]
        [HttpGet]
        public List<FicheInterventionDTO> ObtenirListeFicheIntervention([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine)
        {
            return InterventionControleur.Instance.ObtenirListeFicheIntervention(nomCaserne, matriculeCapitaine);
        }

        /// <summary>
		/// Méthode de service GET ObtenirFicheIntervention
		/// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
		/// <returns>La fiche d'intervention d'une caserne</returns>
        [Route("Intervention/ObtenirFicheIntervention")]
        [HttpGet]
        public FicheInterventionDTO ObtenirFicheIntervention([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebutIntervention)
        {
            return InterventionControleur.Instance.ObtenirFicheIntevention(nomCaserne, matriculeCapitaine, dateDebutIntervention);
        }

        /// <summary>
        /// Méthode de service permettant d'ouvrir une fiche d'intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="fiche">DTO de l'intervention</param>
        [Route("Intervention/OuvrirFicheIntervention")]
        [HttpPost]
        public void OuvrirFicheIntervention([FromQuery] string nomCaserne, [FromBody]  FicheInterventionDTO fiche)
        {
             InterventionControleur.Instance.OuvrirFicheIntervention(nomCaserne, fiche);
        }

        /// <summary>
        /// Méthode de service permettant de modifier une fiche d'intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="fiche">Le DTO de la fiche</param>
        [Route("Intervention/ModifierFicheIntervention")]
        [HttpPost]
        public void ModifierFicheIntervention([FromQuery] string nomCaserne, [FromBody]  FicheInterventionDTO fiche)
        {
            InterventionControleur.Instance.ModifierFicheIntervention(nomCaserne, fiche);
        }

        /// <summary>
        /// Méthode de service permettant de fermer une fiche d'intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="fiche">Le DTO de la fiche</param>
        [Route("Intervention/FermerFicheIntervention")]
        [HttpPost]
        public void FermerFicheIntervention([FromQuery] string nomCaserne, [FromBody] FicheInterventionDTO fiche)
        {
            InterventionControleur.Instance.FermerFicheIntervention(nomCaserne, fiche);
        }
    }
}
