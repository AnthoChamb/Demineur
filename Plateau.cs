using System;

namespace Demineur {
    /// <summary>Classe d'un plateau de jeu de démineur</summary>
    public class Plateau {
        readonly Case[,] plateau;

        /// <summary>Crée un plateau de jeu de démineur carré de la taille spécifiée.</summary>
        /// <param name="taille">Taille du plateau de jeu</param>
        /// <remarks>La notation Grand-O de ce constructeur est O(n^2) où n représente la taille du plateau de jeu carré.</remarks>
        public Plateau(byte taille) {
            plateau = new Case[taille, taille];
            for (byte ligne = 0; ligne < taille; ligne++)
                for (byte col = 0; col < taille; col++)
                    plateau[ligne, col] = new Case();
        }

        /// <summary>Case aux indices précisés.</summary>
        /// <param name="ligne">Indice de la ligne choisie</param>
        /// <param name="col">Indice de la colonne choisie</param>
        /// <returns>Retourne la case aux indices précisés</returns>
        public Case this[int ligne, int col] { get => plateau[ligne, col]; }

        /// <summary>Largeur du plateau de jeu.</summary>
        public int Largeur { get => plateau.GetLength(0); }

        /// <summary>Représentation en chaine du plateau de jeu.</summary>
        /// <returns>Retourne une représentation en chaine du plateau de jeu</returns>
        public override string ToString() {
            string chaine = "";

            for (byte ligne = 0; ligne < Largeur; ligne++)
                for (byte col = 0; col < Largeur; col++)
                    chaine += plateau[ligne, col];
            return chaine;
        }

        /// <summary>Place une mine aux indices précisés et ajuste le compte de mines des cases autours. Évalue d'abord si cette case ne contient pas déjà une mine.</summary>
        /// <param name="ligne">Indice de la ligne de la mine à placer</param>
        /// <param name="col">Indice de la colonne de la mine à placer</param>
        /// <returns>Retourne si la case aux indices précisés ne contient pas déjà une mine</returns>
        /// <remarks>La notation Grand-O de cette méthode va de O(1) si la case aux indices précisés contient déjà une mine jusqu'à O(9) le cas échéant.</remarks>
        public bool PlacerMine(int ligne, int col) {
            if (plateau[ligne, col].Mine)
                return false; // Évalue d'abord si cette case ne contient pas déjà une mine

            plateau[ligne, col].Mine = true; // Place la mine
            for (sbyte i = -1; i <= 1; i++)
                for (sbyte j = -1; j <= 1; j++)
                    try {
                        if (!plateau[ligne + i, col + j].Mine)
                            plateau[ligne + i, col + j].IncrementeCompte(); // Incrémente le compte de mines des cases autours
                    } catch (IndexOutOfRangeException) { } // Évite les exceptions levés par une case se situant en bordure du plateau 
            return true;
        }

        /// <summary>Ouvre la case aux indices précisés et exécute l'action en chaine au besoin.</summary>
        /// <param name="ligne">Indice de la ligne choisie</param>
        /// <param name="col">Indice de la colonne choisie</param>
        /// <remarks>La notation Grand-O de cette méthode va de O(1) si la case ouverte est une mine ou compte au moins une mine autour d'elle jusqu'à O(9n) où n représente le nombre de cases voisines pas encore ouvertes et ne contenant pas une mine d'une case ne contenant pas une mine et comptant aucune mine autour d'elle.</remarks>
        public void OuvrirCase(int ligne, int col) {
            plateau[ligne, col].Ouverte = true;

            if (plateau[ligne, col].Compte == 0 && !plateau[ligne, col].Mine)
                for (sbyte i = -1; i <= 1; i++)
                    for (sbyte j = -1; j <= 1; j++)
                        try {
                            if (!plateau[ligne + i, col + j].Mine && !plateau[ligne + i, col + j].Ouverte)
                                OuvrirCase(ligne + i, col + j); // Exécute l'action en chaine 
                        } catch (IndexOutOfRangeException) { } // Évite les exceptions levés par une case se situant en bordure du plateau
        }

        /// <summary>Évalue si le joueur a gagné.</summary>
        /// <returns>Retourne si le joueur a gagné</returns>
        public bool Gagne() {
            foreach (Case cases in plateau)
                if (!cases.Ouverte && !cases.Mine)
                    return false;
            return true;
        }

        /// <summary>Évalue si le plateau est terminé.</summary>
        /// <returns>Retourne si le plateau est terminé</returns>
        public bool Terminer() {
            foreach (Case cases in plateau)
                if (cases.Ouverte && cases.Mine)
                    return true;
            return Gagne();
        }

        /// <summary>Évalue si il faut jouer le premier coup sur le plateau.</summary>
        /// <returns>Retourne si il faut jouer le premier coup sur le plateau</returns>
        public bool PremierCoup() {
            foreach (Case cases in plateau)
                if (cases.Ouverte)
                    return false;
            return true;
        }

        /// <summary>Ouvre les cases contenant une mine.</summary>
        public void RevelerMines() {
            foreach (Case cases in plateau)
                if (cases.Mine)
                    cases.Ouverte = true;
        }
    }
}
