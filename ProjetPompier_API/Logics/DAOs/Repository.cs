using System.Data.SqlClient;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class Repository
    {
        #region AttributsPropriete

        /// <summary>
        /// La connexion.
        /// </summary>
        protected SqlConnection connexion;

        #endregion AttributsPropriete

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        protected Repository()
        {
			//connexion = new SqlConnection("Server=127.0.0.1; Database=ProjetPompier; User Id=quentin; Password=password;");//Quentin
			connexion = new SqlConnection("Server=127.0.0.1; Database=Caserne; User Id=vincent; Password=Patate123;");//Vincent
            //connexion = new SqlConnection("Server = 127.0.0.1; Database = Caserne; User Id=lyes2;Password=Patate123;");//Lyes

        }

		#endregion Constructeurs

		#region MethodesService

		/// <summary>
		/// Méthode permettant d'ouvrir la connexion.
		/// </summary>
		protected void OuvrirConnexion()
        {
            connexion.Open();
        }

        /// <summary>
        /// Méthode permettant de fermer la connexion.
        /// </summary>
        protected void FermerConnexion()
        {
            connexion.Close();
        }

        #endregion MethodesService
    }
}
