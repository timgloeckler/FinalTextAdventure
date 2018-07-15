using System;
using System.Collections.Generic;
using System.Linq;

namespace Final_TextAdventure
{
    static class TextAdventure
    {
        public static Player Player {get; private set;}
        public static Item WinningItem {get; private set;}
        public static List<Room> Rooms {get; private set;} = new List<Room>();
        public static List<Item> Items {get; private set;} = new List<Item>();
        public static List<NPC> NPCs {get; private set;} = new List<NPC>();
        public static bool IsFinished {get; set;} = false;
        
        static void Main(string[] args)
        {
            LoadGameData();
            ConsoleWriteRed(Environment.NewLine + "Welcome to The Text Adventure!" + Environment.NewLine);
            Console.WriteLine("You returned back to your mansion because you forgot your phone. Search for it!");

            for (;;)
            {
                if(Player.Inventory.Contains(WinningItem))
                {
                    ConsoleWriteGreen("You got the " + WinningItem.Name + " and thus you won the game!");
                    IsFinished = true;
                }

                if (IsFinished == true)
                {
                    ConsoleWriteRed("Thank you for playing.");
                    break;
                }

                ConsoleWriteDarkYellow("Type 'help' and press Enter if you don't know what to do." + Environment.NewLine);

                string input = Console.ReadLine().ToLower();

                string command = input.IndexOf(" ") > -1 
                  ? input.Substring(0, input.IndexOf(" "))
                  : input;

                string parameter = input.IndexOf(" ") > -1 
                  ? input.Substring(input.IndexOf(" ") + 1, input.Length - (input.IndexOf(" ") + 1))
                  : "";
                
                switch(command)
                {
                    case "quit": case "q":
                        IsFinished = true;
                        break;
                    case "help": case "h":
                        Player.Help();
                        break;
                    case "inventory": case "i":
                        Player.CheckInventory();
                        break;
                    case "look": case "l":
                        Player.Look();
                        break;
                    case "go": case "g":
                        Player.Move(parameter);
                        break;
                    case "take": case "t":
                        Player.Take(parameter);
                        break;
                    case "drop": case "d":
                        Player.Drop(parameter);
                        break;
                    case "lookat": case "la":
                        Player.LookAt(parameter);
                        break;
                    case "attack": case "a":
                        Player.Attack(parameter);
                        break;
                    case "info": case "x":
                        Player.PlayerInfo();
                        break;
                    case "use": case "u":
                        Player.Use(parameter);
                        break;
                    case "speak": case "s":
                        Player.SpeakTo(parameter);
                        break;
                    default:
                        ConsoleWriteRed("Unknown command!");
                        break;
                }
            }
        }

        public enum Direction {north=0, east=1, south=2, west=3};

        #region GameData

