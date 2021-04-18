using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

public class RvAnimation
{
    private Texture2D atlas;
    private int id;

    private int columns;
    private int rows;
    private int currentFrame;
    private int totalFrames;

    private bool recentre;
    private Rectangle imageCentre; //if e.g. a sprite swings it's sword away from it's body, we still want the image drawn to be centred on the body of the sprite.

    private double framesPerSecond = 20; //turn this into a parameter we pass in.
    private double frameTimer = 0.0f;

    public RvAnimation(Texture2D atlas, int rows, int columns, bool recentre, Rectangle imageCentre, int id = 0)
    {
        this.atlas = atlas;
        this.rows = rows;
        this.columns = columns;
        this.id = id;
        this.recentre = recentre;
        this.imageCentre = imageCentre;

        currentFrame = 0;
        totalFrames = rows*columns;
    }

    public void reset()
    {
        currentFrame = 0;
        frameTimer = 0.0f;
    }

    public static RvAnimation factory(Game game, string name, int rows, int columns ,int id, bool recentre, Rectangle imageCentre)
    {
        Texture2D atlas = game.Content.Load<Texture2D>(name);
        return new RvAnimation(atlas, rows, columns, recentre, imageCentre, id);
    }

    public static RvAnimation factory(Game game, string name, int rows, int columns ,int id)
    {
        return factory(game, name, rows, columns, id, false, new Rectangle(0, 0, 1, 1));
    }

    public static RvAnimation factory(Game game, RvAnimationWrapper w)
    {
        return factory(game, w.atlasName, w.rows, w.columns, w.id, w.recentre, w.imageCentre);
    }

    public RvAnimationWrapper createWrapper()
    {
        return new RvAnimationWrapper(atlas.Name, id, columns, rows, currentFrame, totalFrames, recentre, imageCentre);
    }

    public void Update(GameTime gameTime)
    {
        frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (frameTimer > 1/framesPerSecond)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            frameTimer = 0.0f;
        }
    }

    public void Draw(RvSpriteBatch spriteBatch, Rectangle destinationRect, float layer)
    {
        bool doClipping = false; //pass this in/set at a later date.

        int width = atlas.Width/columns;
        int height = atlas.Height/rows;
        int row = (int) (currentFrame/columns);
        int column = currentFrame % columns;

        Rectangle trueDestRect = destinationRect;
        if (recentre)
        {
            float trueDestWidth = 100*destinationRect.Width/(float)imageCentre.Width;
            float trueDestHeight = 100*destinationRect.Height/(float)imageCentre.Height;
            float trueDestX = destinationRect.X - trueDestWidth*imageCentre.X/100;
            float trueDestY = destinationRect.Y - trueDestHeight*imageCentre.Y/100;

            trueDestRect = new Rectangle((int)trueDestX, (int)trueDestY, (int)trueDestWidth, (int)trueDestHeight);
        }

        int clipWidth = width;
        int clipHeight = height;
        if (doClipping)
        {
            clipWidth = MathHelper.Min(width, trueDestRect.Width);
            clipHeight = MathHelper.Min(height, trueDestRect.Height);
        }

        Rectangle sourceRect = new Rectangle(width*column, height*row, clipWidth, clipHeight);

        spriteBatch.Draw(atlas, trueDestRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, layer);
    }

    public int getId()
    {
        return id;
    }
}

//used for saving
public class RvAnimationWrapper
{
    [JsonProperty] public string atlasName;
    [JsonProperty] public int id;

    [JsonProperty] public int columns;
    [JsonProperty] public int rows;
    [JsonProperty] public int currentFrame;
    [JsonProperty] public int totalFrames;

    [JsonProperty] public bool recentre;
    [JsonProperty] public Rectangle imageCentre; //if e.g. a sprite swings it's sword away from it's body, we still want the image drawn to be centred on the body of the sprite.

    public RvAnimationWrapper(string atlasName, int id, int columns, int rows, int currentFrame, int totalFrames, bool recentre, Rectangle imageCentre)
    {
        this.atlasName = atlasName;
        this.id = id;
        this.columns = columns;
        this.rows = rows;
        this.currentFrame = currentFrame;
        this.totalFrames = totalFrames;
        this.recentre = recentre;
        this.imageCentre = imageCentre;
    }
}