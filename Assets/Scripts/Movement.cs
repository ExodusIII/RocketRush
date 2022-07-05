using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMTERS -> for tubing, typically set in the editor
    // CACHE -> referances for readability or speed
    // STATE -> private instance (member) variables

    [SerializeField] float mainTrust = 100f;
    [SerializeField] float rotatationTrust = 1f;
    [SerializeField] AudioClip mainEngine;

     [SerializeField] ParticleSystem mainEngineParticles;
     [SerializeField] ParticleSystem leftEngineParticles;
     [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;

    // Start is called before the first frame update   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTrust();
        ProcessRotation();
    }

    void ProcessTrust()
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

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainTrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void ProcessRotation()
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
    }

    void StopRotating()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotatationTrust);
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotatationTrust);
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;

    }
}
