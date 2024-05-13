using Microsoft.AspNetCore.Mvc;
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
    public class EquipeControleur
    {
        #region AttributsProprietes

        /// <summary>
        /// Attribut représentant l'instance unique de la classe EquipeControleur.
        /// </summary>
        private static EquipeControleur instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static EquipeControleur Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new EquipeControleur();
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
        private EquipeControleur() { }

        #endregion Controleurs

        #region MethodesCaserne

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des equipes d'une intervention.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
		/// <returns> La liste des equipes</returns>
        public List<EquipeDTO> ObtenirListeEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention)
        {
            try
            {
                List<EquipeDTO> listeEquipeDTO = EquipeRepository.Instance.ObtenirListeEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention);
                if(listeEquipeDTO.Count == 1 && listeEquipeDTO[0].Code == -1)
                {
                    return new List<EquipeDTO>();
                }
                List<EquipeModel> listeEquipe = new List<EquipeModel>();
                List<PompierModel> listePompierModel = new List<PompierModel>();

                foreach (EquipeDTO uneEquipe in listeEquipeDTO)
                {
                    listePompierModel = new List<PompierModel>();
                    foreach (PompierDTO pompierDTO in uneEquipe.ListePompierEquipe)
                    {
                        listePompierModel.Add(new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom));
                    }


                    listeEquipe.Add(new EquipeModel(
                        uneEquipe.Code,
                        listePompierModel,
                        uneEquipe.VinVehicule
                        ));
                }

                if (listeEquipeDTO.Count == listeEquipe.Count)
                    return listeEquipeDTO;
                else
                    throw new Exception("Erreur lors du chargement des equipes de l'intervention.");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        /// <summary>
        /// Méthode de service permettant d'obtenir une équipe d'une intervention.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="codeEquipe">Vin du véhicule utilisé par l'équipe</param>
        /// <returns>Le DTO de l'équipe recherchée</returns>
        public EquipeDTO ObtenirEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention, int codeEquipe)
        {
            try
            {
                EquipeDTO equipeDTO_Recherchee = EquipeRepository.Instance.ObtenirEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipe);

                List<PompierModel> listePompierModel = new List<PompierModel>();
                foreach (PompierDTO pompierDTO in equipeDTO_Recherchee.ListePompierEquipe)
                {
                    listePompierModel.Add(new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom));
                }
                EquipeModel equipeModel = new EquipeModel(equipeDTO_Recherchee.Code, listePompierModel, equipeDTO_Recherchee.VinVehicule);

                return equipeDTO_Recherchee;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter une équipe à une itervention
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="equipeDTO">L'équipe à ajouter avec code vide</param>
        public void AjouterEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention, EquipeDTO equipeDTO)
        {
            bool OK = false;
            int codeEquipeInt = 0;
            try
            {
                int codeVehicule = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, equipeDTO.VinVehicule).Code;
                int codeIntervention = InterventionRepository.Instance.ObtenirFicheIntervention(nomCaserne, matriculeCapitaine, dateDebutIntervention).CodeTypeIntervention;

                string chiffreCentaineVehicule = codeVehicule.ToString()[0].ToString();
                string codeEquipeSTR = chiffreCentaineVehicule + codeIntervention.ToString();

                codeEquipeInt = int.Parse(codeEquipeSTR);

                EquipeDTO siEquipeExiste = EquipeRepository.Instance.ObtenirEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipeInt);
                if (siEquipeExiste.Code == -1)
                {
                    OK = true;
                }
                else
                {
                    OK = false;
                }
            }
            catch (Exception)
            {
                OK = false;
            }

            if (OK)
            {
                try
                {
                    List<PompierModel> listePompierModel = new List<PompierModel>();
                    foreach (PompierDTO pompierDTO in equipeDTO.ListePompierEquipe)
                    {
                        listePompierModel.Add(new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom));
                    }
                    EquipeModel equipeModel = new EquipeModel(equipeDTO.Code, listePompierModel, equipeDTO.VinVehicule);

                    EquipeRepository.Instance.AjouterEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipeInt, equipeDTO);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Une équipe existe déjà pour ce code à la date de l'intervention.");
            }
        }

        /// <summary>
        /// Méthode de service permettant d'ajouter une équipe à une itervention
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne dans laquelle a lieu l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du capitaine en charge de l'intervention</param>
        /// <param name="dateDebutIntervention">Date du debut de l'intervention</param>
        /// <param name="matriculePompier">Le matricule du pompier à jouter à l'équipe</param>
        public void AjouterPompierEquipe(string nomCaserne, int matriculeCapitaine, string dateDebutIntervention, string vinVehicule, int matriculePompier)
        {
            bool OK = false;
            int codeEquipeInt = 0;
            try
            {
                VehiculeDTO vehiculeIntervention = VehiculeRepository.Instance.ObtenirVehicule(nomCaserne, vinVehicule);
                int codeIntervention = InterventionRepository.Instance.ObtenirFicheIntervention(nomCaserne, matriculeCapitaine, dateDebutIntervention).CodeTypeIntervention;

                string chiffreCentaineVehicule = vehiculeIntervention.Code.ToString()[0].ToString();
                string codeEquipeSTR = chiffreCentaineVehicule + codeIntervention.ToString();

                codeEquipeInt = int.Parse(codeEquipeSTR);

                EquipeDTO equipeExiste = EquipeRepository.Instance.ObtenirEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipeInt);
                if (equipeExiste.Code == -1)
                {
                    //Premier pompier à être ajouté à l'équipe
                    OK = true;
                }
                else
                {
                    int nombreDePlaceRestante = TypesVehiculeRepository.Instance.ObtenirTypeVehicule(vehiculeIntervention.Code).Personnes;
                    if (equipeExiste.ListePompierEquipe.Count < nombreDePlaceRestante)
                    {
                        OK = true;
                    }
                }

                if (OK)
                {
                    PompierDTO pompierDTO = PompierRepository.Instance.ObtenirPompier(matriculePompier, nomCaserne);
                    PompierModel pompierModel = new PompierModel(pompierDTO.Matricule, pompierDTO.Grade, pompierDTO.Nom, pompierDTO.Prenom);
                    List<PompierModel> listePompierModel = new List<PompierModel>();
                    listePompierModel.Add(pompierModel);
                    EquipeModel equipeModel = new EquipeModel(vehiculeIntervention.Code, listePompierModel, vinVehicule);

                    List<PompierDTO> listePompierDTO = new List<PompierDTO>();
                    listePompierDTO.Add(pompierDTO);
                    EquipeDTO equipeDTO = new EquipeDTO(vehiculeIntervention.Code, listePompierDTO, vinVehicule);

                    EquipeRepository.Instance.AjouterEquipe(nomCaserne, matriculeCapitaine, dateDebutIntervention, codeEquipeInt, equipeDTO);
                }
                else
                {
                    throw new Exception("L'équipe est déjà complète.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
