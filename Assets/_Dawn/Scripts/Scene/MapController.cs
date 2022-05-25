using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public KeyCode bigmapActivated = KeyCode.Tab;
    public GameObject minimap;
    public GameObject bigmap;


    private void Update()
    {
        if (Input.GetKey(bigmapActivated))
        {
            minimap.SetActive(false);
            bigmap.SetActive(true);
        }
        else
        {
            minimap.SetActive(true);
            bigmap.SetActive(false);
        }
    }
}
