using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public List<GameObject> itemsTypes;
    [Range(0, 100)]
    public float chance = 70;

    private void OnDestroy()
    {
        if (Random.Range(0, 100) <= chance)
            Instantiate(itemsTypes[Random.Range(0, itemsTypes.Count)], transform.position, Quaternion.identity);
    }
}
