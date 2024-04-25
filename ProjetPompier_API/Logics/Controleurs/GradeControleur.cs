using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classes de type Controleur.
/// </summary>
namespace ProjetPompier_API.Logics.Controleurs
{
    /// <summary>
    /// Classe représentant le controleur de l'application.
    /// </summary>
    public class GradeControleur 
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierControleur.
        /// </summary>
        private static GradeControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static GradeControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new GradeControleur();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Controleurs

        /// <summary>
        /// Constructeur par défaut de la classe.
        /// </summary>
        private GradeControleur() { }

        #endregion Controleurs

        #region MethodesGrade
		/// <summary>
		/// Méthode de service permettant d'obtenir la liste des grades.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
        public List<GradeDTO> ObtenirListeGrade()
        {
            List<GradeDTO> listeGradeDTO = GradeRepository.Instance.ObtenirListeGrade();
			List<GradeModel> listeGrade = new List<GradeModel>();

			foreach (GradeDTO grade in listeGradeDTO)
			{
				listeGrade.Add(new GradeModel(grade.Description));
			}

			if (listeGrade.Count == listeGradeDTO.Count)
				return listeGradeDTO;
			else
				throw new Exception("Erreur lors du chargement des grades, problème avec l'intégrité des données de la base de données.");


		}
		
		/// <summary>
		/// Méthode de service permettant d'obtenir un grade.
		/// </summary>
		/// <param name="idGrade"></param>
		/// <returns></returns>
		public GradeDTO ObtenirGrade(string description)
		{
			GradeDTO gradeDTO = GradeRepository.Instance.ObtenirGrade(description);
			return gradeDTO;
		}

		/// <summary>
		/// Méthode de service permettant d'ajouter un grade.
		/// </summary>
		/// <param name="gradeDTO"></param>
		/// <exception cref="Exception"></exception>
		public void AjouterGrade(GradeDTO gradeDTO)
		{
			bool OK = false;
			try
			{
				GradeRepository.Instance.ObtenirIdGrade(gradeDTO.Description);
			}
			catch (Exception)
			{
				OK = true;
			}

			if (OK)
			{
				GradeModel unGrade = new GradeModel(gradeDTO.Description);
				GradeRepository.Instance.AjouterGrade(gradeDTO);
			}
			else
				throw new Exception("Erreur - Le grade est déjà existant.");

		}

		/// <summary>
		/// Méthode de service permettant de modifier un grade.
		/// </summary>
		/// <param name="descriptionAvantChangement"></param>
		/// <param name="descriptionApresChangement"></param>
		/// <exception cref="Exception"></exception>
		public void ModifierGrade(string descriptionAvantChangement, string descriptionApresChangement)
		{
			GradeDTO gradeDTOBD = ObtenirGrade(descriptionAvantChangement);
			GradeModel gradeBD = new GradeModel(descriptionApresChangement);

			if (descriptionApresChangement != descriptionAvantChangement)
			{
				GradeRepository.Instance.ModifierGrade(gradeDTOBD, descriptionApresChangement);
			}
				
			else
				throw new Exception("Erreur - Veuillez modifier au moins une valeur.");
		}

		/// <summary>
		/// Méthode de service permettant de supprimer un grade.
		/// </summary>
		/// <param name="idGrade"></param>
		/// <exception cref="Exception"></exception>
		public void SupprimerGrade(string description)
		{
			try
			{
				GradeRepository.Instance.ObtenirGrade(description);
				GradeRepository.Instance.SupprimerGrade(description);
			}
			catch (Exception)
			{
				throw new Exception("Erreur - Le grade n'existe pas.");
			}
			
		}

		/// <summary>
		/// Méthode de service permettant de vider la liste des casernes.
		/// </summary>
		public void ViderListeGrade()
		{
			if (ObtenirListeGrade().Count == 0)
				throw new Exception("Erreur - La liste des grades est déjà vide.");
			GradeRepository.Instance.ViderListeGrade();
		}

		#endregion MethodesCaserne
	}
}
