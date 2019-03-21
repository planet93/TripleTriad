using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
    class Program
    {
        static string Message { get; set;}
        static void Main(string[] args)
        {
            BeginGame:
            int size = 3;
            Cells cells = new Cells(size);
            bool move_cells = false;
            Player player1 = new Player("player1")
            {
                Active = true
            };
            Message = $"Ходит игрок {player1.Name}. Выберете карту";
            Player player2 = new Player("player2");
            string defWin = string.Empty;
            bool endGame = false;
            Show(player1, player2,cells);
            while (!endGame)
            {
                Move move = new Move();
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (move_cells)
                        {
                            cells = cells.Left(cells);
                        }
                        else
                        {
                            if (player1.Active)
                            {
                                player1.Hand_Cards = player1.Left(player1.Hand_Cards);
                            }
                            else
                            {
                                player2.Hand_Cards = player2.Left(player2.Hand_Cards);
                            }
                        }
                        
                        Show(player1, player2, cells);
                        break;
                    case ConsoleKey.RightArrow:
                        if (move_cells)
                        {
                            cells = cells.Right(cells);
                        }
                        else
                        {
                            if (player1.Active)
                            {
                                player1.Hand_Cards = player1.Right(player1.Hand_Cards);
                            }
                            else
                            {
                                player2.Hand_Cards = player2.Right(player2.Hand_Cards);
                            }
                        }
                        Show(player1, player2, cells);
                        break;
                    case ConsoleKey.UpArrow:
                        if (move_cells)
                        {
                            cells = cells.Top(cells);
                        }
                        Show(player1, player2, cells);
                        break;
                    case ConsoleKey.DownArrow:
                        if (move_cells)
                        {
                            cells = cells.Botton(cells);
                        }
                        Show(player1, player2, cells);
                        break;
                    case ConsoleKey.Enter:
                        if (cells.Active)
                        {
                            if (player1.Active)
                            {
                                move = Logic.Move_Player(player1, cells);
                                player2.CountCard = Logic.CountCardPlayer(cells, player2); 
                                player1 = move.MPlayer;
                                cells = move.MCells;
                            }
                            else
                            {
                                move = Logic.Move_Player(player2, cells);
                                player1.CountCard = Logic.CountCardPlayer(cells, player1);
                                player2 = move.MPlayer;
                                cells = move.MCells;
                            }
                            if (!move.Empty)
                            {
                                Message = "Клетка уже занята";
                                Show(player1, player2, cells);
                                break;
                            }
                            cells.Active = false;
                            move_cells = false;
                            player1.Active = !player1.Active;
                            player2.Active = !player2.Active;
                        }
                        else
                        {
                            
                            cells.Active = true;
                            move_cells = true;
                        }
                        
                        if (cells.Active)
                        {
                            if (player1.Active)
                            {
                                Message = $"Ходит игрок {player1.Name}. Выберете клетку";
                            }
                            else
                            {
                                Message = $"Ходит игрок {player2.Name}. Выберете клетку";
                            }
                        }
                        else
                        {
                            if (player1.Active)
                            {
                                Message = $"Ходит игрок {player1.Name}. Выберете карту";
                            }
                            else
                            {
                                Message = $"Ходит игрок {player2.Name}. Выберете карту";
                            }
                        }
                        //Проверка на победителя
                        defWin = Logic.DefineWin(player1, player2);
                        if(defWin != string.Empty)
                        {
                            endGame = true;
                            if(defWin == "draw")
                            {
                                Message = "Конец игры. Ничья";
                            }
                            else
                            {
                                Message = $"Конец игры. {player1.Name} {player1.CountCard} - {player2.CountCard} {player2.Name}. Победил {defWin}";
                            }
                        }
                        Show(player1, player2, cells);
                        break;
                    case ConsoleKey.Escape:
                        if (cells.Active)
                        {
                            cells.Active = false;
                            move_cells = false;
                        }
                        else
                        {
                            cells.Active = true;
                            move_cells = true;
                        }

                        if (cells.Active)
                        {
                            if (player1.Active)
                            {
                                Message = $"Ходит игрок {player1.Name}. Выберете клетку";
                            }
                            else
                            {
                                Message = $"Ходит игрок {player2.Name}. Выберете клетку";
                            }
                        }
                        else
                        {
                            if (player1.Active)
                            {
                                Message = $"Ходит игрок {player1.Name}. Выберете карту";
                            }
                            else
                            {
                                Message = $"Ходит игрок {player2.Name}. Выберете карту";
                            }
                        }
                        break;
                }
            }
            Console.SetCursorPosition(0, 30);
            Console.Write("Хотите сыграть еще (Y/N)");
            bool tooGame = true;
            while (tooGame)
            {
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.Y:
                        goto BeginGame;
                    case ConsoleKey.N:
                        tooGame = false;
                        break;
                }
            }
        }
        static void Right(Player player)
        {
            for (int i = 0; i < player.Hand_Cards.Count; i++)
            {
                if (player.Hand_Cards[i].Active)
                {
                    Console.SetCursorPosition((i * 7) + 10, 2);
                    Console.Write($" *** ");
                }
            }
        }
        static void Show(Player player1,Player player2, Cells cells)
        {
            int left = 0;
            int top = 0;
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine($"{player1.Name} {player1.CountCard}");
            
            for (int i = 0; i < player1.Hand_Cards.Count; i++)
            {
                if (player1.Hand_Cards[i].Active && player1.Active)
                {
                    Console.SetCursorPosition((i * 7) + 10, 2);
                    Console.Write($" *** ");
                }
                else
                {
                    Console.SetCursorPosition((i * 7) + 10, 2);
                    Console.Write("     ");
                }
                top = 3;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write("_____");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"| {player1.Hand_Cards[i].Top} |");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"|{player1.Hand_Cards[i].Left}{player1.Hand_Cards[i].PlayerSimbol}{player1.Hand_Cards[i].Right}|");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"| {player1.Hand_Cards[i].Botton} |");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"-----");
            }
            Console.SetCursorPosition(0, 11);
            Console.WriteLine($"{player2.Name} {player2.CountCard}");
            for (int i = 0; i < player2.Hand_Cards.Count; i++)
            {
                if (player2.Hand_Cards[i].Active && player2.Active)
                {
                    Console.SetCursorPosition((i * 7) + 10, 8);
                    Console.Write($" *** ");
                }
                else
                {
                    Console.SetCursorPosition((i * 7) + 10, 8);
                    Console.Write("     ");
                }
                top = 9;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write("_____");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"| {player2.Hand_Cards[i].Top} |");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"|{player2.Hand_Cards[i].Left}{player2.Hand_Cards[i].PlayerSimbol}{player2.Hand_Cards[i].Right}|");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"| {player2.Hand_Cards[i].Botton} |");
                top++;
                Console.SetCursorPosition((i * 7) + 10, top);
                Console.Write($"-----");
            }
            Console.SetCursorPosition(0,14);
            Console.Write("Поле для игры");
            Console.SetCursorPosition(0, 15);
            Console.Write("-------------------");
            top = 16;
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                left = 0;
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {                    
                    Console.SetCursorPosition(left, top);
                    Console.Write($"| {cells.Field[i, j].ActiveName}{cells.Field[i,j].Top}{cells.Field[i, j].ActiveName} ");
                    Console.SetCursorPosition(left, top + 1);
                    Console.Write($"| {cells.Field[i, j].Left}{cells.Field[i,j].ShotName}{cells.Field[i,j].Right} ");
                    Console.SetCursorPosition(left, top + 2);
                    Console.Write($"| {cells.Field[i, j].ActiveName}{cells.Field[i, j].Botton}{cells.Field[i, j].ActiveName} ");
                    left += 6;
                    if (j + 1 == cells.Field.GetLength(1))
                    {
                        Console.SetCursorPosition(left, top);
                        Console.Write("|");
                        Console.SetCursorPosition(left, top + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(left, top + 2);
                        Console.Write("|");
                    }
                }
                top += 4;
                Console.SetCursorPosition(0, top-1);
                Console.Write($"|-----+-----+-----|");
            }
            Console.SetCursorPosition(0, top);
            Console.Write(Message);
        }
    }
}
