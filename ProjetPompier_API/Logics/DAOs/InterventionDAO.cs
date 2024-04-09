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
    /// Classe représentant le répository d'une intervention.
    /// </summary>
    public class InterventionRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static InterventionRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static InterventionRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new InterventionRepository();
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
        private InterventionRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des interventions.
        /// </summary>
        /// <returns>Liste des interventions.</returns>
        public List<FicheInterventionDTO> ObtenirListeFicheIntervention(string nomCaserne, int matriculeCapitaine)
        {
            SqlCommand command = new SqlCommand(" SELECT T_FichesIntervention.DateTemps," +
                                                        "T_FichesIntervention.Adresse," +
                                                        "T_FichesIntervention.TypeIntervention," +
                                                        "T_FichesIntervention.Resume," +
                                                        "T_Pompiers.Matricule " +

                                                        "FROM T_Casernes " +
                                                        "INNER JOIN T_FichesIntervention " +
                                                        "ON T_Casernes.IdCaserne=T_FichesIntervention.IdCaserne " +
                                                        "INNER JOIN T_Pompiers " +
                                                        "ON T_FichesIntervention.IdPompier=T_Pompiers.IdPompier" +

                                                        " WHERE T_Casernes.Nom=@nomCaserne " +
                                                        "AND T_Pompiers.Matricule=@matriculeCapitaine; ", connexion);

            SqlParameter matriculeParam = new SqlParameter("@matriculeCapitaine", SqlDbType.Int, 6);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            matriculeParam.Value = matriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

            List<FicheInterventionDTO> liste = new List<FicheInterventionDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dateTempsIntervention = reader.GetDateTime(0);
                    string dateTempsInterventionSTR = dateTempsIntervention.ToString();
                    FicheInterventionDTO ficheInterventionDTO = new FicheInterventionDTO(dateTempsInterventionSTR, reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    liste.Add(ficheInterventionDTO);
                }
                reader.Close();
                return liste;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des interventions...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ouvrir une intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="dateTemps">Date et heure le l'intervention</param>
        /// <param name="typeIntervention">Type d'intervention</param>
        /// <param name="adresse">Adresse de l'intervention</param>
        /// <param name="resume">Resumé de l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du Capitaine de l'intervention</param>
        public void OuvrirFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " INSERT INTO T_FichesIntervention " +
                                  "(DateTemps, Adresse, TypeIntervention, Resume, IdPompier, IdCaserne) " +
                                  "SELECT @dateTemps, @adresse, @typeIntervention, @resume, T_Pompiers.IdPompier, T_Casernes.IdCaserne " +
                                  "FROM T_Pompiers, T_Casernes " +
                                  "WHERE T_Pompiers.Matricule = @matricule AND T_Casernes.Nom = @nomCaserne;";

            SqlParameter dateTempsParam = new SqlParameter("@dateTemps", SqlDbType.DateTime);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 200);
            SqlParameter typeInterventionParam = new SqlParameter("@typeIntervention", SqlDbType.VarChar, 50);
            SqlParameter resumeParam = new SqlParameter("@resume", SqlDbType.VarChar, 500);
            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int, 6);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            dateTempsParam.Value = fiche.DateTemps;
            adresseParam.Value = fiche.Adresse;
            typeInterventionParam.Value = fiche.TypeIntervention;
            resumeParam.Value = fiche.Resume;
            matriculeParam.Value = fiche.MatriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(dateTempsParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(typeInterventionParam);
            command.Parameters.Add(resumeParam);
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
                throw new DBUniqueException("Erreur lors de l'ouverture d'une intervention...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }


        #endregion


    }
}
