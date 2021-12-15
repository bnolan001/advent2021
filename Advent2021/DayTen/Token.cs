using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTen
{
    public class Token
    {
        public static Token OpenParenthesis = new Token(0, "OpenParenthesis", '(', 0, 0);
        public static Token ClosedParenthesis = new Token(1, "ClosedParenthesis", ')', 3, 1);
        public static Token OpenSquareBracket = new Token(2, "OpenSquareBracket", '[', 0, 0);
        public static Token ClosedSquareBracket = new Token(3, "ClosedSquareBracket", ']', 57, 2);
        public static Token OpenSquiglyBracket = new Token(4, "OpenSquiglyBracket", '{', 0, 0);
        public static Token ClosedSquiglyBracket = new Token(5, "ClosedSquiglyBracket", '}', 1197, 3);
        public static Token LessThan = new Token(6, "LessThan", '<', 0, 0);
        public static Token GreaterThan = new Token(7, "GreaterThan", '>', 25137, 4);

        public int Id { get; private set; }

        public string Name { get; private set; }

        public char Value { get; private set; }

        public int ErrorPoints { get; private set; }

        public int ClosingPoints { get; private set; }

        public Token(int value, string name, char token, int points, int closingPoints)
        {
            ErrorPoints = points;
            Name = name;
            Id = value;
            Value = token;
            ClosingPoints = closingPoints;
        }

        public static List<Token> GetList()
        {
            return new List<Token> { OpenParenthesis, ClosedParenthesis, 
                OpenSquareBracket, ClosedSquareBracket, OpenSquiglyBracket, 
                ClosedSquiglyBracket, LessThan, GreaterThan };
        }

        public static Token GetByValue(char token)
        {
            return GetList().FirstOrDefault(t => t.Value == token);
        }

        public static Token GetById(int id)
        {
            return GetList().FirstOrDefault(t => t.Id == id);
        }
    }
}
