using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnEnemies : MonoBehaviour
{
    public bool isSpawned;  // флаг: за сессию всего один раз спавняться враги
    public List<Transform> spawnPositions;
    public List<GameObject> prefabsEnemies;
    [Space]
    public int generationCount;
    public int generationCountMin;
    public int generationCountMax;
    [Space]
    public List<GameObject> spawnedEnemies;
    public bool enemiesAlive;


    private void Start()
    {
        isSpawned = true;
        enemiesAlive = true;
    }

    public void RemoveFromLiveList(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        enemiesAlive = spawnedEnemies.Count != 0;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSpawned)
            {
                isSpawned = false;
                RandomGenerationCount();
                SimpleGenerationEnemies();
            }
        }
    }

    private void RandomGenerationCount()
    {
        generationCount = Random.Range(generationCountMin, generationCountMax);
        generationCount %= spawnPositions.Count;    //количество генерации не может быть > количества доступных позиций
    }

    private void SimpleGenerationEnemies()
    {
        for (int i = 0; i < generationCount; i++)
        {
            // выбираем незанятую позицию
            var tm = spawnPositions[Random.Range(0, spawnPositions.Count)];
            spawnPositions.Remove(tm);

            // выбираем тип врага
            var type = prefabsEnemies[Random.Range(0, prefabsEnemies.Count)];

            // создам
            var newEnemy = Instantiate(type, tm.position, Quaternion.identity);
            newEnemy.GetComponent<SpawnEnemyController>().roomSpawnEnemies = this;
            spawnedEnemies.Add(newEnemy);
        }
    }

}
