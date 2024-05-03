/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Models
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class FicheInterventionModel
    {
        #region AttributsProprietes
        /// <summary>
        /// Attribut représentant la date et l'heure de l'intervention.
        /// </summary>
        private string dateDebut;
        /// <summary>
        /// Propriété représentant la date et l'heure de l'intervention.
        /// </summary>
        public string DateDebut
        {
            get { return dateDebut; }
            set { dateDebut = value; }
        }

        /// <summary>
        /// Attribut représentant la date et l'heure de la fin de l'intervention.
        /// </summary>
        private string dateFin;

        /// <summary>
        /// Propriété représentant la date et l'heure de la fin de l'intervention.
        /// </summary>
        public string DateFin
        {
            get { return dateFin; }
            set { dateFin = value; }
        }


        /// <summary>
        /// Attribut représentant l'adresse de l'intervention.
        /// </summary>
        private string adresse;
        /// <summary>
        /// Propriété représentant l'adresse de l'intervention.
        /// </summary>
        public string Adresse {
            get { return adresse; }
            set
            {
                if (value.Length <= 200)
                    adresse = value;
                else
                    throw new Exception("L'adresse de l'intervention doit avoir un maximum de 200 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le code du type d'intervention.
        /// </summary>
        private int codeTypeIntervention;
        /// <summary>
        /// Propriété représentant le code du type d'intervention.
        /// </summary>
        public int CodeTypeIntervention {
            get { return codeTypeIntervention; }
            set
            {
                string codeTypeInterventionStr = value.ToString();
                if (codeTypeInterventionStr.Length <= 4)
                    codeTypeIntervention = value;
                else
                    throw new Exception("Le code du type d'intervention n'est pas dans la bonne forme, merci de ne mettre pas plus de 4 chiffres");
            }
        }

        /// <summary>
        /// Attribut représentant le resumé de l'intervention.
        /// </summary>
        private string resume;
        /// <summary>
        /// Propriété représentant le resumé de l'intervention.
        /// </summary>
        public string Resume {
            get { return resume; }
            set
            {
                if (value.Length <= 500)
                    resume = value;
                else
                    throw new Exception("Le resumé de l'intervention doit avoir un maximum de 500 caractères.");
            }
        }

        /// <summary>
        /// Attribut représentant le matricule du pompier capitaine.
        /// </summary>
        private int matriculeCapitaine;
        /// <summary>
        /// Propriété représentant le matricule du pompier capitaine.
        /// </summary>
        public int MatriculeCapitaine
        {
            get { return matriculeCapitaine; }
            set
            {
                string matriculeStr = value.ToString();
                if (matriculeStr.Length <= 6)
                    matriculeCapitaine = value;
                else
                    throw new Exception("Le matricule du pompier capitaine doit contenir six chiffres, mettre des 0 si vide");
            }
        }



        #endregion AttributsProprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="dateDebut">Date et heure de l'intervention</param>
        /// <param name="dateFin">Date et heure de fin de l'intervention/param>
        /// <param name="adresse">Adresse de l'intervention</param>
        /// <param name="typeIntervention">Type d'intervention</param>
        /// <param name="resume">Resumé de l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du pompier capitaine</param>
        public FicheInterventionModel(string dateDebut = "1999-01-01 00:00:00", string dateFin = null, string adresse = "", int codeTypeIntervention = 0, string resume = "", int matriculeCapitaine = 000000, string vinvehicule = "")
        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            Adresse = adresse;
            CodeTypeIntervention = codeTypeIntervention;
            Resume = resume;
            MatriculeCapitaine = matriculeCapitaine;
            VinVehicule = vinvehicule;
        }

        #endregion Constructeurs
    }
}
