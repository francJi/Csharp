namespace BlackJack
{
    using System;
    using System.Collections.Generic;

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    // 카드 한 장을 표현하는 클래스
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public List<Card> ShowDeck()
        {
            return cards;
        }
    }

    // 패를 표현하는 클래스
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        public List<Card> ShowHand()
        {
            return cards;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 여기부터는 학습자가 작성
    // 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
    public class Dealer : Player
    {
        public void DealerDraw(Deck deck)
        {
            while (Hand.GetTotalValue() < 17) 
            {
                Card newCard = DrawCardFromDeck(deck);
                Console.WriteLine($"딜러가 뽑은 카드 : {newCard.Suit} / {newCard.Rank}");
                //Console.WriteLine($"딜러의 점수 총합 : {Hand.GetTotalValue}");
            }
        }
    }

    // 블랙잭 게임을 구현하세요. 
    public class Blackjack
    {
        // 코드를 여기에 작성하세요
        private Player Player { get; set; }
        private Dealer Dealer { get; set; }
        private Deck Deck { get; set; }

        public Blackjack(Player player, Dealer dealer, Deck deck)
        {
            Player = player;
            Dealer = dealer;
            Deck = deck;
        }

        public void GameStart()
        {

            Console.WriteLine("딜러가 카드를 흔들어 섞는 중");
            Thread.Sleep(1000);
            Console.Clear();

            string waitChar = ".";
            for (int i = 0; i < 4; i++)
            {
                waitChar += ".";
                string announcement = "안심하세요. 딜러는 사기를 치지 않습니다. 그렇다고 당신의 편도 아니지만요";
                Console.WriteLine(announcement + waitChar);
                Thread.Sleep(300);
                Console.Clear();
            }
        }
        public void InitShuffle()
        {
            Console.WriteLine("딜러가 카드를 분배하는 중,,,");
            Player.DrawCardFromDeck(Deck);
            Dealer.DrawCardFromDeck(Deck);
            Player.DrawCardFromDeck(Deck);
            Dealer.DrawCardFromDeck(Deck);

            Console.WriteLine($"딜러의 첫번째 카드 : {Dealer.Hand.ShowHand()[0].Suit} / {Dealer.Hand.ShowHand()[0].Rank}");
            Console.WriteLine($"딜러의 두번째 카드 :    ?     /     ?   ");
            Console.WriteLine($"당신의 패 : {Player.Hand.ShowHand()[0].Suit} / {Player.Hand.ShowHand()[0].Rank}");
            Console.WriteLine($"당신의 패 : {Player.Hand.ShowHand()[1].Suit} / {Player.Hand.ShowHand()[1].Rank}");
        }

        public void Ask()
        {

            while (Player.Hand.GetTotalValue() < 21)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("----------------------------------------------------------------------------------------\n\n");
                Console.WriteLine("        당신의 차례      :       딜러는 지금 카드를 한장 뒤집고 있습니다! \n\n");
                Console.WriteLine("----------------------------------------------------------------------------------------");
                Console.WriteLine("카드를 추가로 뽑으시겠습니까? ( y / n )");
                string input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    Card newCard = Player.DrawCardFromDeck(Deck);
                    Console.WriteLine($"당신의 패 : {newCard.Suit} / {newCard.Rank}");
                    Console.WriteLine($"카드의 총합 : {Player.Hand.GetTotalValue()}");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n\n");
            Console.WriteLine("                                   딜러의 차례 \n\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n\n");
            Dealer.DealerDraw(Deck);          
        }

        public void GameEnd()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n\n");
            Console.WriteLine("                                   카드 오픈.... \n\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n\n");

            List<Card> playerHand = Player.Hand.ShowHand();
            int playerTotal = Player.Hand.GetTotalValue();
            int dealerTotal = Dealer.Hand.GetTotalValue();

            foreach (Card card in playerHand)
            {
                Console.WriteLine($"당신의 패 : {card.Suit} / {card.Rank}");
            }
            Console.WriteLine($"당신 카드의 총합 : {playerTotal}\n\n");


            List<Card> dealerHand = Dealer.Hand.ShowHand();
            foreach (Card card in dealerHand)
            {
                Console.WriteLine($"딜러의 패 : {card.Suit} / {card.Rank}");
            }
            Console.WriteLine($"딜러 카드의 총합 : {dealerTotal}\n\n");

            

            bool playerOver = ( playerTotal > 21 ) ? true : false;
            bool dealerOver = ( dealerTotal > 21 ) ? true : false; 

            
            if (playerOver && dealerOver)
            {
                Console.WriteLine("무승부");
            }
            else if (playerOver || (dealerTotal > playerTotal) && dealerTotal < 22)
            {
                Console.WriteLine("ㅋ... 딜러의 승리로군요.");
            }
            else if (dealerOver || (playerTotal > dealerTotal) && playerTotal < 22)
            {
                Console.WriteLine("이런.. 당신의 승리입니다!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Dealer dealer = new Dealer();
            Deck deck = new Deck();

            Blackjack blackjack = new Blackjack(player, dealer, deck);
            while (true)
            {
                if (deck.ShowDeck().Count() < 10)
                {
                    player = new Player();
                    dealer = new Dealer();
                    deck = new Deck();
                    blackjack = new Blackjack(player, dealer, deck);
                    GameStream(blackjack);
                    
                }
                else
                {
                    player = new Player();
                    dealer = new Dealer();
                    blackjack = new Blackjack(player, dealer, deck);
                    GameStream(blackjack);
                }

                string input = Retry(blackjack);
                if (input.ToLower() == "n")
                {
                    break;
                }
            }

            void GameStream(Blackjack blackjack)
            {
                blackjack.GameStart();
                blackjack.InitShuffle();
                blackjack.Ask();
                blackjack.GameEnd();
            }

            string Retry(Blackjack blackjack)
            {
                Console.WriteLine("한판 더 하시겠습니까? 지금까지 뽑은 카드들은 사라집니다. (y / n)");
                string input = Console.ReadLine();
                return input;
            }

            //dealer.DealerDraw(deck);
        }
    }
}