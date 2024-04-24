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
                    PompierDTO pompierDTO = new PompierDTO(reader.GetInt32(1), GradeRepository.Instance.ObtenirGradeParId(reader.GetInt32(2)).Description, reader.GetString(3), reader.GetString(4));
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
		/// <summary>
		/// Méthode de service permettant d'obtenir le id d'un pompier par son matricule et sa caserne. 
		/// </summary>
		/// <param name="matricule"></param>
		/// <param name="nomCaserne"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public int ObtenirIdPompier(int matricule, string nomCaserne)
		{
			SqlCommand command = new SqlCommand(" SELECT IdPompier " +
												"   FROM T_Pompiers " +
												"  WHERE Matricule= @matricule AND IdCaserne= @idCaserne ", connexion);

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			matriculeParam.Value = matricule;
			idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

			command.Parameters.Add(matriculeParam);
			command.Parameters.Add(idCaserneParam);

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

		/// <summary>
		/// Méthode de service permettant d'obtenir un pompier par son matricule et sa caserne.
		/// </summary>
		/// <param name="matricule"></param>
		/// <param name="nomCaserne"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public PompierDTO ObtenirPompier(int matricule, string nomCaserne)
		{
			SqlCommand command = new SqlCommand(" SELECT * " +
												" FROM T_Pompiers " +
												" WHERE Matricule = @matricule AND IdCaserne = @idCaserne ", connexion);

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			matriculeParam.Value = matricule;
			idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

			command.Parameters.Add(matriculeParam);
			command.Parameters.Add(idCaserneParam);

			PompierDTO unPompier;

			try
			{
				OuvrirConnexion();
				SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				unPompier = new PompierDTO(reader.GetInt32(1), GradeRepository.Instance.ObtenirGradeParId(reader.GetInt32(2)).Description, reader.GetString(3), reader.GetString(4));
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

		/// <summary>
		/// Méthode de service permettant d'ajouter un pompier.
		/// </summary>
		/// <param name="idCaserne"></param>
		/// <param name="pompierDTO"></param>
		/// <returns></returns>
		/// <exception cref="DBUniqueException"></exception>
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
			gradeParam.Value = GradeRepository.Instance.ObtenirIdGrade(pompierDTO.Grade);
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

		/// <summary>
		/// Méthode de service permettant de modifier un pompier.
		/// </summary>
		/// <param name="pompierDTO"></param>
		/// <param name="nomCaserne"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public bool ModifierPompier(PompierDTO pompierDTO, string nomCaserne)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " UPDATE T_Pompiers " +
									 " SET Grade = @grade, " +
									 "     Nom = @nom, " +
									 "     Prenom = @prenom " +
								   " WHERE Matricule = @matricule AND IdCaserne = @idCaserne";

			SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.Int);
			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 100);
			SqlParameter prenomParam = new SqlParameter("@prenom", SqlDbType.VarChar, 100);
			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			nomParam.Value = pompierDTO.Nom;
			prenomParam.Value = pompierDTO.Prenom;
			gradeParam.Value = GradeRepository.Instance.ObtenirIdGrade(pompierDTO.Grade);
			matriculeParam.Value = pompierDTO.Matricule;
			idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

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

		/// <summary>
		/// Méthode de service permettant de supprimer un pompier
		/// </summary>
		/// <param name="matricule"></param>
		/// <param name="nomCaserne"></param>
		/// <returns></returns>
		/// <exception cref="DBRelationException"></exception>
		/// <exception cref="Exception"></exception>
		public bool SupprimerPompier(int matricule, string nomCaserne)
		{
			SqlCommand command = new SqlCommand(null, connexion);

			command.CommandText = " DELETE " +
									" FROM T_Pompiers " +
								   " WHERE Matricule = @matricule AND IdCaserne =@idCaserne";

			SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
			SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

			matriculeParam.Value = matricule;
			idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

			command.Parameters.Add(matriculeParam);
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
