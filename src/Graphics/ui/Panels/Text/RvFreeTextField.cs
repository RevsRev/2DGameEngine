using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Collections.Generic;
using System;

public class RvFreeTextField : RvAbstractTextField
{
    public RvFreeTextField(Rectangle bounds) : base(bounds)
    {
    }

    protected override void initKeysToChars()
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
}