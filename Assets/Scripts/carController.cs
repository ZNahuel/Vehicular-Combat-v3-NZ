using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardAcell = 8f, reverseAcell = 4f, maxSpeed = 50f, turnStrength = 180, gravityForce = 10f, dragOnGround = 3f;

    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public Transform leftForntWheel, rigthFrontWheel;
    public float maxWheelTurn = 25f;


    void Start()
    {
        rb.transform.parent = null;
    }

    void Update()
    {

        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAcell * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAcell * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        leftForntWheel.localRotation = Quaternion.Euler(leftForntWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftForntWheel.localRotation.eulerAngles.z);
        rigthFrontWheel.localRotation = Quaternion.Euler(rigthFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rigthFrontWheel.localRotation.eulerAngles.z);


        transform.position = rb.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if (grounded)
        {
            rb.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rb.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }
}
