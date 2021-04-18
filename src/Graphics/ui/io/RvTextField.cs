using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Collections.Generic;
using System;

public class RvTextField : RvAbstractComponent, RvKeyboardListenerI
{
    public const int MODE_DEFAULT = 0;
    public const int MODE_NUMBERS = 1;

    private StringBuilder sb = new StringBuilder();
    private Dictionary<Keys, Tuple<char, char>> keysToChars = new Dictionary<Keys, Tuple<char, char>>();
    private int mode = MODE_DEFAULT;

    public RvTextField(Rectangle bounds) : this(bounds, MODE_DEFAULT)
    {
    }
    public RvTextField(Rectangle bounds, int mode) : base(bounds)
    {
        this.mode = mode;
        RvKeyboard.the().addKeyboardListener(this);
        initKeysToChars();
    }

    private void initKeysToChars()
    {
        switch (mode)
        {
            case MODE_DEFAULT:
                initKeysToCharsDefault();
                break;
            case MODE_NUMBERS:
                initKeysToCharsNumbers();
                break;
            default:
                mode = MODE_DEFAULT;
                initKeysToChars();
                return;
        }
    }

    public override void Draw(RvSpriteBatch spriteBatch)
    {
        //at the moment, this is just to text.
        spriteBatch.DrawRectangle(bounds, Color.White, RvSpriteBatch.DEFAULT_UI_LAYER);
        spriteBatch.DrawRectangleBorder(bounds, Color.Gray, RvSpriteBatch.DEFAULT_UI_BORDER_LAYER);
        spriteBatch.DrawString(sb.ToString(), new Vector2(bounds.X, bounds.Y), 20);
    }

    public void keyPressed(Keys key)
    {
        bool shiftModifier = RvKeyboard.the().getShift();
        processKey(key, shiftModifier);
    }

    private void processKey(Keys key, bool shiftModifier)
    {
        if (key == Keys.Back && sb.Length > 0)
        {
            sb.Remove(sb.Length-1 ,1);
        }

        if (shiftModifier && keysToChars.ContainsKey(key))
        {
            sb.Append(keysToChars[key].Item2);
        }
        else if (keysToChars.ContainsKey(key))
        {
            sb.Append(keysToChars[key].Item1);
        }
    }

    private void initKeysToCharsNumbers()
    {
        addKeyToChar(Keys.D1, '1', '1');
        addKeyToChar(Keys.D2, '2', '2');
        addKeyToChar(Keys.D3, '3', '3');
        addKeyToChar(Keys.D4, '4', '4');
        addKeyToChar(Keys.D5, '5', '5');
        addKeyToChar(Keys.D6, '6', '6');
        addKeyToChar(Keys.D7, '7', '7');
        addKeyToChar(Keys.D8, '8', '8');
        addKeyToChar(Keys.D9, '9', '9');
        addKeyToChar(Keys.D0, '0', '0');
        //addKeyToChar(Keys.OemMinus, '-' '-'); //todo (when we want negative numbers) - similarly for decimal.
    }

    private void initKeysToCharsDefault()
    {
        //letters
        addKeyToChar(Keys.A, 'a', 'A');
        addKeyToChar(Keys.B, 'b', 'B');
        addKeyToChar(Keys.C, 'c', 'C');
        addKeyToChar(Keys.D, 'd', 'D');
        addKeyToChar(Keys.E, 'e', 'E');
        addKeyToChar(Keys.F, 'f', 'F');
        addKeyToChar(Keys.G, 'g', 'G');
        addKeyToChar(Keys.H, 'h', 'H');
        addKeyToChar(Keys.I, 'i', 'I');
        addKeyToChar(Keys.J, 'j', 'J');
        addKeyToChar(Keys.K, 'k', 'K');
        addKeyToChar(Keys.L, 'l', 'L');
        addKeyToChar(Keys.M, 'm', 'M');
        addKeyToChar(Keys.N, 'n', 'N');
        addKeyToChar(Keys.O, 'o', 'O');
        addKeyToChar(Keys.P, 'p', 'P');
        addKeyToChar(Keys.Q, 'q', 'Q');
        addKeyToChar(Keys.R, 'r', 'R');
        addKeyToChar(Keys.S, 's', 'S');
        addKeyToChar(Keys.T, 't', 'T');
        addKeyToChar(Keys.U, 'u', 'U');
        addKeyToChar(Keys.V, 'v', 'V');
        addKeyToChar(Keys.W, 'w', 'W');
        addKeyToChar(Keys.X, 'x', 'X');
        addKeyToChar(Keys.Y, 'y', 'Y');
        addKeyToChar(Keys.Z, 'z', 'Z');

        //numbers
        addKeyToChar(Keys.D1, '1', '!');
        addKeyToChar(Keys.D2, '2', '"');
        addKeyToChar(Keys.D3, '3', '£');
        addKeyToChar(Keys.D4, '4', '$');
        addKeyToChar(Keys.D5, '5', '%');
        addKeyToChar(Keys.D6, '6', '^');
        addKeyToChar(Keys.D7, '7', '&');
        addKeyToChar(Keys.D8, '8', '*');
        addKeyToChar(Keys.D9, '9', '(');
        addKeyToChar(Keys.D0, '0', ')');

        //space
        addKeyToChar(Keys.Space, ' ', ' ');

        //punctuation
        addKeyToChar(Keys.OemQuotes, '\'', '@');
        addKeyToChar(Keys.OemComma, ',', '<');
        addKeyToChar(Keys.Decimal, ',', '>');
        addKeyToChar(Keys.OemSemicolon, ';', ':');

    }
    private void addKeyToChar(Keys key, char charNoShift, char charShift)
    {
        keysToChars[key] = new Tuple<char, char>(charNoShift, charShift);
    }
}