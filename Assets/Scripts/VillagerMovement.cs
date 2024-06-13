using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class VillagerMovement : MonoBehaviour, IPointerDownHandler
{
    public string vCol = "red";
    public bool isOnTeam = false;

    public bool isFollowing = false;
    private NavMeshAgent agent;
    [SerializeField] private GameObject marker;
    [SerializeField] private PlayerController playerController;
    [SerializeField] Transform playerPos;
    //public int ignoreLayer1 = 9;
    //public int ignoreLayer2 = 10;
    //public int ignoreLayerCombo;
    //public LayerMask ignoreLayerMask;
    private Ray ray;
    private RaycastHit hit;
    private AudioSource audioSource;
    [SerializeField] private PlayerController PlayerController;


    void Start()
    {
        // ignoreLayerCombo = (1 << ignoreLayer1) | (1 << ignoreLayer2);
        // ignoreLayerCombo = ~ignoreLayerCombo;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //The ray from the camera looking for collisions
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, 100f * ray.direction, Color.green);


        if (isFollowing)
        {
            agent.SetDestination(playerPos.position);
        }

        //if (Input.GetMouseButtonDown(0) && isReady)
        //{
        //    //Debug.Log("Fired");

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        playerController.playCallThree();
        //        StartCoroutine(playSound(hit));
        //        // Debug.Log("Hit" + hit.collider.gameObject.name, hit.collider.gameObject);

        //        //agent.SetDestination(hit.point);
        //        marker.transform.position = hit.point;
        //    }
        //}


    }

    public void SetToFollow(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void StartFollowing()
    {
        if (isOnTeam)
        {
            //agent.SetDestination(playerPos);
            isFollowing = true;
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void SetDestination(Vector3 dest)
    {

    }

    public IEnumerator playSound(RaycastHit hit)
    {
        //Debug.Log("CoRoutine fired");
        isFollowing = false;
        //yield return new WaitForSeconds(0.3f);
        agent.stoppingDistance = 0.5f;
        agent.SetDestination(hit.point);
        yield return new WaitForSeconds(1.2f);
        audioSource.Play();
    }

    public void SetStoppingDistance(float distance)
    {
        agent.stoppingDistance = distance;
    }
}
