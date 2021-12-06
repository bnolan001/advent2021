using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayFour
{
    public class GameCard
    {
        private List<HashSet<int>> rows;
        private List<HashSet<int>> cols;
        private HashSet<int> numbers;
        private List<int> matchedNumbers;
        private List<int> unmatchedNumbers;

        public GameCard()
        {
            rows = new List<HashSet<int>>();
            cols = new List<HashSet<int>>();
            numbers = new HashSet<int>();
            matchedNumbers = new List<int> ();
            unmatchedNumbers = new List<int> ();
        }

        public void AddRow(List<int> row)
        {
            if (cols.Count == 0)
            {
                foreach(var i in row)
                {
                    cols.Add(new HashSet<int>());
                }
            }
            var hashRow = new HashSet<int>();
            for(var idx = 0; idx < row.Count; idx++)
            {
                hashRow.Add(row[idx]);
                cols[idx].Add(row[idx]);
                unmatchedNumbers.Add(row[idx]);
                numbers.Add(row[idx]);
            }
            rows.Add(hashRow);
        }

        public bool CheckForNumber(int num)
        {
            bool winner = false;
            if (numbers.Contains(num))
            {
                matchedNumbers.Add(num);
                unmatchedNumbers.Remove(num);
                foreach(var row in rows)
                {
                    if (row.Remove(num))
                    {
                        winner |= row.Count == 0;
                    }
                }

                foreach(var col in cols)
                {
                    if (col.Remove(num))
                    {
                        winner |= col.Count == 0;
                    }
                }
            }
            return winner;
        }

        public bool IsValidCard()
        {
            return rows.Count > 0 && cols.Count > 0;
        }

        public int CalculateScore(int num)
        {
            var score = unmatchedNumbers.Sum() * num;

            return score;
        }
    }
}
