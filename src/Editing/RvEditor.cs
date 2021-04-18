using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class RvEditor : IObserver<string>
{
    //The game
    RvGame game;

    //variables used for clicking and dragging objects.
    private RvPhysicalObject boundObject = null;
    int xOffset = 0;
    int yOffset = 0;

    private RvMenu editorMenu;

    public RvEditor(RvGame game)
    {
        this.game = game;
        initEditorMenu();
    }

    private void initEditorMenu()
    {
        editorMenu = new RvMenu();
        editorMenu.addButton("Save");
        editorMenu.addButton("Remove");
        editorMenu.addButton("Knight");
        editorMenu.addButton("fEye");
        editorMenu.Subscribe(this);
    }

    public void bindObject(RvObjectHandler objHandler, RvSpriteBatch spriteBatch)
    {
        MouseState mouseState = Mouse.GetState();

        if (bound())
        {
            if ((mouseState.LeftButton != ButtonState.Pressed))
            {
                unbind();
            }
        }
        else
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 point = mapScreenCoordsToGameCoords(mouseState, spriteBatch);
                List<RvPhysicalObject> objects = objHandler.getObjectsAt(point);

                //for now, we'll just bind the first object
                if (objects.Count > 0)
                {
                    bind(objects[0], point);
                }
            }
        }
    }

    public void Update(GameTime gameTime, RvSpriteBatch spriteBatch)
    {
        if (boundObject != null)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePhysicalCoords = mapScreenCoordsToGameCoords(mouseState, spriteBatch);
            boundObject.setPositionAndHitbox(new Vector2(mousePhysicalCoords.X - xOffset, mousePhysicalCoords.Y - yOffset));
        }
        editorMenu.Update(gameTime);
    }

    public void Draw(RvSpriteBatch spriteBatch)
    {
        editorMenu.Draw(spriteBatch);
    }

    private Vector2 mapScreenCoordsToGameCoords(MouseState mouseState, RvSpriteBatch spriteBatch)
    {
        return mapScreenCoordsToGameCoords(new Vector2(mouseState.X, mouseState.Y), spriteBatch);
    }
    private Vector2 mapScreenCoordsToGameCoords(Vector2 position, RvSpriteBatch spriteBatch)
    {
        Vector2 topLeftGameCoords = spriteBatch.getTopLeftGameCoords();
        return new Vector2(position.X + topLeftGameCoords.X, position.Y + topLeftGameCoords.Y);
    }

    private bool bound()
    {
        return boundObject != null;
    }
    private void bind(RvPhysicalObject physicalObject, Vector2 physicalMouseCoords)
    {
        xOffset = (int)(physicalMouseCoords.X - physicalObject.getPosition().X);
        yOffset = (int)(physicalMouseCoords.Y - physicalObject.getPosition().Y);
        boundObject = physicalObject;
    }
    private void unbind()
    {
        boundObject = null;
        xOffset = 0;
        yOffset = 0;
    }

    //fire the action command all the way to the object we add the menu to (at the moment, this is just the editor!)
    public virtual void OnNext(String actionCommand)
    {
        doAction(actionCommand);
    }

    public virtual void OnError(Exception exception)
    {
        //no implementation (yet)
    }
    public virtual void OnCompleted()
    {
        //also unimplemented!
    }

    private void doAction(string actionCommand)
    {
        if (actionCommand.Equals("Save"))
        {
            doSave();
        }
        if (actionCommand.Equals("Remove"))
        {
            doRemove();
        }
        else if (actionCommand.Equals("Knight"))
        {
            addObject(actionCommand, editorMenu.getPosition());
        }
        else if (actionCommand.Equals("fEye"))
        {
            addObject(actionCommand, editorMenu.getPosition());
        }
    }

    private void doSave()
    {
        game.saveCurrentLevel();
    }

    private void doRemove()
    {
        RvSpriteBatch spriteBatch = game.getSpriteBatch();
        Vector2 position = editorMenu.getPosition();
        Vector2 point = mapScreenCoordsToGameCoords(position, spriteBatch);
        game.getCurrentLevel().removeFromObjectHandler(point);
    }

    private void addObject(string objectName, Vector2 position)
    {
        Vector2 physicalPosition = mapScreenCoordsToGameCoords(position, game.getSpriteBatch());
        if (objectName.Equals("Knight"))
        {
            game.getCurrentLevel().addToObjectHandler(new RvKnight(game, physicalPosition, Vector2.Zero));
        }
        else if (objectName.Equals("fEye"))
        {
            game.getCurrentLevel().addToObjectHandler(new RvFlyingEye(game, physicalPosition, Vector2.Zero));
        }
    }
}