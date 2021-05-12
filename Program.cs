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
        Jack = 10,
        Queen = 10,
        King = 10,
    }
    public class Card
    {
        public Suit Suit { get; set; }
        public CardNumber Number { get; set; }

        public int Score
        {
            get
            {
                if (Number == CardNumber.King
                    || Number == CardNumber.Queen
                    || Number == CardNumber.Jack)
                {
                    return 10;
                }
                if (Number == CardNumber.Ace)
                {
                    return 11;
                }
                else
                {
                    return (int)Number;
                }
            }
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
                    Number = (CardNumber)c,
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
                List<Card> computer = new List<Card>();

                do
                {
                    var card = deck.FirstOrDefault();
                    deck.Remove(card);

                    used.Add(card);
                    count++;

                } while (count < 2);

                //Score<List>(used);

                PlayerDrawOrStick<List>(deck, used);
                ComputerDraw<List>(deck, computer);
                Game<List>(used, computer);
            }

            static void PlayerDrawOrStick<List>(List<Card> deck, List<Card> used)
            {

                int count = 0;

                //I would like the program to print the hand score everytime a new hand is drawn.

                Console.WriteLine("Hand is:   ");

                foreach (Card c in used)
                {
                    Console.WriteLine("{0}   {1}", c.Number, c.Suit);
                }

                Console.WriteLine("Score is:   {0}", used.Sum(x => x.Score));

                Console.WriteLine("Draw [Enter] or Stick [S]?");

                do
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {

                        var card = deck.FirstOrDefault();
                        deck.Remove(card);
                        used.Add(card);
                        var cardscore = used.Sum(x => x.Score);
                        Console.WriteLine("{0}   {1}. The score is   {2}", card.Number, card.Suit, cardscore);

                        if (cardscore > 21)
                        {
                            Console.WriteLine("BUST");
                            break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("final hand is:   ");
                        foreach (Card c in used)
                        {
                            Console.WriteLine("{0}   {1}", c.Number, c.Suit);
                        }
                        count++;
                        break;
                    }
                } while (count < 1);
            }
            static void ComputerDraw<List>(List<Card>deck, List<Card>computer)
            {
                int count = 0;

                do
                {
                    var card = deck.FirstOrDefault();
                    deck.Remove(card);
                    computer.Add(card);
                    count++;
                } while (count < 2);

                Console.WriteLine("Computer hand is:   ");
                foreach (Card c in computer)
                {
                    Console.WriteLine("{0}   {1}", c.Suit, c.Number);
                }

                var score = computer.Sum(x => x.Score);

                Console.WriteLine("The score is:   {0}", score);

            }
            static void Game<List>(List<Card>used, List<Card>computer)
            {
                Console.Clear();

                var comps = computer.Sum(x => x.Score);
                var useds = used.Sum(x => x.Score);

                if (comps > useds)
                {
                    Console.WriteLine("House Wins {0}  to  {1}", comps, useds);
                }
                else Console.WriteLine("Player Wins  {0}   to   {1}", useds, comps);
            }
        }
    }
}
