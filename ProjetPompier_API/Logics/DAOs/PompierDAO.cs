using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.Exceptions;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe représentant le répository d'un pompier.
    /// </summary>
    public class PompierRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static PompierRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static PompierRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new PompierRepository();
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
        private PompierRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des pompiers.
        /// </summary>
        /// <returns>Liste des pompiers.</returns>
        public List<PompierDTO> ObtenirListePompier(string nomCaserne)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                "   FROM T_Pompiers WHERE IdCaserne=@id ", connexion);

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);

            idParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(idParam);

            List<PompierDTO> liste = new List<PompierDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PompierDTO pompierDTO = new PompierDTO(reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                    liste.Add(pompierDTO);
                }
                reader.Close();
                return liste;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des pompiers...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

		public int ObtenirIdPompier(int matricule)
		{
			SqlCommand command = new SqlCommand(" SELECT IdPompier " +
												"   FROM T_Pompiers " +
												"  WHERE Matricule= @matricule ", connexion);

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);

			matriculeParam.Value = matricule;

			command.Parameters.Add(matriculeParam);

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
				throw new Exception("Erreur lors de l'obtention d'un id d'une caserne par son matricule...", ex);
			}
			finally
			{
				FermerConnexion();
			}
		}


		public PompierDTO ObtenirPompier(int matricule)
		{
			SqlCommand command = new SqlCommand(" SELECT * " +
												" FROM T_Pompiers " +
												" WHERE Matricule = @matricule ", connexion);

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);

			matriculeParam.Value = matricule;

			command.Parameters.Add(matriculeParam);

			CaserneDTO uneCaserne;

			try
			{
				OuvrirConnexion();
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				PompierDTO unPompier = new PompierDTO(reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
				reader.Close();
				return unPompier;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de l'obtention d'un pompier par son matricule...", ex);
			}
			finally
			{
				FermerConnexion();
			}
		}

		public bool AjouterPompier(int idCaserne, PompierDTO pompierDTO)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " INSERT INTO T_Pompiers (Matricule, Grade, Nom, Prenom, IdCaserne) " +
								  " VALUES (@matricule, @grade, @nom, @prenom, @idCaserne) ";

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.Int);
			SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 100);
			SqlParameter prenomParam = new SqlParameter("@prenom", SqlDbType.VarChar, 100);
			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			nomParam.Value = pompierDTO.Nom;
			prenomParam.Value = pompierDTO.Prenom;
			gradeParam.Value = pompierDTO.Grade;
			matriculeParam.Value = pompierDTO.Matricule;
			idCaserneParam.Value = idCaserne;

			command.Parameters.Add(nomParam);
			command.Parameters.Add(prenomParam);
			command.Parameters.Add(gradeParam);
			command.Parameters.Add(matriculeParam);
			command.Parameters.Add(idCaserneParam);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				throw new DBUniqueException("Erreur lors de l'ajout d'un pompier...", ex);
				return false;
			}
			finally
			{
				FermerConnexion();
			}
		}


		public bool ModifierPompier(PompierDTO pompierDTO)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " UPDATE T_Pompiers " +
									 " SET Grade = @grade, " +
									 "     Nom = @nom, " +
									 "     Prenom = @prenom " +
								   " WHERE Matricule = @matricule ";

			SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.Int);
			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 100);
			SqlParameter prenomParam = new SqlParameter("@prenom", SqlDbType.VarChar, 100);

			nomParam.Value = pompierDTO.Nom;
			prenomParam.Value = pompierDTO.Prenom;
			gradeParam.Value = pompierDTO.Grade;
			matriculeParam.Value = pompierDTO.Matricule;

			command.Parameters.Add(nomParam);
			command.Parameters.Add(prenomParam);
			command.Parameters.Add(gradeParam);
			command.Parameters.Add(matriculeParam);

			try
			{
				OuvrirConnexion();
				command.Prepare();
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la modification d'un pompier...", ex);
			}
			finally
			{
				FermerConnexion();
			}

			return true;
		}


		public bool SupprimerPompier(int matricule)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE " +
									" FROM T_Pompiers " +
								   " WHERE Matricule = @matricule ";

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);

			matriculeParam.Value = matricule;

			command.Parameters.Add(matriculeParam);

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
					throw new DBRelationException("Impossible de supprimer le pompier.", e);
				}
				else throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la supression d'un pompier...", ex);
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
		public void ViderListePompier(string nomCaserne)
		{

			int idCaserne = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE FROM T_Pompiers WHERE IdCaserne = @idCaserne";

			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			idCaserneParam.Value = idCaserne;

			command.Parameters.Add(idCaserneParam);

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
					throw new DBRelationException("Impossible de supprimer les pompiers.", e);
				}
				else throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erreur lors de la supression des pompiers...", ex);
			}

			finally
			{
				FermerConnexion();
			}
		}




		#endregion


	}
}
