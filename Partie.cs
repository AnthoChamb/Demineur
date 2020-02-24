using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    /// <summary>Classe d'une partie de démineur.</summary>
    public class Partie {
        Joueur joueur;

        /// <summary>Niveau de difficulté de la partie</summary>
        public enum Difficulte : byte {
            /// <summary>Niveau de difficulté facile</summary>
            FACILE,
            /// <summary>Niveau de difficulté intermédiaire</summary>
            INTERMEDIAIRE,
            /// <summary>Niveau de difficulté difficile</summary>
            DIFFICILE,
            /// <summary>Niveau de difficulté extrême</summary>
            EXTREME
        }
            
        /// <summary>Taille du plateau de la partie</summary>
        public enum Taille : byte {
            /// <summary>Petite taille de plateau</summary>
            PETIT = 10,
            /// <summary>Moyenne taille de plateau</summary>
            MOYEN = 20,
            /// <summary>Grande taille de plateau</summary>
            GRAND = 30
        }

        /// <summary>Crée une partie et lui assigne un joueur.</summary>
        /// <param name="joueur">Joueur assisgné à la partie</param>
        /// <param name="difficulte">Niveau de difficulté de la partie</param>
        /// <param name="taille">Taille du plateau de la partie</param>
        /// <exception cref="ArgumentNullException">Le joueur assigné à la partie ne peut pas être la valeur null</exception>
        public Partie(Joueur joueur, Difficulte difficulte, Taille taille) {
            this.joueur = joueur ?? throw new ArgumentNullException();

        }
    }
}
