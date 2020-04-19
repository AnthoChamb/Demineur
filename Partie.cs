using System;
using System.Diagnostics;

namespace Demineur {
    /// <summary>Classe d'une partie de démineur.</summary>
    public class Partie {
        readonly Joueur joueur;
        Plateau plateau;
        readonly Difficulte difficulte;
        readonly Taille taille;

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
            Stopwatch chrono = new Stopwatch(); // Chronomètre la durée de la partie d'un joueur

            if (joueur != null)
                chrono.Start();

            while (!plateau.Terminer())
                JouerTour();

            if (plateau.Gagne()) {
                if (joueur != null) {
                    chrono.Stop();
                    if (chrono.ElapsedMilliseconds < joueur[difficulte, taille])
                        joueur[difficulte, taille] = chrono.ElapsedMilliseconds; // Mets à jour le meilleur temps du joueur
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
        /// <remarks>La notation Grand-O de cette méthode est d'au moins O(9n) où n représente le nombre de mines à disperser, sa complexité pouvant varié dû à sa nature aléatoire.</remarks>
        void DisperserMines(int ligne, int col) {
            double nbMines = (double)difficulte / 100 * (byte)taille * (byte)taille; // Nombre de mines à disperser
            Random alea = new Random();

            int aleaLigne, aleaCol; // Indices générés aléatoirement

            for (int i = 0; i < nbMines;) {
                aleaLigne = alea.Next((byte)taille);
                aleaCol = alea.Next((byte)taille);
                // Ajoute une mine si les indices aléatoires ne sont pas les mêmes que les indices à jouer puis si ceux-ci n'ont pas déjà une mine
                if ((aleaLigne != ligne || aleaCol != col) && plateau.PlacerMine(aleaLigne, aleaCol)) // Plateau.PlacerMine(int ligne, int col) cache une complexité O(9)
                    i++;                    
            }
                
        }

        /// <summary>Exécute un tour de la partie.</summary>
        void JouerTour() {
            MenuPartie.AfficherPlateau(plateau.ToString(), plateau.Largeur);
            
            int ligne, col; // Ligne et la colonne à jouer

            if (joueur == null) { // Intelligence artificielle
                int coups = IA.JouerCoup(plateau.ToString(), plateau.Largeur);
                ligne = coups / plateau.Largeur;
                col = coups % plateau.Largeur;
            } else { // Joueur humain
                do {
                    MenuPartie.DemandeJoueur();
                    ligne = col = 0;
                    string[] entree = MenuPartie.EntreeJoueur().Split(' ');

                    try {
                        col = int.Parse(entree[0]);
                        if (col < 1 || col > plateau.Largeur) // L'entrée de l'utilisateur n'est pas un indice du plateau valide
                            MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (FormatException) { // L'entrée de l'utilisateur n'est pas un nombre entier
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    } catch (OverflowException) { // L'entrée de l'utilisateur dépasse la valeur d'un int
                        MenuPartie.EntreeIncorrecte("colonne", plateau.Largeur);
                    }

                    try {
                        ligne = int.Parse(entree[1]);
                        if (ligne < 1 || ligne > plateau.Largeur) { // L'entrée de l'utilisateur n'est pas un indice du plateau valide
                            MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                        } else if (col <= plateau.Largeur && col > 0 && plateau[ligne - 1, col - 1].Ouverte) { // La case désirée ne peut pas être déjà ouverte, cela est vérifié que si le numéro de colonne est valide
                            MenuPartie.CaseOuverte(ligne, col);
                            col = 0; // Le numéro de colonne ne remplit plus la condition de sortie
                        }
                    } catch (FormatException) { // L'entrée de l'utilisateur n'est pas un nombre entier
                        MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                    } catch (OverflowException) { // L'entrée de l'utilisateur dépasse la valeur d'un int
                        MenuPartie.EntreeIncorrecte("ligne", plateau.Largeur);
                    } catch (IndexOutOfRangeException) { // L'entrée de l'utilisateur ne contient pas une espace
                        MenuPartie.ErreurEspace();
                    }
                } while (col < 1 || col > plateau.Largeur || ligne < 1 || ligne > plateau.Largeur); // La colonne et la ligne désirée doit être un nombre valide pour continuer
                // Décrémente les entrées de l'utilisateur
                ligne--; 
                col--;
            }

            if (plateau.PremierCoup())
                DisperserMines(ligne, col);

            plateau.OuvrirCase(ligne, col);
        }
    }
}
