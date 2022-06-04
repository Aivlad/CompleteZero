using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestAction : MonoBehaviour
{
    public KeyCode buttonAction = KeyCode.Z;

    public ShakeCameraEffect shake;
    public float cooldown = 3f;
    private float currentTime;

    private void Start()
    {
        shake = GetComponent<ShakeCameraEffect>();
        currentTime = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(buttonAction) && currentTime >= cooldown)
        {
            Action();
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void Action()
    {
        shake.Shake();
    }
}
