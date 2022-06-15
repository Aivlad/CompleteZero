using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMainManager : MonoBehaviour
{
    public SpawnRoomController spawnRoomController;
    private GameObject player;

    private void Start()
    {
        // сгенерировать карту
        spawnRoomController.LevelGeneration();

        // дать игроку оружие
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            Invoke("HandOverWeapon", 0.3f);
    }

    /// <summary>
    /// Вручить игроку оружие
    /// </summary>
    private void HandOverWeapon()
    {

        SwordOfLightAttackSysten solas = player.GetComponent<SwordOfLightAttackSysten>();
        WandAttackSystem was = player.GetComponent<WandAttackSystem>();
        PoisonedThrowingKnivesAttackSystem ptkas = player.GetComponent<PoisonedThrowingKnivesAttackSystem>();
        MorgenshternAttackSysten mas = player.GetComponent<MorgenshternAttackSysten>();

        PlayerLinksHandLuggage visualHand = player.GetComponent<PlayerLinksHandLuggage>();

        if (!PlayerPrefs.HasKey(KeysPlayerPrefs.SOUND_KEY_PLAYER_PREFS))
        {
            // в теори ветка не достижима т.к. в сцене вручения оружия все создавалось все создавалось
            solas.enabled = true;
            was.enabled = false;
            ptkas.enabled = false;
            mas.enabled = false;
            visualHand.ActivatedSword();
            return;
        }
        string nameSystem = PlayerPrefs.GetString(KeysPlayerPrefs.WEAPON_KEY_PLAYER_PREFS);
        if (nameSystem == "WandAttackSystem")
        {
            solas.enabled = false;
            was.enabled = true;
            ptkas.enabled = false;
            mas.enabled = false;
            visualHand.ActivatedWand();
        }
        else if (nameSystem == "PoisonedThrowingKnivesAttackSystem")
        {
            solas.enabled = false;
            was.enabled = false;
            ptkas.enabled = true;
            mas.enabled = false;
            visualHand.ActivatedKnives();
        }
        else if (nameSystem == "MorgenshternAttackSysten")
        {
            solas.enabled = false;
            was.enabled = false;
            ptkas.enabled = false;
            mas.enabled = true;
            visualHand.ActivatedMorgenshtern();
        }
        else
        {
            solas.enabled = true;
            was.enabled = false;
            ptkas.enabled = false;
            mas.enabled = false;
            visualHand.ActivatedSword();
        }
    }
}
