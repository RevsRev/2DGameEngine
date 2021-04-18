using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

public class RvTheanoDidotFont : RvAbstractFont
{
    //This font is missing a few characters (e.g. ":") - maybe replace with a better font at some point.

    private static readonly int THEANO_DIDOT_FONT_LINE_SPACING = 5;
    private static readonly float THEANO_DIDOT_FONT_SPACING = 20.0f;
    private static readonly char THEANO_DIDOT_FONT_DEFAULT_CHARACTER = '£';

    //the character vector has to be in order(seriously annoying) - doing the easy fix and ignoring some characters for now.
    private static readonly int THEANO_DIDOT_NUM_CHARS = 83;

    //make private to ensure we use the factory method!
    private RvTheanoDidotFont(Texture2D texture, int lineSpacing, Single spacing, Nullable<Char> defaultCharacter) : base(texture, lineSpacing, spacing, defaultCharacter)
    {
    }

    public static RvTheanoDidotFont factory(ContentManager conent)
    {
        Texture2D texture = conent.Load<Texture2D>(RvContentFiles.FONTS + "TheanoDidot");
        return new RvTheanoDidotFont(texture, THEANO_DIDOT_FONT_LINE_SPACING, THEANO_DIDOT_FONT_SPACING, THEANO_DIDOT_FONT_DEFAULT_CHARACTER);
    }

    public override List<Rectangle> factoryGlyphBounds()
    {
        List<Rectangle> capitalLetters = divideRegionIntoRectangles(new Vector2(0,0), 938, 160, 2, 13);
        List<Rectangle> lowerCaseLetters = divideRegionIntoRectangles(new Vector2(0,184), 938, 160, 2, 13);
        List<Rectangle> numbers = divideRegionIntoRectangles(new Vector2(0, 368), 721, 80, 1, 10);
        List<Rectangle> symbolsOne = divideRegionIntoRectangles(new Vector2(0, 472), 938, 80, 1, 13);
        List<Rectangle> symbolsTwo = divideRegionIntoRectangles(new Vector2(0, 552), 8*720, 80, 1, 8);

        List<Rectangle> retval = new List<Rectangle>();
        retval.AddRange(symbolsOne);
        retval.AddRange(symbolsTwo);
        retval.AddRange(numbers);
        retval.AddRange(capitalLetters);
        retval.AddRange(lowerCaseLetters);

        retval = clipRectangles(retval, 20, 20, 32, 40);

        return retval;
    }

    public override List<Rectangle> factoryCropping()
    {
        // int cropX = 250;
        // int cropY = 250;
        // int width = 500;
        // int height = 500;

        List<Rectangle> retval = new List<Rectangle>();
        for (int i=0; i<THEANO_DIDOT_NUM_CHARS; i++)
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
            '.', ',', ';', ':', '@', '#', '\'', '!', '"', '/', '?', '<', '>',
            '%', '&', '*', '(', ')', '£', '$', ' ',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
    }
    public override List<Vector3> factoryKerning()
    {
        List<Vector3> retval = new List<Vector3>();
        for (int i=0; i<THEANO_DIDOT_NUM_CHARS; i++)
        {
            retval.Add(Vector3.Zero);
        }
        return retval;
    }
}