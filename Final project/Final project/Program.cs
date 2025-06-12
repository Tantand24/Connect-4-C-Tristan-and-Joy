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
