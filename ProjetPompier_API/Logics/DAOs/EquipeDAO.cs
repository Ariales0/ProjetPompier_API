/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetPompier_API.Logics.DAOs
{
    /// <summary>
    /// Classe DAO d'une equipe.
    /// </summary>
    public class EquipeRepository
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

    }
}
