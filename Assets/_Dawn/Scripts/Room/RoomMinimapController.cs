using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMinimapController : MonoBehaviour
{
    public GameObject minimapChildLocation;
    public GameObject minimapChildWalls;
    private CameraFollowTarget cameraController;
    private bool isActivated;

    private void Start()
    {
        var camera = GameObject.FindGameObjectWithTag("MinimapCamera");
        if (camera != null)
        {
            cameraController = camera.GetComponent<CameraFollowTarget>();
        }

        minimapChildLocation.SetActive(false);
        minimapChildWalls.SetActive(false);

        isActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cameraController != null)
        {
            cameraController.SetTarget(transform);
            minimapChildLocation.SetActive(true);
            minimapChildWalls.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActivated && collision.CompareTag("Player") && cameraController != null)
        {
            cameraController.SetTarget(transform);
            minimapChildLocation.SetActive(true);
            minimapChildWalls.SetActive(true);
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cameraController != null)
        {
            minimapChildLocation.SetActive(false);
        }
    }
}
