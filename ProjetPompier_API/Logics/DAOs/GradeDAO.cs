﻿using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Exceptions;


/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe représentant le répository d'un grade.
    /// </summary>
    public class GradeRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static GradeRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static GradeRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new GradeRepository();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur privée du repository.
        /// </summary>
        private GradeRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des grades.
        /// </summary>
        /// <returns>Liste des grades.</returns>
        public List<GradeDTO> ObtenirListeGrade()
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                "   FROM T_Grades ", connexion);

            List<GradeDTO> listeGrade = new List<GradeDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GradeDTO gradeDTO = new GradeDTO(reader.GetString(1));
                    listeGrade.Add(gradeDTO);
                }
                reader.Close();
                return listeGrade;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des grades...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

		/// <summary>
		/// Méthode de service permettant d'obtenir un grade par sa description.
		/// </summary>
		/// <param name="description">La description du grade</param>
		/// <returns>Le DTO du grade</returns>
		public GradeDTO ObtenirGrade(string description)
		{
			SqlCommand command = new SqlCommand(" SELECT * " +
												" FROM T_Grades " +
												" WHERE Description = @description", connexion);

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar, 50);
			descriptionParam.Value = description;
			command.Parameters.Add(descriptionParam);

			GradeDTO unGradeDTO;

			try
			{
				OuvrirConnexion();
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				GradeDTO unGrade = new GradeDTO(reader.GetString(1));
				reader.Close();
				return unGrade;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de l'obtention d'un grade par sa description...", ex);
			}
			finally
			{
				FermerConnexion();
			}
		}

		/// <summary>
		/// Méthode de service permettant d'ajouter un grade.
		/// </summary>
		/// <param name="gradeDTO">L'objet gradeDTO pour ajout</param>
		/// <returns></returns>
		public bool AjouterGrade(GradeDTO gradeDTO)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " INSERT INTO T_Grades (Description) " +
								  " VALUES (@description) ";

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar, 50);
			descriptionParam.Value = gradeDTO.Description;
			command.Parameters.Add(descriptionParam);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				throw new DBUniqueException("Erreur lors de l'ajout d'un grade...", ex);
				return false;
			}
			finally
			{
				FermerConnexion();
			}
		}

		/// <summary>
		/// Méthode de service permettant de modifier un grade.
		/// </summary>
		/// <param name="gradeDTO">L'objet gradeDTO à modifier</param>
		/// <param name="nouvelleDescription">Nouvelle description pour le grade à modifier</param>
		/// <returns></returns>
		public bool ModifierGrade(GradeDTO gradeDTO, string nouvelleDescription)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " UPDATE T_Grades " +
                                     " SET Description = @nouvelleDescription " +
								   " WHERE IdGrade = @description ";

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar,50);
            SqlParameter nouvelleDescriptionParam = new SqlParameter("@nouvelleDescription", SqlDbType.VarChar, 50);

            nouvelleDescriptionParam.Value = nouvelleDescription;
			descriptionParam.Value = gradeDTO.Description ;

			command.Parameters.Add(descriptionParam);
			command.Parameters.Add(nouvelleDescriptionParam);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la modification d'un grade...", ex);
			}
			finally
			{
				FermerConnexion();
			}
			return true;
		}

		/// <summary>
		/// Méthode de service permettant de supprimer un grade.
		/// </summary>
		/// <param name="description">Description du grade à  supprimer</param>
		/// <returns></returns>
		public bool SupprimerGrade(string description)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE " +
									" FROM T_Grades " +
								   " WHERE Description = @descriptionGrade";

			SqlParameter descriptionGradeParam = new SqlParameter("@descriptionGrade", SqlDbType.VarChar, 50);
			descriptionGradeParam.Value = description;
			command.Parameters.Add(descriptionGradeParam);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				if (e.Number == 547)
				{
					throw new DBRelationException("Impossible de supprimer le grade.", e);
				}
				else throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la supression d'un grade...", ex);
			}

			finally
			{
				FermerConnexion();
			}
			return true;
		}

		/// <summary>
		/// Méthode de service permettant de vider la liste des grades.
		/// </summary>
		public void ViderListeGrade()
		{
			SqlCommand command = new SqlCommand(" DELETE FROM T_Grades", connexion);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				if (e.Number == 547)
				{
					throw new DBRelationException("Impossible de supprimer les grades.", e);
				}
				else throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la supression des grades...", ex);
			}

			finally
			{
				FermerConnexion();
			}
		}

        #endregion
    }
}
