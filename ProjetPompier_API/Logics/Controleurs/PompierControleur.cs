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
            List<PompierDTO> listePompierDTO = PompierRepository.Instance.ObtenirListePompier(nomCaserne);
            if (seulementCapitaine)
            {
                List<PompierDTO> listePompierCapitaineDTO = new List<PompierDTO>();
                List<PompierModel> listePompierCapitaine = new List<PompierModel>();

                foreach (PompierDTO pompier in listePompierDTO)
                {
                    if(pompier.Grade==1)
                    {
                        listePompierCapitaine.Add(new PompierModel(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
                        listePompierCapitaineDTO.Add(new PompierDTO(pompier.Matricule, pompier.Grade, pompier.Nom, pompier.Prenom));
                    }
                }
                if (listePompierCapitaine.Count == listePompierCapitaineDTO.Count)
                    return listePompierCapitaineDTO;
                else
                    throw new Exception("Erreur lors du chargement des capitaines de la caserne, problème avec l'intégrité des données de la base de données.");
            }
            else 
            {
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

            
        }
		

		public PompierDTO ObtenirPompier(int matricule)
		{
			PompierDTO pompierDTO = PompierRepository.Instance.ObtenirPompier(matricule);
			PompierModel pompier = new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom);
			return new PompierDTO(pompier);
		}


        public int ObtenirIdPompier(int matricule)
        {
			return PompierRepository.Instance.ObtenirIdPompier(matricule);
		}
		
		public void AjouterPompier(string nomCaserne, PompierDTO pompierDTO)
		{
			bool OK = false;
            int idCaserne = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);
			try
			{
				PompierRepository.Instance.ObtenirIdPompier(pompierDTO.Matricule);
			}
			catch (Exception)
			{
				OK = true;
			}

			if (OK)
			{
				PompierModel unPompier = new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom);
				PompierRepository.Instance.AjouterPompier(idCaserne, pompierDTO);
			}
			else
				throw new Exception("Erreur - Le pompier est déjà existant.");

		}


		public void ModifierPompier(PompierDTO pompierDTO)
		{
			PompierDTO pompierDTOBD = ObtenirPompier(pompierDTO.Matricule);
			PompierModel pompierBD = new PompierModel(pompierDTOBD.Matricule, pompierDTOBD.Grade, pompierDTOBD.Nom, pompierDTOBD.Prenom);

			if (pompierDTO.Grade != pompierBD.Grade || pompierDTO.Nom!= pompierBD.Nom|| pompierDTO.Prenom != pompierBD.Prenom)
				PompierRepository.Instance.ModifierPompier(pompierDTO);
			else
				throw new Exception("Erreur - Veuillez modifier au moins une valeur.");
		}


		public void SupprimerPompier(int matricule)
		{
			PompierDTO pompierDTOBD = ObtenirPompier(matricule);
			PompierRepository.Instance.SupprimerPompier(pompierDTOBD.Matricule);
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
