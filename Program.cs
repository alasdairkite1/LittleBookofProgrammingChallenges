using System;
using System.Linq;
using System.Collections.Generic;

namespace CardDeckTest
{
    public enum Suit
    {
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4,
    }

    public enum CardNumber
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
    public class Card
    {
        public Suit Suit { get; set; }
        public CardNumber CardNumber { get; set; }
    }
    class Program
    {
        static void Main()
        {
            setupdeck();
        }
        static void setupdeck()
        {
            List<Card> deck = new List<Card>();

            deck = Enumerable.Range(1, 4).SelectMany(s => Enumerable.Range(1, 13).Select(c => new Card()
            {
                Suit = (Suit)s,
                CardNumber = (CardNumber)c,
            }
                )
           )
                .ToList();
            shuffle<Card>(deck);
        }
        static void shuffle<List>(List<Card> deck)
        {
            Random _random = new Random();

            int n = deck.Count;

            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + _random.Next(n - i);
                Card temporary = deck[r];
                deck[r] = deck[i];
                deck[i] = temporary;
            }
            TakeCard<Card>(deck, n);
        }
        static void TakeCard<List>(List<Card> deck, int n)
        {
            int count = 0;

            List<Card> used = new List<Card>();

            do
            {
                var card = deck.FirstOrDefault();
                deck.Remove(card);

                used.Add(card);
                count++;

            } while (count < 2);

            //Score<List>(used);

            DrawOrStick<List>(deck, used);
        }

        static void Score<List>(List<Card> used)
        {
            //FUnction used to calculate the sum of the hand

        }

            static void DrawOrStick<List>(List<Card> deck, List<Card> used)
            {

            int count = 0;

            //At this point of the program add a display hand function for each card on the used list

            Console.WriteLine("Draw [Enter] or Stick [S]?");

            Console.WriteLine("Hand is:   ");

            foreach (Card c in used)
            {
                Console.WriteLine("{0}   {1}", c.CardNumber, c.Suit);
            }

            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {

                    var card = deck.FirstOrDefault();
                    deck.Remove(card);
                    used.Add(card);
                    Console.WriteLine("{0}   {1}", card.CardNumber, card.Suit);
                    count++;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("final hand is:   ");
                    foreach (Card c in used)
                    {
                        Console.WriteLine("{0}   {1}", c.CardNumber, c.Suit);
                    }
                    break;
                }
            } while (count < 5);
        }
    }
}
