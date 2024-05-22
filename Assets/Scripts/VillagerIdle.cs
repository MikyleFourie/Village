using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VillagerIdle : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold waypoints
    private NavMeshAgent agent;
    private int currentWaypointIndex;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned.");
            return;
        }

        // Start the movement loop
        StartCoroutine(MoveToNextWaypoint());
    }



    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            // Set the agent's destination to the current waypoint
            agent.SetDestination(waypoints[currentWaypointIndex].position);

            // Wait until the agent has reached the destination
            while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Wait for a short duration before moving to the next waypoint
            float randomValue = Random.Range(1f, 3f);
            yield return new WaitForSeconds(randomValue);

            // Randomly select the next waypoint index
            currentWaypointIndex = Random.Range(0, waypoints.Length);
        }
    }
}
