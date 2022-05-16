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
            DeactivationDoors();    // достаточно 1го вызова поднятия оружия
            gameObject.SetActive(false);
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


}
