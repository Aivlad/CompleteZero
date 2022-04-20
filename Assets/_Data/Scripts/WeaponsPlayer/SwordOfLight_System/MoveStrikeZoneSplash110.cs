using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStrikeZoneSplash110 : MonoBehaviour
{
    public PlayerMovement playerMovement;
    [Space]
    public Transform placementPointRight;
    public Transform placementPointDown;
    public Transform placementPointLeft;
    public Transform placementPointUp;
    [Space]
    public Transform splash;

    private void Update()
    {
        PlayerMovement.Facing currentLook = playerMovement.GetFacing();
        if (currentLook == PlayerMovement.Facing.DOWN)
        {
            splash.position = placementPointDown.position;
            splash.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (currentLook == PlayerMovement.Facing.UP)
        {
            splash.position = placementPointUp.position;
            splash.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (currentLook == PlayerMovement.Facing.LEFT)
        {
            splash.position = placementPointLeft.position;
            splash.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (currentLook == PlayerMovement.Facing.RIGHT)
        {
            splash.position = placementPointRight.position;
            splash.rotation = Quaternion.Euler(0, 0, -45);
        }
    }


}
