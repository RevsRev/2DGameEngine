using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

public class RvSceneryHandler
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

    public static RvSceneryHandler factory(Game game, RvSceneryHandlerWrapper w)
    {
        List<RvSceneryWrapper> sceneryWrappers = w.sceneryWrappersVec;
        List<RvScenery> sceneryVec = new List<RvScenery>();
        for (int i=0; i<sceneryWrappers.Count; i++)
        {
            sceneryVec.Add(RvScenery.factory(game, sceneryWrappers[i]));
        }
        return new RvSceneryHandler(sceneryVec);
    }

    public RvSceneryHandlerWrapper createWrapper()
    {
        List<RvSceneryWrapper> sceneryWrappers = new List<RvSceneryWrapper>();
        for (int i=0; i<sceneryVec.Count; i++)
        {
            RvScenery scenery = sceneryVec[i];
            sceneryWrappers.Add(scenery.createWrapper());
        }
        return new RvSceneryHandlerWrapper(sceneryWrappers);
    }

    private void loadScenery(Game game)
    {
        sceneryVec.Add(RvScenery.factory(game, RvContentFiles.SCENRY + "spooky_background", Vector2.Zero, 1200, 1000, SCENE_ID_SPOOKY));
        sceneryVec.Add(RvScenery.factory(game, RvContentFiles.SCENRY + "dark_forest", Vector2.Zero, RvSystem.SCR_WIDTH, RvSystem.SCR_HEIGHT, SCENE_ID_FOREST));
    }

    public void Update(GameTime gameTime)
    {
        for (int i=0; i<sceneryVec.Count; i++)
        {
            sceneryVec[i].Update(gameTime);
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

public class RvSceneryHandlerWrapper
{
    [JsonProperty] public List<RvSceneryWrapper> sceneryWrappersVec = new List<RvSceneryWrapper>();

    public RvSceneryHandlerWrapper(List<RvSceneryWrapper> sceneryWrappersVec)
    {
        this.sceneryWrappersVec = sceneryWrappersVec;
    }
}