using UnityEngine;

public class ElevatorParenter : MonoBehaviour
{
    //[SerializeField] private Transform player;
    //[SerializeField] private bool isOnPlatform;
    //[SerializeField] private Vector3 prevPos;

    private void Start()
    {
        //prevPos = transform.parent.transform.position;
    }

    private void Update()
    {
        //if (isOnPlatform)
        //{
        //    Vector3 movement = transform.parent.transform.position - prevPos;
        //    player.position += movement;
        //}
        //prevPos = transform.parent.transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("PING: collided with: " + other);
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("PING: collided with: " + other);
            //player = other.transform;
            //isOnPlatform = true;

            //player.position = new Vector3(player.position.x,
            //    player.position.y + 0.1f,
            //    player.position.z);
            other.collider.transform.parent.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //isOnPlatform = false;
            other.collider.transform.parent.transform.parent = null;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        FixedJoint fj = gameObject.AddComponent<FixedJoint>();
    //        fj.connectedBody = other.GetComponentInParent<Rigidbody>();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Destroy(GetComponentInParent<FixedJoint>());
    //    }

    //}

}
