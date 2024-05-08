using System.Data.SqlClient;
using System.Data;

using ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe DAO d'une equipe.
    /// </summary>
    public class EquipeRepository : Repository
    {
        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static EquipeRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static EquipeRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new EquipeRepository();
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
        private EquipeRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des equipes d'une intevention.
        /// </summary>
        /// <returns>Liste des equipes d'une intervention.</returns>
        public List<EquipeDTO> ObtenirListeEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention)
        {
            SqlCommand command = new SqlCommand(" SELECT T_Equipes.Code, " +
                                                        "T_Pompiers.Matricule," +
                                                        "T_Grades.Description," +
                                                        "T_Pompiers.Nom," +
                                                        "T_Pompiers.Prenom," +
                                                        "T_Vehicules.Vin  " +

                                                        "FROM T_Equipes " +
                                                        "INNER JOIN T_FichesIntervention ON T_Equipes.IdIntervention = T_FichesIntervention.IdFicheIntervention " +
                                                        "INNER JOIN T_Pompiers ON T_Equipes.IdPompier = T_Pompiers.IdPompier " +
                                                        "INNER JOIN T_Grades ON T_Pompiers.IdGrade = T_Grades.IdGrade " +
                                                        "INNER JOIN T_Vehicules ON T_Equipes.IdVehicule=T_Vehicules.IdVehicule " +
                                                        "INNER JOIN T_Casernes ON T_FichesIntervention.IdCaserne=T_Casernes.IdCaserne " +


                                                        " WHERE T_Casernes.Nom = @nomCaserne " +
                                                        "AND T_FichesIntervention.IdPompier = (SELECT IdPompier FROM T_Pompiers WHERE Matricule = @matriculeCapitaine) " +
                                                        "AND T_FichesIntervention.DateDebut = @dateDebutIntervention " +
                                                        "GROUP BY T_Equipes.Code, " +
                                                        "T_Pompiers.Matricule, " +
                                                        "T_Grades.Description, T_Pompiers.Nom, " +
                                                        "T_Pompiers.Prenom, T_Vehicules.Vin ", connexion);

            SqlParameter matriculeParam = new SqlParameter("@matriculeCapitaine", SqlDbType.Int, 6);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);
            SqlParameter dateDebutInterventionParam = new SqlParameter("@dateDebutIntervention", SqlDbType.DateTime);

            matriculeParam.Value = matriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;
            dateDebutInterventionParam.Value = dateDebutIntervention;

            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);
            command.Parameters.Add(dateDebutInterventionParam);

            List<EquipeDTO> listeEquipe = new List<EquipeDTO>();
            List<PompierDTO> listePompierEquipe = new List<PompierDTO>();


            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                int dernierCodeEquipe = -1;
                int codeEquipe = -1;
                string vinVehiculeEquipe = "";
                string dernierVinVehiculeEquipe = "";
                while (reader.Read())
                {
                    codeEquipe = reader.GetInt32(0);
                    vinVehiculeEquipe = reader.GetString(5);
                    if (dernierCodeEquipe == -1) { dernierCodeEquipe = codeEquipe; dernierVinVehiculeEquipe = vinVehiculeEquipe; }

                    if (dernierCodeEquipe != codeEquipe)
                    {
                        EquipeDTO uneEquipe = new EquipeDTO(dernierCodeEquipe, listePompierEquipe, dernierVinVehiculeEquipe);
                        listeEquipe.Add(uneEquipe);
                        listePompierEquipe = new List<PompierDTO>();
                        dernierCodeEquipe = codeEquipe;
                        dernierVinVehiculeEquipe = vinVehiculeEquipe;
                    }
                    PompierDTO pompier = new PompierDTO(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    listePompierEquipe.Add(pompier);

                }
                EquipeDTO derniereEquipe = new EquipeDTO(codeEquipe, listePompierEquipe, vinVehiculeEquipe);
                listeEquipe.Add(derniereEquipe);
                reader.Close();
                return listeEquipe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des équipes...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir une équipe d'une intevention.
        /// </summary>
        /// <returns>L'équipe de l'intervention</returns>
        public EquipeDTO ObtenirEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention, int codeEquipe)
        {
            SqlCommand command = new SqlCommand(" SELECT T_Equipes.Code, " +
                                                        "T_Pompiers.Matricule," +
                                                        "T_Grades.Description," +
                                                        "T_Pompiers.Nom," +
                                                        "T_Pompiers.Prenom," +
                                                        "T_Vehicules.Vin  " +

                                                        "FROM T_Equipes " +
                                                        "INNER JOIN T_FichesIntervention ON T_Equipes.IdIntervention = T_FichesIntervention.IdFicheIntervention " +
                                                        "INNER JOIN T_Pompiers ON T_Equipes.IdPompier = T_Pompiers.IdPompier " +
                                                        "INNER JOIN T_Grades ON T_Pompiers.IdGrade = T_Grades.IdGrade " +
                                                        "INNER JOIN T_Vehicules ON T_Equipes.IdVehicule=T_Vehicules.IdVehicule " +
                                                        "INNER JOIN T_Casernes ON T_FichesIntervention.IdCaserne=T_Casernes.IdCaserne " +


                                                        " WHERE T_Casernes.Nom = @nomCaserne " +
                                                        "AND T_FichesIntervention.IdPompier = (SELECT IdPompier FROM T_Pompiers WHERE Matricule = @matriculeCapitaine) " +
                                                        "AND T_FichesIntervention.DateDebut = @dateDebutIntervention " +
                                                        "AND T_Equipes.Code = @codeEquipe " +
                                                        "GROUP BY T_Equipes.Code, " +
                                                        "T_Pompiers.Matricule, " +
                                                        "T_Grades.Description, T_Pompiers.Nom, " +
                                                        "T_Pompiers.Prenom, T_Vehicules.Vin ", connexion);

            SqlParameter matriculeParam = new SqlParameter("@matriculeCapitaine", SqlDbType.Int);
            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);
            SqlParameter dateDebutInterventionParam = new SqlParameter("@dateDebutIntervention", SqlDbType.DateTime);
            SqlParameter codeEquipeParam = new SqlParameter("@codeEquipe", SqlDbType.Int);

            matriculeParam.Value = matriculeCapitaine;
            nomCaserneParam.Value = nomCaserne;
            dateDebutInterventionParam.Value = dateDebutIntervention;
            codeEquipeParam.Value = codeEquipe;

            command.Parameters.Add(matriculeParam);
            command.Parameters.Add(nomCaserneParam);
            command.Parameters.Add(dateDebutInterventionParam);
            command.Parameters.Add(codeEquipeParam);

            EquipeDTO equipeDTO = new EquipeDTO();
            List<PompierDTO> listePompierEquipe = new List<PompierDTO>();


            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                int codeEquipeBDD = -1;
                string vinVehiculeBDD = "";
                while (reader.Read())
                {
                    if (codeEquipeBDD == -1) { codeEquipeBDD = reader.GetInt32(0); vinVehiculeBDD = reader.GetString(5); }

                    PompierDTO pompier = new PompierDTO(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    listePompierEquipe.Add(pompier);

                }
                equipeDTO = new EquipeDTO(codeEquipeBDD, listePompierEquipe, vinVehiculeBDD);
                reader.Close();
                return equipeDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de équipe...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }
        #endregion

    }
}
