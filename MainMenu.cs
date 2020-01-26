using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{
    public class MainMenu
    {
        public int printMenu()
        {
            int answer = 1;
            bool isCorrect = false;
            while (isCorrect == false)
            {
                Console.WriteLine("Welcome to Joel's Frisbee Program");
                Console.WriteLine("Choose the game type: ");
                Console.WriteLine("1. Play Single Game");
                Console.WriteLine("2. Simulate Single Game");
                Console.WriteLine("3. Play Full Season");
                Console.WriteLine("4. Simulate Full Season");
                if (!int.TryParse(Console.ReadLine(), out answer))
                {
                    Console.WriteLine("Please enter a number between 1 and 4");
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            }
            return answer;
        }

    }
}
