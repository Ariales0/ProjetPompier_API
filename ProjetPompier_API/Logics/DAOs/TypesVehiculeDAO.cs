using ProjetPompier_API.Logics.DTOs;
using ProjetPompier_API.Logics.Exceptions;
using System.Data.SqlClient;
using System.Data;
using ProjetPompier_API.DTOs;

namespace ProjetPompier_API.Logics.DAOs
{
    public class TypesVehiculeRepository : Repository
    {

        #region AttributsProprietes

        /// <summary>
        /// Instance unique du repository.
        /// </summary>
        private static TypesVehiculeRepository instance;

        /// <summary>
        /// Propriété permettant d'accèder à l'instance unique de la classe.
        /// </summary>
        public static TypesVehiculeRepository Instance
        {
            get
            {
                //Si l'instance est null...
                if (instance == null)
                {
                    //... on crée l'instance unique...
                    instance = new TypesVehiculeRepository();
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
        private TypesVehiculeRepository() : base() { }

        #endregion

        #region MethodesService

        /// <summary>
        /// Méthode de service permettant d'obtenir la liste des types de vehicule.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<TypesVehiculeDTO> ObtenirListeTypesVehicule()
        {
            SqlCommand command = new SqlCommand("SELECT * " +
                                                "  FROM TypesVehicule", connexion);

            List<TypesVehiculeDTO> listeTypesVehicule = new List<TypesVehiculeDTO>();

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TypesVehiculeDTO typeVehicule = new TypesVehiculeDTO(reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                    listeTypesVehicule.Add(typeVehicule);
                }
                reader.Close();
                return listeTypesVehicule;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention de la liste des types de vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        public int ObtenirIdTypeVehicule(int code)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                 "  FROM TypesVehicule " +
                                                 " WHERE Code = @code", connexion);

            SqlParameter paramCode = new SqlParameter("@code", SqlDbType.Int);

            paramCode.Value = code;

            command.Parameters.Add(paramCode);

            int id;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
                reader.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention d'un id d'un type par son code...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }   

        public TypesVehiculeDTO ObtenirTypeVehicule(int code)
        {
            SqlCommand command = new SqlCommand(" SELECT * " +
                                                 "  FROM TypesVehicule " +
                                                 " WHERE Code = @code", connexion);

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);

            idParam.Value = code;

            command.Parameters.Add(idParam);

            TypesVehiculeDTO typeVehicule;

            try
            {
                OuvrirConnexion();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                typeVehicule = new TypesVehiculeDTO(reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
                reader.Close();
                return typeVehicule;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'obtention d'un type de vehicule par son Id...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        public void AjouterTypeVehicule(TypesVehiculeDTO typeVehicule)
        {
            SqlCommand command = new SqlCommand(" INSERT INTO T_TypesVehicule " +
                                                " VALUES (@code, @type, @personnes)", connexion);

            SqlParameter codeParam = new SqlParameter("@code", SqlDbType.Int);
            SqlParameter typeParam = new SqlParameter("@type", SqlDbType.VarChar);
            SqlParameter personnesParam = new SqlParameter("@personnes", SqlDbType.Int);

            codeParam.Value = typeVehicule.Code;
            typeParam.Value = typeVehicule.Type;
            personnesParam.Value = typeVehicule.Personnes;

            command.Parameters.Add(codeParam);
            command.Parameters.Add(typeParam);
            command.Parameters.Add(personnesParam);

            try
            {
                OuvrirConnexion();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout d'un type de vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        public void ModifierTypeVehicule(TypesVehiculeDTO typeVehicule)
        {
            SqlCommand command = new SqlCommand(" UPDATE T_TypesVehicule " +
                                                "    SET Type = @type, " +
                                                "    NombrePersonne = @personnes " +
                                                "  WHERE Code = @code", connexion);

            SqlParameter codeParam = new SqlParameter("@code", SqlDbType.Int);
            SqlParameter typeParam = new SqlParameter("@type", SqlDbType.VarChar);
            SqlParameter personnesParam = new SqlParameter("@personnes", SqlDbType.Int);

            codeParam.Value = typeVehicule.Code;
            typeParam.Value = typeVehicule.Type;
            personnesParam.Value = typeVehicule.Personnes;

            command.Parameters.Add(codeParam);
            command.Parameters.Add(typeParam);
            command.Parameters.Add(personnesParam);

            try
            {
                OuvrirConnexion();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification d'un type de vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }


        public void SupprimerTypeVehicule(int code)
        {
            SqlCommand command = new SqlCommand(" DELETE FROM T_TypesVehicule " +
                                                " WHERE Code = @code", connexion);

            SqlParameter codeParam = new SqlParameter("@code", SqlDbType.Int);

            codeParam.Value = code;

            command.Parameters.Add(codeParam);

            try
            {
                OuvrirConnexion();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression d'un type de vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        public void ViderListeTypesVehicule()
        {
            SqlCommand command = new SqlCommand(" DELETE FROM T_TypesVehicule", connexion);

            try
            {
                OuvrirConnexion();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de la liste des types de vehicule...", ex);
            }
            finally
            {
                FermerConnexion();
            }
        }

        #endregion

    }
}
