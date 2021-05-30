using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;

public class RvLevel : RvAbstractWrappable, RvUpdatableI
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

    

    public static RvLevel loadLevel(string levelName, Game game)
    {
        string levelAsJson = File.ReadAllText(RvContentFiles.LEVELS + levelName + ".txt");
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        RvLevelWrapper w =  JsonConvert.DeserializeObject<RvLevelWrapper>(levelAsJson, settings);

        return w.unWrap();
    }

    public override RvLevelWrapper wrap()
    {
        RvObjectHandlerWrapper objectHandlerWrapper = objectHandler.wrap();
        RvSceneryHandlerWrapper sceneryHandlerWrapper = sceneryHandler.wrap();
        return new RvLevelWrapper(levelName, objectHandlerWrapper, sceneryHandlerWrapper);
    }

    public async void saveLevel()
    {
        RvLevelWrapper wrapperToSave = wrap();
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        string output = JsonConvert.SerializeObject(wrapperToSave, settings);

        await File.WriteAllTextAsync(RvContentFiles.LEVELS + levelName + ".txt", output);
    }

    public void update(GameTime gameTime)
    {
        objectHandler.update(gameTime);
        sceneryHandler.update(gameTime);
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
    public RvObjectHandler GetObjectHandler()
    {
        return objectHandler;
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

public class RvLevelWrapper : RvAbstractWrapper
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

    public override RvLevel unWrap()
    {
        return new RvLevel(levelName, objectHandlerWrapper.unWrap(), sceneryHandlerWrapper.unWrap());
    }
}