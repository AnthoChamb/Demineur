using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Demineur {
    /// <summary>Classe d'un jeu de démineur.</summary>
    public class Demineur {
        List<Joueur> joueurs;

        /// <summary>Crée un nouveau joueur de démineur.</summary>
        public Demineur() {
            joueurs = new List<Joueur>();
        }

        /// <summary>Démarre l'exécution du jeu de démineur.</summary>
        public void Demarrer() {
            Partie partie = new Partie(new Joueur("Anthony"), Partie.Difficulte.FACILE, Partie.Taille.PETIT);
            partie.Jouer();
        }

        /// <summary>Ouvre un fichier binaire et y récupère la liste de joueurs.</summary>
        void OuvrirJoueurs() {
            Stream flux = File.Open("joueurs.bin", FileMode.Open);
            BinaryFormatter formatteur = new BinaryFormatter();
            joueurs = (List<Joueur>) formatteur.Deserialize(flux);
            flux.Close();
        }

        /// <summary>Ouvre un fichier binaire et y écrit la liste de joueurs.</summary>
        void SauvegarderJoueurs() {
            Stream flux = File.Open("joueurs.bin", FileMode.Create);
            BinaryFormatter formatteur = new BinaryFormatter();
            formatteur.Serialize(flux, joueurs);
            flux.Close();
        }
    }
}
