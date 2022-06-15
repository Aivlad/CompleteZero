using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLinksHandLuggage : MonoBehaviour
{
    public GameObject sword;
    public GameObject wand;
    public GameObject morgenshtern;
    public GameObject leftKnife;
    public GameObject rightKnife;

    private List<GameObject> weapons;

    private void Start()
    {
        weapons = new List<GameObject>();
        weapons.Add(sword);
        weapons.Add(wand);
        weapons.Add(morgenshtern);
        weapons.Add(leftKnife);
        weapons.Add(rightKnife);
    }

    private void OffWeapons()
    {
        foreach (var item in weapons)
        {
            item.SetActive(false);
        }
    }

    public void ActivatedSword()
    {
        OffWeapons();
        sword.SetActive(true);
    }

    public void ActivatedWand()
    {
        OffWeapons();
        wand.SetActive(true);
    }

    public void ActivatedMorgenshtern()
    {
        OffWeapons();
        morgenshtern.SetActive(true);
    }

    public void ActivatedKnives()
    {
        OffWeapons();
        leftKnife.SetActive(true);
        rightKnife.SetActive(true);
    }
}
