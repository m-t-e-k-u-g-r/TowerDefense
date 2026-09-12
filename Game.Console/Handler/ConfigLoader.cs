namespace Game.Console.Handler;

using Core.Entities;
using Core.Entities.Config;
using Core.Entities.Error;
using Core.Logic;
using Newtonsoft.Json;

public class ConfigLoader
{
    public Result<GameConfig> LoadConfig(string path)
    {
        var jsonString = File.ReadAllText(path);
        var deserialized = JsonConvert.DeserializeObject<GameConfig>(jsonString);
        if (deserialized == null) throw new NullReferenceException();
        var valid = ConfigValidator.CheckConfiguration(deserialized);
        return valid 
            ? new Result<GameConfig>(deserialized, null)
            : new Result<GameConfig>(null, new GameError(GameErrorCode.InvalidConfiguration, "Invalid configuration"));
    }
}