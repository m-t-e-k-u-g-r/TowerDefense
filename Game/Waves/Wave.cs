namespace Game.Waves;

public class Wave
{
    public int duration { get; }
    public float timer { get; private set; }
    private SpawnGroup[] Groups;

    public Wave(int duration, SpawnGroup[] groups)
    {
        this.duration = duration;
        timer = 0;
        Groups = groups;
    }

    public List<SpawnRequest> Update(float deltaTime)
    {
        List<SpawnRequest> requests = [];
        timer += deltaTime;
        foreach (var group in Groups)
        {
            if (group.spawnCount >= group.enemyCount) { continue; }

            int enemiesToHaveBeenSpawned = (int)Math.Floor(group.enemyCount * (timer / duration));
            requests.Add(new SpawnRequest(
                group.type, 
                enemiesToHaveBeenSpawned - group.spawnCount
            ));
            group.spawnCount = enemiesToHaveBeenSpawned;
        }

        return requests;
    }
}