namespace Game.Waves;

public class Wave(string name, int duration, SpawnGroup[] groups)
{
    public string Name => name;
    public int Duration { get; } = duration;
    public float Timer { get; private set; }

    public List<SpawnRequest> Update(float deltaTime)
    {
        List<SpawnRequest> requests = [];
        Timer += deltaTime;
        foreach (var group in groups)
        {
            if (group.SpawnCount >= group.EnemyCount) { continue; }

            var enemiesToHaveBeenSpawned = (int)Math.Floor(group.EnemyCount * (Timer / Duration));
            requests.Add(new SpawnRequest(
                group.Type, 
                enemiesToHaveBeenSpawned - group.SpawnCount
            ));
            group.SpawnCount = enemiesToHaveBeenSpawned;
        }

        return requests;
    }
}