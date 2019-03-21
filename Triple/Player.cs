using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
    public class Player
    {
        /// <summary>
        /// Карты в начале игры в руке игрока
        /// </summary>
        public List<Card> Hand_Cards { get; set; }
        /// <summary>
        /// Максимальное количество очков на карточке
        /// </summary>
        public int MaxNumber { get; set; } = 11;
        /// <summary>
        /// Максимальное количество карт в руке
        /// </summary>
        public int ColCardsHand { get; set; } = 5;
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Активный игрок
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Количество карт у игрока
        /// </summary>
        public int CountCard { get; set; }
        public string ShortName
        {
            get
            {
                if (Name == "player1")
                {
                    return "+";
                }
                if(Name == "player2")
                {
                    return "-";
                }
                return " ";
            }
        }

        public Player(string playerName)
        {
            Name = playerName;
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(new Card());
                if (i == 0)
                {
                    cards[i].Active = true;
                }
                cards[i] = Logic.InitCard(MaxNumber, cards[i]);
                cards[i].NamePlayer = playerName;
                cards[i].ShotName = playerName;
            }
            CountCard = cards.Count;
            Hand_Cards = cards;
        }
        public List<Card> Right(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Active)
                {
                    if (i + 1 < cards.Count)
                    {
                        cards[i].Active = false;
                        cards[i + 1].Active = true;
                        return cards;
                    }
                }
            }
            return cards;
        }
        public List<Card>  Left(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Active)
                {
                    if (i > 0)
                    {
                        cards[i].Active = false;
                        cards[i - 1].Active = true;
                        return cards;
                    }
                }
            }
            return cards;
        }
    }
}
