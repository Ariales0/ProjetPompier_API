using ProjetPompier_API.Logics.DTOs;
using System.Data.SqlClient;
using System.Data;

namespace ProjetPompier_API.Logics.DAOs
{
    public class VehiculeRepository : Repository
    {

        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static VehiculeRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static VehiculeRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new VehiculeRepository();
                }
                //...on retourne l'instance unique.
                return instance;
            }
        }

        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur privée du repository.
        /// </summary>
        private VehiculeRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Methode permettant d'obtenir la liste des vehicules.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="disponibleSeulement">Les vehicules qui ne sont pas dans une interventions</param>
        /// <returns>Retourne la liste des vehicules</returns>
        public List<VehiculeDTO> ObtenirListeVehicules(string nomCaserne, bool disponibleSeulement)
        {
            string requeteVehiculeCaserne = "SELECT T_Vehicules.Vin, " +
                                                "T_TypesVehicule.Code, " +
                                                "T_Vehicules.Marque, " +
                                                "T_Vehicules.Modele, " +
                                                "T_Vehicules.Annee " +
                                                "FROM T_Vehicules " +
                                                "INNER JOIN T_Casernes ON T_Casernes.IdCaserne = T_Vehicules.IdCaserne " +
                                                "INNER JOIN T_TypesVehicule ON T_TypesVehicule.IdTypeVehicule = T_Vehicules.IdTypeVehicule " +
                                                "WHERE T_Casernes.Nom = @nomCaserne";

            string extensionDisponible = " AND T_Vehicules.IdVehicule NOT IN " +
                                                               "(" +
                                                               "SELECT T_Equipes.IdVehicule " +
                                                               "FROM T_Equipes " +
                                                               "INNER JOIN T_FichesIntervention ON T_Equipes.IdIntervention = T_FichesIntervention.IdFicheIntervention " +
                                                               "WHERE T_FichesIntervention.DateFin IS NULL " +
                                                               ")";

            string requeteComplete = (disponibleSeulement) ? requeteVehiculeCaserne + extensionDisponible : requeteVehiculeCaserne;
            SqlCommand command = new SqlCommand(requeteComplete, connexion);

            SqlParameter nomCaserneParam = new SqlParameter("@nomCaserne", SqlDbType.VarChar, 100);
            nomCaserneParam.Value = nomCaserne;
            command.Parameters.Add(nomCaserneParam);

            List<VehiculeDTO> listeVehicules = new List<VehiculeDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VehiculeDTO vehiculeDTO = new VehiculeDTO(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    listeVehicules.Add(vehiculeDTO);
                }
                reader.Close();
                return listeVehicules;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Methodes permettant d'obtenir l'id d'un vehicule par son vin.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <returns>Retourne le id</returns>
        /// <exception cref="Exception"></exception>
        public int ObtenirIdVehicule(string nomCaserne, string vinVehicule)
        {
            SqlCommand command = new SqlCommand(" SELECT IdVehicule " +
                                                    "   FROM T_Vehicules WHERE Vin=@vin AND IdCaserne= @idCaserne  ", connexion);

            SqlParameter vinParam = new SqlParameter("@vin", SqlDbType.VarChar, 17);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            vinParam.Value = vinVehicule;
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(vinParam);
            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de l'id du véhicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Methode permettant d'obtenir un vehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <returns>Retourne le vehicule</returns>
        /// <exception cref="Exception"></exception>
        public VehiculeDTO ObtenirVehicule(string nomCaserne, string vinVehicule)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                "   FROM T_Vehicules WHERE IdVehicule=@idVehicule AND IdCaserne= @idCaserne  ", connexion);

            SqlParameter idVehiculeParam = new SqlParameter("@idVehicule", SqlDbType.Int);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            idVehiculeParam.Value = ObtenirIdVehicule(nomCaserne, vinVehicule);
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(idVehiculeParam);
            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                VehiculeDTO vehiculeDTO = new VehiculeDTO(reader.GetString(1), TypesVehiculeRepository.Instance.ObtenirTypeVehiculeParId(reader.GetInt32(2)).Code, reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                return vehiculeDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention du véhicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Methodes permettant d'ajouter un vehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehiculeDTO">Le DTO du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void AjouterVehicule(string nomCaserne, VehiculeDTO vehiculeDTO)
        {
            SqlCommand command = new SqlCommand(" INSERT INTO T_Vehicules (Vin, IdTypeVehicule, Marque, Modele, Annee, IdCaserne) " +
                                                               " VALUES (@vin, @idTypeVehicule, @marque, @modele, @annee, @idCaserne) ", connexion);

            SqlParameter vinParam = new SqlParameter("@vin", SqlDbType.VarChar, 17);
            SqlParameter idTypeVehiculeParam = new SqlParameter("@idTypeVehicule", SqlDbType.Int);
            SqlParameter marqueParam = new SqlParameter("@marque", SqlDbType.VarChar, 30);
            SqlParameter modeleParam = new SqlParameter("@modele", SqlDbType.VarChar, 50);
            SqlParameter anneeParam = new SqlParameter("@annee", SqlDbType.Int);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            vinParam.Value = vehiculeDTO.Vin;
            idTypeVehiculeParam.Value = TypesVehiculeRepository.Instance.ObtenirIdTypeVehicule(vehiculeDTO.Code);
            marqueParam.Value = vehiculeDTO.Marque;
            modeleParam.Value = vehiculeDTO.Modele;
            anneeParam.Value = vehiculeDTO.Annee;
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(vinParam);
            command.Parameters.Add(idTypeVehiculeParam);
            command.Parameters.Add(marqueParam);
            command.Parameters.Add(modeleParam);
            command.Parameters.Add(anneeParam);
            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du véhicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Methodes permettant de modifier un vehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehiculeDTO">Le DTO du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void ModifierVehicule(string nomCaserne, VehiculeDTO vehiculeDTO)
        {
            SqlCommand command = new SqlCommand(" UPDATE T_Vehicules " +
                                                "    SET IdTypeVehicule = @idTypeVehicule, Marque = @marque, Modele = @modele, Annee = @annee " +
                                                "  WHERE Vin = @vin AND IdCaserne = @idCaserne ", connexion);

            SqlParameter idTypeVehiculeParam = new SqlParameter("@idTypeVehicule", SqlDbType.Int);
            SqlParameter vinParam = new SqlParameter("@vin", SqlDbType.VarChar, 17);
            SqlParameter marqueParam = new SqlParameter("@marque", SqlDbType.VarChar, 30);
            SqlParameter modeleParam = new SqlParameter("@modele", SqlDbType.VarChar, 50);
            SqlParameter anneeParam = new SqlParameter("@annee", SqlDbType.Int);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            idTypeVehiculeParam.Value = TypesVehiculeRepository.Instance.ObtenirIdTypeVehicule(vehiculeDTO.Code);
            vinParam.Value = vehiculeDTO.Vin;
            marqueParam.Value = vehiculeDTO.Marque;
            modeleParam.Value = vehiculeDTO.Modele;
            anneeParam.Value = vehiculeDTO.Annee;
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(idTypeVehiculeParam);
            command.Parameters.Add(vinParam);
            command.Parameters.Add(marqueParam);
            command.Parameters.Add(modeleParam);
            command.Parameters.Add(anneeParam);
            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du véhicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// methodes permettant de supprimer un vehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du vehicule</param>
        /// <exception cref="Exception"></exception>
        public void SupprimerVehicule(string nomCaserne, string vinVehicule)
        {
            SqlCommand command = new SqlCommand(" DELETE FROM T_Vehicules " +
                                                " WHERE Vin = @vin AND IdCaserne = @idCaserne ", connexion);

            SqlParameter vinParam = new SqlParameter("@vin", SqlDbType.VarChar, 17);
            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            vinParam.Value = vinVehicule;
            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(vinParam);
            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du véhicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        /// <summary>
        /// Methode permettant de vider la liste des vehicules.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <exception cref="Exception"></exception>
        public void ViderListeVehicules(string nomCaserne)
        {
            SqlCommand command = new SqlCommand(" DELETE FROM T_Vehicules " +
                                                " WHERE IdCaserne = @idCaserne ", connexion);

            SqlParameter idCaserneParam = new SqlParameter("@idCaserne", SqlDbType.Int);

            idCaserneParam.Value = CaserneRepository.Instance.ObtenirIdCaserne(nomCaserne);

            command.Parameters.Add(idCaserneParam);

            try
            {
                OuvrirConnexion();
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de la liste des véhicules...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        #endregion

    }
}
