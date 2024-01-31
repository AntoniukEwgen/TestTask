using UnityEngine;
using TMPro;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnHeight = 1.0f;

    public TextMeshProUGUI spawnCountText;
    public TextMeshProUGUI killCountText;

    private int spawnCount = 0;
    private int killCount = 0;

    public ParticleSystem clickEffect;


    void Update()
    {
        CheckMouseInputAndSpawnObject();
        UpdateSpawnCountText();
        UpdateKillCountText();
        CheckHeroCount();
    }

    void CheckMouseInputAndSpawnObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnObjectAtMousePosition();
        }
    }

    void SpawnObjectAtMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 spawnPosition = hit.point + new Vector3(0, spawnHeight, 0);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnCount++;

            if (clickEffect != null)
            { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
        }
    }

    void UpdateSpawnCountText()
    {
        spawnCountText.text = spawnCount.ToString();
    }

    void UpdateKillCountText()
    {
        killCountText.text = killCount.ToString();
    }

    void CheckHeroCount()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        if (heroes.Length < spawnCount)
        {
            killCount += spawnCount - heroes.Length;
            spawnCount = heroes.Length;
        }
    }
}
