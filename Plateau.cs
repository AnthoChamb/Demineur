using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    /// <summary>Classe d'un plateau de jeu de démineur</summary>
    public class Plateau {
        Case[,] plateau;

        Plateau(byte taille) {
            plateau = new Case[taille, taille];
            for (byte ligne = 0; ligne < taille; ligne++)
                for (byte col = 0; col < taille; col++)
                    plateau[ligne, col] = new Case();
        }

        /// <summary>Place une mine aux indices précisés et ajuste le compte de mines autours.</summary>
        /// <param name="ligne">Indice de la ligne de la mine à placer</param>
        /// <param name="col">Indice de la colonne de la mine à placer</param>
        public void PlacerMine(byte ligne, byte col) {
            plateau[ligne, col].Mine = true;
            for (int i = -1; i < 2; i+=2)
                for (int j = -1; j < 2; j+=2)
                    plateau[ligne + i, col + j].AjouteMine();
        }
    }
}
