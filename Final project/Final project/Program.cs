namespace Final_project
{
    public abstract class player
    {
        public string Name { get; set; }
        public bool XorO { get; set; }

        public virtual void MakeMove()
        {
            //run the update board method
        }
    }

    public class human : player
    {

    }

    //public class AI : player

    public class board
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
