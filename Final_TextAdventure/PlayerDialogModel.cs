using System;
using System.Collections.Generic;

namespace Final_TextAdventure
{
    class PlayerDialogModel
    {
        public Creature DialogPartner {get; private set;}
        public List<PlayerDialogLine> DialogLines = new List<PlayerDialogLine>();
        public int DialogPhase {get; set;}

        public PlayerDialogModel (Creature dialogPartner)
        {
            DialogPartner = dialogPartner;
            DialogPhase = 0;    
        }
    }
}