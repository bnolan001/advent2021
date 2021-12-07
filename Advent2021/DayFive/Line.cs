namespace DayFive
{
    internal class Line
    {
        public string raw;
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        private static char[] splitChars = { ' ', '-', ',', '>' };
        public  Direction Direction { get; set;}
        public Line()
        {
        }

        public void SetDefinition(string coordinates)
        {
            raw = coordinates; 
            var split = raw.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            StartX = int.Parse(split[0]);
            StartY = int.Parse(split[1]);
            EndX = int.Parse(split[2]);
            EndY = int.Parse(split[3]);

            if (StartX == EndX)
            {
                Direction = Direction.Horizontal;
            } else if (StartY == EndY) {
                Direction = Direction.Vertical;
            }
            else
            {
                Direction = Direction.Diagonal;
            }
        }

        public int GetMaxVertical()
        {
            return StartX > EndY ? StartX : EndY;
        }

        public int GetMaxHorizontal()
        {
            return StartX > EndX ? StartX : EndX;
        }
    }

    internal enum Direction
    {
        Vertical,
        Horizontal,
        Diagonal
    }
}
