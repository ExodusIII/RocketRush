using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  int currentSceneIndex;
  int nextSceneIndex;
  [SerializeField] float delayInvoke = 1f;
  [SerializeField] AudioClip successSound;
  [SerializeField] AudioClip crashSound;

  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;  

  AudioSource audioSource;

  bool isTransitioning = false;
  bool isCollitionDisabled = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
    {
        ResponseToDebugKeys();
    }

    void ResponseToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKey(KeyCode.C))
        {
            isCollitionDisabled = true;
        }
    }

    void OnCollisionEnter(Collision other) 
  {

    if(isTransitioning || isCollitionDisabled) {return;}
    
      switch(other.gameObject.tag)
      {
          case "Friendly":
          Debug.Log("You bumped Friendly");
          break;       
          case "Finish":
          Debug.Log("Congrutulations Here comes next level!");
          StartSuccessSequence();
          break;      
          default:
          Debug.Log("You bumped undefined object");
          StartCrashSequence(); 
          break;

      }
  }

    void StartSuccessSequence()
    {
      // todo add SFX     
      isTransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(successSound); 
      successParticles.Play();
       
      // todo add particle effect
      GetComponent<Movement>().enabled = false;
      Invoke("LoadNextLevel", delayInvoke);
    }

    void StartCrashSequence()
  {
    // todo add SFX 
      isTransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(crashSound); 
      crashParticles.Play();

    // todo add particle effect
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", delayInvoke);
  }
  void ReloadLevel()
  {
    currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
  void LoadNextLevel()
  {
    currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    nextSceneIndex = currentSceneIndex + 1;
    if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}
