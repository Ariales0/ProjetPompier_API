using ProjetPompier_API.Logics.DAOs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;

namespace ProjetPompier_API.Logics.Controleurs
{
    public class TypesVehiculeControleur
    {

        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe TypesVehiculeControleur.
        /// </summary>
        private static TypesVehiculeControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static TypesVehiculeControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new TypesVehiculeControleur();
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
        private TypesVehiculeControleur() { }

        #endregion Controleurs

        #region Methodes

        /// <summary>
        /// Methode permettant d'obtenir la liste des types de véhicule.
        /// </summary>
        /// <returns>Retourne la liste des types</returns>
        /// <exception cref="Exception"></exception>
        public List<TypeVehiculeDTO> ObtenirListeTypesVehicule()
        {
            List<TypeVehiculeDTO> listeTypesVehiculeDTO = TypesVehiculeRepository.Instance.ObtenirListeTypesVehicule();
            List<TypeVehiculeModel> listeTypesVehiculeModels = new List<TypeVehiculeModel>();

            foreach (TypeVehiculeDTO type in listeTypesVehiculeDTO)
            {
                listeTypesVehiculeModels.Add(new TypeVehiculeModel(type.Code, type.Type, type.Personnes));
            }
   //         try
   //         {
			//	if (listeTypesVehiculeModels.Count == 0)
			//	{
			//		throw new Exception("La liste des types de véhicule est vide.");
			//	}
			//}
   //         catch(Exception e)
   //         {

   //         }
            

            return listeTypesVehiculeDTO;
        }

        /// <summary>
        /// Methode permettant d'obtenir un type de véhicule.   
        /// </summary>
        /// <param name="code">Le code du type</param>
        /// <returns>Retourne Le typeVehicule</returns>
        /// <exception cref="Exception"></exception>
        public TypeVehiculeDTO ObtenirTypeVehicule(int code)
        {
            TypeVehiculeDTO typeVehiculeDTO = TypesVehiculeRepository.Instance.ObtenirTypeVehicule(code);

            if (typeVehiculeDTO == null)
            {
                throw new Exception("Le type de véhicule n'a pas été trouvé.");
            }

            return typeVehiculeDTO;
        }   

        /// <summary>
        /// Methodes permettant d'ajouter un type de véhicule.
        /// </summary>
        /// <param name="typeVehiculeDTO">Le type vehiculeDTO</param>
        /// <exception cref="Exception"></exception>
        public void AjouterTypeVehicule(TypeVehiculeDTO typeVehiculeDTO)
        {
            bool OK = false;
            try
            {
                TypesVehiculeRepository.Instance.ObtenirTypeVehicule(typeVehiculeDTO.Code);
            }
            catch (Exception)
            {
                OK = true;
            }
            if (OK)
            {
                TypesVehiculeRepository.Instance.AjouterTypeVehicule(typeVehiculeDTO);
            }
            else
            {
                throw new Exception("Le type de véhicule existe déjà.");
            }
        }

        /// <summary>
        /// Methode permettant de modifier un type de véhicule.
        /// </summary>
        /// <param name="typeVehiculeDTO">Le type vehiculeDTO</param>
        /// <exception cref="Exception"></exception>
        public void ModifierTypeVehicule(TypeVehiculeDTO typeVehiculeDTO)
        {
            TypeVehiculeDTO typesVehiculeBD = TypesVehiculeRepository.Instance.ObtenirTypeVehicule(typeVehiculeDTO.Code);
            if (typeVehiculeDTO.Type != typesVehiculeBD.Type || typeVehiculeDTO.Personnes != typesVehiculeBD.Personnes)
            {
                TypesVehiculeRepository.Instance.ModifierTypeVehicule(typeVehiculeDTO);
            }
            else
            {
                throw new Exception("Aucune modification n'a été apportée.");
            }
        }

        /// <summary>
        /// Methode permettant de supprimer un type de véhicule.
        /// </summary>
        /// <param name="code">Le code du type</param>
        public void SupprimerTypeVehicule(int code)
        {
            TypesVehiculeRepository.Instance.SupprimerTypeVehicule(code);
        }

        /// <summary>
        /// Methode permettant de vider la liste des types de véhicule.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ViderListeTypesVehicule()
        {
            if (TypesVehiculeRepository.Instance.ObtenirListeTypesVehicule().Count == 0)
            {
                throw new Exception("La liste des types de véhicule est déjà vide.");
            }
            TypesVehiculeRepository.Instance.ViderListeTypesVehicule();
        }

        #endregion Methodes

    }
}
