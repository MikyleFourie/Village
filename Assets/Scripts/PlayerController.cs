using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    Vector3 forward, right, left, backward;
    Vector3 direction, rightMovement, upmovement, heading;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0))*forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            Move();
        
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
}
