using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicScatterAroundWave : MonoBehaviour
{
    public KeyCode action = KeyCode.E;
    [Space]
    public GameObject objectPrefab;             // префаб объекта разброса
    public int countObjects;                    // количество предметов разброса на одном кругу
    public float scatteringRadiusFirstWave;     // радиус круга разброса (до первого контура круга)
    [Space]
    public int countWaves;              // количество волн
    public float distanceBetweenWaves;  // дистанция между волнами
    public float delayBetweenWaves;     // задрежка появления волн

    private Vector3 center;         // эпицентр
    private float angle;            // угол первого объекта разброса
    private float angleRotation;    // угол поворота между объектами разброса

    [Space]
    public bool isReadyAttack;  // готовность атаки
    public float cooldown;      // откат

    private void Start()
    {
        isReadyAttack = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(action) && isReadyAttack)
        {
            isReadyAttack = false;
            angle = Random.Range(0, 360);
            angleRotation = 360 / countObjects;
            center = transform.position;
            StartCoroutine(RunWave(1, scatteringRadiusFirstWave));
            StartCoroutine(Recharge());
        }
    }

    // равномерный разброс по контуру круга
    private Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    // разброс одного круга
    private IEnumerator RunWave(int numberWave, float scatteringRadiusCurrentWave)
    {
        if (numberWave <= countWaves)
        {
            float localAngle = angle;
            for (int j = 0; j < countObjects; j++)
            {
                Vector3 pos = RandomCircle(center, scatteringRadiusCurrentWave, localAngle);
                Instantiate(objectPrefab, pos, Quaternion.identity);

                localAngle += angleRotation;
            }
            yield return new WaitForSeconds(delayBetweenWaves);
            StartCoroutine(RunWave(numberWave + 1, scatteringRadiusCurrentWave + distanceBetweenWaves));
        }        
    }

    // перезарядка механики
    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }
}
