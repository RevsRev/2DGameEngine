using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;

public class RvLevel : RvAbstractWrappable, RvUpdatableI
{
    private RvGameObjectHandler objectHandler;

    private string levelName;

    public RvLevel(string levelName, RvGameObjectHandler objectHandler)
    {
        this.levelName = levelName;
        this.objectHandler = objectHandler;
    }

    

    public static RvLevel loadLevel(string levelName, Game game)
    {
        string levelAsJson = "";
        try
        {
            levelAsJson = File.ReadAllText(RvContentFiles.LEVELS + levelName + ".txt");
        }
        catch(FileNotFoundException e)
        {
            return new RvLevel(levelName, RvGameObjectHandler.factory());
        }
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        RvLevelWrapper w =  JsonConvert.DeserializeObject<RvLevelWrapper>(levelAsJson, settings);
        return w.unWrap();
    }

    public override RvLevelWrapper wrap()
    {
        RvGameObjectHandlerWrapper objectHandlerWrapper = objectHandler.wrap();
        return new RvLevelWrapper(levelName, objectHandlerWrapper);
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
    }
    //06/06/2021 moving all this to a drawable object handler.
    // public void Draw()
    // {
    //     objectHandler.Draw();     
    // }

    public void setObjectHandler(RvGameObjectHandler objectHandler)
    {
        this.objectHandler = objectHandler;
    }
    public RvGameObjectHandler GetObjectHandler()
    {
        return objectHandler;
    }

    public void addToObjectHandler(RvAbstractGameObject obj)
    {
        objectHandler.addObject(obj);
    }

    public void removeFromObjectHandler(Vector2 position)
    {
        objectHandler.removeObjectAt(position);
    }
}

public class RvLevelWrapper : RvAbstractWrapper
{
    [JsonProperty] public RvGameObjectHandlerWrapper objectHandlerWrapper;
    [JsonProperty] public string levelName;

    public RvLevelWrapper(string levelName, RvGameObjectHandlerWrapper objectHandlerWrapper)
    {
        this.objectHandlerWrapper = objectHandlerWrapper;
        this.levelName = levelName;
    }

    public override RvLevel unWrap()
    {
        return new RvLevel(levelName, objectHandlerWrapper.unWrap());
    }
}