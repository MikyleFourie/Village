using System.Collections;
using UnityEngine;

public class Elevate : MonoBehaviour
{
    public float duration = 2f;
    private float lerpedValue;
    // Start is called before the first frame update
    [SerializeField] private Transform elTrans;
    [SerializeField] Vector3 start, bottom, middle, top;
    [SerializeField] private int numInTrig = 0;
    private IEnumerator currCo;
    [SerializeField] private UIController UIController;
    void Start()
    {
        currCo = Go(1, duration);
        bottom = elTrans.position;
        start = bottom;
        middle = new Vector3(bottom.x, bottom.y + 1, bottom.z);
        top = new Vector3(bottom.x, bottom.y + 2, bottom.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "v1" || other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                Debug.Log("it ran");
                StartCoroutine(UIController.ShowBubble("I'm not heavy enough!"));
            }


            numInTrig++;
            StopCoroutine(currCo);

            currCo = Go(numInTrig, duration);
            StartCoroutine(currCo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "v1" || other.tag == "Player")
        {
            numInTrig--;
            StopCoroutine(currCo);

            currCo = Go(numInTrig, duration);
            StartCoroutine(currCo);
        }
    }


    IEnumerator Go(int pos, float dur)
    {
        start = elTrans.position;
        float timeElapsed = 0;
        float t;

        if (pos == 0)
        {
            Debug.Log("Ping");
            while (timeElapsed < dur)
            {
                t = timeElapsed / dur;
                elTrans.position = Vector3.Lerp(start, bottom, t);

                timeElapsed += Time.deltaTime;

                yield return null;

            }

        }

        if (pos == 1)
        {
            Debug.Log("Ping");
            while (timeElapsed < dur)
            {
                t = timeElapsed / dur;
                elTrans.position = Vector3.Lerp(start, middle, t);

                timeElapsed += Time.deltaTime;

                yield return null;

            }

        }

        if (pos == 2)
        {
            Debug.Log("Ping");
            while (timeElapsed < dur)
            {
                t = timeElapsed / dur;
                elTrans.position = Vector3.Lerp(start, top, t);

                timeElapsed += Time.deltaTime;

                yield return null;

            }

        }


    }


}
