using System.Collections.Generic;

namespace Demineur {
    /// <summary>Classe d'un jeu de démineur.</summary>
    public class Demineur {
        List<Joueur> joueurs;
        Partie partie;

        /// <summary>Crée un nouveau joueur de démineur.</summary>
        public Demineur() {
            joueurs = new List<Joueur>();
        }

        /// <summary>Démarre l'exécution du jeu de démineur.</summary>
        public void Demarrer() {
            partie = new Partie(new Joueur("Anthony"), Partie.Difficulte.FACILE, Partie.Taille.PETIT);
            partie.Jouer();
        }
    }
}
