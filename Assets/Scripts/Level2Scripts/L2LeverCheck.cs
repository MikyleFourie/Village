using System.Collections;
using UnityEngine;

public class L2LeverCheck : MonoBehaviour
{
    [SerializeField] UIController UIController;
    [SerializeField] Level2Manager Level2Manager;
    GameObject lever;
    string parentStr;
    [SerializeField] private int identity;

    public Quaternion trigBoxRotation;
    public Quaternion leverFirstRotation;
    public Quaternion leverEndRotation;

    private void Start()
    {
        lever = transform.parent.gameObject;
        //trigBoxRotation = Quaternion.identity;
        //leverFirstRotation = lever.transform.rotation;
        //leverEndRotation = Quaternion.Euler(300, 0, 270);


        parentStr = transform.parent.name;

        if (parentStr == "Lever (1)")
        {
            identity = 1;
        }
        else if (parentStr == "Lever (2)")
        {
            identity = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "villager")
        {
            Debug.Log("Detected Villager");
            StartCoroutine(tickOn());


        }

        if (other.tag == "Player")
        {
            Debug.Log("Detected Player");
            StartCoroutine(UIController.ShowBubble("I can't pull this lever"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "villager")
        {
            if (identity == 1)
            {
                Level2Manager.switch1 = false;
                lever.transform.rotation = leverFirstRotation;
                transform.localRotation = trigBoxRotation;
            }
            else if (identity == 2)
            {
                Level2Manager.switch2 = false;
                lever.transform.rotation = leverFirstRotation;
                transform.localRotation = trigBoxRotation;
            }
        }
    }
    IEnumerator tickOn()
    {
        if (identity == 1)
        {
            Level2Manager.switch1 = true;
            lever.transform.rotation = leverEndRotation;
            transform.localRotation = trigBoxRotation;
        }
        else if (identity == 2)
        {
            Level2Manager.switch2 = true;
            lever.transform.rotation = leverEndRotation;
            transform.localRotation = trigBoxRotation;
        }

        //yield return null;
        yield return new WaitForSeconds(2.5f);

        if (identity == 1)
        {
            Level2Manager.switch1 = false;
            lever.transform.rotation = leverFirstRotation;
            transform.localRotation = trigBoxRotation;
        }
        else if (identity == 2)
        {
            Level2Manager.switch2 = false;
            lever.transform.rotation = leverFirstRotation;
            transform.localRotation = trigBoxRotation;
        }

    }


}
