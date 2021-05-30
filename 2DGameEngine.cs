using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class RvGame : Game
{
    private static readonly int GAME_STATE_START_MENU   = 0;
    private static readonly int GAME_STATE_PLAYING      = 1;
    private static readonly int GAME_STATE_PAUSED       = 2;
    private static readonly int GAME_STATE_LOADING      = 3;
    private static readonly int GAME_STATE_EDIT         = 4;

    private GraphicsDeviceManager graphics;

    RvEditor editor;
    private static RvGame instance; //Making this accessible from anywhere because it's getting annoying having to pass into lots of things.

    private int gameState = GAME_STATE_START_MENU;
    private string currentLevel;
    Dictionary<string, RvLevel> levels = new Dictionary<string, RvLevel>();

    //testing
    RvTextField textField;
    RvPhysicalObjectCreator physicalObjectCreator;

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
        InitializeInstance();

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

    private void InitializeInstance()
    {
        instance = this;
    }
    public static RvGame the()
    {
        //this is always initialised right at the start of the program, so no need for lock(padlock) pattern.
        return instance;
    }

    protected override void LoadContent()
    {
        loadLevels();

        //testing
        textField = new RvTextField(new Rectangle(100,100,300,40));
        physicalObjectCreator = new RvPhysicalObjectCreator(new Vector2(400,400));
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
        if (Keyboard.GetState().IsKeyDown(Keys.F3))
        {
            RvDebug.toggleDebugMode();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
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
        RvMouse.the().Update(gameTime);
        RvScreenHandler.the().Update(gameTime);

        if (Keyboard.GetState().IsKeyDown(Keys.F1))
        {
            RvScreenHandler.the().doPopup("RvPhysicalObjectCreator");
        }

        base.Update(gameTime);
    }

    private void updateGameStatePlaying(GameTime gameTime)
    {
        RvLevel level = levels[currentLevel];
        level.update(gameTime);
    }
    private void updateGameStateEdit(GameTime gameTime)
    {
        RvLevel level = levels[currentLevel];
        //level.Update(gameTime, editor);
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

        RvScreenHandler.the().Draw();
        RvMiscDrawableHandler.the().Draw();

        base.Draw(gameTime);
    }

    public void drawGame(GameTime gameTime)
    {
        base.Draw(gameTime);
    }

    public void drawCurrentLevel()
    {
        RvLevel level = levels[currentLevel];

        RvSpriteBatch.the().Begin(SpriteSortMode.BackToFront, null);
        level.Draw();

        if (gameState == GAME_STATE_EDIT)
        {
            //editor.Draw();
        }
        RvSpriteBatch.the().End();
    }

    public void saveCurrentLevel()
    {
        levels[currentLevel].saveLevel();
    }
}