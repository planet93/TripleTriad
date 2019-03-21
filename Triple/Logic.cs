using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
    static public class Logic
    {
        static Random random = new Random();
        /// <summary>
        /// Заполнение одной карты
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        static public Card InitCard(int max, Card card)
        {
            bool checkNum = true;
            card.Top = random.Next(1, max - 3);
            if (card.Top == max - 3)
            {
                card.Botton = 1;
                card.Left = 1;
                card.Right = 1;
                return card;
            }
            while (checkNum)
            {
                card.Botton = random.Next(1, max - 2);
                if((card.Top+card.Botton) < max - 2)
                {
                    checkNum = false;
                }
            }
            if ((card.Top + card.Botton) == max - 2)
            {
                card.Left = 1;
                card.Right = 1;
                return card;
            }
            checkNum = true;
            while (checkNum)
            {
                card.Left = random.Next(1, max - 1);
                if ((card.Top + card.Botton+card.Left) < max - 1)
                {
                    checkNum = false;
                }
            }
            card.Right = max - (card.Top + card.Botton + card.Left);

            return card;
        }
        public static Move Move_Player(Player player,Cells cells)
        {
            Move move = new Move();
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if (cells.Field[i,j].Active)
                    {
                        for(int n = 0; n < player.Hand_Cards.Count; n++)
                        {
                            if (player.Hand_Cards[n].Active)
                            {
                                if (cells.Field[i, j].Empty)
                                {
                                    DefinitionCells(cells, player.Hand_Cards[n], i, j);
                                    cells.Field[i, j] = player.Hand_Cards[n];
                                    cells.Field[i, j].Empty = false;
                                    player.Hand_Cards.RemoveAt(n);
                                    if(player.Hand_Cards.Count != 0)
                                    {
                                        player.Hand_Cards[0].Active = true;
                                    }
                                    player.CountCard = CountCardPlayer(cells, player);
                                    move.MCells = cells;
                                    move.MPlayer = player;
                                    return move;
                                }
                                move.MCells = cells;
                                move.MPlayer = player;
                                move.Empty = false;
                                return move;
                            }
                           
                        }
                    }
                }
            }

            return null;
        }
        public static Cells DefinitionCells(Cells cells, Card card,int i, int j)
        {
            //Проверка карты слева
            if (j - 1 > -1)
            {
                if(cells.Field[i,j-1].ShotName != " " && cells.Field[i,j-1].ShotName != card.ShotName)
                {
                    if (cells.Field[i, j - 1].Right < card.Left)
                    {
                        cells.Field[i, j - 1].NamePlayer = card.NamePlayer;
                    }
                }
            }
            //Проверка карты справа
            if (j + 1 < cells.Field.GetLength(1))
            {
                if (cells.Field[i, j + 1].ShotName != " " && cells.Field[i, j + 1].ShotName != card.ShotName)
                {
                    if (cells.Field[i, j + 1].Left < card.Right)
                    {
                        cells.Field[i, j + 1].NamePlayer = card.NamePlayer;
                    }
                }
            }
            //Проверка карты сверху
            if (i - 1 > -1)
            {
                if (cells.Field[i-1, j].ShotName != " " && cells.Field[i-1, j].ShotName != card.ShotName)
                {
                    if (cells.Field[i - 1, j].Botton < card.Top)
                    {
                        cells.Field[i - 1, j].NamePlayer = card.NamePlayer;
                    }
                }
            }
            //Проверка карты снизу
            if (i + 1 < cells.Field.GetLength(0))
            {
                if (cells.Field[i + 1, j].ShotName != " " && cells.Field[i + 1, j].ShotName != card.ShotName)
                {
                    if (cells.Field[i + 1, j].Top < card.Botton)
                    {
                        cells.Field[i + 1, j].NamePlayer = card.NamePlayer;
                    }
                }
            }
            return cells;
        }
        public static int CountCardPlayer(Cells cells,Player player)
        {
            int cards = player.Hand_Cards.Count;
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if(cells.Field[i,j].ShotName == player.ShortName)
                    {
                        cards++;
                    }
                }
            }
            return cards;
        }
        public static string DefineWin(Player player1,Player player2)
        {
            if (player1.Hand_Cards.Count == 0 || player2.Hand_Cards.Count == 0)
            {
                if (player1.CountCard > player2.CountCard)
                {
                    return player1.Name;
                }
                if (player1.CountCard < player2.CountCard)
                {
                    return player2.Name;
                }
                if(player1.CountCard == player2.CountCard)
                {
                    return "draw";
                }
            }
            return string.Empty;
        }
    }
    public class Move
    {
        public Cells MCells { get; set; }
        public Player MPlayer { get; set; }
        public bool Empty { get; set; } = true;
    }

}
