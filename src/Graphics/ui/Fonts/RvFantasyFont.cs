using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

public class RvFantasyFont : RvAbstractFont
{
    //This font is missing a few characters (e.g. ":") - maybe replace with a better font at some point.

    private static readonly int FANTASY_FONT_LINE_SPACING = 5;
    private static readonly float FANTASY_FONT_SPACING = 35.0f;
    private static readonly char FANTASY_FONT_DEFAULT_CHARACTER = '0';

    //the character vector has to be in order(seriously annoying) - doing the easy fix and ignoring some characters for now.
    private static readonly int FANTASY_NUM_CHARS = 62;

    //make private to ensure we use the factory method!
    private RvFantasyFont(Texture2D texture, int lineSpacing, Single spacing, Nullable<Char> defaultCharacter) : base(texture, lineSpacing, spacing, defaultCharacter)
    {
    }

    public static RvFantasyFont factory(ContentManager conent)
    {
        Texture2D texture = conent.Load<Texture2D>(RvContentFiles.FONTS + "fantasy_font");
        return new RvFantasyFont(texture, FANTASY_FONT_LINE_SPACING, FANTASY_FONT_SPACING, FANTASY_FONT_DEFAULT_CHARACTER);
    }

    public override List<Rectangle> factoryGlyphBounds()
    {
        List<Rectangle> capitalLetters = divideRegionIntoRectangles(new Vector2(0,0), 1300, 200, 2, 13);
        List<Rectangle> lowerCaseLetters = divideRegionIntoRectangles(new Vector2(0,250), 1300, 200, 2, 13);
        List<Rectangle> numbers = divideRegionIntoRectangles(new Vector2(0, 500), 1000, 100, 1, 10);
        //List<Rectangle> symbols = divideRegionIntoRectangles(new Vector2(0, 650), 1000, 200, 2, 10); not including because of stupid character ordering.

        List<Rectangle> retval = new List<Rectangle>();
        retval.AddRange(numbers);
        retval.AddRange(lowerCaseLetters);
        retval.AddRange(capitalLetters);

        retval = clipRectangles(retval, 25, 25, 50, 50);

        return retval;
    }

    public override List<Rectangle> factoryCropping()
    {
        // int cropX = 250;
        // int cropY = 250;
        // int width = 500;
        // int height = 500;

        List<Rectangle> retval = new List<Rectangle>();
        for (int i=0; i<FANTASY_NUM_CHARS; i++)
        {
            //retval.Add(new Rectangle(cropX, cropY, width, height));
            retval.Add(new Rectangle(0,0,0,0));
        }
        return retval;
    }

    public override List<char> factoryCharacters()
    {
        return new List<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            /*,
            ')', '!', '@', '#', '$', '%', '^', '&', '*', '(',
            ',', '.', '/', ';', '\'', '[', '<', '?', '"', '}'*/ //not including due to stupid ordering.
        };
    }
    public override List<Vector3> factoryKerning()
    {
        List<Vector3> retval = new List<Vector3>();
        for (int i=0; i<FANTASY_NUM_CHARS; i++)
        {
            retval.Add(Vector3.Zero);
        }
        return retval;
    }
}