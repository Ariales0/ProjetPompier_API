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
                    TypeInterventionDTO unTypeIntervention = new TypeInterventionDTO(reader.GetInt32(1),reader.GetString(2));
                    listeDTOTypesIntervention.Add(unTypeIntervention);
                }
                reader.Close();
                return listeDTOTypesIntervention;
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

       

        #endregion
    }
}
