using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool redToggled = false;
    [SerializeField] private UIController uiController;
    [SerializeField] private float moveSpeed = 4f;

    Vector3 forward, right, left, backward;
    Vector3 direction, rightMovement, upmovement, heading;

    [SerializeField] List<string> tempteam = new List<string>();
    [SerializeField] List<string> permteam = new List<string>();
    public GameObject temp;
    public VillagerMovement tempVMscript;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] GameObject marker;
    Vector3 markerOrigin;

    void Start()
    {
        markerOrigin = marker.transform.position;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (redToggled)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, 100f * ray.direction, Color.green);

            if (Physics.Raycast(ray, out hit))
            {
                marker.transform.position = hit.point;
            }

        }
        else
        {
            marker.transform.position = markerOrigin;
        }



        if (Input.anyKey)
            Move();

        if (Input.GetKeyDown(KeyCode.R))
        {
            redToggled = uiController.toggleRed();

            if (redToggled)
            {
                foreach (var item in permteam)
                {
                    temp = GameObject.Find(item);
                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    tempVMscript.isReady = true;
                }
            }
            else
            {
                foreach (var item in permteam)
                {
                    temp = GameObject.Find(item);
                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    tempVMscript.isReady = false;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(uiController.showCall());
            Call();
        }

    }

    void Move()
    {

        direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
        rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        upmovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        heading = Vector3.Normalize(rightMovement + upmovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upmovement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "v1")
            tempteam.Add(other.transform.parent.name);



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "v1")
            tempteam.Remove(other.transform.parent.name);
    }

    private void Call()
    {

        foreach (var item in tempteam)
        {
            temp = GameObject.Find(item);

            tempVMscript = temp.GetComponent<VillagerMovement>();

            if (tempVMscript != null)
            {
                if (!tempVMscript.isOnTeam)
                {
                    permteam.Add(item);
                    tempVMscript.isOnTeam = true;
                    // AddToTeam(item, tempVMscript);
                }
            }


        }

        foreach (var item in permteam)
        {
            temp = GameObject.Find(item);
            tempVMscript = temp.GetComponent<VillagerMovement>();

            tempVMscript.RecallAll(this.gameObject.transform.position);
        }
    }

    private void AddToTeam(string member, VillagerMovement memberSc)
    {
        //permteam.Add(member);
        //memberSc.SetToFollow(this.gameObject.transform.position);
    }

}
