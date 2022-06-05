using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraEffect : MonoBehaviour
{
    public float _duration = .8f;
    private Transform cameraTransform;
    private Vector3 originalPosition;

    void Start()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = camera.GetComponent<Transform>();        
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(_Shake());
    //}

    public void Shake()
    {
        StartCoroutine(_Shake());
    }

    IEnumerator _Shake()
    {
        originalPosition = cameraTransform.transform.position;

        float x;
        float y;
        float timeLeft = Time.time;

        while ((timeLeft + _duration) > Time.time)
        {
            x = Random.Range(-0.3f, 0.3f);
            y = Random.Range(-0.3f, 0.3f);

            cameraTransform.position = new Vector3(x + originalPosition.x, y + originalPosition.y, originalPosition.z); 
            yield return new WaitForSeconds(0.025f);
        }

        cameraTransform.position = originalPosition;

        enabled = false;
    }
}
