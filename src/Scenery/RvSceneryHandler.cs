using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

public class RvSceneryHandler : RvAbstractWrappable, RvUpdatableI
{
    public static readonly int SCENE_ID_SPOOKY = 0;
    public static readonly int SCENE_ID_FOREST = 1;

    private List<RvScenery> sceneryVec = new List<RvScenery>();

    public RvSceneryHandler(Game game)
    {
        loadScenery(game);
    }
    public RvSceneryHandler(List<RvScenery> sceneryVec)
    {
        this.sceneryVec = sceneryVec;
    }

    public override RvSceneryHandlerWrapper wrap()
    {
        List<RvSceneryWrapper> sceneryWrappers = new List<RvSceneryWrapper>();
        for (int i=0; i<sceneryVec.Count; i++)
        {
            RvScenery scenery = sceneryVec[i];
            sceneryWrappers.Add(scenery.wrap());
        }
        return new RvSceneryHandlerWrapper(sceneryWrappers);
    }

    private void loadScenery(Game game)
    {
        sceneryVec.Add(RvScenery.factory(game, RvContentFiles.SCENRY + "spooky_background", Vector2.Zero, 1200, 1000, SCENE_ID_SPOOKY));
        sceneryVec.Add(RvScenery.factory(game, RvContentFiles.SCENRY + "dark_forest", Vector2.Zero, RvSystem.SCR_WIDTH, RvSystem.SCR_HEIGHT, SCENE_ID_FOREST));
    }

    public void update(GameTime gameTime)
    {
        for (int i=0; i<sceneryVec.Count; i++)
        {
            sceneryVec[i].update(gameTime);
        }
    }
    public void Draw()
    {
        //At the moment, I only want to draw the forest!
        // for (int i=0; i<sceneryVec.Count; i++)
        // {
        //     sceneryVec[i].Draw(spriteBatch);
        // }
        sceneryVec[SCENE_ID_FOREST].Draw();
    }
}

public class RvSceneryHandlerWrapper : RvAbstractWrapper
{
    [JsonProperty] public List<RvSceneryWrapper> sceneryWrappers = new List<RvSceneryWrapper>();

    public RvSceneryHandlerWrapper(List<RvSceneryWrapper> sceneryWrappersVec)
    {
        this.sceneryWrappers = sceneryWrappersVec;
    }

    public override RvSceneryHandler unWrap()
    {
        List<RvScenery> sceneryVec = new List<RvScenery>();
        for (int i=0; i<sceneryWrappers.Count; i++)
        {
            sceneryVec.Add(sceneryWrappers[i].unWrap());
        }
        return new RvSceneryHandler(sceneryVec);
    }
}