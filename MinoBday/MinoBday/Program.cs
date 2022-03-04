using System;

namespace MinoBday 
{
    internal class Program
    {
        /// <summary>
        /// Before the party begins, all guests make an agreement
        /// They decide that they will always eat a cupcake in the labyrinth,
        /// requesting one to eat if necessary
        /// Additionally, they decide that they will leave the cupcake wrappers
        /// on the ground in the labyrinth in their own individual pile.
        /// 
        /// Once a guest goes into the labyrinth and creates their pile, they should
        /// count the number of piles of wrappers. If this number equals the number of 
        /// guests, this means that the party can be over. As this is not talking
        /// to other guests, it is fully allowed to do.
        /// 
        /// Functionally, there are many ways that a group could do something similar to this, 
        /// making lines on the ground to denote how many unique guests have entered, 
        /// creating groups in the party itself, etc. However, this one I feel is unique and 
        /// thematic and lets all the guests enjoy cupcakes!
        /// </summary>


        // N = 10 by default
        private static int guests = 10;
        // There is initially a cupcake
        private static bool cupcake = true;
        // Used for the cupcake wrapper method
        private static int[] wrappers = new int[guests];
        // Determines when the party is over
        private static bool partyTime = true;
        static void Main(string[] args)
        {
            // Initializing variables
            Thread[] threads = new Thread[guests];
            var rand = new Random();
            int i = 0;
            initWrappers();

            // Runs the loop where the minotaur randomly selects a guest to go into the labyrinth
            do
            {
                // Simulates the minotaur randomly choosing a guest to enter the 
                i = rand.Next(10);
                Console.WriteLine($"The Minotaur has chose guest {i}");
                threads[i] = new Thread(new ThreadStart(() => Labyrinth(i)));
                threads[i].Start();
                // The join is here to prevent any other threads from entering the labyrinth until the first one has completed it
                threads[i].Join();
            } while (partyTime);

            // The code will exit the loop when a guest has filled up the last wrapper pile, thus meaning that every guest has entered.
            Console.WriteLine("The party has concluded, every guest has entered the labyrinth");

        }

        // Simulates a guest running the labyrinth
        static void Labyrinth(int i)
        {
            Console.WriteLine($"Guest {i} entering the labyrinth");
            // The guest will always request a cupcake if its not there and then eat it.
            if (!cupcake)
                requestCupcake();
            cupcake = false;
            // The guest leaves the wrapper on the ground in their pile
            wrappers[i]++;
            // The guest then checks the wrappers to see if the number of wrapper piles equals the number of guests
            checkWrappers();
            Console.WriteLine($"Guest {i} has left the labyrinth");
        }

        // Checks the piles of wrappers to determine if every guest has entered the labyrinth
        // leaves partyTime as true if there are still guests who need to enter the labyrinth
        // sets partyTime to false if they are the last guest to enter
        static void checkWrappers()
        {
            for(int i = 0; i < guests; i++)
            {
                if (wrappers[i] > 0)
                    continue;
                else
                    return;
            }
            partyTime = false;
        }

        // Simply requests a cupcake, added this more for flavor than anything else
        static void requestCupcake()
        {
            cupcake = true;
        }

        // Simply eats the cupcake, added this more for flavor than anything else
        static void eatCupcake()
        {
            cupcake = false;
        }

        // Initializes all of the wrapper piles to have 0 wrappers in them
        static void initWrappers()
        {
            for(int i = 0; i < guests; i++)
            {
                wrappers[i] = 0;
            }
        }

        // Used for testing to verify that all piles have at least 1 wrapper at the end of the party
        static void printWrappers()
        {
            for (int i = 0; i < guests; i++)
            {
                Console.Write(wrappers[i] + ", ");
            }
            Console.Write("\n");
        }
    }
}