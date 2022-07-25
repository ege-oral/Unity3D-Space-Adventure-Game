using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource myAudio;
    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotateLeftRightSpeed = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
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
            
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if(!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(mainEngine);
            }
            if(!thrustParticles.isPlaying)
            {
                thrustParticles.Play();
            }
            
        }
        else
        {
            myAudio.Stop();
            thrustParticles.Stop();
        }
    }

    private void PlayerRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateLeftRightSpeed);

            if(!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateLeftRightSpeed);

            if(!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
        
        // Unfreezing X and Y value.
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void ApplyRotation(float rotationSpeed)
    {
        // Freezing rotation so we can manually rotate.
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

}
