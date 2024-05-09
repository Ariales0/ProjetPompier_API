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
        public List<PompierDTO> ObtenirListePompier(string nomCaserne, bool seulementCapitaine)
        {
            string requeteObtenirListePompier = "SELECT T_Pompiers.Matricule, " +
                                                       "T_Grades.Description, " +
                                                       "T_Pompiers.Nom, " +
                                                       "T_Pompiers.Prenom " +
                                                       "FROM T_Pompiers " +
                                                       "INNER JOIN T_Grades ON T_Grades.IdGrade = T_Pompiers.IdGrade " +
                                                       "INNER JOIN T_Casernes ON T_Casernes.IdCaserne = T_Pompiers.IdCaserne " +
                                                       "WHERE T_Casernes.Nom = @nomCaserne";

            string extensionCapitaineSeulement = " AND T_Grades.Description = 'Capitaine'";

            string requeteComplete = (seulementCapitaine) ? requeteObtenirListePompier + extensionCapitaineSeulement : requeteObtenirListePompier;
            SqlCommand command = new SqlCommand(requeteComplete, connexion);

            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);
            nomCaserneParam.Value = nomCaserne;
            command.Parameters.Add(nomCaserneParam);

            List<PompierDTO> liste = new List<PompierDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PompierDTO pompierDTO = new PompierDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
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
        /// Méthode de service permettant d'obtenir la liste des pompiers.
        /// </summary>
        /// <returns>Liste des pompiers.</returns>
        public List<PompierDTO> ObtenirListePompierDisponible(string nomCaserne)
        {
            string requete = "SELECT T_Pompiers.Matricule, " +
                                    "T_Grades.Description, " +
                                    "T_Pompiers.Nom, " +
                                    "T_Pompiers.Prenom " +
                                    "FROM T_Pompiers " +
                                    "INNER JOIN T_Grades ON T_Grades.IdGrade = T_Pompiers.IdGrade " +
                                    "INNER JOIN T_Casernes ON T_Casernes.IdCaserne = T_Pompiers.IdCaserne " +
                                    "WHERE T_Casernes.Nom = @nomCaserne AND T_Grades.Description != 'Capitaine' " +
                                    "AND T_Pompiers.IdPompier NOT IN " +
                                    "(" +
                                    "SELECT T_Equipes.IdPompier " +
                                    "FROM T_Equipes " +
                                    "INNER JOIN T_FichesIntervention ON T_Equipes.IdIntervention = T_FichesIntervention.IdFicheIntervention " +
                                    "WHERE T_FichesIntervention.DateFin IS NULL " +
                                    ")";

            SqlCommand command = new SqlCommand(requete, connexion);

            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);
            nomCaserneParam.Value = nomCaserne;
            command.Parameters.Add(nomCaserneParam);

            List<PompierDTO> liste = new List<PompierDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PompierDTO pompierDTO = new PompierDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
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
        /// Méthode de service permettant d'obtenir un pompier par son matricule et sa caserne.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="nomCaserne"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PompierDTO ObtenirPompier(int matricule, string nomCaserne)
        {
            SqlCommand command = new SqlCommand("SELECT T_Pompiers.Matricule, " +
                                                       "T_Grades.Description, " +
                                                       "T_Pompiers.Nom, " +
                                                       "T_Pompiers.Prenom " +
                                                       "FROM T_Pompiers " +
                                                       "INNER JOIN T_Grades ON T_Grades.IdGrade = T_Pompiers.IdGrade " +
                                                       "INNER JOIN T_Casernes ON T_Casernes.IdCaserne = T_Pompiers.IdCaserne " +
                                                       "WHERE T_Casernes.Nom = @nomCaserne AND T_Pompiers.Matricule = @matricule" , connexion);

            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            matriculeParam.Value = matricule;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

            PompierDTO unPompier;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                unPompier = new PompierDTO(reader.GetInt32(0),reader.GetString(1), reader.GetString(2), reader.GetString(3));
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
        /// <param name="nomCaserne">Nom de la caserne</param>
        /// <param name="pompierDTO">Pompier à ajouer</param>
        /// <returns></returns>
        /// <exception cref="DBUniqueException"></exception>
        public bool AjouterPompier(string nomCaserne, PompierDTO pompierDTO)
        {
            string obtenirIdCaserne = "(SELECT IdCaserne FROM T_Casernes WHERE Nom = @nomCaserne)";
            string obtenirIdGrade = "(SELECT IdGrade FROM T_Grades WHERE Description = @grade)";

            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO T_Pompiers (Matricule, IdGrade, Nom, Prenom, IdCaserne) " +
                                  " VALUES (@matricule, " + obtenirIdGrade + ", @nom, @prenom, " + obtenirIdCaserne + ") ";

            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
            SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.VarChar, 50);
            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 100);
            SqlParameter prenomParam = new SqlParameter("@prenom", SqlDbType.VarChar, 100);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            nomParam.Value = pompierDTO.Nom;
            prenomParam.Value = pompierDTO.Prenom;
            gradeParam.Value = pompierDTO.Grade;
            matriculeParam.Value = pompierDTO.Matricule;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(prenomParam);
            command.Parameters.Add(gradeParam);
            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

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
            string obtenirIdCaserne = "(SELECT IdCaserne FROM T_Casernes WHERE Nom = @nomCaserne)";
            string obtenirIdGrade = "(SELECT IdGrade FROM T_Grades WHERE Description = @grade)";

            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE T_Pompiers " +
                                     " SET IdGrade = " + obtenirIdGrade + ", " +
                                     "     Nom = @nom, " +
                                     "     Prenom = @prenom " +
                                   " WHERE Matricule = @matricule AND IdCaserne = " + obtenirIdCaserne + " ";

            SqlParameter gradeParam = new SqlParameter("@grade", SqlDbType.VarChar, 50);
            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
            SqlParameter nomParam = new SqlParameter("@nom", SqlDbType.VarChar, 100);
            SqlParameter prenomParam = new SqlParameter("@prenom", SqlDbType.VarChar, 100);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            nomParam.Value = pompierDTO.Nom;
            prenomParam.Value = pompierDTO.Prenom;
            gradeParam.Value = pompierDTO.Grade ;
            matriculeParam.Value = pompierDTO.Matricule;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(nomParam);
            command.Parameters.Add(prenomParam);
            command.Parameters.Add(gradeParam);
            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

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
        public bool SupprimerPompier(int matricule, string nomCaserne)
        {
            string obtenirIdCaserne = "(SELECT IdCaserne FROM T_Casernes WHERE Nom = @nomCaserne)";
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE " +
                                    " FROM T_Pompiers " +
                                   " WHERE Matricule = @matricule AND IdCaserne = " + obtenirIdCaserne + " ";

            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            matriculeParam.Value = matricule;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

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

            string obtenirIdCaserne = "(SELECT IdCaserne FROM T_Casernes WHERE Nom = @nomCaserne)";

            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " DELETE FROM T_Pompiers WHERE IdCaserne = " + obtenirIdCaserne + " ";

            SqlParameter nomCaserneParam = new SqlParameter("@idCaserne", SqlDbType.VarChar, 100);
            nomCaserneParam.Value = nomCaserne;
            command.Parameters.Add(nomCaserneParam);

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
