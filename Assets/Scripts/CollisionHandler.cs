using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    public static int numberOfDeath = 0;
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource myAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] TextMeshProUGUI cheatSheet;


    bool isTransitioning = false;
    bool collisionDisabled = false;


    private void Awake() 
    {
        if(numberOfDeath >= 20)
        {
            cheatSheet.gameObject.SetActive(true);
        }
    }
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
                break;

            case "Finish":
                StartSuccessSequence();
                break;
            
            default:
                StartCrashSequence();
                numberOfDeath += 1;
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
