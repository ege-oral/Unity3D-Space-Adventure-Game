using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    static int numberOfDeath = 0;
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource myAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    bool isTransitioning = false;
    bool collisionDisabled = false;



    private void Start() 
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        CheatKeys();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This object is friendly.");
                break;

            case "Fuel":
                Debug.Log("You picked up fuel.");
                break;

            case "Finish":
                Debug.Log("Finish have been reached.");
                StartSuccessSequence();
                break;
            
            default:
                Debug.Log("Sorry you blew up.");
                StartCrashSequence();
                numberOfDeath += 1;
                Debug.Log(numberOfDeath);
                break;
        }
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Max level has been reached.");
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }        
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        myAudio.PlayOneShot(crash);
        crashParticles.Play();
        StopPlyayerMovement();
        Invoke("ReloadLevel", delayTime);
    }

    private void StartSuccessSequence()
    {
        
        isTransitioning = true;
        myAudio.Stop();
        myAudio.PlayOneShot(success);
        successParticles.Play();
        StopPlyayerMovement();
        Invoke("LoadNextLevel", delayTime);
    }

    private void StopPlyayerMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    private void CheatKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision.
        }
    }
}
