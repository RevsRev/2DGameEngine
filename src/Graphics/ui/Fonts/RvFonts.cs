using Microsoft.Xna.Framework;
using System.Collections.Generic;

//A class to hold all fonts.
//If we want more fonts, should replace the list with a map.
public class RvFonts
{
    private List<RvAbstractFont> fonts;
    private int currentFont = 0;

    public RvFonts(Game game)
    {
        fonts = new List<RvAbstractFont>
        {
            RvFantasyFont.factory(game.Content)
        };
    }

    public RvAbstractFont getCurrentFont()
    {
        return fonts[currentFont];
    }
}