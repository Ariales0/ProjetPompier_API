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
    public class PompierControleur
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe PompierControleur.
        /// </summary>
        private static PompierControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static PompierControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new PompierControleur();
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
        private PompierControleur() { }

        #endregion Controleurs

        #region MethodesCaserne

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des pompiers.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="seulementCapitaine">Argument booleen qui indique si on souhaite uniquement les capitaines</param>
        /// <returns>Liste contenant les pompiers d'une caserne.</returns>
        public List<PompierDTO> ObtenirListePompier(string nomCaserne, bool seulementCapitaine)
        {
            List<PompierDTO> listePompierDTO = PompierRepository.Instance.ObtenirListePompier(nomCaserne, seulementCapitaine);
            List<PompierModel> listePompier = new List<PompierModel>();

            foreach (PompierDTO pompier in listePompierDTO)
            {
                listePompier.Add(new PompierModel(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
            }
            if (listePompier.Count == listePompierDTO.Count)
                return listePompierDTO;
            else
                throw new Exception("Erreur lors du chargement des pompiers de la caserne, problème avec l'intégrité des données de la base de données.");
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir la ObtenirListePompierDisponible des pompiers.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne qui prend en charge l'intervention.</param>
        /// <param name="seuelementDisponible">Argument booleen qui indique si on souhaite uniquement les pompiers qui ne sont pas en intervention</param>
        /// <returns>Liste contenant les pompiers d'une caserne.</returns>
        public List<PompierDTO> ObtenirListePompierDisponible(string nomCaserne)
        {
            List<PompierDTO> listePompierDTO = PompierRepository.Instance.ObtenirListePompierDisponible(nomCaserne);
            List<PompierModel> listePompier = new List<PompierModel>();

            foreach (PompierDTO pompier in listePompierDTO)
            {
                listePompier.Add(new PompierModel(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
            }
            if (listePompier.Count == listePompierDTO.Count)
                return listePompierDTO;
            else
                throw new Exception("Erreur lors du chargement des pompiers de la caserne, problème avec l'intégrité des données de la base de données.");
        }

        /// <summary>
        /// Méthode de service permettant d'obtenir un pompier.
        /// </summary>
        /// <param name="matricule">Le matricule du pompier</param>
        /// <param name="nomCaserne">Le nom de la caserne du pompier</param>
        /// <returns>Le pompier recherché</returns>
        public PompierDTO ObtenirPompier(int matricule, string nomCaserne)
        {
            PompierDTO pompierDTO = PompierRepository.Instance.ObtenirPompier(matricule, nomCaserne);
            PompierModel pompier = new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom);
            return new PompierDTO(pompier);
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter un pompier.
        /// </summary>
        /// <param name="pompierDTO">Le pompier à ajouter</param>
        /// <param name="nomCaserne">Le nom de la caserne du pompier</param>
        /// <returns></returns>
        public void AjouterPompier(string nomCaserne, PompierDTO pompierDTO)
        {
            bool OK = false;
            try
            {
                PompierModel unPompier = new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom);
                PompierRepository.Instance.AjouterPompier(nomCaserne, pompierDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Méthode de service permettant de modifier un pompier.
        /// </summary>
        /// <param name="pompierDTO"></param>
        /// <param name="nomCaserne"></param>
        /// <exception cref="Exception"></exception>
        public void ModifierPompier(PompierDTO pompierDTO, string nomCaserne)
        {
            PompierDTO pompierDTOBD = ObtenirPompier(pompierDTO.Matricule, nomCaserne);
            PompierModel pompierBD = new PompierModel(pompierDTOBD.Matricule, pompierDTOBD.Grade, pompierDTOBD.Nom, pompierDTOBD.Prenom);

            if (pompierDTO.Grade != pompierBD.Grade || pompierDTO.Nom != pompierBD.Nom || pompierDTO.Prenom != pompierBD.Prenom)
                PompierRepository.Instance.ModifierPompier(pompierDTO, nomCaserne);
            else
                throw new Exception("Erreur - Veuillez modifier au moins une valeur.");
        }

        /// <summary>
        /// Méthode de service permettant de supprimer un pompier.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="nomCaserne"></param>
        public void SupprimerPompier(int matricule, string nomCaserne)
        {
            PompierDTO pompierDTOBD = ObtenirPompier(matricule, nomCaserne);
            PompierRepository.Instance.SupprimerPompier(pompierDTOBD.Matricule, nomCaserne);
        }

        /// <summary>
        /// Méthode de service permettant de vider la liste des casernes.
        /// </summary>
        public void ViderListePompier(string nomCaserne)
        {
            if (ObtenirListePompier(nomCaserne, false).Count == 0)
                throw new Exception("Erreur - La liste des Casernes est déjà vide.");
            PompierRepository.Instance.ViderListePompier(nomCaserne);
        }

        #endregion MethodesCaserne
    }
}
