using System;

namespace Demineur {
    class Program {
        /// <summary>Point d'entré du programme.</summary>
        /// <param name="args">Arguments d'exécution en ligne de commande</param>
        static void Main(string[] args) {
            Demineur demineur = new Demineur();
            demineur.Demarrer();
        }
    }
}
