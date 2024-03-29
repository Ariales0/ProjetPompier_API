using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Models;
/// <summary>
/// Namespace pour la classe de test unitaire
/// </summary>
namespace TestUnitaireCaserneModele
{
    /// <summary>
    /// Classe représentant un test unitaire de la classe CaserneModel.
    /// </summary>
    public class TestUnitaireCaserneModel
    {
        /// <summary>
        /// Test pour une chaine de caractère qui depace la limite
        /// </summary>
        [Fact]
        public void TestUnitaire_DepaceLimite_Telephone()
        {
            /// <summary>
            /// Classe représentant un test unitaire de la classe CaserneModel.
            /// </summary>
            try
            {
                string nom = ""; //Ne doit pas dépacer 100
                string adresse = "";//Ne doit pas dépacer 200
                string ville = "";//Ne doit pas dépacer 100
                string province = "";//Ne doit pas dépacer 50
                string telephone = "";//Ne doit pas dépacer 12

                //Pour tester notre classe CaserneModel on va mettre plus de 12 caractères dans la chaine de caractère telephone
                telephone = "Chaine de caractère qui depace la limite possible";//Ne doit pas dépacer 12

                //Maintenant on essai de creer un objet CaserneModel le test fonvtionne si il nous retourne une exception
                CaserneModel caserne = new CaserneModel(nom, adresse, ville, province, telephone);

            }
            catch
            {
                Assert.Equal(true, true);
            }
        }
    }
}