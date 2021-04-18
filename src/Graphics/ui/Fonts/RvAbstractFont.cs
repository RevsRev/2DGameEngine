using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public abstract class RvAbstractFont
{
    //For the sprite font
    Texture2D texture;
    List<Rectangle> glyphBounds;
    List<Rectangle> cropping;
    List<Char> characters;
    int lineSpacing;
    Single spacing;
    List<Vector3> kerning;
    Nullable<Char> defaultCharacter;

    public RvAbstractFont(Texture2D texture, int lineSpacing, Single spacing, Nullable<Char> defaultCharacter)
    {
        this.texture = texture;
        this.lineSpacing = lineSpacing;
        this.spacing = spacing;
        this.defaultCharacter = defaultCharacter;

        glyphBounds = factoryGlyphBounds();
        cropping = factoryCropping();
        characters = factoryCharacters();
        kerning = factoryKerning();

        sortCharacters();
    }

    public SpriteFont createSpriteFont()
    {
        return new SpriteFont(texture, glyphBounds, cropping, characters, lineSpacing, spacing, kerning, defaultCharacter);
    }

    public abstract List<Rectangle> factoryGlyphBounds();
    public abstract List<Rectangle> factoryCropping();
    public abstract List<char> factoryCharacters();
    public abstract List<Vector3> factoryKerning();

    //Annoyingly, sprite fonts will break if characters aren't in ascending order. So we have to sort everything here.
    private void sortCharacters()
    {
        //I can't be bothered to anything mildly clever, and this doesn't need to be fast because it only runs once per font we load.
        //So I'm going to do a bubble sort!
        for (int i=0; i<characters.Count; i++)
        {
            for (int j=0; j<characters.Count-i-1; j++)
            {
                if (characters[j] > characters[j+1])
                {
                    doSwap(characters, j, j+1);
                    doSwap(glyphBounds, j, j+1);
                    doSwap(cropping, j, j+1);
                    doSwap(kerning, j, j+1);
                }
            }
        }
    }
    private void doSwap<T>(List<T> list, int indexOne, int indexTwo)
    {
        T elAtIndexOne = list[indexOne];
        T elAtIndexTwo = list[indexTwo];

        list[indexOne] = elAtIndexTwo;
        list[indexTwo] = elAtIndexOne;
    }

    public static List<Rectangle> divideRegionIntoRectangles(Vector2 topLeftCorner, int width, int height, int rows, int columns)
    {
        int rectWidth = width/columns;
        int rectHeight = height/rows;
        int numRectangles = rows*columns;

        int X = (int)topLeftCorner.X;
        int Y = (int)topLeftCorner.Y;

        List<Rectangle> retval = new List<Rectangle>();
        for (int i=0; i<numRectangles; i++)
        {
            int rowNum = i/columns;
            int colNum = i%columns;

            retval.Add(new Rectangle(X + colNum*rectWidth, Y + rowNum*rectHeight, rectWidth, rectHeight));
        }
        return retval;
    }

    public static List<Rectangle> clipRectangles(List<Rectangle> rectangles, int x, int y, int width, int height)
    {
        List<Rectangle> retval = new List<Rectangle>();
        for (int i=0; i<rectangles.Count; i++)
        {
            Rectangle rectangle = rectangles[i];
            retval.Add(new Rectangle(rectangle.X + x, rectangle.Y + y, width, height));
        }
        return retval;
    }

    public Vector2 getLetterSize()
    {
        return new Vector2(glyphBounds[0].Width, glyphBounds[0].Height);
    }
}