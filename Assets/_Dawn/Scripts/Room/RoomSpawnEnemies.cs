using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnEnemies : MonoBehaviour
{
    public bool isSpawned;  // ����: �� ������ ����� ���� ��� ���������� �����
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
        generationCount %= spawnPositions.Count;    //���������� ��������� �� ����� ���� > ���������� ��������� �������
    }

    private void SimpleGenerationEnemies()
    {
        for (int i = 0; i < generationCount; i++)
        {
            // �������� ��������� �������
            var tm = spawnPositions[Random.Range(0, spawnPositions.Count)];
            spawnPositions.Remove(tm);

            // �������� ��� �����
            var type = prefabsEnemies[Random.Range(0, prefabsEnemies.Count)];

            // ������
            var newEnemy = Instantiate(type, tm.position, Quaternion.identity);
            newEnemy.GetComponent<SpawnEnemyController>().roomSpawnEnemies = this;
            spawnedEnemies.Add(newEnemy);
        }
    }

}
