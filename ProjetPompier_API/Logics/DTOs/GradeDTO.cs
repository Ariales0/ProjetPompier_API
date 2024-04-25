﻿using ProjetPompier_API.Logics.Models;
/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.DTOs
{
	/// <summary>
	/// Classe représentant le DTO d'un grade.
	/// </summary>
	public class GradeDTO
	{
		#region Proprietes

		/// <summary>
		/// Propriété représentant la description du grade.
		/// </summary>
		public string Description { get; set; }

		#endregion Proprietes

		#region Constructeurs
		/// <summary>
		/// Constructeur avec paramètres.
		/// </summary>
		/// <param name="description">Description du grade</param>
		public GradeDTO(string description = "")
		{
			Description = description;
		}

		public GradeDTO(GradeModel leGrade)
		{
			Description = leGrade.Description;
		}

		public GradeDTO() { }

		#endregion Constructeurs
	}
}