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
            //////Lyes
            //string utilisateur = "User Id=lyes2; ";
            //string motDePasse = "Password=Patate123;";

            //Vincent
   //         string utilisateur = "User Id=vincent; ";
   //         string motDePasse = "Password=Patate123;";
			//string ipServeur = "Server=127.0.0.1; ";
			//string baseDeDonnees = "Database=ProjetPompier; ";

			////Quentin
			string utilisateur = "User Id=quentin; ";
			string motDePasse = "Password=password;";

			string ipServeur = "Server=127.0.0.1; ";
			string baseDeDonnees = "Database=ProjetPompier; ";

			connexion = new SqlConnection(ipServeur + baseDeDonnees + utilisateur + motDePasse);

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
