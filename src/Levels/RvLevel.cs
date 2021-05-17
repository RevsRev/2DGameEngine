using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;

public class RvLevel
{
    private RvObjectHandler objectHandler;
    private RvSceneryHandler sceneryHandler;

    private string levelName;

    public RvLevel(string levelName, RvObjectHandler objectHandler, RvSceneryHandler sceneryHandler)
    {
        this.levelName = levelName;
        this.objectHandler = objectHandler;
        this.sceneryHandler = sceneryHandler;
    }

    public static RvLevel factory(Game game, RvLevelWrapper w)
    {
        return new RvLevel(w.levelName, RvObjectHandler.factory(game, w.objectHandlerWrapper), RvSceneryHandler.factory(game, w.sceneryHandlerWrapper));
    }

    public static RvLevel loadLevel(string levelName, Game game)
    {
        string levelAsJson = File.ReadAllText(RvContentFiles.LEVELS + levelName + ".txt");
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        RvLevelWrapper w =  JsonConvert.DeserializeObject<RvLevelWrapper>(levelAsJson, settings);

        return factory(game, w);
    }

    public RvLevelWrapper createLevelWrapper()
    {
        RvObjectHandlerWrapper objectHandlerWrapper = objectHandler.createWrapper();
        RvSceneryHandlerWrapper sceneryHandlerWrapper = sceneryHandler.createWrapper();

        return new RvLevelWrapper(levelName, objectHandlerWrapper, sceneryHandlerWrapper);
    }

    public async void saveLevel()
    {
        RvLevelWrapper wrapperToSave = createLevelWrapper();
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        string output = JsonConvert.SerializeObject(wrapperToSave, settings);

        await File.WriteAllTextAsync(RvContentFiles.LEVELS + levelName + ".txt", output);
    }

    public void Update(GameTime gameTime)
    {
        objectHandler.Update(gameTime);
        sceneryHandler.Update(gameTime);
    }
    public void Update(GameTime gameTime, RvEditor editor)
    {
        editor.bindObject(objectHandler);
        editor.Update(gameTime);
    }
    public void Draw()
    {
        objectHandler.Draw();
        sceneryHandler.Draw();      
    }

    public void setObjectHandler(RvObjectHandler objectHandler)
    {
        this.objectHandler = objectHandler;
    }
    public void setSceneryHandler(RvSceneryHandler sceneryHandler)
    {
        this.sceneryHandler = sceneryHandler;
    }

    public void addToObjectHandler(RvPhysicalObject physicalObject)
    {
        objectHandler.addObject(physicalObject);
    }

    public void removeFromObjectHandler(Vector2 position)
    {
        objectHandler.removeObjectAt(position);
    }
}

public struct RvLevelWrapper
{
    [JsonProperty] public RvObjectHandlerWrapper objectHandlerWrapper;
    [JsonProperty] public RvSceneryHandlerWrapper sceneryHandlerWrapper;

    [JsonProperty] public string levelName;

    public RvLevelWrapper(string levelName, RvObjectHandlerWrapper objectHandlerWrapper, RvSceneryHandlerWrapper sceneryHandlerWrapper)
    {
        this.objectHandlerWrapper = objectHandlerWrapper;
        this.sceneryHandlerWrapper = sceneryHandlerWrapper;
        this.levelName = levelName;
    }
}