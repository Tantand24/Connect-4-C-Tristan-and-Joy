namespace Final_project
{
    public abstract class player
    {
        public string Name { get; set; }
        public bool XorO { get; set; }

        public virtual void MakeMove()
        {

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

        }

        public void CheckWin()
        {

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
