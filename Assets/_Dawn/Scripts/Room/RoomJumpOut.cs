using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomJumpOut : MonoBehaviour
{
    public enum Direction
    {
        none,
        left,
        top,
        right,
        bottom
    }

    public Direction directionJump;
    public float forceJump;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(directionJump)
            {
                case Direction.left:
                    collision.transform.position += new Vector3(-forceJump, 0, 0);
                    break;
                case Direction.right:
                    collision.transform.position += new Vector3(forceJump, 0, 0);
                    break;
                case Direction.top:
                    collision.transform.position += new Vector3(0, forceJump, 0);
                    break;
                case Direction.bottom:
                    collision.transform.position += new Vector3(0, -forceJump, 0);
                    break;
            }
        }
    }
}
