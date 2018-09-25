using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 10000f;
    [SerializeField] float mainThrust = 50000f;
    [SerializeField] AudioClip mainEngineClip;
    [SerializeField] AudioClip nextlevelClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem nextlevelParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;
    float loadLevelDelay = 1f;

    //debug: C toggle collisions and L for next level in Debug Conditions
    private bool collisionsAreEnabled = true;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if (Debug.isDebugBuild)
        {
            DebugConditions();
        }
    }

    private void DebugConditions()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsAreEnabled = !collisionsAreEnabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !collisionsAreEnabled) { return;}// guard condition only to ignore collisions when dead

        if (collision.contacts[0].thisCollider.name == "RocketBody")
        {
            DyingScene();
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Obstacle":
                DyingScene();
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                print("Ouch.");
                break;
        }
    }

    private void DyingScene()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        deathParticles.Play();
        Invoke("LoadSamelevel", loadLevelDelay);
    }

    private void LoadNextLevel()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(nextlevelClip);
        nextlevelParticles.Play();
        Invoke("LoadNextScene", loadLevelDelay); 
    }

    private void LoadSamelevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        // check if there is another scene in build settings after this one
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void RespondToThrustInput()
    {
        //todo restrict flying out of bounds
        /*transform.localPosition = new Vector3()
        int height = 0;
        print(height);*/
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyTrust();
        }
        else
        {
            StopApplyTrust();
        }
    }

    private void StopApplyTrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyTrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngineClip);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        rigidBody.angularVelocity = Vector3.zero; //stop the phsics engine rotating the object.
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }
}