        public static void LoadGameData ()
        {

            

            #region Items

            Items.Add(new Weapon("Knife", "A sharp and shiny kitchen knife.", true, 50));
            Items.Add(new Weapon("Remotecontrol", "*Advertisement* This is a Sony remote control *Advertisement*", true, 20));
            Items.Add(new Weapon("Baseballbat", "Probably not used to hit a baseball in this context", true, 15));
            Items.Add(new Weapon("PoolCue", "Sports implement consisting of a tapering rod used to strike a cue ball in pool or billiards", true, 30));

            Items.Add(new Item("Winebottle", "It says Cabernet Sauvignon, 2003, California on it.", true));
            Items.Add(new Item("Beer", "A tasty bottle from Bavaria", true));
            Items.Add(new Item("Candle", "Looks like a candle.", true));
            Items.Add(new Item("Book", "A book with recipies for thai meals.", true));
            Items.Add(new Item("Tequila", "Mexican booze (most ejoyable with lemon and salt.)", true));
            Items.Add(new Item("Sunglasses", "You wear them - it darkens your sight.", true));
            Items.Add(new Item("Towel", "Use it to dry yourself.", true));
            Items.Add(new Item("Phone", "Waterproof portable phone.", true));
            Items.Add(new Item("Soap", "Use it to clean your hands properly. Hygiene is important!", true));
            Items.Add(new Item("Toiletpaper", "Well, it's toiletpaper.", true));
            Items.Add(new Item("Wallet", "Filled with a stack of bills.", true));
            Items.Add(new Item("Armchair", "Looks pretty comfy!", false));
            Items.Add(new Item("Pooltable", "A six-pocket billiards table on which pool is played.", false));
        
            Items.Add(new Healer("Firstaidkit", "Could possibly help you if you're hurt.", true, 50));
            Items.Add(new Healer("Banana", "This Thing is yellow and bended.", true, 30));
            Items.Add(new Healer("Popcorn", "In case you prefer sweet popcorn, they're salty.", true, 20));

            WinningItem = GetItemByName("Phone");

            #endregion

            #region Rooms

            Rooms.Add(new Room("Livingroom", "A luxurious, modern and wide opened living room"));
            Rooms.Add(new Room("Kitchen", "If you were a chef, you would love this Kitchen. But unfortunately you don't know how to cook."));
            Rooms.Add(new Room("Homecinema", "A bunch of armchairs lined up in front of a gigantic Screen surrounded by an increndible soundsystem"));
            Rooms.Add(new Room("Bathroom", "For beeing next to a home theatre, it's pretty enormous"));
            Rooms.Add(new Room("Garden", "If Fengh Shui was a person, he would have been the garden architect"));
            Rooms.Add(new Room("Pool", "Filled with refreshing turquoise water"));
            Rooms.Add(new Room("Entrance", "Some fancy cars, a huge fountain and exotic plants."));

            #endregion

            #region RoomNeighbors

            GetRoomByName("Entrance").Neighbors.Add(Direction.north, GetRoomByName("Livingroom"));

            GetRoomByName("Livingroom").Neighbors.Add(Direction.west, GetRoomByName("Kitchen"));
            GetRoomByName("Livingroom").Neighbors.Add(Direction.east, GetRoomByName("Homecinema"));
            GetRoomByName("Livingroom").Neighbors.Add(Direction.north, GetRoomByName("Garden"));
            GetRoomByName("Livingroom").Neighbors.Add(Direction.south, GetRoomByName("Entrance"));


            GetRoomByName("Homecinema").Neighbors.Add(Direction.east, GetRoomByName("Bathroom"));
            GetRoomByName("Homecinema").Neighbors.Add(Direction.west, GetRoomByName("Livingroom"));

            GetRoomByName("Bathroom").Neighbors.Add(Direction.west, GetRoomByName("Homecinema"));

            GetRoomByName("Kitchen").Neighbors.Add(Direction.east, GetRoomByName("Livingroom"));

            GetRoomByName("Garden").Neighbors.Add(Direction.south, GetRoomByName("Livingroom"));
            GetRoomByName("Garden").Neighbors.Add(Direction.north, GetRoomByName("Pool"));

            GetRoomByName("Pool").Neighbors.Add(Direction.south, GetRoomByName("Garden"));

            #endregion

            #region Player

            Player = new Player("Player", "The main Player", 100, 5, GetRoomByName("Entrance"));

            #endregion

            #region AddItemsToRooms

            GetRoomByName("Livingroom").Items.Add(GetItemByName("Beer"));
            GetRoomByName("Livingroom").Items.Add(GetItemByName("Book"));
            GetRoomByName("Livingroom").Items.Add(GetItemByName("Candle"));
            GetRoomByName("Livingroom").Items.Add(GetItemByName("Pooltable"));
            GetRoomByName("Livingroom").Items.Add(GetItemByName("PoolCue"));

            GetRoomByName("Kitchen").Items.Add(GetItemByName("Knife"));
            GetRoomByName("Kitchen").Items.Add(GetItemByName("Banana"));
            GetRoomByName("Kitchen").Items.Add(GetItemByName("Winebottle"));

            GetRoomByName("Homecinema").Items.Add(GetItemByName("Popcorn"));
            GetRoomByName("Homecinema").Items.Add(GetItemByName("Remotecontrol"));
            GetRoomByName("Homecinema").Items.Add(GetItemByName("Tequila"));
            GetRoomByName("Homecinema").Items.Add(GetItemByName("Armchair"));


            GetRoomByName("Garden").Items.Add(GetItemByName("Sunglasses"));
            GetRoomByName("Garden").Items.Add(GetItemByName("Towel"));

            GetRoomByName("Bathroom").Items.Add(GetItemByName("Soap"));
            GetRoomByName("Bathroom").Items.Add(GetItemByName("Toiletpaper"));
            GetRoomByName("Bathroom").Items.Add(GetItemByName("Firstaidkit"));


            GetRoomByName("Pool").Items.Add(GetItemByName("Phone"));

            #endregion

            #region NPCs

            NPCs.Add(new NPC("Mom", "Just a typical Mom.", 100, 15, false, false, true));
            NPCs.Add(new NPC("Thief", "Standing behind the bar it seems like he is the owner of the tavern.", 100, 10, true, true, true));
            NPCs.Add(new NPC("Mosquito", "A mosquito, that always stings", 60, 2, true, true, false));

            #endregion

            #region AddItemsToInventories

            Player.Inventory.Add(GetItemByName("Wallet"));
            

            GetNPCByName("Thief").Inventory.Add(GetItemByName("BaseballBat"));

            #endregion

            #region AddNPCsToRooms

            GetRoomByName("Kitchen").NPCs.Add(GetNPCByName("Mom"));
            GetRoomByName("Homecinema").NPCs.Add(GetNPCByName("Thief"));
            GetRoomByName("Garden").NPCs.Add(GetNPCByName("Mosquito"));

            #endregion

            #region NPCDialogLines

            GetNPCByName("Thief").DialogLines.Add(new CreatureDialogLine("Fuck! Я думал, что здесь никого нет. Вы владелец?", 0, null));
            GetNPCByName("Thief").DialogLines.Add(new CreatureDialogLine("I kill you!", 1, null));
            GetNPCByName("Thief").DialogLines.Add(new CreatureDialogLine("U want die?", 2, null));

            GetNPCByName("Mom").DialogLines.Add(new CreatureDialogLine("Hello my Darling!", 0, null));
            GetNPCByName("Mom").DialogLines.Add(new CreatureDialogLine("Can't you keep your stuff together? I might help you if you bring my sunglasses. I left them outside.", 1, null));
            GetNPCByName("Mom").DialogLines.Add(new CreatureDialogLine("How dare you?! Well, if you don't need your phone..", 2, null));
            GetNPCByName("Mom").DialogLines.Add(new CreatureDialogLine("Forget about it.. First my sunglasses!", 3, null));
            GetNPCByName("Mom").DialogLines.Add(new CreatureDialogLine("Thank you very much. Don't forget about the fact, that your phone is waterproof! I mean, you literally take it anywhere with you...)", 4, null));

            #endregion

            #region PlayerDialogModels

            Player.Dialogs.Add(new PlayerDialogModel(GetNPCByName("Thief")));
            Player.Dialogs.Add(new PlayerDialogModel(GetNPCByName("Mom")));

            #endregion

            #region PlayerDialogLines

            GetPlayerDialogModelByDialogPartnerName("Thief").DialogLines.Add(new PlayerDialogLine("This is my House! What do you want here?! Get out!", 0, null, 1, 1));
            GetPlayerDialogModelByDialogPartnerName("Thief").DialogLines.Add(new PlayerDialogLine("What are u doing here? Would you please leave or I call the police.", 0, null, 2, 2));
            GetPlayerDialogModelByDialogPartnerName("Thief").DialogLines.Add(new PlayerDialogLine("No.", 2, null, 1, 1));
            
            GetPlayerDialogModelByDialogPartnerName("Mom").DialogLines.Add(new PlayerDialogLine("Hey Mom! Have you seen my phone anywhere?", 0, null, 1, 1));
            GetPlayerDialogModelByDialogPartnerName("Mom").DialogLines.Add(new PlayerDialogLine("U probably don't even know where it is and just want to use me...", 1, null, 2, 2));
            GetPlayerDialogModelByDialogPartnerName("Mom").DialogLines.Add(new PlayerDialogLine("Here, your sunglasses - take it back! (Giving sunglasses to Mom)", 1, GetItemByName("Sunglasses"), 1, 4));
            GetPlayerDialogModelByDialogPartnerName("Mom").DialogLines.Add(new PlayerDialogLine("So where is my phone?!", 2, null, 1, 3));
            GetPlayerDialogModelByDialogPartnerName("Mom").DialogLines.Add(new PlayerDialogLine("Here, your sunglasses - take it back! (Giving sunglasses to Mom)", 3, GetItemByName("Sunglasses"), 1, 4));

            #endregion
        }

        #endregion

        #region Helper

        public static Room GetRoomByName(string name){
            return Rooms.Find(x => x.Name == name);
        }

        public static Item GetItemByName(string name){
            return Items.Find(x => x.Name == name);
        }

        public static NPC GetNPCByName(string name){
            return NPCs.Find(x => x.Name == name);
        }

        public static PlayerDialogModel GetPlayerDialogModelByDialogPartnerName(string name){
            return Player.Dialogs.Find(x => x.DialogPartner.Name == name);
        }

        #endregion

        #region GUIHelper

        public static void ConsoleWriteGreen (string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteBlue (string input)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteRed (string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteDarkYellow (string input)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
