
using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.DTOs;

/// <summary>
/// Classe représentant le DTO d'un véhicule.
/// </summary>
public class VehiculeDTO
{
    #region Proprietes
    /// <summary>
    /// Propriété représentant le VIN du véhicule.
    /// </summary>
    public string Vin { get; set; }

    /// <summary>
    /// Propriété représentant le type du véhicule.
    /// </summary>
    public string TypeVehicule { get; set; }
    /// <summary>
    /// Propriété représentant la marque du véhicule.
    /// </summary>
    public string Marque { get; set; }
    /// <summary>
    /// Propriété représentant le modèle du véhicule.
    /// </summary>
    public string Modele { get; set; }
    /// <summary>
    /// Propriété représentant l'année du véhicule.
    /// </summary>
    public int Annee { get; set; }



    #endregion Proprietes

    #region Constructeurs
    /// <summary>
    /// Constructeur vide.
    /// </summary>
    public VehiculeDTO() 
    { 
    }

    /// <summary>
    /// Constructeur avec paramètres.
    /// </summary>
    /// <param name="vin">Le vin</param>
    /// <param name="typeVehicule">Le type de vehicule</param>
    /// <param name="marque">La marque</param>
    /// <param name="modele">Le modele</param>
    /// <param name="annee">L'annee</param>
    public VehiculeDTO(string vin = "", string typeVehicule = "", string marque = "", string modele = "", int annee = 0000)
    {
	    Vin = vin;
        TypeVehicule = typeVehicule;
        Marque = marque;
        Modele = modele;
        Annee = annee;
    }

    /// <summary>
    /// Constructeur avec le modèle VehiculeModel en paramètre.
    /// </summary>
    /// <param name="leVehicule">Le modele</param>
    public VehiculeDTO(VehiculeModel leVehicule)
    {
        Vin = leVehicule.Vin;
        TypeVehicule = leVehicule.TypeVehicule;
        Marque = leVehicule.Marque;
        Modele = leVehicule.Modele;
        Annee = leVehicule.Annee;
    }



    #endregion Constructeurs
}
