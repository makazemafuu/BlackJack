using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace TPBlackJack
{
    class Program
    {
        static void Main(string[] args)
        {

            //initialisation du dictionnaire pour les cartes et leurs valeurs

            Dictionary<string, int> valeurCartes = new Dictionary<string, int>();
            valeurCartes.Add("As", 1);
            valeurCartes.Add("2", 2);
            valeurCartes.Add("3", 3);
            valeurCartes.Add("4", 4);
            valeurCartes.Add("5", 5);
            valeurCartes.Add("6", 6);
            valeurCartes.Add("7", 7);
            valeurCartes.Add("8", 8);
            valeurCartes.Add("9", 9);
            valeurCartes.Add("10", 10);
            valeurCartes.Add("V", 10);
            valeurCartes.Add("D", 10);
            valeurCartes.Add("R", 10);

            //listes vides pour le joueur, l'ordinateur et les paquets de cartes

            List<string> joueurH = new List<string>();
            List<string> joueurO = new List<string>();

            List<string> paquet = new List<string>();


            //initialisation de la variable prénom
            string name;
            Console.WriteLine("Pearse - Hello & WELCOME au jeu du Black Jack !");
            Console.WriteLine("Pearse - Quel est votre prénom ?");
            Console.Write("\n");
            name = Console.ReadLine();
            Console.Write("\n");

            //gestion de l'As, en gros le joueur à le choix, de base l'As = 1 mais s'il le souhaite, l'As peut devenir 11

            bool As;
            Console.WriteLine("Souhaitez-vous que l'As compte pour 11 points au lieu d'1 point ? (o/n)");
            As = (Console.ReadLine() == "o");
            Console.Write("\n");

            if (As)
            {
                valeurCartes["As"] = 11;
            }

            //permet à l'utilisateur de choisir le nombre de paquets de cartes, en multipliant par 4
            //puisqu'il y a 4 couleurs

            int choixPaquet;
            Console.WriteLine("Avec combien de paquets souhaitez-vous jouer ?");
            Console.Write("\n");
            choixPaquet = int.Parse(Console.ReadLine());
            Console.Write("\n");

            for (int i = 0; i < (choixPaquet * 4); i++)
            {
                foreach (string carte in valeurCartes.Keys.ToList())
                {
                    paquet.Add(carte);
                }
            }

            //générer le paquet de carte
            //var ListShuffled = myList.OrderBy(x => Guid.NewGuid()).ToList(); permet de mélanger les valeurs de la variable paquet
            paquet = paquet.OrderBy(x => Guid.NewGuid()).ToList();

            //permet d'afficher la variable paquet

            /*Console.WriteLine("Pearse - Voici la liste des cartes :");
            Console.Write("\n");

            for (int i = 0; i < paquet.Count; i++)
            {
                if (i == paquet.Count - 1)
                {
                    Console.Write("{0}", paquet[i]);
                }
                else
                {
                    Console.Write("{0} ", paquet[i]);
                }
            }
            Console.Write("\n");
            Console.Write("\n");*/

            //distribution des cartes
            //boucle qui se répète 2 fois


            for (int i = 0; i < 2; i++)
            {
                joueurH.Add(paquet[0]);
                paquet.Remove(paquet[0]);

                joueurO.Add(paquet[0]);
                paquet.Remove(paquet[0]);

            }

            //déroulement du jeu

            bool stopJoueur = false;
            bool stopOrdinateur = false;
            bool finPartie = false;
            string choixJoueur;

            //manière d'initialiser le score au début du jeu

            int scoreH = 0;

            foreach (string carte in joueurH)
            {
                scoreH += valeurCartes[carte];
            }

            int scoreO = 0;

            foreach (string carte in joueurO)
            {
                scoreO += valeurCartes[carte];
            }

            //permet d'afficher les variables joueurH et joueurO et de faire l'affichage du jeu

            Console.WriteLine("Pearse - Faite vos jeux !");
            Console.Write("\n");

            Console.Write("({0} pts)", scoreH); Console.WriteLine($" {name} : " + "{0} {1}", joueurH[0], joueurH[1]);
            Console.WriteLine("Ordinateur : ? {0}", joueurO[1]);
            Console.Write("\n");

            //boucle avec la décision du joueur, de l'ordinateur, et l'initialisation des scores
            //mise à jour aussi à la fin pour le score

            while (!finPartie)
            {
                //décision & tour du joueur

                if (!stopJoueur)
                {
                    Console.WriteLine("Voulez-vous piocher une nouvelle carte ? Choississez votre réponse parmis les choix proposés ci-dessous.");
                    Console.WriteLine("o - Oui");
                    Console.WriteLine("n - Non");
                    Console.Write("\n");

                    choixJoueur = Console.ReadLine();
                    Console.Write("\n");

                    if (choixJoueur == "o")
                    {

                        Console.WriteLine($"{name} : Je pioche.");
                        joueurH.Add(paquet[0]);
                        paquet.Remove(paquet[0]);

                        scoreH = 0;

                        foreach (string carte in joueurH)
                        {
                            scoreH += valeurCartes[carte];
                        }

                        Console.Write("({0} pts)", scoreH); Console.Write($" {name} :");
                        //méthode .ForEach va exécuter l'action spécifiée sur chaque élément de List<T>
                        //names.ForEach(Print); displays the contents of the list using the Print method
                        joueurH.ForEach(X => Console.Write(" " + X));
                        Console.Write("\n");

                        /*for (int i = 0; i < paquet.Count; i++)
                        {
                            if (i == paquet.Count - 1)
                            {
                                Console.Write("{0}", paquet[i]);
                            }
                            else
                            {
                                Console.Write("{0} ", paquet[i]);
                            }
                        }
                        Console.Write("\n");*/
                    }

                    else
                    {
                        Console.Write("({0} pts)", scoreH); Console.WriteLine($" {name} : Je m'arrête là.");
                        stopJoueur = true;

                    }
                }
                Console.Write("\n");

                //décision & tour de l'ordinateur

                if (!stopOrdinateur)
                {
                    //l'ordinateur à condition de s'arrêter s'il a un score supérieur à 15

                    if (scoreO <= 15)
                    {
                        Console.WriteLine("Ordinateur : Je pioche.");
                        joueurO.Add(paquet[0]);
                        paquet.Remove(paquet[0]);

                        //ici on veut que la première carte de l'ordinateur reste cachée par un "?"
                        //on va donc le spécifier dans un Console.Write
                        //puis faire une boucle for qui commence à l'index 1 et non 0
                        //pour que la carte à l'index 0 ne soit pas affiché tout simplement

                        //remarque : il y a deux manières dans un Console.Write d'utiliser les variables
                        //avec le "Console.Write($"x {variable}");", la "variable + " "" ou un ""{0}" (etc), variable")

                        Console.Write("Ordinateur : ? ");

                        for (int i = 1; i < joueurO.Count; i++)
                        {
                            Console.Write(joueurO[i] + " ");
                        }

                        Console.Write("\n");

                        /*for (int i = 0; i < paquet.Count; i++)
                        {
                            if (i == paquet.Count - 1)
                            {
                                Console.Write("{0}", paquet[i]);
                            }
                            else
                            {
                                Console.Write("{0} ", paquet[i]);
                            }
                        }
                        Console.Write("\n");*/
                    }

                    else
                    {
                        Console.WriteLine("Ordinateur : Je m'arrête là.");
                        stopOrdinateur = true;
                    }

                }
                Console.Write("\n");

                //fin de partie avec conditions
                //1. partie se termine si les joueurs s'arrêtent tous de piocher
                //2. partie se termine si l'un des joueurs à un score > à 21 pts

                //on reset le score à 0 pour recompter, sinon le score est automatiquement faussé

                scoreH = 0;

                foreach (string carte in joueurH)
                {
                    scoreH += valeurCartes[carte];
                }

                scoreO = 0;

                foreach (string carte in joueurO)
                {
                    scoreO += valeurCartes[carte];
                }

                //si les deux se sont stoppé, on calcule le gagnant ou l'égalité

                if (stopOrdinateur & stopJoueur)
                {
                    if (scoreO == scoreH)
                    {
                        Console.WriteLine($"Egalité ! Votre score à chacun est de {scoreH} points. Bravo à vous deux !");
                        finPartie = true;
                    }

                    else if (scoreO < scoreH ^ scoreH == 21)
                    {
                        Console.WriteLine("Il faut être le plus proche possible de 21 points sans le dépasser.");
                        Console.WriteLine($"Vous avez donc gagné ! Votre score est de {scoreH} points et celui de l'ordinateur de {scoreO} points. Félicitation ! :D");
                        Console.WriteLine("Et si vous avez eu 21 points, encore mieux : c'est un BLACK JACK ! GG");
                        finPartie = true;
                    }

                    else
                    {
                        Console.WriteLine("Il faut être le plus proche possible de 21 points sans le dépasser.");
                        Console.WriteLine($"Vous avez donc perdu ! Votre score est de {scoreH} points et celui de l'ordinateur de {scoreO} points. Dommage ! ='(");
                        Console.WriteLine("Et si l'ordinateur a eu 21 points, encore mieux : c'est un BLACK JACK ! GG à lui.");
                        finPartie = true;
                    }
                }

                //sinon, on cherche un dépassement ou un BlackJack

                else
                {
                    if (scoreO == 21 & scoreH == 21)
                    {
                        Console.WriteLine($"Wow, un score de 21 points est un Black Jack. C'est donc égalité par double Black Jack ici ! Votre score à chacun étant de {scoreH} points. Félicitation à vous deux !");
                        finPartie = true;
                    }

                    else if (scoreO == 21)
                    {
                        Console.WriteLine($"L'ordinateur a eu un score de {scoreO} points et vous de {scoreH} points. Un score de 21 points est un BlackJack ! Vous avez donc perdu. ='(");
                        finPartie = true;
                    }

                    else if (scoreH == 21)
                    {
                        Console.WriteLine($"Votre score est de {scoreH} points et celui de l'ordinateur de {scoreO} points. Un score de 21 points est un BlackJack ! Vous avez donc gagné. Félicitation ! :D");
                        finPartie = true;
                    }

                    else if (scoreO > 21)
                    {
                        Console.WriteLine($"Votre score est de {scoreH} points. Bravo, vous avez gagné ! L'Ordinateur a dépassé 21 points, il est à {scoreO} points.");
                        finPartie = true;
                    }

                    else if (scoreH > 21)
                    {
                        Console.WriteLine($"Votre score est de {scoreH} points. Vous avez perdu ='( il ne faut pas dépasser les 21 points. Le score de l'ordinateur est à {scoreO} points.");
                        finPartie = true;
                    }
                }
            }

        }
    }
}