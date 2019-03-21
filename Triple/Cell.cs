using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple
{
   public class Cells
    {
        public Card[,]  Field { get; set; }
        public bool Active { get; set; }
        public Cells (int size)
        {
            Card[,] fields = new Card[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    fields[i, j] = new Card();
                }
            }
            fields[0, 0].Active = true;
            Field = fields;
        } 

        public Cells Right(Cells cells)
        {
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if (cells.Field[i, j].Active)
                    {
                        if (j < cells.Field.GetLength(1) - 1)
                        {
                            cells.Field[i, j + 1].Active = true ;
                            cells.Field[i, j].Active = false ;
                            return cells;
                        }
                    }
                }
            }
            return cells;
        }
        public Cells Left(Cells cells)
        {
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if (cells.Field[i, j].Active)
                    {
                        if (j > 0)
                        {
                            cells.Field[i, j - 1].Active = true ;
                            cells.Field[i, j].Active = false;
                            return cells;
                        }
                    }
                }
            }
            return cells;
        }
        public Cells Botton(Cells cells)
        {
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if (cells.Field[i, j].Active)
                    {
                        if (i < cells.Field.GetLength(0) - 1)
                        {
                            cells.Field[i + 1, j].Active = true;
                            cells.Field[i, j].Active = false ;
                            return cells;
                        }
                    }
                }
            }
            return cells;
        }
        public Cells Top(Cells cells)
        {
            for (int i = 0; i < cells.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cells.Field.GetLength(1); j++)
                {
                    if (cells.Field[i, j].Active)
                    {
                        if (i > 0)
                        {
                            cells.Field[i - 1, j].Active = true ;
                            cells.Field[i, j].Active = false;
                            return cells;
                        }
                    }
                }
            }
            return cells;
        }
    }
}
