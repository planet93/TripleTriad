using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
    public class Card
    {
        public int Top { get; set; }
        public int Botton { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        /// <summary>
        /// Пустая карта в клетке
        /// </summary>
        public bool Empty { get; set; } = true;
        /// <summary>
        /// Если истина, то карта в руке
        /// </summary>
        public bool Delete { get; set; } = true;
        /// <summary>
        /// Кому пренадлежит карта
        /// </summary>
        public string NamePlayer { get; set; }
        //public string ShotName { get; set; } = " ";
        public string ActiveName
        {
            get
            {
                if (Active)
                {
                    return "*";
                }
                return " ";
            }
            set { }
        }
        public string ShotName { get
            {
                if (NamePlayer == "player1")
                {
                    return "+";
                }
                if (NamePlayer == "player2")
                {
                    return "-";
                }
                return " ";
            }
            set { }
        }
        public string PlayerSimbol
        {
            get
            {
                
                if (NamePlayer == "player1")
                {
                    return "+";
                }
                return "-";
            }
            set { }
        }
        public bool Active { get; set; }
    }
}
