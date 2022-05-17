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
                    PlayerPrefs.SetString(KeysPlayerPrefs.WEAPON_KEY_PLAYER_PREFS, "SwordOfLightAttackSysten");
                    break;
                case 1:
                    solas.enabled = false;
                    was.enabled = true;
                    ptkas.enabled = false;
                    mas.enabled = false;
                    PlayerPrefs.SetString(KeysPlayerPrefs.WEAPON_KEY_PLAYER_PREFS, "WandAttackSystem");
                    break;
                case 2:
                    solas.enabled = false;
                    was.enabled = false;
                    ptkas.enabled = true;
                    mas.enabled = false;
                    PlayerPrefs.SetString(KeysPlayerPrefs.WEAPON_KEY_PLAYER_PREFS, "PoisonedThrowingKnivesAttackSystem");
                    break;
                case 3:
                    solas.enabled = false;
                    was.enabled = false;
                    ptkas.enabled = false;
                    mas.enabled = true;
                    PlayerPrefs.SetString(KeysPlayerPrefs.WEAPON_KEY_PLAYER_PREFS, "MorgenshternAttackSysten");
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
