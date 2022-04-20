using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicScatterAroundWave : MonoBehaviour
{
    public KeyCode action = KeyCode.E;
    [Space]
    public GameObject objectPrefab;             // ������ ������� ��������
    public int countObjects;                    // ���������� ��������� �������� �� ����� �����
    public float scatteringRadiusFirstWave;     // ������ ����� �������� (�� ������� ������� �����)
    [Space]
    public int countWaves;              // ���������� ����
    public float distanceBetweenWaves;  // ��������� ����� �������
    public float delayBetweenWaves;     // �������� ��������� ����

    private Vector3 center;         // ��������
    private float angle;            // ���� ������� ������� ��������
    private float angleRotation;    // ���� �������� ����� ��������� ��������

    [Space]
    public bool isReadyAttack;  // ���������� �����
    public float cooldown;      // �����

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

    // ����������� ������� �� ������� �����
    private Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    // ������� ������ �����
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

    // ����������� ��������
    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }
}
