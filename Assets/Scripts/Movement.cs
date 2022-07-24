using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustSpeed = 1000f;

    [SerializeField] float rotateLeftRightSpeed = 100f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerThrust();
        PlayerRotation();
    }

    private void PlayerThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //rb.AddRelativeForce(0, 1f * thrustSpeed * Time.deltaTime, 0);
            // OR
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
    }

    private void PlayerRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateLeftRightSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateLeftRightSpeed);
        }
        rb.freezeRotation = false;
    }

    private void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
