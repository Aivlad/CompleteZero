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

    public void IncreaseSpeed(SceneManagerNPCState stateControllerNPC, RoomSpawnEnemies currentRoomsSpawnEnemies, PlayerMovement playerMovement, PlayerCharacteristics playerCharacteristics)
    {
        float speedPlayer = playerMovement.speed ;
        float threshold = speedPlayer - СalculatePercentage(speedPlayer, 8f);   // верхний порог увеличения скорости врага
        var enemies = currentRoomsSpawnEnemies.spawnedEnemies;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                var currentMovementEnmeyController = enemy.GetComponent<MovementTowardsGoal>();
                var typeEnemy = currentMovementEnmeyController.type;
                var baseSpeed = stateControllerNPC.GetSpeed(typeEnemy);
                var increaseValue = СalculatePercentage(baseSpeed, 3f);
                if (currentMovementEnmeyController.speed + increaseValue < threshold)
                {
                    currentMovementEnmeyController.speed = baseSpeed + increaseValue;
                }
            }
        }
    }
}
