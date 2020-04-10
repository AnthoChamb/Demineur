﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Demineur
{
    /// <summary>Classe d'un jeu de démineur.</summary>
    public class Demineur
    {
        List<Joueur> joueurs;

        /// <summary>Crée un nouveau jeu de démineur.</summary>
        public Demineur()
        {
            OuvrirJoueurs();
        }

        /// <summary>Sauvegarde lors de la fin du jeu de démineur.</summary>
        ~Demineur()
        {
            SauvegarderJoueurs();
        }

        /// <summary>Démarre l'exécution du jeu de démineur.</summary>
        public void Demarrer()
        {
            bool sortie = true;
            while (sortie)
            {
                switch (MenuPrincipal.AfficherMenu())
                {
                    case "1":
                        Partie partie = null;
                        bool continuer = true;

                        while (continuer)
                        {
                            switch (MenuPrincipal.AfficherCommencerPartie()) {
                                case "1":
                                    partie = new Partie(SelectionDifficulte(), SelectionTaille());
                                    continuer = false;
                                    break;
                                case "2":
                                    partie = new Partie(SelectionJoueur(), SelectionDifficulte(), SelectionTaille());
                                    continuer = false;
                                    break;
                                case "3":
                                    continuer = false;
                                    break;

                                default:
                                    MenuPrincipal.EntreeIncorrecte();
                                    MenuPrincipal.AttenteUtilisateur();
                                    break;
                            }
                        }
                        partie.Jouer();

                        break;
                    case "2":
                        NouveauJoueur();
                        break;
                    case "3":
                       
                        break;
                }

            }

        }

        /// <summary>Ouvre un fichier binaire et y récupère la liste de joueurs.</summary>
        void OuvrirJoueurs()
        {
            try
            {
                Stream flux = File.Open("joueurs.bin", FileMode.Open);
                BinaryFormatter formatteur = new BinaryFormatter();
                joueurs = (List<Joueur>)formatteur.Deserialize(flux);
                flux.Close();
            }
            catch (FileNotFoundException)
            {
                joueurs = new List<Joueur>();
            }
        }

        /// <summary>Ouvre un fichier binaire et y écrit la liste de joueurs.</summary>
        void SauvegarderJoueurs()
        {
            Stream flux = File.Open("joueurs.bin", FileMode.Create);
            BinaryFormatter formatteur = new BinaryFormatter();
            formatteur.Serialize(flux, joueurs);
            flux.Close();
        }

        /// <summary>Vérification et ajout d'un nouveau joueur.</summary>
        void NouveauJoueur()
        {

            bool doublon = true;
            bool ajout = true;
            string nom;

            while (ajout)
            {
                MenuPrincipal.NouveauJoueur();
                nom = MenuPrincipal.EntreeUtilisateur();

                foreach (Joueur element in joueurs)
                {
                    if (nom == element.Nom)
                    {
                        MenuPrincipal.DoublonJoueur();
                        doublon = false;
                        MenuPrincipal.AttenteUtilisateur();
                    }
                }
                if (doublon)
                {
                    joueurs.Add(new Joueur(nom));
                    ajout = false;
                }
            }

        }

        Joueur SelectionJoueur()
        {
            Joueur joueur = null;
            for (int i = 0; i < joueurs.Count; i++)
            {
                MenuPrincipal.AfficherJoueur(i + 1, joueurs[i].Nom);
            }
            while (joueur == null)
            {
                MenuPrincipal.DemandeJoueur();

                try
                {
                    joueur = joueurs[int.Parse(MenuPrincipal.EntreeUtilisateur()) - 1];
                }
                catch
                {
                    MenuPrincipal.EntreeIncorrecte();
                    MenuPrincipal.AttenteUtilisateur();
                }
            }
            return joueur;
        }
        Partie.Difficulte SelectionDifficulte()
        {

            bool selection = true;
            Partie.Difficulte difficulte = Partie.Difficulte.FACILE;
            while (selection)
            {

                switch (MenuPrincipal.AfficherChoixDifficulte())
                {
                    case "1":
                        difficulte = Partie.Difficulte.FACILE;
                        selection = false;
                        break;
                    case "2":
                        difficulte = Partie.Difficulte.INTERMEDIAIRE;
                        selection = false;
                        break;
                    case "3":
                        difficulte = Partie.Difficulte.DIFFICILE;
                        selection = false;
                        break;
                    case "4":
                        difficulte = Partie.Difficulte.EXTREME;
                        selection = false;
                        break;

                    case "5":
                        break;
                    default:
                        MenuPrincipal.EntreeIncorrecte();
                        MenuPrincipal.AttenteUtilisateur();
                        break;
                }

            }
            return difficulte;
        }

        Partie.Taille SelectionTaille()
        {
            bool selection = true;
            Partie.Taille taille = Partie.Taille.PETIT;
            while (selection)
            {

                switch (MenuPrincipal.AfficherChoixTaille())
                {
                    case "1":
                        taille = Partie.Taille.PETIT;
                        selection = false;
                        break;
                    case "2":
                        taille = Partie.Taille.MOYEN;
                        selection = false;
                        break;
                    case "3":
                        taille = Partie.Taille.GRAND;
                        selection = false;
                        break;
                    case "4":
                        break;

                    default:
                        MenuPrincipal.EntreeIncorrecte();
                        MenuPrincipal.AttenteUtilisateur();
                        break;
                }

            }
            return taille;
        }
        void AfficherTopClassement()
        {
            Partie.Difficulte difficulte = SelectionDifficulte();
            Partie.Taille taille = SelectionTaille();

            joueurs.Sort((a, b) => a[difficulte, taille].CompareTo(b[difficulte, taille]));

            MenuPrincipal.AfficherEnteteClassement(difficulte.ToString(), taille.ToString());
           
            for(int i = 0; i < joueurs.Count; i++)
            {
                if (joueurs[i][difficulte, taille] != long.MaxValue)
                    MenuPrincipal.AfficherClassement(i + 1, joueurs[i].Nom, joueurs[i][difficulte, taille]);
            }


        }


    }




}
