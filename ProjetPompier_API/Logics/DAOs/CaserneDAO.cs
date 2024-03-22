using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Exceptions;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe représentant le répository d'une caserne.
    /// </summary>
    public class CaserneRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static CaserneRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static CaserneRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new CaserneRepository();
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
        private CaserneRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des casernes.
        /// </summary>
        /// <returns>Liste des casernes.</returns>
        public List<CaserneDTO> ObtenirListeCaserne()
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                "   FROM Casernes ", connexion);

            List<CaserneDTO> liste = new List<CaserneDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CaserneDTO caserneDTO = new CaserneDTO(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    liste.Add(caserneDTO);
                }
                reader.Close();
                return liste;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des casernes...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir le ID d'une caserne selon son nom unique.
        /// </summary>
        /// <param name="nomCaserne">Le titre de la caserne.</param>
        /// <returns>Le ID de la caserne.</returns>
        public int ObtenirIdCaserne(string nomCaserne)
        {
            SqlCommand command = new SqlCommand(" SELECT Id " +
                                                "   FROM Casernes " +
                                                "  WHERE Nom = @nom ", connexion);

            SqlParameter nomCaserneParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(nomCaserneParam);

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
                throw new Exception("Erreur lors de l'obtention d'un id d'une caserne par son nom...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir une caserne selon son nom unique.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne.</param>
        /// <returns>Le DTO de la caserne.</returns>
        public CaserneDTO ObtenirCaserne(string nomCaserne)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                " FROM Casernes " +
                                                " WHERE nom = @nom ", connexion);

            SqlParameter nomCaserneParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);

            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(nomCaserneParam);

            CaserneDTO uneCaserne;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                uneCaserne = new CaserneDTO(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                reader.Close();
                return uneCaserne;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention d'une caserne par son nom...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter une caserne.
        /// </summary>
        /// <param name="caserneDTO">Le DTO de la caserne.</param>
        public bool AjouterCaserne(CaserneDTO caserneDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO Casernes (Nom, Adresse, Ville, Province, Telephone) " +
                                  " VALUES (@nom, @adresse, @ville, @province, @telephone) ";

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 100);
            SqlParameter villeParam = new SqlParameter("@ville", SqlDbType.VarChar, 75);
            SqlParameter provinceParam = new SqlParameter("@province", SqlDbType.VarChar, 50);
            SqlParameter telephoneParam = new SqlParameter("@telephone", SqlDbType.VarChar, 12);

            nomParam.Value = caserneDTO.Nom;
            adresseParam.Value = caserneDTO.Adresse;
            villeParam.Value = caserneDTO.Ville;
            provinceParam.Value = caserneDTO.Province;
            telephoneParam.Value = caserneDTO.Telephone;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(villeParam);
            command.Parameters.Add(provinceParam);
            command.Parameters.Add(telephoneParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DBUniqueException("Erreur lors de l'ajout d'une caserne...", ex);
            }
            finally
            {
                FermerConnexion();
            }

            return true;
        }

        /// <summary>
        /// Méthode de service permettant de modifier une caserne.
        /// </summary>
        /// <param name="caserneDTO">Le DTO d'une caserne.</param>
        public bool ModifierCaserne(CaserneDTO caserneDTO)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE Casernes " +
                                     " SET Adresse = @adresse, " +
                                     "     Ville = @ville, " +
                                     "     Province = @province, " +
                                     "     Telephone = @telephone " +
                                   " WHERE Nom = @nom ";

            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 50);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 100);
            SqlParameter villeParam = new SqlParameter("@ville", SqlDbType.VarChar, 75);
            SqlParameter provinceParam = new SqlParameter("@province", SqlDbType.VarChar, 50);
            SqlParameter telephoneParam = new SqlParameter("@telephone", SqlDbType.VarChar, 12);

            nomParam.Value = caserneDTO.Nom;
            adresseParam.Value = caserneDTO.Adresse;
            villeParam.Value = caserneDTO.Ville;
            provinceParam.Value = caserneDTO.Province;
            telephoneParam.Value = caserneDTO.Telephone;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(villeParam);
            command.Parameters.Add(provinceParam);
            command.Parameters.Add(telephoneParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification d'une caserne...", ex);
            }
            finally
            {
                FermerConnexion();
            }

            return true;
        }

        /// <summary>
        /// Méthode de service permettant de supprimer une caserne.
        /// </summary>
        /// <param name="caserneDTO">Le DTO d'une caserne.</param>
        public bool SupprimerCaserne(string nomCaserne)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE " +
                                    " FROM Casernes " +
                                   " WHERE Id = @id ";

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);

            idParam.Value = ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(idParam);

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
                    throw new DBRelationException("Impossible de supprimer la caserne.", e);
                }
                else throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la supression d'une caserne...", ex);
            }

            finally
            {
                FermerConnexion();
            }
            return true;
        }

        /// <summary>
        /// Méthode de service permettant de vider la liste des casernes.
        /// </summary>
        public void ViderListeCaserne()
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE FROM Casernes";
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
                    throw new DBRelationException("Impossible de supprimer les casernes.", e);
                }
                else throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la supression des Casernes...", ex);
            }

            finally
            {
                FermerConnexion();
            }
        }

        #endregion


    }
}
