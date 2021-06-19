using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class RvEditor : RvPopupMenuListenerI
{
    //The game
    RvGame game;

    public RvEditor(RvGame game)
    {
        this.game = game;
        RvMouse.the().addListener(this);
    }

    public void doClick(RvMouseEvent e)
    {
        RvPopupMenuListenerI.onClick(e, this);
    }
    public bool respectsClickableRegion()
    {
        return false;
    }

    public RvPopupMenu buildPopupMenu()
    {
        RvPopupMenu retval = new RvPopupMenu();
        retval.addPopupMenuItem("Save");
        //retval.addPopupMenuItem("Remove");
        retval.addPopupMenuItem("Add Object");
        retval.addPopupMenuItem("Knight");
        // retval.addPopupMenuItem("fEye");
        return retval;
    }

    public bool okToBuildMenu(int x, int y)
    {
        RvGameObjectHandler objHandler = game.getCurrentLevel().GetObjectHandler();
        return (objHandler.getObjectsAt(new Vector2(x,y)).Count == 0);
    }

    public void performPopupMenuAction(String actionStr)
    {
        doAction(actionStr);
    }

    public static Vector2 mapScreenCoordsToGameCoords(MouseState mouseState)
    {
        return mapScreenCoordsToGameCoords(new Vector2(mouseState.X, mouseState.Y));
    }
    public static Vector2 mapScreenCoordsToGameCoords(Vector2 position)
    {
        Vector2 topLeftGameCoords = RvSpriteBatch.the().getTopLeftGameCoords();
        return new Vector2(position.X + topLeftGameCoords.X, position.Y + topLeftGameCoords.Y);
    }

    private void doAction(string actionCommand)
    {
        if (actionCommand.Equals("Save"))
        {
            doSave();
        }
        else if (actionCommand.Equals("Add Object"))
        {
            RvScreenHandler.the().doPopup("RvSObjectCreator");
        }
        //for testing purposes.
        else if (actionCommand.Equals("Knight"))
        {
            addObject("Knight", Vector2.Zero);
        }
        // if (actionCommand.Equals("Remove"))
        // {
        //     //doRemove();
        // }
        // else if (actionCommand.Equals("Knight"))
        // {
        //     //addObject(actionCommand, editorMenu.getPosition());
        // }
        // else if (actionCommand.Equals("fEye"))
        // {
        //     //addObject(actionCommand, editorMenu.getPosition());
        // }
    }

    private void doSave()
    {
        game.saveCurrentLevel();
    }

    // private void doRemove()
    // {
    //     Vector2 position = editorMenu.getPosition();
    //     Vector2 point = mapScreenCoordsToGameCoords(position);
    //     game.getCurrentLevel().removeFromObjectHandler(point);
    // }

    private void addObject(string objectName, Vector2 position)
    {
        Vector2 physicalPosition = mapScreenCoordsToGameCoords(position);
        if (objectName.Equals("Knight"))
        {
            game.getCurrentLevel().addToObjectHandler(new RvKnight(physicalPosition));
        }
        else if (objectName.Equals("fEye"))
        {
            game.getCurrentLevel().addToObjectHandler(new RvFlyingEye(physicalPosition));
        }
    }
}