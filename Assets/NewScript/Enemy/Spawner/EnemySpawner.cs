using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI killCountText;
    public int enemyCount = 5;

    private int totalEnemyCount = 0;
    private int killCount = 0;
    public float spawnRadius = 5.0f;
    public float spawnHeight = 1.0f;
    public float spawnInterval = 5.0f;

    void Start()
    {
        SpawnEnemiesPeriodically().Forget();
    }

    async UniTaskVoid SpawnEnemiesPeriodically()
    {
        while (true)
        {
            SpawnEnemies();
            await UniTask.Delay((int)(spawnInterval * 1000));
        }
    }

    void Update()
    {
        UpdateEnemyCountText();
        UpdateKillCountText();
        CheckEnemyCount();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GenerateRandomPositionInRadius();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            totalEnemyCount++;
        }
    }

    Vector3 GenerateRandomPositionInRadius()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position;
        randomDirection.y = spawnHeight;

        return randomDirection;
    }

    void UpdateEnemyCountText()
    {
        enemyCountText.text = totalEnemyCount.ToString();
    }

    void UpdateKillCountText()
    {
        killCountText.text = killCount.ToString();
    }

    void CheckEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < totalEnemyCount)
        {
            killCount += totalEnemyCount - enemies.Length;
            totalEnemyCount = enemies.Length;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
