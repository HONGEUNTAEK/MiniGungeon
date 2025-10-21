using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public float spawnTerm = 5;
    public float fasterEverySpawn = 0.05f;
    public float minSpawnTerm = 1;
    float timeAfterLasgSpawn = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAfterLasgSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterLasgSpawn += Time.deltaTime;

        if (timeAfterLasgSpawn > spawnTerm)
        {
            timeAfterLasgSpawn -= spawnTerm;

            SpawnEnemy();

            spawnTerm -= fasterEverySpawn;
            if (spawnTerm < minSpawnTerm)
            {
                spawnTerm = minSpawnTerm;
            }
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-9f, 9f);
        float y = Random.Range(-4.5f, 4.5f);

        GameObject obj = GetComponent<ObjectPool>().Get();
        obj.transform.position = new Vector3(x, y, 0);
        obj.GetComponent<EnemyController>().Spawn(player);
    }
}
