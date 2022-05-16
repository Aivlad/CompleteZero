using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    // ������ �� ������
    public SwordOfLightAttackSysten solas;
    public WandAttackSystem was;
    public PoisonedThrowingKnivesAttackSystem ptkas;
    public MorgenshternAttackSysten mas;
    // ������, ������� ������������ ������ �� ������
    public int indexScriptActivated;
    public List<GameObject> objectsUnderYourFeet;   // �������� �� �����

    [Space]
    public List<GameObject> doors; // ������ �� �����, ������� ��������� ����� � ����� ����� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //������������ ���, ����� indexScriptActivated
            switch(indexScriptActivated)
            {
                case 0:
                    solas.enabled = true;
                    was.enabled = false;
                    ptkas.enabled = false;
                    mas.enabled = false;
                    break;
                case 1:
                    solas.enabled = false;
                    was.enabled = true;
                    ptkas.enabled = false;
                    mas.enabled = false;
                    break;
                case 2:
                    solas.enabled = false;
                    was.enabled = false;
                    ptkas.enabled = true;
                    mas.enabled = false;
                    break;
                case 3:
                    solas.enabled = false;
                    was.enabled = false;
                    ptkas.enabled = false;
                    mas.enabled = true;
                    break;
            }
            ActivationObjectsUnderYourFeet();
            DeactivationDoors();    // ���������� 1�� ������ �������� ������
            gameObject.SetActive(false);
        }
    }

    // ���������� ������, ������� ��� ���������
    private void ActivationObjectsUnderYourFeet()
    {
        foreach (var obj in objectsUnderYourFeet)
        {
            obj.SetActive(true);
        }
    }

    // ����������� ������
    private void DeactivationDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false);
        }
    }


}
