using System;

using System.Collections.Generic;

namespace Demineur {
    /// <summary>Classe d'un joueur.</summary>
    public class Joueur {
        string nom;
        Dictionary<Tuple<Partie.Difficulte, Partie.Taille>, int> temps;

        /// <summary>Crée un nouveau joueur.</summary>
        /// <param name="nom">Nom du joueur</param>
        /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public Joueur(string nom) {
            this.nom = nom ?? throw new ArgumentNullException();
            temps = new Dictionary<Tuple<Partie.Difficulte, Partie.Taille>, int>();
        }

        /// <summary>Nom du joueur.</summary>
        /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public string Nom { get => nom; set => nom = value ?? throw new ArgumentNullException(); }

        /// <summary>Nom du joueur.</summary>
        /// <returns>Retourne le nom du joueur</returns>
        public override string ToString() => nom;
    }
}
