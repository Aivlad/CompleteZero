using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    // ссылки на оружие
    public SwordOfLightAttackSysten solas;
    public WandAttackSystem was;
    public PoisonedThrowingKnivesAttackSystem ptkas;
    public MorgenshternAttackSysten mas;
    // индекс, который соответсвует ссылке на оружие
    public int indexScriptActivated;
    public List<GameObject> objectsUnderYourFeet;   // предметы со сцены

    [Space]
    public List<GameObject> doors; // ссылка на дверь, которая откроется когда в руках будет оружие

    [Space]
    public List<GameObject> bodyParts;  // части тела, которые надо активировать
    public Transform leftHand;
    public Transform rightHand;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //деактивируем все, кроме indexScriptActivated
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
            DeactivationDoors();    // достаточно 1го вызова поднятия оружия
            gameObject.SetActive(false);

            // деактивация ненужного и активация нужного
            DeactivationСontentsInArms();
            ActivationBodyParts();
        }
    }

    // активируем оружие, которое уже поднимали
    private void ActivationObjectsUnderYourFeet()
    {
        foreach (var obj in objectsUnderYourFeet)
        {
            obj.SetActive(true);
        }
    }

    // деактивация дверей
    private void DeactivationDoors()
    {
        foreach (var door in doors)
        {
            door.SetActive(false);
        }
    }

    /// <summary>
    ///  Активация частей тела (объектов из bodyParts)
    /// </summary>
    private void ActivationBodyParts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].SetActive(true);
        }
    }

    /// <summary>
    ///  Деактивация дочерних объектов в левой и правой руках
    /// </summary>
    private void DeactivationСontentsInArms()
    {
        foreach (Transform obj in leftHand)
        {
            obj.gameObject.SetActive(false);
        }
        foreach (Transform obj in rightHand)
        {
            obj.gameObject.SetActive(false);
        }

    }


}
