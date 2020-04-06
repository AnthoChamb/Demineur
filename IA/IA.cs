using System;

namespace Demineur {
    /// <summary>Classe de l'intelligence artificielle.</summary>
    public static class IA {
        /// <summary>Demande à l'intelligence artificielle de jouer un coups.</summary>
        /// <param name="plateau">Représentation en chaine du plateau de jeu</param>
        /// <param name="largeur">Largeur du plateau de jeu</param>
        /// <returns>Retourne l'indice sur une dimension du coups à jouer</returns>
        /// <remarks>L'intelligence artificielle peut se tromper, elle cherche à jouer un coups plus efficace que simplement aléatoire. Elle se base sur le compte du nombre de mines des connues dans le voisinage.</remarks>
        public static int JouerCoups(string plateau, int largeur) {
            if (PremierCoup(plateau)) {
                Random alea = new Random();
                return alea.Next(largeur * largeur); // Joue un premier coups aléatoire
            }

            char[,] cases = ConvertirChaine(plateau, largeur);

            int compte, min = -1, coups = -1;

            for (int ligne = 0; ligne < cases.GetLength(0); ligne++) {
                for (int col = 0; col < cases.GetLength(1); col++) {
                    if (cases[ligne, col] == '.') {
                        compte = 0; // Compte du nombre de mines des cases connues dans le voisinage 
                        for (sbyte i = -1; i <= 1; i++)
                            for (sbyte j = -1; j <= 1; j++)
                                try {
                                    if (cases[ligne + i, col + j] != '.')
                                        compte += int.Parse(cases[ligne + i, col + j].ToString());
                                } catch (IndexOutOfRangeException) { }
                        if (compte > 0 && (compte < min || min == -1)) {
                            min = compte; // Garde le compte du coups à jouer
                            coups = ligne * largeur + col; // Garde l'indice sur une dimension du coups à jouer
                        }
                    }
                }
            }

            return coups;
        }

        /// <summary>Converti la chaine du plateau en tableau de caractères en deux dimensions.</summary>
        /// <param name="plateau">Représentation en chaine du plateau de jeu</param>
        /// <param name="largeur">Largeur du plateau de jeu</param>
        /// <returns>Retourne un tableau de caractères en deux dimensions du plateau</returns>
        static char[,] ConvertirChaine(string plateau, int largeur) {
            char[,] cases = new char[largeur, largeur];
            for (int i = 0; i < plateau.Length; i++)
                cases[i / largeur, i % largeur] = plateau[i];
            return cases;
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
    }
}
