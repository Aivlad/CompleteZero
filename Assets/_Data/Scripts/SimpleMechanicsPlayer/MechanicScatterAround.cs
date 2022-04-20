using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicScatterAround : MonoBehaviour
{
    public KeyCode action = KeyCode.E;
    [Space]
    public GameObject objectPrefab; // ������ ������� ��������
    public int countObjects;        // ���������� ��������� ��������
    public float scatteringRadius;  // ������ ����� ��������

    private void Update()
    {
        if (Input.GetKeyDown(action))
        {
            Vector3 center = transform.position;
            float angle = Random.Range(0, 360);
            float angleRotation = 360 / countObjects;
            for (int i = 0; i < countObjects; i++)
            {
                Vector3 pos = RandomCircle(center, scatteringRadius, angle);
                Instantiate(objectPrefab, pos, Quaternion.identity);

                angle += angleRotation;
            }
        }
    }

    // ��������� ������� �� ������� �����
    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = Random.value * 360; 
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
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
}
