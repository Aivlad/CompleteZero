using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfLightAttackSysten : MonoBehaviour
{
    public KeyCode attackButton = KeyCode.Mouse0;
    [Space]
    public GameObject swordOfLightSystemSplash;
    [Space]
    public bool isReadyAttack;
    public float cooldown;

    private void Start()
    {
        isReadyAttack = true;
    }

    private void Update()
    {
        if (Input.GetKey(attackButton))
        {
            if (isReadyAttack)
            {
                swordOfLightSystemSplash.SetActive(true);
                StartCoroutine(StubAttackVisualization());
                isReadyAttack = false;
                StartCoroutine(Recharge());
            }
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

    IEnumerator StubAttackVisualization()
    {
        yield return new WaitForSeconds(0.2f);
        swordOfLightSystemSplash.SetActive(false);
    }
}
