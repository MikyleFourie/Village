using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class VillagerMovement : MonoBehaviour, IPointerDownHandler
{
    public bool isOnTeam = false;
    public bool isReady = false;
    private NavMeshAgent agent;
    [SerializeField] private GameObject marker;
    //public int ignoreLayer1 = 9;
    //public int ignoreLayer2 = 10;
    //public int ignoreLayerCombo;
    //public LayerMask ignoreLayerMask;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        // ignoreLayerCombo = (1 << ignoreLayer1) | (1 << ignoreLayer2);
        // ignoreLayerCombo = ~ignoreLayerCombo;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, 100f * ray.direction, Color.green);



        if (Input.GetMouseButtonDown(0) && isReady)
        {

            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("Hit" + hit.collider.gameObject.name, hit.collider.gameObject);
                agent.SetDestination(hit.point);
                marker.transform.position = hit.point;
            }
        }


    }

    public void SetToFollow(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void RecallAll(Vector3 playerPos)
    {
        if (isOnTeam)
        {
            agent.SetDestination(playerPos);
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
