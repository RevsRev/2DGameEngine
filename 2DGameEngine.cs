using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

public class RvGame : Game
{
    private static readonly int GAME_STATE_START_MENU   = 0;
    private static readonly int GAME_STATE_PLAYING      = 1;
    private static readonly int GAME_STATE_PAUSED       = 2;
    private static readonly int GAME_STATE_LOADING      = 3;
    private static readonly int GAME_STATE_EDIT         = 4;

    private GraphicsDeviceManager graphics;
    private RvSpriteBatch spriteBatch;

    RvEditor editor;

    private int gameState = GAME_STATE_START_MENU;
    private string currentLevel;
    Dictionary<string, RvLevel> levels = new Dictionary<string, RvLevel>();

    //testing
    RvTextField textField;

    public RvGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        editor = new RvEditor(this);
    }

    protected override void Initialize()
    {
        initializeGraphics();

        RvDebug.MODE_DEBUG = true;
        RvDebug.PIXEL = Content.Load<Texture2D>(RvContentFiles.DRAWING + "white_pixel");

        base.Initialize();
    }

    private void initializeGraphics()
    {
        graphics.PreferredBackBufferHeight = RvSystem.SCR_HEIGHT;
        graphics.PreferredBackBufferWidth = RvSystem.SCR_WIDTH;
        graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        spriteBatch = new RvSpriteBatch(Content, GraphicsDevice, new Vector2(RvSystem.SCR_WIDTH/2, RvSystem.SCR_HEIGHT/2), RvSystem.SCR_WIDTH, RvSystem.SCR_HEIGHT);
        loadLevels();

        //testing
        textField = new RvTextField(new Rectangle(100,100,300,40));
    }

    private void loadLevels()
    {
        //This will do for now. In future we should also be saving level names to a file/reading all files in a directory.
        List<string> levelNames = new List<string>{"testLevel"};
        foreach (string levelName in levelNames)
        {
            levels[levelName] = RvLevel.loadLevel(levelName, this);
        }
    }
    public RvLevel getCurrentLevel()
    {
        return levels[currentLevel];
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.K))
        {
            RvDebug.toggleDebugMode();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.M))
        {
            if (gameState == GAME_STATE_EDIT)
            {
                gameState = GAME_STATE_PLAYING;
            }
            else if (gameState == GAME_STATE_PLAYING)
            {
                gameState = GAME_STATE_EDIT;
            }
        }

        if (gameState == GAME_STATE_START_MENU)
        {
            //do start menu. For now, we going to immediately set the current level and change the game state.
            //gameState = GAME_STATE_PLAYING;
            gameState = GAME_STATE_EDIT;
            currentLevel = "testLevel";
        }
        else if (gameState == GAME_STATE_PLAYING)
        {
            //do playing
            updateGameStatePlaying(gameTime);
        }
        else if (gameState == GAME_STATE_EDIT)
        {
            updateGameStateEdit(gameTime);
        }
        //etc... for other game states.

        //testing
        RvKeyboard.the().Update(gameTime);

        base.Update(gameTime);
    }

    private void updateGameStatePlaying(GameTime gameTime)
    {
        RvLevel level = levels[currentLevel];
        level.Update(gameTime, spriteBatch);
    }
    private void updateGameStateEdit(GameTime gameTime)
    {
        RvLevel level = levels[currentLevel];
        level.Update(gameTime, spriteBatch, editor);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        if (gameState == GAME_STATE_START_MENU)
        {
            //do drawing for start menu.
        }
        else if (gameState == GAME_STATE_PLAYING
            || gameState == GAME_STATE_EDIT)
        {
            //do playing
            drawCurrentLevel();
        }

        //testing
        spriteBatch.Begin();
        textField.Draw(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }

    public void drawCurrentLevel()
    {
        RvLevel level = levels[currentLevel];

        spriteBatch.Begin(SpriteSortMode.BackToFront, null);
        level.Draw(spriteBatch);

        if (gameState == GAME_STATE_EDIT)
        {
            editor.Draw(spriteBatch);
        }

        spriteBatch.End();
    }

    public void saveCurrentLevel()
    {
        levels[currentLevel].saveLevel();
    }

    public RvSpriteBatch getSpriteBatch()
    {
        return spriteBatch;
    }
}