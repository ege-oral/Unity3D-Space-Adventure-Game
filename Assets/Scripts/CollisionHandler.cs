using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
                LoadNextLevel();
                break;
            
            default:
                Debug.Log("Sorry you blew up.");
                ReloadLevel();
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
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }        
    }
}
