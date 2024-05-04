using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Exceptions;


/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    public class TypesInterventionRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static TypesInterventionRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static TypesInterventionRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new TypesInterventionRepository();
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
        private TypesInterventionRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des types d'intervention.
        /// </summary>
        /// <returns>Liste des types d'intervention.</returns>
        public List<TypeInterventionDTO> ObtenirListeTypesIntervention()
        {
            SqlCommand command = new SqlCommand(" SELECT *  FROM T_TypesIntervention ", connexion);

            List<TypeInterventionDTO> listeDTOTypesIntervention = new List<TypeInterventionDTO>();
            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TypeInterventionDTO unTypeIntervention = new TypeInterventionDTO(reader.GetInt32(1), reader.GetString(2));
                    listeDTOTypesIntervention.Add(unTypeIntervention);
                }
                reader.Close();
                return listeDTOTypesIntervention;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des types d'intervention.", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir l'id d'un type d'intervention par son code.
        /// </summary>
        /// <param name="codeIntervention">Le code du type d'intervention</param>
        /// <returns>L'id du type d'intervention</returns>
        public int ObtenirIdTypeIntervention(int codeIntervention)
        {
            SqlCommand command = new SqlCommand(" SELECT IdTypeIntervention " +
                                                "   FROM T_TypesIntervention " +
                                                "  WHERE Code= @code ", connexion);

            SqlParameter paramSqlCodeIntervention = new SqlParameter("@code", SqlDbType.Int);

            paramSqlCodeIntervention.Value = codeIntervention;

            command.Parameters.Add(paramSqlCodeIntervention);

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int idTypeIntervention;
                idTypeIntervention = reader.GetInt32(0);
                reader.Close();
                return idTypeIntervention;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de l'id du type d'intervention du code : " + codeIntervention + ".", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir un type d'intervetion par son code.
        /// </summary>
        /// <param name="codeIntervention">La code du type d'intervention</param>
        /// <returns>Le DTO du type d'intervention</returns>
        public TypeInterventionDTO ObtenirTypeIntervention(int codeIntervention)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM T_TypesIntervention " +
                                                " WHERE Code = @code", connexion);

            SqlParameter paramSqlCodeIntervention = new SqlParameter("@code", SqlDbType.Int);

            paramSqlCodeIntervention.Value = codeIntervention;

            command.Parameters.Add(paramSqlCodeIntervention);

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                TypeInterventionDTO unTypeIntervention = new TypeInterventionDTO(reader.GetInt32(1), reader.GetString(2));
                reader.Close();
                return unTypeIntervention;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention d'un type d'intervention par sson code :" + codeIntervention + ".", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter un type d'intervention à la base de données.
        /// </summary>
        /// <param name="leTypeIntervention"></param>
        /// <returns></returns>
        public bool AjouterTypeIntervention(TypeInterventionDTO leTypeIntervention)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO T_TypesIntervention (Code, Description) " +
                                  " VALUES (@code, @description) ";

            SqlParameter paramSqlDescription = new SqlParameter("@description", SqlDbType.VarChar, 50);
            SqlParameter paramSqlCode = new SqlParameter("@code", SqlDbType.Int);

            paramSqlCode.Value = leTypeIntervention.Code;
            paramSqlDescription.Value = leTypeIntervention.Description;

            command.Parameters.Add(paramSqlDescription);
            command.Parameters.Add(paramSqlCode);
            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new DBUniqueException("Erreur lors de l'ajout d'un type d'intervention.", ex);
                return false;
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant de modifier un type d'intervention.
        /// Seule la description est modifiable, le code est unique.
        /// </summary>
        /// <param name="leTypeInterventionModification"></param>
        /// <returns>True si c'est ok, false sinon</returns>
        public bool ModifierTypeIntervention(TypeInterventionDTO leTypeInterventionModification)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE T_TypesIntervention " +
                                     " SET Description = @description " +
                                   " WHERE Code = @code ";

            SqlParameter paramSqlNouvelleDescription = new SqlParameter("@description", SqlDbType.VarChar, 50);
            SqlParameter paramSqlCode = new SqlParameter("@code", SqlDbType.Int);

            paramSqlCode.Value = leTypeInterventionModification.Code;
            paramSqlNouvelleDescription.Value = leTypeInterventionModification.Description;

            command.Parameters.Add(paramSqlNouvelleDescription);
            command.Parameters.Add(paramSqlCode);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification d'un type d'intervention.", ex);
            }
            finally
            {
                FermerConnexion();
            }

            return true;
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un type d'intervention.
        /// </summary>
        /// <param name="codeIntervention">Le code unique de l'intervention à supprimer.</param>
        /// <returns></returns>
        public bool SupprimerTypeIntervention(int codeIntervention)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE " +
                                    " FROM T_TypesIntervention " +
                                   " WHERE Code = @code";

            SqlParameter paramSqlCodeIntervention = new SqlParameter("@code", SqlDbType.Int);

            paramSqlCodeIntervention.Value = codeIntervention;

            command.Parameters.Add(paramSqlCodeIntervention);

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
                    throw new DBRelationException("Impossible de supprimer le type d'intervention.", e);
                }
                else throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la supression du type d'intervention...", ex);
            }

            finally
            {
                FermerConnexion();
            }
            return true;
        }

        /// <summary>
        /// Méthode de service permettant de vider la liste des types d'intervention.
        /// </summary>
        public void ViderListeTypesIntervention()
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE FROM T_TypesIntervention";

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
                    throw new DBRelationException("Impossible de supprimer les types d'intervention.", e);
                }
                else throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la supression des types d'intervention.", ex);
            }

            finally
            {
                FermerConnexion();
            }
        }

        #endregion
    }
}
