using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    bool isTransitioning = false;
    AudioSource myAudio;
    private void Start() 
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) { return; }

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
        if(currentSceneIndex == SceneManager.sceneCount)
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
        StopPlyayerMovement();
        Invoke("ReloadLevel", delayTime);
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        myAudio.PlayOneShot(success);
        StopPlyayerMovement();
        Invoke("LoadNextLevel", delayTime);
    }

    private void StopPlyayerMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
