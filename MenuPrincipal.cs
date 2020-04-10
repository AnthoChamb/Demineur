using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    /// <summary>
    /// Classe du menu principal. 
    /// </summary>
   public class MenuPrincipal {
        /// <summary>Message de bienvenue.</summary>
        public static void Bienvenue() => Console.WriteLine("Bienvenue au jeu de démineur.");

        /// <summary>Affiche le menu principal.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string AfficherMenu() {

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("----------Bienvenue dans le jeu Démineur-----------");
            Console.WriteLine("--                                               --");
            Console.WriteLine("  Veuillez sélectioner l'une des options suivantes : ");
            Console.WriteLine("--         1- Commencer une partie.              --");
            Console.WriteLine("--         2- Créer un joueur.                   --");
            Console.WriteLine("--         3- Afficher le classement.            --");
            Console.WriteLine("--                                               --");
            Console.Write("--   Faites un choix parmis les options  : ");

            return EntreeUtilisateur();     
           
        }
        /// <summary>Affiche le menu pour commencer une partie.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string AfficherCommencerPartie()
        {
            Console.WriteLine("  Veuillez sélectioner l'une des options suivantes : ");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--           Commencer une partie :              --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--     1- Débuter une partie avec l'I.A          --");
            Console.WriteLine("--     2- Débuter une partie multijoueur.        --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--     3- Retour                       --");
            Console.Write("--   Faites un choix parmis les options  : ");

            return EntreeUtilisateur();
        }
        /// <summary>Affiche les choix disponibles pour la difficulté de la partie.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string AfficherChoixDifficulte() {
            Console.WriteLine("  Veuillez sélectioner l'une des options suivantes : ");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--           Niveau de difficulté :              --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--               1- Facile                       --");
            Console.WriteLine("--               2- Intermédiaire                --");
            Console.WriteLine("--               3- Difficile                    --");
            Console.WriteLine("--               4- Extrême                      --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--               5- Retour                       --");
            Console.Write("--   Faites un choix parmis les options : ");

            return EntreeUtilisateur();
        }
        /// <summary>Affiche les choix disponibles pour la taille du plateau.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string AfficherChoixTaille() {
            Console.WriteLine("  Veuillez sélectioner l'une des options suivantes : ");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--            Taille du plateau :                --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--               1- Petit                        --");
            Console.WriteLine("--               2- Moyen                        --");
            Console.WriteLine("--               3- Grand                        --");
            Console.WriteLine("--                                               --");
            Console.WriteLine("--               4- Retour                       --");
            Console.Write("--   Faites un choix parmis les options  : ");

            return EntreeUtilisateur();
        }


        /// <summary>Reçoit l'entree de l'utilisateur.</summary>
        /// <returns>Retourne l'entree de l'utilisateur</returns>
        public static string EntreeUtilisateur() => Console.ReadLine();

        /// <summary>Attend quelconque entree de l'utilisateur.</summary>
        public static void AttenteUtilisateur() {
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        } 
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
        /// <summary>Informe l'utilisateur que le nom choisi est invalide vu qu'il est déjà affcté à un autre joueur.</summary>
        public static void DoublonJoueur() => Console.WriteLine("Le nom que vous avez tenté d'entrer est déjà utilisé, veuillez en utiliser un autre.");
        /// <summary>Affiche l'entête du classement.</summary>
        /// <param name="difficulte">Niveau de difficulté de la partie</param>
        /// <param name="taille">Taille du plateau de jeu de la partie</param>
        public static void AfficherEnteteClassement(string difficulte, string taille) => Console.WriteLine("Classement selon la sélection : "+ difficulte + ", " + taille);

        /// <summary>Affiche une entrée du classement selon le joueur.</summary>
        /// <param name="position">Position du joueur dans le classement</param>
        /// <param name="joueur">Nom du joueur</param>
        /// <param name="temps">Le temps du joueur en milliseconde</param>
        public static void AfficherClassement(int position, string joueur, long temps) => Console.WriteLine("#" + position + "- " + joueur + "\t" + TimeSpan.FromMilliseconds(temps).TotalSeconds + " secondes");
    }
}
