    ﻿namespace Final_project
{
    public interface IPlayer
 {
     string Name { get; }
     string Character { get; }
     int GetMove();
 }

 public class Human : IPlayer
 {
     public string Name { get; private set; }
     public string Character { get; private set; }

     public Human(string name, string character)
     {
         Name = name;
         Character = character;
     }
     
        public int GetMove()
        {
            int column;
            Console.Write($"{Name}, choose a column (1-7): ");

            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out column) && column >= 1 && column <= 7)
                    return column - 1;

                Console.WriteLine("Invalid input. Please choose a number between 1 and 7.");
            }
        }
    }
    //public class AI : player

        public class Boards
    {
        private string[,] _board { get; set; }
        private int _row { get; }
        private int _column { get; }

        public Boards()
        {
            _row = _column = 7;
            _board = new string[_row, _column];
            Setup();
        }

        public void Setup()
        {
            for (int i = 0; i < _row; i++)
            {
                if (i == 6)
                {
                    for (int j = 0; j < _column; j++)
                    {
                        _board[i, j] = $"{j + 1}";
                    }
                }
                else
                {
                    for (int j = 0; j < _column; j++)
                    {
                        _board[i, j] = "_";
                    }
                }

            }
        }

        public void Display()
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _column; j++)
                {
                    Console.Write(_board[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void Update(int columnNumber, IPlayer player)
        {
            for (int i = 0; i < _row; i++)
            {
                if (_board[i, columnNumber - 1] != "_")
                {
                    if (i - 1 != -1)
                    {
                        _board[i - 1, columnNumber - 1] = player.Character;
                        break;
                    }
                    else
                    {
                        throw new GameException("Column is full");
                    }
                }
            }
        }

        public bool isWin()
        {
            for (int i = 0; i < _row - 1; i++)
            {
                if (i < _row - 4)
                {
                    for (int j = 0; j < _column; j++)
                    {
                        // check diagonal
                        if (j < _column - 3)
                        {
                            //left
                            if (_board[i, j] != "_" && _board[i, j] == _board[i + 1, j + 1] && _board[i + 1, j + 1] == _board[i + 2, j + 2] && _board[i + 2, j + 2] == _board[i + 3, j + 3])
                            {
                                return true;
                            }

                            //right
                            if (_board[i + 3, j] != "_" && _board[i + 3, j] == _board[i + 2, j + 1] && _board[i + 2, j + 1] == _board[i + 1, j + 2] && _board[i + 1, j + 2] == _board[i, j + 3])
                            {
                                return true;
                            }
                        }

                        //check vertical 
                        if (_board[i, j] != "_" && _board[i, j] == _board[i + 1, j] && _board[i + 1, j] == _board[i + 2, j] && _board[i + 2, j] == _board[i + 3, j])
                        {
                            return true;
                        }
                    }
                }

                //check horizontal
                for (int j = 0; j < _column - 4; j++)
                {
                    if (_board[i, j] != "_" && _board[i, j] == _board[i, j + 1] && _board[i, j + 1] == _board[i, j + 2] && _board[i, j + 2] == _board[i, j + 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isTie()
        {
            for (int j = 0; j < _column; j++)
            {
                if (_board[0, j] == "_")
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {

        }
    }

    public class Games
    {
        private List<Boards> pastBoardGamelist;
        private Boards gameBoard;

        private List<string> winningPlayer;
        private IPlayer player1;
        private IPlayer player2;
        private string _currentplayer;

        private List<int> pastGameturn;
        private int TurnCounter;

        public Games()
        {
            pastBoardGamelist = new List<Boards>();
            pastGameturn = new List<int>();
            winningPlayer = new List<string>();
        }

        //method for adding player
        public void AddOrUpdateplayer()
        {
            string input;
            int postion;
            string name;
            bool isAiOn;

            if (player1 == null && player2 == null)
            {
                while (true)
                {
                    try
                    {
                        if (player1 == null && player2 == null)
                        {
                            Console.WriteLine("Input a 1 or 2 for either player 1 or 2");
                            input = Console.ReadLine();
                            postion = int.Parse(input);
                            if (postion > 2 && postion < 1)
                            {
                                throw new GameException("player postion is out of bound");
                            }

                            Console.WriteLine("Input Player name");
                            input = Console.ReadLine();
                            name = input;
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                throw new GameException("Cant leave Name blank");
                            }

                            Console.WriteLine("Do you want ai turn on for this player (Y/N)");
                            input = Console.ReadLine();
                            string ai = input.ToUpper();
                            if (ai == "Y")
                            {
                                isAiOn = true;
                            }
                            else if (ai == "N")
                            {
                                isAiOn = false;
                            }
                            else
                            {
                                throw new GameException("Input must be y or n");
                            }
                        }
                        else if (player1 == null)
                        {
                            postion = 1;

                            Console.WriteLine("Input Player1 name");
                            input = Console.ReadLine();
                            name = input;
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                throw new GameException("Can't leave Name blank");
                            }
                            else if (name == player2.Name)
                            {
                                throw new GameException("Can't have the same name as player 2");
                            }

                            Console.WriteLine("Do you want ai turn on for this player (Y/N)");
                            input = Console.ReadLine();
                            string ai = input.ToUpper();
                            if (ai == "Y")
                            {
                                isAiOn = true;
                            }
                            else if (ai == "N")
                            {
                                isAiOn = false;
                            }
                            else
                            {
                                throw new GameException("Input must be y or n");
                            }
                        }
                        else if (player2 == null)
                        {
                            postion = 2;

                            Console.WriteLine("Input Player2 name");
                            input = Console.ReadLine();
                            name = input;
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                throw new GameException("Can't leave Name blank");
                            }
                            else if (name == player1.Name)
                            {
                                throw new GameException("Can't have the same name as player 1");
                            }

                            Console.WriteLine("Do you want ai turn on for this player (Y/N)");
                            input = Console.ReadLine();
                            string ai = input.ToUpper();
                            if (ai == "Y")
                            {
                                isAiOn = true;
                            }
                            else if (ai == "N")
                            {
                                isAiOn = false;
                            }
                            else
                            {
                                throw new GameException("Input must be y or n");
                            }
                        }
                        else
                        {
                            break;
                        }
                        createPlayer(postion, name, isAiOn);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Not valid input for postion");
                    }
                    catch (GameException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Input a 1 or 2 for either player 1 or 2");
                        input = Console.ReadLine();
                        postion = int.Parse(input);
                        if (postion > 2 && postion < 1)
                        {
                            throw new GameException("player postion is out of bound");
                        }

                        Console.WriteLine("Input Player name");
                        input = Console.ReadLine();
                        name = input;
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            throw new GameException("Cant leave Name blank");
                        }
                        else if (postion == 1 && player2.Name == name)
                        {
                            throw new GameException("Can't have the same name as player 2");
                        }
                        else if (postion == 2 && player1.Name == name)
                        {
                            throw new GameException("Can't have the same name as player 1");
                        }

                        Console.WriteLine("Do you want ai turn on for this player (Y/N)");
                        input = Console.ReadLine();
                        string ai = input.ToUpper();
                        if (ai == "Y")
                        {
                            isAiOn = true;
                        }
                        else if (ai == "N")
                        {
                            isAiOn = false;
                        }
                        else
                        {
                            throw new GameException("Input must be y or n");
                        }
                        createPlayer(postion, name, isAiOn);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Not valid input for postion");
                    }
                    catch (GameException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        //update a player to a human or ai 
        public void createPlayer(int playerPostion, string playerName, bool isAi)
        {
            if (!isAi)
            {
                if (playerPostion == 1)
                {
                    player1 = new Human(playerName, "x");
                }
                else if (playerPostion == 2)
                {
                    player2 = new Human(playerName, "o");
                }
            }
            else
            {

            }
        }

        //to setup the game
        public void setup()
        {
            gameBoard = new Boards();
            _currentplayer = player1.Name;
            TurnCounter = 1;
        }

        //the main game loop to take input and update board
        public void mainGameloop()
        {
            string input;
            while (!isWinOrTie())
            {
                try
                {
                    Console.WriteLine("It {0} turn", _currentplayer);
                    Console.WriteLine("Turn number {0}", TurnCounter);
                    gameBoard.Display();

                    if (_currentplayer == player1.Name)
                    {
                        gameBoard.Update(player1.GetMove(), player1);
                        if (isWinOrTie())
                        {
                            break;
                        }
                        _currentplayer = player2.Name;
                        TurnCounter++;
                    }
                    else if (_currentplayer == player2.Name)
                    {
                        gameBoard.Update(player2.GetMove(), player2);
                        if (isWinOrTie())
                        {
                            break;
                        }
                        _currentplayer = player1.Name;
                        TurnCounter++;
                    }

                    //if (input == "forfeit")
                    //{
                    //    forfeit();
                    //    break;
                    //}
                    //else if (string.IsNullOrWhiteSpace(input))
                    //{
                    //    throw new GameExecption("Please input something");
                    //}

                    if (TurnCounter % 2 != 0)
                    {
                        Console.Clear();
                    }
                }
                catch (GameException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        //return a bool and save the game state if there was a win or a tie to a list
        public bool isWinOrTie()
        {
            if (gameBoard.isWin())
            {
                Console.WriteLine("The game is over");
                Console.WriteLine("The winner is {0}\n", _currentplayer);
                pastBoardGamelist.Add(gameBoard);
                pastGameturn.Add(TurnCounter);
                winningPlayer.Add($"the winning player was {_currentplayer}");
                return true;
            }
            else if (gameBoard.isTie())
            {
                Console.WriteLine("The game is over, it a tie\n");
                pastBoardGamelist.Add(gameBoard);
                pastGameturn.Add(TurnCounter);
                winningPlayer.Add("it was a tie");
                return true;
            }
            else
            {
                return false;
            }
        }

        //to allow a player to forfeit
        public void forfeit()
        {

        }

        //to start a new game after a game have ended
        public void newGame()
        {
            Console.WriteLine("swtiching player 1 and player 2 postion");
            IPlayer tempPlayer = player1;
            player1 = player2;
            player2 = tempPlayer;
            setup();
            mainGameloop();
        }

        //display all past game
        public void ShowPastGame()
        {
            for (int i = 0; i < pastBoardGamelist.Count; i++)
            {
                pastBoardGamelist[i].Display();
                Console.WriteLine("The game end in {0} turns, and {1}\n", pastGameturn[i], winningPlayer[i]);
            }
        }

        //Just prompt to out text for menu
        public void menuPrompt()
        {
            if (pastBoardGamelist.Count == 0)
            {
                Console.WriteLine("The game main menu");
                Console.WriteLine("To update player info or change a player to ai type: player");
                Console.WriteLine("To play the game type: play");
            }
            else
            {
                Console.WriteLine("The game main menu");
                Console.WriteLine("To update player info or change a player to ai type: player");
                Console.WriteLine("To setup a new game and play type: Newgame");
                Console.WriteLine("To view past games type: past");
            }
        }

        //Menu loop
        public void gameloop()
        {
            string input;

            Console.WriteLine("Welcome To Tristan and Joy Connect 4 game");
            AddOrUpdateplayer();
            Console.Clear();

            while (true)
            {
                try
                {
                    if (pastBoardGamelist.Count() == 0)
                    {
                        menuPrompt();
                        input = Console.ReadLine();
                        string cmd = input.ToLower();

                        if (cmd == "player")
                        {
                            AddOrUpdateplayer();
                        }
                        else if (cmd == "play")
                        {
                            Console.Clear();
                            setup();
                            mainGameloop();
                        }
                    }
                    else
                    {
                        menuPrompt();
                        input = Console.ReadLine();
                        string cmd = input.ToLower();

                        if (cmd == "player")
                        {
                            AddOrUpdateplayer();
                        }
                        else if (cmd == "newgame")
                        {
                            Console.Clear();
                            newGame();
                        }
                        else if (cmd == "past")
                        {
                            Console.Clear();
                            ShowPastGame();
                        }
                    }
                }
                catch (GameException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Games mainprogram = new Games();
            mainprogram.gameloop();
        }
    }
}
