using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    /// <summary>Classe d'un plateau de jeu de démineur</summary>
    public class Plateau {
        readonly Case[,] plateau;

        /// <summary>Crée un plateau de jeu de démineur carré de la taille spécifiée.</summary>
        /// <param name="taille">Taille du plateau de jeu</param>
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

        /// <summary>Place une mine aux indices précisés et ajuste le compte de mines autours. Évalue d'abord si cette case ne contient pas déjà une mine.</summary>
        /// <param name="ligne">Indice de la ligne de la mine à placer</param>
        /// <param name="col">Indice de la colonne de la mine à placer</param>
        /// <returns>Retourne si cette case ne contient pas déjà une mine</returns>
        public bool PlacerMine(int ligne, int col) {
            if (plateau[ligne, col].Mine)
                return false; // Évalue d'abord si cette case ne contient pas déjà une mine

            plateau[ligne, col].Mine = true; // Place la mine
            for (sbyte i = -1; i <= 1; i++)
                for (sbyte j = -1; j <= 1; j++)
                    try {
                        if (!plateau[ligne + i, col + j].Mine)
                            plateau[ligne + i, col + j].AjouteMine(); // Ajuste le compte de mines
                    } catch (IndexOutOfRangeException) { }
            return true;
        }

        /// <summary>Ouvre la case aux indices précisés et exécute l'action en chaine au besoin.</summary>
        /// <param name="ligne">Indice de la ligne choisie</param>
        /// <param name="col">Indice de la colonne choisie</param>
        public void OuvrirCase(int ligne, int col) {
            plateau[ligne, col].Ouverte = true;

            if (plateau[ligne, col].Compte == 0 && !plateau[ligne, col].Mine)
                for (sbyte i = -1; i <= 1; i++)
                    for (sbyte j = -1; j <= 1; j++)
                        try {
                            if (!plateau[ligne + i, col + j].Mine && !plateau[ligne + i, col + j].Ouverte)
                                OuvrirCase(ligne + i, col + j);
                        } catch (IndexOutOfRangeException) { }
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

        /// <summary>Ouvre les cases contenant une mine.</summary>
        public void RevelerMines() {
            foreach (Case cases in plateau)
                if (cases.Mine)
                    cases.Ouverte = true;
        }
    }
}
