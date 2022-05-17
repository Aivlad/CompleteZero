using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSoundAccompanimentDoorActivated : MonoBehaviour
{
    public RoomSoundAccompanimentDoor source;

    private void OnEnable()
    {
        source.PlaySoundDoorClose();
    }

    private void OnDisable()
    {
        source.PlaySoundDoorOpen();
    }
}
