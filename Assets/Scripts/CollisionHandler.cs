using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
     private void OnCollisionEnter(Collision other) 
    {
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
        StopPlyayerMovement();
        Invoke("ReloadLevel", delayTime);
    }

    private void StartSuccessSequence()
    {
        StopPlyayerMovement();
        Invoke("LoadNextLevel", delayTime);
    }

    private void StopPlyayerMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
