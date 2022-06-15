using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAttackSystem : MonoBehaviour
{
    public KeyCode attackButton = KeyCode.Mouse0;
    [Space]
    public GameObject placeOfImpactPrefab;
    public float maxDistanceAttack;
    [Space]
    public bool isReadyAttack;
    public float cooldown;

    [Header("Audio")]
    public PlayerSoundtrack playerSoundtrack;

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
                //audio
                if (playerSoundtrack != null)
                    playerSoundtrack.PlaySound(false);

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPosition = transform.position;
                if (Vector2.Distance(myPosition, mousePosition) <= maxDistanceAttack)
                {
                    Instantiate(placeOfImpactPrefab, mousePosition, Quaternion.identity);

                    isReadyAttack = false;
                    StartCoroutine(Recharge());
                }
            }
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(cooldown);
        isReadyAttack = true;
    }

}
