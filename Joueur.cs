using System;

namespace Demineur {
    /// <summary>Classe d'un joueur.</summary>
    public class Joueur : IComparable<Joueur> {
        string nom;
        uint victoire;

        /// <summary>Crée un nouveau joueur.</summary>
        /// <param name="nom">Nom du joueur</param>
        /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public Joueur(string nom) {
            this.nom = nom ?? throw new ArgumentNullException();
            victoire = 0;
        }

        /// <summary>Nom du joueur.</summary>
        /// /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public string Nom { get => nom; set => nom = value ?? throw new ArgumentNullException(); }
        /// <summary>Nombre de victoire du joueurs.</summary>
        public uint Victoire { get => victoire; }

        /// <summary>Ajoute une victoire au joueur.</summary>
        public void Gagne() => victoire++;

        /// <summary>Nom du joueur.</summary>
        /// <returns>Retourne le nom du joueur</returns>
        public override string ToString() => nom;

        /// <summary>Compare le nombre de victoire de cette instance avec celui d'un autre joueur et indique si celui-ci précède, suit ou se situe à la même position d'un tri que cette instance.</summary>
        /// <param name="joueur">Le joueur à comparer avec cette instance.</param>
        /// <returns>Retourne un entier indiquant si le joueur spécifié en paramètre précède, suit ou se situe à la même position d'un tri que cette instance.</returns>
        public int CompareTo(Joueur joueur) => victoire.CompareTo(joueur.victoire);
    }
}
