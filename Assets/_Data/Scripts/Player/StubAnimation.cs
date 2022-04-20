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
    private MovementTowardsGoal objMovement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objMovement = GetComponent<MovementTowardsGoal>();
    }

    private void Update()
    {
        if (playerMovement != null && spriteRenderer != null)
        {
            PlayerView();
        }
        else if (objMovement != null && spriteRenderer != null)
        {
            ObjectView();
        }
    }

    private void PlayerView()
    {
        PlayerMovement.Facing currentLook = playerMovement.GetFacing();
         if (currentLook == PlayerMovement.Facing.LEFT)
        {
            spriteRenderer.sprite = lookLeft;
        }
        else if (currentLook == PlayerMovement.Facing.RIGHT)
        {
            spriteRenderer.sprite = lookRight;
        }
        else if(currentLook == PlayerMovement.Facing.DOWN)
        {
            spriteRenderer.sprite = lookDown;
        }
        else if (currentLook == PlayerMovement.Facing.UP)
        {
            spriteRenderer.sprite = lookUp;
        }        
    }

    private void ObjectView()
    {
        MovementTowardsGoal.Facing currentLook = objMovement.GetFacing();
        if (currentLook == MovementTowardsGoal.Facing.LEFT)
        {
            spriteRenderer.sprite = lookLeft;
        }
        else if (currentLook == MovementTowardsGoal.Facing.RIGHT)
        {
            spriteRenderer.sprite = lookRight;
        }
        else if(currentLook == MovementTowardsGoal.Facing.DOWN)
        {
            spriteRenderer.sprite = lookDown;
        }
        else if (currentLook == MovementTowardsGoal.Facing.UP)
        {
            spriteRenderer.sprite = lookUp;
        }        
    }
}
