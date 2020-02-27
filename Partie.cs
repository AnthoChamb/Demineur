using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Demineur {
    /// <summary>Classe d'une partie de démineur.</summary>
    public class Partie {
        readonly Joueur joueur;
        Plateau plateau;
        Difficulte difficulte;
        Taille taille;

        /// <summary>Niveau de difficulté de la partie</summary>
        public enum Difficulte : byte {
            /// <summary>Niveau de difficulté facile</summary>
            FACILE = 10,
            /// <summary>Niveau de difficulté intermédiaire</summary>
            INTERMEDIAIRE = 15,
            /// <summary>Niveau de difficulté difficile</summary>
            DIFFICILE = 20,
            /// <summary>Niveau de difficulté extrême</summary>
            EXTREME = 30
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
            plateau = new Plateau((byte) taille);
            this.difficulte = difficulte;
            this.taille = taille;

            double nbMines = (double) difficulte / 100 * (byte) taille * (byte) taille;
            Random alea = new Random();

            for (byte i = 0; i < nbMines;)
                if (plateau.PlacerMine(alea.Next((byte) taille), alea.Next((byte) taille)))
                    i++;
        }

        /// <summary>Joue la partie.</summary>
        public void Jouer() {
            Stopwatch chrono = new Stopwatch();
            chrono.Start(); //
            while (!plateau.Gagne()) {
                MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
                MenuPartie.DemandeJoueur();
                byte ligne, col; // Entrées d'utilisateur pour la ligne et la colonne désirée

                do {
                    ligne = col = 0;
                    string[] entree = MenuPartie.EntreeJoueur().Split(' ');

                    try {
                        col = byte.Parse(entree[0]);
                        if (col < 1 || col > plateau.Largeur)
                            MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (FormatException) {
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (OverflowException) {
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    }

                    try {
                        ligne = byte.Parse(entree[1]);
                        if (ligne < 1 || ligne > plateau.Largeur) {
                            MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                        } else if (col <= plateau.Largeur && col > 0 && plateau[ligne - 1, col - 1].Ouverte) { // La case désirée ne peut pas être déjà ouverte, cela est vérifié que si le numéro de colonne est valide
                            MenuPartie.CaseOuverte(ligne, col);
                            col = 0; // Le numéro de colonne ne remplit plus la condition de sortie
                        }
                    } catch (FormatException) {
                        MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                    } catch (OverflowException) {
                        MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                    } catch (IndexOutOfRangeException) {
                        MenuPartie.ErreurEspace();
                    }
                } while (col < 1 || col > plateau.Largeur || ligne < 1 || ligne > plateau.Largeur); // La colonne et la ligne désirée doit être un nombre valide pour continuer

                plateau.OuvrirCase(ligne - 1, col - 1);
                if (plateau[ligne - 1, col - 1].Mine) {
                    plateau.RevelerMines();
                    MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
                    MenuPartie.MineOuverte();
                    return;
                }
            }
            chrono.Stop();
            if (chrono.ElapsedMilliseconds > joueur[difficulte, taille]) 
                joueur[difficulte, taille] = chrono.ElapsedMilliseconds;
            MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
            MenuPartie.PartieGagne();
        }
    }
}
