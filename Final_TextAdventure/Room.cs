using System;
using System.Collections.Generic;

namespace Final_TextAdventure
{
    class Room: GameObject
    {
        public Dictionary<TextAdventure.Direction, Room> Neighbors = new Dictionary<TextAdventure.Direction, Room>();
        public List<Item> Items = new List<Item>();
        public List<NPC> NPCs = new List<NPC>();
        public bool AlreadyVisited {get; set;}

        public Room (string name, string description){
            Name = name;
            Description = description;       
            AlreadyVisited = false;
        }

        public void StartUp(){
            this.AlreadyVisited = true;
            foreach (NPC npc in NPCs)
            {
                if(npc.IsActive)
                    npc.Dialog(TextAdventure.Player, npc);
                if (npc.IsAggressive)
                    npc.Fight(TextAdventure.Player, npc);
            }
        }
    }
}