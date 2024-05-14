using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;

namespace TestFonctionnelIteration5
{
    public class TestFonctionnel_Iteration5
    {
        /// <summary>
        /// Test fonctionnel d'un ajout d'un type d'intervention
        /// </summary>
        [Fact]
        public void NouveauTypeIntervention()
        {
            //Le type d'intervention pour notre test
            TypeInterventionDTO typeInterventionDTO = new TypeInterventionDTO(99,"Nouveau type intervention");

            //On recupere les types intervention deja existante
            List<TypeInterventionDTO> listeDesTypeInterventionBDD = TypesInterventionControleur.Instance.ObtenirListeTypesIntervention();
            int nombreDeType = listeDesTypeInterventionBDD.Count;

            //On ajout le nouveau type dans la base de donnees
            TypesInterventionControleur.Instance.AjouterTypeIntervention(typeInterventionDTO);

            //On verifie qu'il y a un type d' intervention en plus dans la base de données
            List<TypeInterventionDTO> nouvelleListeDesTypeInterventionBDD = TypesInterventionControleur.Instance.ObtenirListeTypesIntervention();
            int nouveauNombreDeType = nouvelleListeDesTypeInterventionBDD.Count;
            //Le nombre de type actuel devrait etre plus grand de 1
            Assert.Equal(nombreDeType+1, nouveauNombreDeType);

            //On supprime notre type de test
            TypesInterventionControleur.Instance.SupprimerTypeIntervention(typeInterventionDTO.Code);
        }
    }
}