using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform spawnLander;

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderSpawnPoint()
    {
        return spawnLander.position;
    }
}
