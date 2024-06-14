using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool redToggled = false;
    public bool blueToggled = false;
    [SerializeField] private UIController uiController;
    [SerializeField] private float moveSpeed = 4f;

    Vector3 forward, right, left, backward;
    Vector3 direction, rightMovement, upmovement, heading;

    [SerializeField] List<string> tempteam = new List<string>();
    [SerializeField] List<string> redteam = new List<string>();
    [SerializeField] List<string> blueteam = new List<string>();

    public GameObject temp;
    public VillagerMovement tempVMscript;
    public VillagerAttributes tempVAscript;

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
        if (redToggled || blueToggled)
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

        //Toggles the RED state 
        if (Input.GetKeyDown(KeyCode.R))
        {
            //UI overlay
            redToggled = uiController.toggleRed();

        }

        //Toggles the BLUE state 
        if (Input.GetKeyDown(KeyCode.B))
        {
            //UI overlay
            blueToggled = uiController.toggleBlue();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playCallFour();
            StartCoroutine(Pulse());
            Call();
        }



        if (redToggled && Input.GetMouseButtonDown(0))
        {//Directs one red villager on Red Team

            //Debug.Log("Clicked");
            //redToggled = uiController.toggleRed();
            //Debug.Log("redToggled:" + redToggled);



            if (Physics.Raycast(ray, out hit))
            {
                playCallThree();



                foreach (var item in redteam)
                {
                    temp = GameObject.Find(item);

                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    if (tempVMscript != null && tempVMscript.isFollowing)
                    {
                        uiController.RemoveFromFollowing(0);
                        uiController.AddToPlaced(0);
                        StartCoroutine(tempVMscript.playSound(hit));
                        break;
                    }
                }



            }
        }

        if (blueToggled && Input.GetMouseButtonDown(0))
        {//Directs one red villager on Red Team

            //Debug.Log("Clicked");
            //redToggled = uiController.toggleRed();
            //Debug.Log("redToggled:" + redToggled);



            if (Physics.Raycast(ray, out hit))
            {
                playCallTwo();



                foreach (var item in blueteam)
                {
                    temp = GameObject.Find(item);

                    tempVMscript = temp.GetComponent<VillagerMovement>();

                    if (tempVMscript != null && tempVMscript.isFollowing)
                    {
                        uiController.RemoveFromFollowing(1);
                        uiController.AddToPlaced(1);
                        StartCoroutine(tempVMscript.playSound(hit));
                        break;
                    }
                }



            }
        }

    }

    //Makes Player move
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


    //Adds villagers to temp team
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "villager")
            tempteam.Add(other.transform.parent.name);



    }

    //Removes villagers from temp team
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "villager")
            tempteam.Remove(other.transform.parent.name);
    }

    //Calls villagers
    private void Call()
    {

        if (redToggled)
        {
            //If RED is toggled, calls all red villagers
            redToggled = uiController.toggleRed();
            foreach (var item in redteam)
            {
                //Get all Red Team members to follow
                temp = GameObject.Find(item);
                tempVMscript = temp.GetComponent<VillagerMovement>();


                if (!tempVMscript.isFollowing)
                {
                    uiController.RemoveFromPlaced(0);
                    uiController.AddToFollowing(0);
                }

                tempVMscript.SetStoppingDistance(2.5f);
                tempVMscript.StartFollowing();


            }
        }
        else if (blueToggled)
        {
            //If BLUE is toggled, calls all red villagers
            blueToggled = uiController.toggleBlue();
            foreach (var item in blueteam)
            {
                //Get all blue Team members to follow
                temp = GameObject.Find(item);
                tempVMscript = temp.GetComponent<VillagerMovement>();


                if (!tempVMscript.isFollowing)
                {
                    uiController.RemoveFromPlaced(1);
                    uiController.AddToFollowing(1);
                }

                tempVMscript.SetStoppingDistance(2.5f);
                tempVMscript.StartFollowing();


            }
        }
        else
        {
            //If NO toggle, calls all villagers in range and adds to proper TEAM
            foreach (var item in tempteam)
            {
                temp = GameObject.Find(item);

                tempVAscript = temp.GetComponent<VillagerAttributes>();
                tempVMscript = temp.GetComponent<VillagerMovement>();

                if (tempVMscript != null && !tempVMscript.isOnTeam)
                {
                    if (tempVAscript.vCol == VillagerAttributes.VillagerColor.Red)
                    {
                        //Add to red team and set to follow

                        redteam.Add(item);
                        tempVMscript.isOnTeam = true;
                        tempVMscript.SetStoppingDistance(2.5f);
                        tempVMscript.StartFollowing();

                        uiController.AddToFollowing(0);
                    }
                    else if (tempVAscript.vCol == VillagerAttributes.VillagerColor.Blue)
                    {
                        //Add to blue team and set to follow

                        blueteam.Add(item);
                        tempVMscript.isOnTeam = true;
                        tempVMscript.SetStoppingDistance(2.5f);
                        tempVMscript.StartFollowing();

                        uiController.AddToFollowing(1);
                    }
                }


            }
        }



    }


    public void playCallOne()
    {
        audioSource.clip = call[0];
        audioSource.Play();
    }

    public void playCallTwo()
    {
        blueToggled = uiController.toggleBlue();
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
