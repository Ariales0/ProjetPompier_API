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
            SqlCommand command = new SqlCommand(" SELECT T_FichesIntervention.DateDebut," +
                                                        "T_FichesIntervention.DateFin," +
                                                        "T_FichesIntervention.Adresse," +
                                                        "T_FichesIntervention.IdTypeIntervention," +
                                                        "T_FichesIntervention.Resume," +
                                                        "T_Pompiers.Matricule " +

                                                        "FROM T_Casernes " +
                                                        "INNER JOIN T_FichesIntervention " +
                                                        "ON T_Casernes.IdCaserne=T_FichesIntervention.IdCaserne " +
                                                        "INNER JOIN T_Pompiers " +
                                                        "ON T_FichesIntervention.IdPompier=T_Pompiers.IdPompier" +
                                                        "INNER JOIN T_TypesIntervention " +
                                                        "ON T_FichesIntervention.IdTypeIntervention = T_TypesIntervention.IdTypeIntervention" +

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
                    string dateFin = "";
                    if (!reader.IsDBNull(1))
                    {
                        dateFin = reader.GetDateTime(1).ToString();
                    }
                    FicheInterventionDTO ficheInterventionDTO = new FicheInterventionDTO(reader.GetDateTime(0).ToString(), dateFin ,reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
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
        /// Méthode de service permettant d'obtenir une fiche d'intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="matriculeCapitaine">Le matricule du capitaine</param>
        /// <returns>Retourne une FicheDTO</returns>
        /// <exception cref="Exception"></exception>
        public FicheInterventionDTO ObtenirFicheIntervention(string nomCaserne, int matriculeCapitaine, string dateIntervention)
        {
            SqlCommand command = new SqlCommand(" SELECT T_FichesIntervention.DateDebut," +
                                            "T_FichesIntervention.DateFin, " +
                                            "T_FichesIntervention.Adresse," +
                                            "T_FichesIntervention.IdTypeIntervention," +
                                            "T_FichesIntervention.Resume," +
                                            "T_Pompiers.Matricule " +

                                            "FROM T_Casernes " +
                                            "INNER JOIN T_FichesIntervention " +
                                            "ON T_Casernes.IdCaserne=T_FichesIntervention.IdCaserne " +
                                            "INNER JOIN T_Pompiers " +
                                            "ON T_FichesIntervention.IdPompier=T_Pompiers.IdPompier " +
                                            "INNER JOIN T_TypesIntervention " +
                                            "ON T_FichesIntervention.IdTypeIntervention = T_TypesIntervention.IdTypeIntervention" +

                                            "WHERE T_Casernes.Nom=@nomCaserne " +
                                            "AND T_Pompiers.Matricule=@matriculeCapitaine " +
                                            "AND T_FichesIntervention.DateDebut=@dateIntervention", connexion);


            SqlParameter dateInterventionParam = new SqlParameter("@dateIntervention", SqlDbType.DateTime);
            SqlParameter matriculeParam = new SqlParameter("@matriculeCapitaine", SqlDbType.Int, 6);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);

            dateInterventionParam.Value = dateIntervention;
            matriculeParam.Value = matriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(dateInterventionParam);
            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);

            FicheInterventionDTO uneFicheIntervention;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string dateFin = "";
                if (!reader.IsDBNull(1))
                {
                    dateFin = reader.GetDateTime(1).ToString();
                }
                uneFicheIntervention = new FicheInterventionDTO(reader.GetDateTime(0).ToString(), dateFin, reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                reader.Close();
                return uneFicheIntervention;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention d'une fiche d'intervention...", ex);
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

            command.CommandText = "INSERT INTO T_FichesIntervention (DateDebut, DateFin ,Adresse, IdTypeIntervention, Resume, IdPompier, IdCaserne)" +
                                  "VALUES (@dateDebut, @dateFin, @adresse, @idTypeIntervention, @resume, @idPompier, @idCaserne);";

            int idCaserne = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);
            int idPompier = PompierRepository.Instance.ObtenirIdPompier(fiche.MatriculeCapitaine, nomCaserne);
            int idTypeIntervention = TypeInterventionRepositiory.Instance.ObtenirIdTypeIntervention(fiche.CodeTypeIntervention);

            SqlParameter dateDebutParam = new SqlParameter("@dateDebut", SqlDbType.DateTime);
            SqlParameter dateFinParam = new SqlParameter("@dateFin", SqlDbType.DateTime);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 200);
            SqlParameter idTypeInterventionParam = new SqlParameter("@idTypeIntervention", SqlDbType.Int);
            SqlParameter resumeParam = new SqlParameter("@resume", SqlDbType.VarChar, 500);
            SqlParameter idPompierParam = new SqlParameter("@idPompier", SqlDbType.Int);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            dateDebutParam.Value = fiche.DateDebut;
            dateFinParam.Value =  DBNull.Value; 
            adresseParam.Value = fiche.Adresse;
            idTypeInterventionParam.Value = idTypeIntervention;
            resumeParam.Value = fiche.Resume;
            idPompierParam.Value = idPompier;
            idCaserneParam.Value = idCaserne;


            command.Parameters.Add(dateDebutParam);
            command.Parameters.Add(dateFinParam);
            command.Parameters.Add(adresseParam);
            command.Parameters.Add(idTypeIntervention);
            command.Parameters.Add(resumeParam);
            command.Parameters.Add(idPompierParam);
            command.Parameters.Add(idCaserneParam);

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

        /// <summary>
        /// Méthode de service permettant de modifier une intervention.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="fiche">La ficheDTO</param>
        /// <exception cref="DBUniqueException"></exception>
        public void ModifierIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE T_FichesIntervention " +
                                  "SET Adresse = @adresse, " +
                                  "Resume = @resume " +
                                  "WHERE IdPompier = @idPompier " +
                                  "AND IdCaserne = IdCaserne;";

            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);
            SqlParameter idPompierParam = new SqlParameter("@idPompier", SqlDbType.Int);
            SqlParameter dateDebutParam = new SqlParameter("@dateDebut", SqlDbType.DateTime);
            SqlParameter dateFinParam = new SqlParameter("@dateFin", SqlDbType.DateTime);
            SqlParameter adresseParam = new SqlParameter("@adresse", SqlDbType.VarChar, 200);
            SqlParameter resumeParam = new SqlParameter("@resume", SqlDbType.VarChar, 500);
            SqlParameter matriculeParam = new SqlParameter("@matricule", SqlDbType.Int, 6);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);


            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);
            idPompierParam.Value = PompierRepository.Instance.ObtenirIdPompier(fiche.MatriculeCapitaine, nomCaserne);
            dateDebutParam.Value = fiche.DateDebut;
            dateFinParam.Value = DBNull.Value;
            adresseParam.Value = fiche.Adresse;
            resumeParam.Value = fiche.Resume;
            matriculeParam.Value = fiche.MatriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;

            command.Parameters.Add(idCaserneParam);
            command.Parameters.Add(idPompierParam);
            command.Parameters.Add(dateDebutParam);
            command.Parameters.Add(dateFinParam);
            command.Parameters.Add(adresseParam);
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
                throw new DBUniqueException("Erreur lors de la modification d'une intervention...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant de fermer une fiche d'intervention dans une caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="fiche">La ficheDTO</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="DBUniqueException"></exception>
        public void FermerFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            SqlCommand command = new SqlCommand(null, connexion);

            command.CommandText = " UPDATE T_FichesIntervention " +
                                  "SET DateFin = @dateFin " +
                                  "WHERE IdPompier = @idPompier " +
                                  "AND IdCaserne = IdCaserne;";

            SqlParameter dateFinParam = new SqlParameter("@dateFin", SqlDbType.DateTime);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);
            SqlParameter idPompierParam = new SqlParameter("@idPompier", SqlDbType.Int);

            if (fiche.DateFin == null)
            {
                throw new Exception("Erreur - La date de fin de l'intervention est null.");
            }

            dateFinParam.Value = fiche.DateFin;
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);
            idPompierParam.Value = PompierRepository.Instance.ObtenirIdPompier(fiche.MatriculeCapitaine, nomCaserne);

            command.Parameters.Add(dateFinParam);
            command.Parameters.Add(idCaserneParam);
            command.Parameters.Add(idPompierParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DBUniqueException("Erreur lors de la fermeture d'une intervention...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        #endregion


    }
}
