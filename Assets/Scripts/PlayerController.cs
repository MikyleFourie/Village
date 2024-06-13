using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool redToggled = false;
    [SerializeField] private UIController uiController;
    [SerializeField] private float moveSpeed = 4f;

    Vector3 forward, right, left, backward;
    Vector3 direction, rightMovement, upmovement, heading;

    [SerializeField] List<string> tempteam = new List<string>();
    [SerializeField] List<string> redteam = new List<string>();
    public GameObject temp;
    public VillagerMovement tempVMscript;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] GameObject marker;
    Vector3 markerOrigin;

    [SerializeField] ParticleSystem particleSys;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] call;

    void Start()
    {
        particleSys = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        markerOrigin = marker.transform.position;

        //Sets the directional axes for the game
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
            //Projects marker orb to ray
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, 100f * ray.direction, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                marker.transform.position = hit.point;
            }

        }
        else
        {
            marker.transform.position = markerOrigin;
        }


        //Handles movement
        if (Input.anyKey)
            Move();

        //Toggles the RED state on
        if (Input.GetKeyDown(KeyCode.R))
        {
            //UI overlay
            redToggled = uiController.toggleRed();

            /*
            if (redToggled)
            {
                foreach (var item in tempteam) //Make every Villager in TempTeam ready to move
                {
                    temp = GameObject.Find(item);
                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    tempVMscript.isReady = true;
                }
            }
            else
            {
                foreach (var item in tempteam)
                {
                    temp = GameObject.Find(item);
                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    tempVMscript.isReady = false;
                }
            }
            */
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //audioSource.clip = call[1];
            //audioSource.Play();
            playCallFour();
            //StartCoroutine(uiController.showCall());
            StartCoroutine(Pulse());
            Call();
        }



        if (redToggled && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicked");
            //redToggled = uiController.toggleRed();
            //Debug.Log("redToggled:" + redToggled);



            if (Physics.Raycast(ray, out hit))
            {
                playCallThree();

                //temp = GameObject.Find(redteam[0]);
                //
                //tempVMscript = temp.GetComponent<VillagerMovement>();
                //
                //if (tempVMscript != null && tempVMscript.isFollowing)
                //{
                //    StartCoroutine(tempVMscript.playSound(hit));
                //}

                foreach (var item in redteam)
                {
                    temp = GameObject.Find(item);

                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    if (tempVMscript != null && tempVMscript.isFollowing)
                    {
                        uiController.RemoveFromFollowing();
                        uiController.AddToPlaced();
                        StartCoroutine(tempVMscript.playSound(hit));
                        break;
                    }
                }



            }
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
        if (other.gameObject.tag == "villager")
            tempteam.Add(other.transform.parent.name);



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "villager")
            tempteam.Remove(other.transform.parent.name);
    }

    private void Call()
    {

        if (redToggled)
        {
            redToggled = uiController.toggleRed();
            foreach (var item in redteam)
            {



                //Get all Red Team members to follow
                temp = GameObject.Find(item);
                tempVMscript = temp.GetComponent<VillagerMovement>();


                if (!tempVMscript.isFollowing)
                {
                    uiController.RemoveFromPlaced();
                    uiController.AddToFollowing();
                }

                tempVMscript.SetStoppingDistance(2.5f);
                tempVMscript.StartFollowing();


            }
        }
        else
        {
            foreach (var item in tempteam)
            {
                temp = GameObject.Find(item);

                tempVMscript = temp.GetComponent<VillagerMovement>();

                if (tempVMscript != null && !tempVMscript.isOnTeam)
                {
                    if (tempVMscript.vCol == "red")
                    {
                        //Add to red team and set to follow

                        redteam.Add(item);
                        tempVMscript.isOnTeam = true;
                        tempVMscript.SetStoppingDistance(2.5f);
                        tempVMscript.StartFollowing();

                        uiController.AddToFollowing();
                    }
                }


            }
        }



    }

    //private void AddToTeam(string member, VillagerMovement memberSc)
    //{
    //    //permteam.Add(member);
    //    //memberSc.SetToFollow(this.gameObject.transform.position);
    //}

    public void playCallOne()
    {
        audioSource.clip = call[0];
        audioSource.Play();
    }

    public void playCallTwo()
    {
        audioSource.clip = call[1];
        audioSource.Play();
    }

    public void playCallThree()
    {
        redToggled = uiController.toggleRed();
        audioSource.clip = call[2];
        audioSource.Play();
    }

    public void playCallFour()
    {
        audioSource.clip = call[3];
        audioSource.Play();
    }

    IEnumerator Pulse()
    {
        particleSys.Play();
        yield return new WaitForSeconds(1f);
        particleSys.Stop();
    }
}
