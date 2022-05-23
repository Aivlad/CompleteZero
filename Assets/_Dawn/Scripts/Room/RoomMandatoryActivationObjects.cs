using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMandatoryActivationObjects : MonoBehaviour
{
    public List<GameObject> objectsWithMandatoryActivation;

    private void Start()
    {
        foreach (var item in objectsWithMandatoryActivation)
        {
            item.SetActive(true);
        }
    }
}
