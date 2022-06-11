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


    public IEnumerator IncreaseSpeed(SceneManagerNPCState stateControllerNPC, RoomSpawnEnemies currentRoomsSpawnEnemies, PlayerMovement playerMovement, float percent)
    {
        yield return new WaitForSeconds(0.3f);

        float speedPlayer = playerMovement.speed ;
        float threshold = speedPlayer - �alculatePercentage(speedPlayer, 8f);   // ������� ����� ���������� �������� �����
        var enemies = currentRoomsSpawnEnemies.spawnedEnemies;
        //Debug.Log($"����� ���������� ��� {enemies.Count} �� {percent}");
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                //Debug.Log($"- {enemy.name}");
                var currentMovementEnmeyController = enemy.GetComponent<MovementTowardsGoal>();
                var typeEnemy = currentMovementEnmeyController.type;
                var baseSpeed = stateControllerNPC.GetSpeed(typeEnemy);
                var increaseValue = �alculatePercentage(baseSpeed, percent);
                if (currentMovementEnmeyController.speed + increaseValue < threshold)
                {
                    currentMovementEnmeyController.speed = baseSpeed + increaseValue;
                }
            }
        }
    }
}
