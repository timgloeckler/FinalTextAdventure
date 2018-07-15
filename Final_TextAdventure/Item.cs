using System;

namespace Final_TextAdventure
{
    class Item: GameObject
    {
        public bool Carryable {get; protected set;}

        public Item (string name, string description, bool carryable){
            Name = name;
            Description = description;  
            Carryable = carryable;         
        }
    }
}