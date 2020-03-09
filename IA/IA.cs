using System;

namespace IA {
    /// <summary>Classe de l'intelligence artificielle.</summary>
    public static class IA {
        /// <summary>Demande à l'intelligence artificielle de trouver le meilleur coups à jouer.</summary>
        /// <param name="plateau">Représentation en chaine du plateau de jeu</param>
        /// <param name="largeur">Largeur du plateau de jeu</param>
        /// <returns>Retourne l'indice du meilleur coups à jouer</returns>
        public static int JouerTour(string plateau, int largeur) {
            if (PremierCoup(plateau)) {
                Random alea = new Random();
                return alea.Next(largeur * largeur);
            }

            char[,] cases = ConvertirChaine(plateau, largeur);

            for (int ligne = 0; ligne < largeur; ligne++)
                for (int col = 0; col < largeur; col++)
                    if (cases[ligne, col] != '.' && cases[ligne, col] != '0') {

                    }

            return -1;
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
