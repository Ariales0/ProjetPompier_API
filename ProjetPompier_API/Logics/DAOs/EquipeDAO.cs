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
        public List<EquipeDTO> ObtenirListeEquipe()
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
        }
        #endregion

    }
}
