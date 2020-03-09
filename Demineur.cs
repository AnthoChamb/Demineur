using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Demineur {
    /// <summary>Classe d'un jeu de démineur.</summary>
    public class Demineur {
        List<Joueur> joueurs;

        /// <summary>Crée un nouveau jeu de démineur.</summary>
        public Demineur() {
            joueurs = new List<Joueur>();
            OuvrirJoueurs();
        }
        /// <summary>Sauvegarde lors de la fin du jeu de démineur.</summary>
        ~Demineur() {
            SauvegarderJoueurs();
        }

        /// <summary>Démarre l'exécution du jeu de démineur.</summary>
        public void Demarrer() {
            bool sortie = true;
            while (sortie) {
                switch (MenuPrincipal.AfficherMenu()) {
                    case "1":
                        Partie partie = new Partie(new Joueur("Anthony"), Partie.Difficulte.FACILE, Partie.Taille.PETIT);
                        partie.Jouer();
                        break;
                    case "2":
                        NouveauJoueur();
                        break;
                    case "3":

                        break;
                }

            }

        }

        /// <summary>Ouvre un fichier binaire et y récupère la liste de joueurs.</summary>
        void OuvrirJoueurs() {
            Stream flux = File.Open("joueurs.bin", FileMode.Open);
            BinaryFormatter formatteur = new BinaryFormatter();
            joueurs = (List<Joueur>)formatteur.Deserialize(flux);
            flux.Close();
        }

        /// <summary>Ouvre un fichier binaire et y écrit la liste de joueurs.</summary>
        void SauvegarderJoueurs() {
            Stream flux = File.Open("joueurs.bin", FileMode.Create);
            BinaryFormatter formatteur = new BinaryFormatter();
            formatteur.Serialize(flux, joueurs);
            flux.Close();
        }

        void NouveauJoueur() {
            
            bool doublon = true;
            bool ajout = true;
            string nom;

            while (ajout) {
                MenuPrincipal.NouveauJoueur();
                nom =  MenuPrincipal.EntreeUtilisateur();

                foreach (Joueur element in joueurs) {
                    if (nom == element.Nom) {
                        MenuPrincipal.DoublonJoueur();
                        doublon = false;
                    }
                }
                if (doublon) {
                    joueurs.Add(new Joueur(nom));
                    ajout = false;
                }
            }

        }
    }
}
