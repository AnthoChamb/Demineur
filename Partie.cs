using System;
using System.Diagnostics;

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
            this.difficulte = difficulte;
            this.taille = taille;
        }

        /// <summary>Crée une partie pour l'intelligence artificielle.</summary>
        /// <param name="difficulte">Niveau de difficulté de la partie</param>
        /// <param name="taille">Taille du plateau de la partie</param>
        public Partie(Difficulte difficulte, Taille taille) {
            joueur = null;
            this.difficulte = difficulte;
            this.taille = taille;
        }

        /// <summary>Joue la partie.</summary>
        public void Jouer() {
            plateau = new Plateau((byte)taille);
            
            Stopwatch chrono = new Stopwatch();

            if (joueur != null)
                chrono.Start();

            while (!plateau.Terminer())
                JouerTour();

            if (plateau.Gagne()) {
                if (joueur != null) {
                    chrono.Stop();
                    if (chrono.ElapsedMilliseconds > joueur[difficulte, taille])
                        joueur[difficulte, taille] = chrono.ElapsedMilliseconds;
                }
                MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
                MenuPartie.PartieGagne();
            } else {
                plateau.RevelerMines();
                MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
                MenuPartie.MineOuverte();
            }
        }

        /// <summary>Disperse les mines sur le plateau de jeu en excluant les indices à jouer.</summary>
        /// <param name="ligne">Indice de la ligne à jouer</param>
        /// <param name="col">Indice de la colonne à jouer</param>
        void DisperserMines(int ligne, int col) {
            double nbMines = (double)difficulte / 100 * (byte)taille * (byte)taille;
            Random alea = new Random();

            int aleaLigne, aleaCol; // Indices générés aléatoirement

            for (int i = 0; i < nbMines;) {
                aleaLigne = alea.Next((byte)taille);
                aleaCol = alea.Next((byte)taille);
                // Évalue si les indices aléatoires sont les mêmes que les indices à jouer puis si ceux-ci ont déjà une mine
                if ((aleaLigne != ligne || aleaCol != col) && plateau.PlacerMine(aleaLigne, aleaCol))
                    i++;                    
            }
                
        }

        /// <summary>Évalue si il faut jouer le premier coup de la partie.</summary>
        /// <param name="plateau">Représentation en chaine du plateau de jeu</param>
        /// <returns>Retourne si il faut jouer le premier coup de la partie</returns>
        static bool PremierCoup(string plateau) {
            foreach (char element in plateau)
                if (element != '.')
                    return false;
            return true;
        }

        /// <summary>Exécute un tour de la partie.</summary>
        void JouerTour() {
            MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
            MenuPartie.DemandeJoueur();

            int ligne, col; // Ligne et la colonne à jouer

            if (joueur == null) {
                int coups = IA.JouerCoups(plateau.ToString(), plateau.Largeur);
                ligne = coups / plateau.Largeur;
                col = coups % plateau.Largeur;
                Console.WriteLine((col + 1) + " " + (ligne + 1));
            } else {
                do {
                    ligne = col = 0;
                    string[] entree = MenuPartie.EntreeJoueur().Split(' ');

                    try {
                        col = int.Parse(entree[0]);
                        if (col < 1 || col > plateau.Largeur)
                            MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (FormatException) {
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (OverflowException) {
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    }

                    try {
                        ligne = int.Parse(entree[1]);
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
                // Décrémente les entrées de l'utilisateur
                ligne--; 
                col--;
            }

            if (PremierCoup(plateau.ToString()))
                DisperserMines(ligne, col);

            plateau.OuvrirCase(ligne, col);
        }
    }
}
