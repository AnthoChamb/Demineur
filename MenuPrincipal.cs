using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    class MenuPrincipal {
        /// <summary>Message de bienvenue.</summary>
        public static void Bienvenue() => Console.WriteLine("Bienvenue au jeu de démineur.");

        /// <summary>Affiche le menu principal.</summary>
        public static void AfficherMenu() => Console.WriteLine("Faites un choix parmis les options suivantes:\n"); // TODO : Ajouter les choix du menu

        /// <summary>Reçoit l'entree de l'utilisateur.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string EntreeUtilisateur() => Console.ReadLine();

        /// <summary>Informe l'utilisateur d'une entrée incorrecte.</summary>
        public static void EntreeIncorrecte() => Console.WriteLine("Entrée incorrecte. Veuillez réessayer.");

        /// <summary>Demande à l'utlisateur de choisir un joueur dans la liste pour jouer une partie.</summary>
        public static void DemandeJoueur() => Console.WriteLine("Choisissez un joueur dans la liste ci-dessous pour jouer la partie :");

        /// <summary>Affiche un joueur avec son numéro de choix dans la liste.</summary>
        /// <param name="choix">Numéro du choix</param>
        /// <param name="joueur">Nom du joueur</param>
        public static void AfficherJoueur(int choix, string joueur) => Console.WriteLine(choix + ". " + joueur);

        /// <summary>Informe l'utilisateur qu'il manque de joueurs dans la liste pour commencer une partie.</summary>
        public static void PartieImpossible() => Console.WriteLine("Il n'y a pas assez de joueurs disponibles pour commencer une partie.");

        /// <summary>Demande à l'utilisateur d'entrer un nom pour un nouveau joueur.</summary>
        public static void NouveauJoueur() => Console.WriteLine("Entrez un nom pour un nouveau joueur :");

        /// <summary>Affiche l'entête du classement.</summary>
        /// <param name="difficulte">Niveau de difficulté de la partie</param>
        /// <param name="taille">Taille du plateau de jeu de la partie</param>
        public static void AfficherEnteteClassement(string difficulte, string taille) {

        }

        /// <summary>Affiche le classement du joueur.</summary>
        /// <param name="position">Position du joueur dans le classement</param>
        /// <param name="joueur">Nom du joueur</param>
        /// <param name="secondes">Le temps du joueur en secondes</param>
        public static void AfficherClassement(int position, string joueur, int secondes) => Console.WriteLine("#" + position + "- " + joueur + "\t" + secondes + " secondes");
    }
}
