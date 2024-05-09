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
    public class CaserneControleur
    {
        #region AttributsProprietes
        /// <summary>
        /// Attribut représentant l'instance unique de la classe CaserneControleur.
        /// </summary>
        private static CaserneControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static CaserneControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new CaserneControleur();
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
        private CaserneControleur() { }

        #endregion Controleurs

        #region MethodesCaserne
        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des casernes.
        /// </summary>
        /// <returns>Liste contenant les casernes.</returns>
        public List<CaserneDTO> ObtenirListeCaserne()
        {
            List<CaserneDTO> listeCaserneDTO = CaserneRepository.Instance.ObtenirListeCaserne();
            List<CaserneModel> listeCaserne = new List<CaserneModel>();

            foreach (CaserneDTO caserne in listeCaserneDTO)
            {
                listeCaserne.Add(new CaserneModel(caserne.Nom, caserne.Adresse, caserne.Ville, caserne.Province, caserne.Telephone));
            }

            if (listeCaserne.Count == listeCaserneDTO.Count)
                return listeCaserneDTO;
            else
                throw new Exception("Erreur lors du chargement des Casernes, problème avec l'intégrité des données de la base de données.");
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir la caserne .
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne.</param>
        /// <returns>Le DTO de la caserne.</returns>
        public CaserneDTO ObtenirCaserne(string nomCaserne)
        {
            CaserneDTO caserneDTO = CaserneRepository.Instance.ObtenirCaserne(nomCaserne);
            CaserneModel caserne = new CaserneModel(caserneDTO.Nom, caserneDTO.Adresse, caserneDTO.Ville, caserneDTO.Province, caserneDTO.Telephone);
            return new CaserneDTO(caserne);
        }

        /// <summary>
        /// Méthode de service permettant de créer une caserne.
        /// </summary>
        /// <param name="caserneDTO">Le DTO de la caserne.</param>
        public void AjouterCaserne(CaserneDTO caserneDTO)
        {
            bool OK = false;
            try
            {
                CaserneRepository.Instance.ObtenirCaserne(caserneDTO.Nom);
            }
            catch (Exception)
            {
                OK = true;
            }

            if (OK)
            {
                CaserneModel uneCaserne = new CaserneModel(caserneDTO.Nom, caserneDTO.Adresse, caserneDTO.Ville, caserneDTO.Province, caserneDTO.Telephone);
                CaserneRepository.Instance.AjouterCaserne(caserneDTO);
            }
            else
                throw new Exception("Erreur - La caserne est déjà existant.");

        }

        /// <summary>
        /// Méthode de service permettant de modifier la caserne.
        /// </summary>
        /// <param name="caserneDTO">Le DTO de la caserne.</param>
        public void ModifierCaserne(CaserneDTO caserneDTO)
        {
            CaserneDTO caserneDTOBD = ObtenirCaserne(caserneDTO.Nom);
            CaserneModel caserneBD = new CaserneModel(caserneDTOBD.Nom, caserneDTOBD.Adresse, caserneDTOBD.Ville, caserneDTOBD.Province, caserneDTOBD.Telephone);

            if (caserneDTO.Adresse != caserneBD.Adresse || caserneDTO.Ville != caserneBD.Ville || caserneDTO.Province != caserneBD.Province || caserneDTO.Telephone != caserneBD.Telephone)
                CaserneRepository.Instance.ModifierCaserne(caserneDTO);
            else
                throw new Exception("Erreur - Veuillez modifier au moins une valeur.");
        }

        /// <summary>
        /// Méthode de service permettant de supprimer la caserne.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne.</param>
        public void SupprimerCaserne(string nomCaserne)
        {
            CaserneDTO CaserneDTOBD = ObtenirCaserne(nomCaserne);
            CaserneRepository.Instance.SupprimerCaserne(CaserneDTOBD.Nom);
        }

        /// <summary>
        /// Méthode de service permettant de vider la liste des casernes.
        /// </summary>
        public void ViderListeCaserne()
        {
            if (ObtenirListeCaserne().Count == 0)
                throw new Exception("Erreur - La liste des Casernes est déjà vide.");
            CaserneRepository.Instance.ViderListeCaserne();
        }

        #endregion MethodesCaserne
    }
}
