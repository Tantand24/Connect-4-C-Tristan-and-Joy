namespace Final_project
{
     public interface IPlayer
 {
     string Name { get; }
     string Character { get; } 
     int GetMove(string[,] board);
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

  
     public int GetMove(string[,] board)
     {
         int column;
         Console.Write($"{Name}, make a move! Enter the column (1-7): "); 

         do
         {
             if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= board.GetLength(1))
                    return column - 1;

             Console.Write("Invalid input. Make another choice, choose a column (1-7): ");
         } while (true);
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
                    throw new GameExecption("Column is full");
                }
            }
        }
    }

    public bool isWin()
    {
        for (int i = 0; i < _row - 1; i++)
        {
            if(i < _row - 4)
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
            if (_board[0,j] == "_")
            {
                return false;
            }
        }
        return true;
    }
}

public class GameExecption : Exception
{
    public GameExecption(string message) : base(message)
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

    //add or update a player to a human or ai 
    public void addOrChangePlayer(int playerPostion, string playerName, bool isAi)
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
            else
            {
                throw new GameExecption("Invalid player postion");
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
                input = Console.ReadLine();
                int move;

                if (_currentplayer == player1.Name)
                {
                    gameBoard.Update(player1.GetMove(input), player1);
                    _currentplayer = player2.Name;
                    TurnCounter++;
                }
                else if (_currentplayer == player2.Name)
                {
                    gameBoard.Update(player2.GetMove(input), player2);
                    _currentplayer = player1.Name;
                    TurnCounter++;
                }

                if(TurnCounter % 2 != 0)
                {
                    Console.Clear();
                }
                isWinOrTie();
            }
            catch (GameExecption e)
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
            Console.WriteLine("The winner is {0}", _currentplayer);
            pastBoardGamelist.Add(gameBoard);
            pastGameturn.Add(TurnCounter);
            winningPlayer.Add($"the winning player was {_currentplayer}");
            return true;
        }
        else if (gameBoard.isTie())
        {
            Console.WriteLine("The game is over, it a tie");
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

    }
    
    //display all past game
    public void ShowPastGame()
    {
        if(pastBoardGamelist.Count == 0)
        {
            throw new GameExecption("No past game history, Please a game");
        }
        for (int i = 0; i < pastBoardGamelist.Count; i++)
        {
            pastBoardGamelist[i].Display();
            Console.WriteLine("The game end in {0} turns, and {1}", pastGameturn[i], winningPlayer[i]);
        }
    }

    //Menu loop
    public void gameloop()
    {
        string input;
        
        while ((input = Console.ReadLine()) != "exit")
        {
            string cmd = input;

            try
            {
                if (cmd == "update player")
                {
                    
                }
                else if(cmd == "setup")
                {
                    int player1 = 1;
                    int player2 = 2;
                    string player1name = "player1";
                    string player2name = "player2";
                    addOrChangePlayer(player1, player1name, false);
                    addOrChangePlayer(player2, player2name, false);
                    setup();
                }
                else if (cmd == "play")
                {
                    mainGameloop();
                }
            }
            catch (GameExecption e)
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
            Console.WriteLine("Hello, World!");
        }
    }
}
