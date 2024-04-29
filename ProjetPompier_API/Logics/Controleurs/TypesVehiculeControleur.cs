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

        public List<TypesVehiculeDTO> ObtenirListeTypesVehicule()
        {
            List<TypesVehiculeDTO> listeTypesVehiculeDTO = TypesVehiculeRepository.Instance.ObtenirListeTypesVehicule();
            List<TypesVehiculeModel> listeTypesVehiculeModels = new List<TypesVehiculeModel>();

            foreach (TypesVehiculeDTO type in listeTypesVehiculeDTO)
            {
                listeTypesVehiculeModels.Add(new TypesVehiculeModel(type.Code, type.Type, type.Personnes));
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

        public TypesVehiculeDTO ObtenirTypeVehicule(int code)
        {
            TypesVehiculeDTO typeVehiculeDTO = TypesVehiculeRepository.Instance.ObtenirTypeVehicule(code);

            if (typeVehiculeDTO == null)
            {
                throw new Exception("Le type de véhicule n'a pas été trouvé.");
            }

            return typeVehiculeDTO;
        }   

        public void AjouterTypeVehicule(TypesVehiculeDTO typeVehiculeDTO)
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

        public void ModifierTypeVehicule(TypesVehiculeDTO typeVehiculeDTO)
        {
            TypesVehiculeDTO typesVehiculeBD = TypesVehiculeRepository.Instance.ObtenirTypeVehicule(typeVehiculeDTO.Code);
            if (typeVehiculeDTO.Type != typesVehiculeBD.Type || typeVehiculeDTO.Personnes != typesVehiculeBD.Personnes)
            {
                TypesVehiculeRepository.Instance.ModifierTypeVehicule(typeVehiculeDTO);
            }
            else
            {
                throw new Exception("Aucune modification n'a été apportée.");
            }
        }

        public void SupprimerTypeVehicule(int code)
        {
            TypesVehiculeRepository.Instance.SupprimerTypeVehicule(code);
        }

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
