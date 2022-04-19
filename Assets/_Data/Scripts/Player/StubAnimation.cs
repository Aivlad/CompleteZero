using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StubAnimation : MonoBehaviour
{
    public Sprite lookUp;
    public Sprite lookLeft;
    public Sprite lookDown;
    public Sprite lookRight;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMovement != null && spriteRenderer != null)
        {
            PlayerMovement.Facing currentLook = playerMovement.GetFacing();
            if (currentLook == PlayerMovement.Facing.DOWN)
            {
                spriteRenderer.sprite = lookDown;
            }
            else if (currentLook == PlayerMovement.Facing.UP)
            {
                spriteRenderer.sprite = lookUp;
            }
            else if (currentLook == PlayerMovement.Facing.LEFT)
            {
                spriteRenderer.sprite = lookLeft;
            }
            else if (currentLook == PlayerMovement.Facing.RIGHT)
            {
                spriteRenderer.sprite = lookRight;
            }
        }
    }
}
