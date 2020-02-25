using System;

namespace Demineur {
    /// <summary>Classe du menu des parties.</summary>
    public static class MenuPartie {
        /// <summary>Formatte le plateau de jeu et l'affiche à l'écran</summary>
        /// <param name="plateau">Représentation en chaine du plateau de jeu</param>
        /// <param name="largeur">Largeur du plateau de jeu</param>
        /// <returns>Une chaine formattée du plateau de jeu</returns>
        public static void AfficherPlateau(string plateau, int largeur) {
            string format = plateau[0].ToString();

            for (int i = 1; i < plateau.Length; i++)
                format += (i % largeur == 0 ? "\n" : " ") + plateau[i];
             Console.WriteLine(format);
        }

        /// <summary>Demande l'entrée du joueur.</summary>
        public static void DemandeJoueur() => Console.Write("Entrez le numéro de la colonne puis de la ligne désirée, séparé par un espace: ");

        /// <summary>Récupère l'entrée du joueur</summary>
        /// <returns>Retourne l'entrée du joueur</returns>
        public static string EntreeJoueur() => Console.ReadLine();

        /// <summary>Informe l'utilisateur d'une entrée incorrecte.</summary>
        /// <param name="champs">Champs incorrecte, ligne ou colonne</param>
        /// <param name="largeur">Largeur du plateau de jeu</param>
        public static void EntreeIncorrecte(string champs, int largeur) => Console.WriteLine("Entrée du numéro de " + champs + " incorrecte. Veuillez bien entrer un numéro de " + champs + " entre 1 et " +  largeur + ".");

        /// <summary>Infore l'utilisateur qu'il manque un espace</summary>
        public static void ErreurEspace() => Console.WriteLine("Aucun espace entré. Veuillez bien entrer un espace pour séparer le numéro de colonne et le numéro de ligne");


        /// <summary>Informe l'utilisateur que la case est déjà ouverte</summary>
        /// <param name="ligne">Indice de la ligne choisie</param>
        /// <param name="col">Indice de la colonne choisie</param>
        public static void CaseOuverte(int ligne, int col) => Console.WriteLine("La case à la ligne numéro " + ligne + " de la colonne numéro " + col + " est déjà ouverte. Veuillez entrer une autre paire de colonne et de ligne.");

        /// <summary>Informe l'utilisateur qu'il a ouvert une mine.</summary>
        public static void MineOuverte() => Console.WriteLine("Vous avez ouvert une mine. Partie terminée.");

        /// <summary>Informe l'utilisateur qu'il a gagné la partie.</summary>
        public static void PartieGagne() => Console.WriteLine("Félicitations! Vous avez gagné la partie.");
    }
}
