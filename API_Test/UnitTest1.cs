using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Controleurs;
using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Modeles;

namespace API_Test
{
    [Collection("Sequential")]
    public class UnitTest1
    {
        //[Fact]
        //public void TestMethodeObtenirListeCaserne()
        //{
        //    CaserneDTO maCaserneDTO = new CaserneDTO("CaseneTest", "adresse", "ville", "province", "tel");
        //    CaserneControleur.Instance.AjouterCaserne(maCaserneDTO);
        //    CaserneModel maCaserne = new CaserneModel(maCaserneDTO.Nom, maCaserneDTO.Adresse, maCaserneDTO.Ville, maCaserneDTO.Province, maCaserneDTO.Telephone);

        //    List<CaserneDTO> listeCasernes = CaserneControleur.Instance.ObtenirListeCaserne();
        //    int nombreCaserne = listeCasernes.Count();

        //    Assert.Equal(nombreCaserne, listeCasernes = listeCasernes.Count());
    }
} 