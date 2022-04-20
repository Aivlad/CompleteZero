using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPointsFeed : MonoBehaviour
{
    public List<Transform> movePoints;  // список точек перемещения
    public int indexCurrentPoint;       // текущая точка
    
    private MovementTowardsGoal mtg;    // куда мы будем кидать точки

    [Space]
    public float actionExecutionTime;  // потенциально: npc должен что-то делать на точке, это время нахождения на точке (время выполнения действия)

    private void Start()
    {
        mtg = GetComponent<MovementTowardsGoal>();
        mtg.SetStopDistance(0);
        mtg.ClearTarget();

        movePoints.RemoveAll(x => x == null);
        indexCurrentPoint = 0;

        
        StartCoroutine(NextPoint());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TargetMovementNPC"))
        {
            StartCoroutine(NextPoint(actionExecutionTime));
        }
    }

    private IEnumerator NextPoint(float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        if (mtg != null && movePoints.Count > 0)
        {
            mtg.SetTarget(movePoints[indexCurrentPoint]);
            indexCurrentPoint++;
            indexCurrentPoint %= movePoints.Count;
        }
    }

}
