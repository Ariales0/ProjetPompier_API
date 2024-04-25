
/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.DTOs
{
    /// <summary>
    /// Classe représentant le DTO d'un type de véhicule.
    /// </summary>
    public class TypesVehiculeDTO
    {
        #region Proprietes

        /// <summary>
        /// Propriété représentant le type du véhicule.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Propriété représentant le code du véhicule.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Propriété représentant le nombre de personne du véhicule.
        /// </summary>
        public int Personnes { get; set; }

        #endregion Proprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public TypesVehiculeDTO() 
        { 
        }

        public TypesVehiculeDTO(int code = 0000, string type = "", int personnes = 0)
        {
            Type = type;
            Code = code;
            Personnes = personnes;
        }

        #endregion Constructeurs
    }
}
