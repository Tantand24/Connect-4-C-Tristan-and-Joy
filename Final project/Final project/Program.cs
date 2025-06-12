namespace Final_project
{
    public interface IPlayer
    {
        string Name { get; }
        string Character { get; }

        void MakeMove(Board board);

    public class Human : IPlayer
    {
        public string Name { get; }
        public string Character { get; }

        public Human(string name, string character)
        {
            Name = name;
            Character = character;
        }

        public void MakeMove(Board board) 
        {
            Console.WriteLine($"{Name}, Make your move! Enter a column (1-7): ");
            int column;

            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
            {
                Console.Write("Invalid input. Make another choice: ");
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
                    _board[i - 1, columnNumber - 1] = player.Character;
                    break;
                }
            }
            Display();
        }
    
        public bool isCheckWin()
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
    }
            
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
