using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.Exceptions;
using ProjetPompier_API.DTOs;

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
        /// <returns>Liste des pompiers.</returns>
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

		public int ObtenirIdGrade(string description)
		{
			SqlCommand command = new SqlCommand(" SELECT Id " +
												"   FROM T_Grades " +
												"  WHERE Description= @description ", connexion);

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar, 200);

			descriptionParam.Value = description;

			command.Parameters.Add(descriptionParam);

			int id;

			try
			{
				OuvrirConnexion();
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				id = reader.GetInt32(0);
				reader.Close();
				return id;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de l'obtention d'un id d'un grade par sa description...", ex);
			}
			finally
			{
				FermerConnexion();
			}
		}


		public GradeDTO ObtenirGrade(int id)
		{
			SqlCommand command = new SqlCommand(" SELECT * " +
												" FROM T_Grades " +
												" WHERE Id = @Id", connexion);

			SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);

			idParam.Value = id;

			command.Parameters.Add(idParam);

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
				throw new Exception("Erreur lors de l'obtention d'un grade par son Id...", ex);
			}
			finally
			{
				FermerConnexion();
			}
		}

		public bool AjouterGrade(GradeDTO gradeDTO)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " INSERT INTO T_Grades (Description) " +
								  " VALUES (@description) ";

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar, 200);
			

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


		public bool ModifierGrade(GradeDTO gradeDTO, string nouvelleDescription)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " UPDATE T_Grades " +
									 " SET Description = @description " +
								   " WHERE Id = @idGrade ";

			SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.VarChar,200);
			SqlParameter idGradeParam = new SqlParameter("@idGrade", SqlDbType.Int);

			
			idGradeParam.Value = ObtenirIdGrade(gradeDTO.Description);
			descriptionParam.Value = nouvelleDescription;


			command.Parameters.Add(descriptionParam);
			command.Parameters.Add(idGradeParam);

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


		public bool SupprimerGrade(int idGrade)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE " +
									" FROM T_Grades " +
								   " WHERE Id = @idGrade";

			SqlParameter idGradeParam = new SqlParameter("@idGrade", SqlDbType.Int);

			idGradeParam.Value = idGrade;

			command.Parameters.Add(idGradeParam);

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
		/// Méthode de service permettant de vider la liste des pompiers.
		/// </summary>
		public void ViderListeGrade()
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE FROM T_Grades";

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
