using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Level2Manager : MonoBehaviour
{
    Vector3 start, end;
    [SerializeField] GameObject gate;
    [SerializeField] Transform origin, destination;
    public bool switch1 = false;
    public bool switch2 = false;
    bool gateClosed = true;
    private NavMeshObstacle NavMeshObstacle;

    // Start is called before the first frame update
    void Start()
    {
        start = origin.position;
        end = destination.position;

        NavMeshObstacle = gate.GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gateClosed && switch1 && switch2)
        {
            StartCoroutine(Go(2f));
        }
    }

    IEnumerator Go(float dur)
    {
        gateClosed = false;
        NavMeshObstacle.carving = false;

        float timeElapsed = 0;
        float t;


        while (timeElapsed < dur)
        {
            t = timeElapsed / dur;
            gate.transform.position = Vector3.Lerp(start, end, t);

            timeElapsed += Time.deltaTime;

            yield return null;

        }


    }

}
