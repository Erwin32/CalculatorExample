using System.Globalization;
using System.IO;
using System.Text;

namespace TestProject.Calculator.MathOperations
{
    public class Tokenizer
    {
        public Tokenizer(TextReader reader)
        {
            Reset(reader);
        }

        public void Reset(TextReader reader)
        {
            myReader = reader;
            NextChar();
            NextToken();
        }

        private TextReader myReader;
        private char myCurrentChar;

        public Token Token { get; private set; }

        public decimal Number { get; private set; }

        // Read the next character from the input strem
        // and store it in _currentChar, or load '\0' if EOF
        private void NextChar()
        {
            int ch = myReader.Read();
            myCurrentChar = ch < 0 ? '\0' : (char)ch;
        }

        // Read the next token from the input stream
        public void NextToken()
        {
            // Skip whitespace
            while (char.IsWhiteSpace(myCurrentChar))
            {
                NextChar();
            }

            // Special characters
            switch (myCurrentChar)
            {
                case '\0':
                    Token = Token.EndOfExpression;
                    return;

                case '+':
                    NextChar();
                    Token = Token.Add;
                    return;

                case '-':
                    NextChar();
                    Token = Token.Subtract;
                    return;
                case '*':
                    NextChar();
                    Token = Token.Multiply;
                    return;

                case '/':
                    NextChar();
                    Token = Token.Divide;
                    return;

                case '(':
                    NextChar();
                    Token = Token.OpenParens;
                    return;

                case ')':
                    NextChar();
                    Token = Token.CloseParens;
                    return;
            }

            // Number?
            if (char.IsDigit(myCurrentChar) || myCurrentChar =='.')
            {
                // Capture digits/decimal point
                var sb = new StringBuilder();
                bool haveDecimalPoint = false;
                while (char.IsDigit(myCurrentChar) || (!haveDecimalPoint && myCurrentChar == '.'))
                {
                    sb.Append(myCurrentChar);
                    haveDecimalPoint = myCurrentChar == '.';
                    NextChar();
                }

                // Parse it
                Number = decimal.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                Token = Token.Number;
                return;
            }

            throw new InvalidDataException($"Unexpected character: {myCurrentChar}");
        }
    }
}