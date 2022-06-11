using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemiesSpeedIncreaseSpecification : MonoBehaviour
{

    /// <summary>
    /// Рассчитать процент от valueOneHundredPercent
    /// </summary>
    /// <param name="valueOneHundredPercent">значение в 100%</param>
    /// <param name="percentageCalculated">сколько % найти</param>
    /// <returns></returns>
    private float СalculatePercentage(float valueOneHundredPercent, float percentageCalculated)
    {
        return (valueOneHundredPercent * percentageCalculated) / 100;
    }


    public IEnumerator IncreaseSpeed(SceneManagerNPCState stateControllerNPC, RoomSpawnEnemies currentRoomsSpawnEnemies, PlayerMovement playerMovement, float percent)
    {
        yield return new WaitForSeconds(0.3f);

        float speedPlayer = playerMovement.speed ;
        float threshold = speedPlayer - СalculatePercentage(speedPlayer, 8f);   // верхний порог увеличения скорости врага
        var enemies = currentRoomsSpawnEnemies.spawnedEnemies;
        //Debug.Log($"Вызов увеличения для {enemies.Count} на {percent}");
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                //Debug.Log($"- {enemy.name}");
                var currentMovementEnmeyController = enemy.GetComponent<MovementTowardsGoal>();
                var typeEnemy = currentMovementEnmeyController.type;
                var baseSpeed = stateControllerNPC.GetSpeed(typeEnemy);
                var increaseValue = СalculatePercentage(baseSpeed, percent);
                if (currentMovementEnmeyController.speed + increaseValue < threshold)
                {
                    currentMovementEnmeyController.speed = baseSpeed + increaseValue;
                }
            }
        }
    }
}
