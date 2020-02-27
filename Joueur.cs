using System;

using System.Collections.Generic;

namespace Demineur {
    /// <summary>Classe d'un joueur.</summary>
    public class Joueur {
        string nom;
        readonly Dictionary<(Partie.Difficulte, Partie.Taille), long> temps;

        /// <summary>Crée un nouveau joueur.</summary>
        /// <param name="nom">Nom du joueur</param>
        /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public Joueur(string nom) {
            this.nom = nom ?? throw new ArgumentNullException();
            temps = new Dictionary<(Partie.Difficulte, Partie.Taille), long>();
        }

        /// <summary>Nom du joueur.</summary>
        /// <exception cref="ArgumentNullException">Le nom du joueur ne peut pas être la valeur null</exception>
        public string Nom { get => nom; set => nom = value ?? throw new ArgumentNullException(); }

        /// <summary>Meilleurs temps en millisecondes du joueur pour la difficulté et la taille d'une partie.</summary>
        /// <param name="difficulte">Niveau de difficulté de la partie</param>
        /// <param name="taille">Taille du plateau de jeu de la partie</param>
        /// <returns>Retourne le meilleur temps en millisecondes du joueur pour la difficulté et la taille d'une partie</returns>
        public long this[Partie.Difficulte difficulte, Partie.Taille taille] { get => temps[(difficulte, taille)]; set => temps[(difficulte, taille)] = value;  }

        /// <summary>Nom du joueur.</summary>
        /// <returns>Retourne le nom du joueur</returns>
        public override string ToString() => nom;
    }
}
