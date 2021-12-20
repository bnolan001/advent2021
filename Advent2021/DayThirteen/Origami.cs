using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayThirteen
{
    public class Origami
    {
        public List<List<int>> Paper { get; set; } = new List<List<int>>();

        public List<Coordinate> Coordinates { get; set; } = new List<Coordinate>();

        public List<Coordinate> Folds { get; set; } = new List<Coordinate>();

        public void CreatePaper()
        {
            var maxCols = Coordinates.Select(c => c.X).Max() + 1;
            var maxRows = Coordinates.Select(c => c.Y).Max() + 1;
            
            for (int row = 0; row < maxRows; row++)
            {
                var paperRow = new List<int>();
                for (int col = 0; col < maxCols; col++)
                {
                    paperRow.Add(0);
                }
                Paper.Add(paperRow);
            }

            foreach (var coord in Coordinates)
            {
                Paper[coord.Y][coord.X] = 1;
            }
        }

        public void FoldPaper(Coordinate fold)
        {
            if (fold.X > 0)
                FoldOnXAxis(fold.X);
            else
                FoldOnYAxis(fold.Y);

            TrimPaperSize(fold);
        }

        private void FoldOnXAxis(int foldX)
        {
            var maxCols = Coordinates.Select(c => c.X).Max() + 1;

            for (var col = foldX + 1; col < maxCols; col++)
            {
                for (var row = 0; row < Paper.Count; row++)
                {
                    var copyToX = foldX - (col - foldX);
                    if (copyToX >= 0)
                    {
                        Paper[row][copyToX] = Paper[row][copyToX] + Paper[row][col];
                    }
                }
            }
        }

        private void FoldOnYAxis(int foldY)
        {
            var maxCols = Coordinates.Select(c => c.X).Max() + 1;

            for (var row = foldY + 1; row < Paper.Count; row++)
            {
                var copyToY = foldY - (row - foldY);
                if (copyToY >= 0)
                {
                    for (var col = 0; col < Paper[row].Count; col++)
                    {
                        Paper[copyToY][col] = Paper[copyToY][col] + Paper[row][col];
                    }
                }
            }
        }

        public void TrimPaperSize(Coordinate fold)
        {

            if (fold.Y > 0)
            {
                Paper.RemoveRange(fold.Y, Paper.Count - fold.Y);
            }
            else
            {
                for (var row = 0; row < Paper.Count; row++)
                {
                    Paper[row].RemoveRange(fold.X, Paper[row].Count - fold.X);
                }
            }
        }

    }
    public record struct Coordinate
    {
        public int X { get; init; }
        public int Y { get; init; }
    }
}
