using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemiesSpeedIncreaseSpecification : MonoBehaviour
{

    /// <summary>
    /// ���������� ������� �� valueOneHundredPercent
    /// </summary>
    /// <param name="valueOneHundredPercent">�������� � 100%</param>
    /// <param name="percentageCalculated">������� % �����</param>
    /// <returns></returns>
    private float �alculatePercentage(float valueOneHundredPercent, float percentageCalculated)
    {
        return (valueOneHundredPercent * percentageCalculated) / 100;
    }

    public void IncreaseSpeed(SceneManagerNPCState stateControllerNPC, RoomSpawnEnemies currentRoomsSpawnEnemies, PlayerMovement playerMovement, PlayerCharacteristics playerCharacteristics)
    {
        float speedPlayer = playerMovement.speed ;
        float threshold = speedPlayer - �alculatePercentage(speedPlayer, 8f);   // ������� ����� ���������� �������� �����
        var enemies = currentRoomsSpawnEnemies.spawnedEnemies;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                var currentMovementEnmeyController = enemy.GetComponent<MovementTowardsGoal>();
                var typeEnemy = currentMovementEnmeyController.type;
                var baseSpeed = stateControllerNPC.GetSpeed(typeEnemy);
                var increaseValue = �alculatePercentage(baseSpeed, 3f);
                if (currentMovementEnmeyController.speed + increaseValue < threshold)
                {
                    currentMovementEnmeyController.speed = baseSpeed + increaseValue;
                }
            }
        }
    }
}
