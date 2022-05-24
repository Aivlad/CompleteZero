using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCobraController : MonoBehaviour
{
    public SceneManagerNPCState.TypesOfEnemies type;
    public SceneManagerNPCState sceneManager;
    [Space]
    public GameObject systemMelee;
    public GameObject systemRanger;
    public MovementTowardsGoal movementTowardsGoal;

    private float rangerDistance;
    

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerNPCState>();
        rangerDistance = sceneManager.GetRangerDistance(type);
    }

    private void Update()
    {
        if (movementTowardsGoal.GetTargetDistance() > rangerDistance)
        {
            systemMelee.SetActive(false);
            systemRanger.SetActive(true);
        }
        else
        {
            systemMelee.SetActive(true);
            systemRanger.SetActive(false);
        }
    }


}
