using System;
using System.Collections.Generic;
using System.Text;

namespace Demineur {
    /// <summary>Classe d'une case.</summary>
    public class Case {
        bool mine, ouverte;
        byte compte;

        /// <summary>Crée un case vide.</summary>
        public Case() {
            mine = ouverte = false;
            compte = 0;
        }

        /// <summary>Si la case contient une mine.</summary>
        public bool Mine { get => mine; set => mine = value; }

        /// <summary>Si la case est ouverte.</summary>
        public bool Ouverte { get => ouverte; set => ouverte = value; }

        /// <summary>Le compte de mines autour de la case.</summary>
        public byte Compte { get => compte; }

        /// <summary>Incrémente le compte de mines autour de la case.</summary>
        public void AjouteMine() => compte++;

        /// <summary>Une représentation en chaine de la case.</summary>
        /// <example>Une case ouverte retourne son compte de mines, une mine ouverte retourne un "X" et toutes cases fermées retournent un "."</example>
        /// <returns>Retourne une représentation en chaine de la case.</returns>
        public override string ToString() => ouverte ? mine ? "X" : compte.ToString() : ".";

    }
}
