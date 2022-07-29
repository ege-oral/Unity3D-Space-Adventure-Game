using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void PlayerRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
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

    private void StartThrusting()
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

    private void StopThrusting()
    {
        myAudio.Stop();
        thrustParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotateLeftRightSpeed);

            if(!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotateLeftRightSpeed);

            if(!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
}
