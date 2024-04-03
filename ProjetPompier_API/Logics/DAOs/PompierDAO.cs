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
                                                "   FROM T_Pompier WHERE IdCasere=@id ", connexion);

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
                    PompierDTO pompierDTO = new PompierDTO(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
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

       

        #endregion


    }
}
