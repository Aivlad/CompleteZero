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

    [Header("Balance data")]
    private SaveDataToPlainTextFile balanceManager;
    private bool isReportSent;
    public float pastTense;
    public string totalText;


    private void Start()
    {
        isSpawned = true;
        enemiesAlive = true;

        //balance
        var balanceManagerSource = GameObject.FindGameObjectWithTag("BalanceManager");
        if (balanceManagerSource != null)
        {
            balanceManager = balanceManagerSource.GetComponent<SaveDataToPlainTextFile>();
        }
        else
        {
            //Debug.LogWarning("Balance manager = null");
        }
        isReportSent = true;    // true - чтобы раньше времени не отправить
    }

    private void Update()
    {
        //balance
        if (!isReportSent && balanceManager != null)
        {
            pastTense += Time.deltaTime;

            if (spawnedEnemies.Count == 0)
            {
                isReportSent = true;
                string text = $"CombatDuration (from the last created enemy to the last destroyed enemy):\t{pastTense}";
                //balanceManager.RoomSaveText(text);
                totalText += text + "\n";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //balance
            if (balanceManager != null)
            {
                collision.GetComponent<PlayerCharacteristics>().ResetToatalDamageAndStrokes();
                balanceManager.RoomSaveText("[Entered another room]");
                totalText = "";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //balance
            if (balanceManager != null)
            {
                var playerCharacteristics = collision.GetComponent<PlayerCharacteristics>();
                var damage = playerCharacteristics.GetTotalDamage();
                var count = playerCharacteristics.GetTotalStrokes();
                totalText += $"DamageTaken:\t\t\t\t\t\t\t\t\t{damage}\nDamageTicks:\t\t\t\t\t\t\t\t\t{count}\n";

                var playerMovement = collision.GetComponent<PlayerMovement>();
                var actionsPerRoom = playerMovement.GetActionsPerRoomAndZeroing();
                totalText += $"ActionsPerRoom (old name: ActionsPerMinute):\t\t\t\t\t{actionsPerRoom}\n";

                var playerDamageController = collision.GetComponent<PlayerDamageController>();
                var outgoingDamage = playerDamageController.GetOutingDamageAndZeroing();
                totalText += $"OutgoingDamage (net damage):\t\t\t\t\t\t\t{outgoingDamage}\n";

                if (pastTense != 0)
                    totalText += $"DamagePerSecond (OutgoingDamage/CombatDuration*60):\t\t\t\t{outgoingDamage/pastTense*60}\n";

                balanceManager.RoomSaveText(totalText);
            }
        }
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
    public void RemoveFromLiveList(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        enemiesAlive = spawnedEnemies.Count != 0;
    }

    private void RandomGenerationCount()
    {
        generationCount = Random.Range(generationCountMin, generationCountMax);
        generationCount %= spawnPositions.Count;    //количество генерации не может быть > количества доступных позиций
    }

    private void SimpleGenerationEnemies()
    {
        //balance
        Dictionary<string, int> enemies = new Dictionary<string, int>();
        float totalEnemyHealth = 0f;


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

            //balance
            string key = type.name;
            if (enemies.ContainsKey(key))
            {
                enemies[key] = enemies[key] + 1;
            }
            else
            {
                enemies.Add(key, 1);
            }
            totalEnemyHealth += type.GetComponent<ObjectCharacteristics>().GetHealthTotal();
        }

        //balance
        if (balanceManager != null)
        {
            string text = "";
            text += $"Total NPC: {generationCount}:\n";
            foreach (var enemy in enemies)
            {
                text += $"- Type: {enemy.Key} \t Count: {enemy.Value}\n";
            }
            text += $"TotalEnemyHealth:\t\t\t\t\t\t\t\t{totalEnemyHealth}";
            //balanceManager.RoomSaveText(text);
            totalText += text + "\n";
            
            isReportSent = false;   // только сейчас даем отмашку на разрешение замера
            pastTense = 0f;
        }
    }

}
