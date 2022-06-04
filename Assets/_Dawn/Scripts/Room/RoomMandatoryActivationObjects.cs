using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMandatoryActivationObjects : MonoBehaviour
{
    public List<GameObject> objectsWithMandatoryActivation;
    [Space]
    public Camera camerad;
    public Color color;

    private void Start()
    {
        foreach (var item in objectsWithMandatoryActivation)
        {
            item.SetActive(true);
        }

        camerad.backgroundColor = color;
    }
}
