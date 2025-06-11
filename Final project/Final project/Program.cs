namespace Final_project
{
    public abstract class Player
    {
        public string Name { get; set; }
        public bool XorO { get; set; }

        public Player(string name, string xoro)
        {
              Name = name;
              XorO = xoro;
        }

        public virtual void MakeMove()
        {
            //run the update board method
        }
    }

    public class Human :Player
    {
        public Human(string name, string xoro) : base(name, xoro){}

        public override MakeMove(Board board)
        {
             Console.WriteLine($"{Name}, Make your move (1-7): ");
             int column; 
        }
        
    }

    //public class AI : player

    public class Board
    {
        public board[[6][6][6][6][6][6][6]];
        public bool IsGameEnd = false;

        public void DisplayBoard()
        {
            //display board
        }

        public void CheckWin()
        {
            //check vertical 

            //check horizontal 

            // check left diagonal

            // check right diagonal
            
        }

        public void UpdateBoard()
        {
            // take in user input to update board
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
