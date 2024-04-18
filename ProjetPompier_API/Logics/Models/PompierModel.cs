/// <summary>
/// Namespace pour les classe de type Models.
/// </summary>
namespace ProjetPompier_API.Logics.Models
{
	/// <summary>
	/// Classe représentant un repository.
	/// </summary>
	public class PompierModel
	{
		#region AttributsProprietes
		/// <summary>
		/// Attribut représentant le matricule du pompier.
		/// </summary>
		private int matricule;
		/// <summary>
		/// Propriété représentant le matricule du pompier.
		/// </summary>
		public int Matricule
		{
			get { return matricule; }
			set
			{
				string matriculeStr = value.ToString();
				if (matriculeStr.Length <= 6)
					matricule = value;
				else
					throw new Exception("Le matricule du pompier doit contenir six chiffres, mettre des 0 si vide");
			}
		}

		/// <summary>
		/// Attribut représentant le grade du pompier.
		/// </summary>
		private int grade;
		/// <summary>
		/// Propriété représentant le grade du pompier.
		/// </summary>
		public int Grade
		{
			get { return grade; }
			set
			{
				if (value > 0)
					grade = value;
				else
					throw new Exception("L'id du grade d'un pompier doit être plus grand que 0.");
			}
		}

		/// <summary>
		/// Attribut représentant le nom du pompier.
		/// </summary>
		private string nom;
		/// <summary>
		/// Propriété représentant le nom du pompier.
		/// </summary>
		public string Nom
		{
			get { return nom; }
			set
			{
				if (value.Length <= 100)
					nom = value;
				else
					throw new Exception("Le nom du pompier doit avoir un maximum de 100 caractères.");
			}
		}

		/// <summary>
		/// Attribut représentant le prenom du pompier.
		/// </summary>
		private string prenom;
		/// <summary>
		/// Propriété représentant le prenom du pompier.
		/// </summary>
		public string Prenom
		{
			get { return prenom; }
			set
			{
				if (value.Length <= 100)
					prenom = value;
				else
					throw new Exception("Le prennom du pompier doit avoir un maximum de 100 caractères.");
			}
		}


		#endregion AttributsProprietes

		#region Constructeurs

		/// <summary>
		/// Constructeur paramétré
		/// </summary>
		/// <param name="matricule">Matricule du pompier</param>
		/// <param name="grade">Grade du pompier</param>
		/// <param name="nom">Nom du pompier</param>
		/// <param name="prenom">Prenom du pompier</param>
		public PompierModel(int unMatricule = 000000, int unGrade = 0, string unNom = "", string unPrenom = "")
		{
			Matricule = unMatricule;
			Grade = unGrade;
			Nom = unNom;
			Prenom = unPrenom;
		}

		#endregion Constructeurs
	}
}
