using System;

namespace Final_TextAdventure
{
    class Healer: Item
    {
        public int Cure {get; private set;}

        public Healer (string name, string description, bool carryable, int cure): base (name,  description, carryable)
        {
            Name = name;
            Description = description;
            Carryable = carryable;
            Cure = cure;        
        }
    }
}