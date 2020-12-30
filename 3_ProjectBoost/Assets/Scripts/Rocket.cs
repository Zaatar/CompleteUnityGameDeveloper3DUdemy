using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSoundClip;
    [SerializeField] AudioClip nextLevelSoundClip;
    [SerializeField] ParticleSystem jetParticleSystem;
    [SerializeField] ParticleSystem successParticleSystem;
    [SerializeField] ParticleSystem deathParticleSystem;
    float loadSceneTimer = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {
        ALIVE, DYING, TRANSCENDING
    }
    State state = State.ALIVE;
    bool collisionsDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<Rigidbody>() == null)
            print("Rigidbody is not defined on your game object");
        rigidBody = this.GetComponent<Rigidbody>();
        if(this.GetComponent<AudioSource>() == null)
            print("AudioSource is not defined on your game object");
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.ALIVE) {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if(Debug.isDebugBuild){
            RespondToDebugKeys();
        }
    }

    void OnCollisionEnter(Collision collision){
        if(state != State.ALIVE || !collisionsDisabled)
            return;

        switch(collision.gameObject.tag)
        {
            case "Friendly": 
                print("OK");
                break;
            case "Finish":
                state = State.TRANSCENDING;
                PlayAppropriateSoundClip();
                successParticleSystem.Play();
                Invoke("LoadNextScene", loadSceneTimer);
                break;
            default:
                state = State.DYING;
                PlayAppropriateSoundClip();
                deathParticleSystem.Play();
                Invoke("LoadFirstScene", loadSceneTimer);
                break;
        }
    }

    private void RespondToThrustInput(){
        if(Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            audioSource.Stop();
            jetParticleSystem.Stop();
        }
    }

    private void ApplyThrust(){
        float thrustVelocity = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrustVelocity * Time.deltaTime);
            if(!audioSource.isPlaying)
                PlayAppropriateSoundClip();
        jetParticleSystem.Play();
    }

    private void RespondToRotateInput() {
        rigidBody.angularVelocity = Vector3.zero;
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if(Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
    }

    private void RespondToDebugKeys() {
        if(Input.GetKeyDown(KeyCode.L))
            LoadNextScene();
        if(Input.GetKeyDown(KeyCode.C))
            collisionsDisabled = !collisionsDisabled;
    }

    private void LoadNextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneToLoadIndex = 0;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings-1) {
            sceneToLoadIndex = 0;
        } else {
            sceneToLoadIndex = currentSceneIndex + 1;
        }
        SceneManager.LoadScene(sceneToLoadIndex);
        state = State.ALIVE;
    }

    private void LoadFirstScene(){
        SceneManager.LoadScene(0);
        state = State.ALIVE;
    }

    private void PlayAppropriateSoundClip(){
        audioSource.Stop();
        switch(state) {
            case State.ALIVE:
                audioSource.PlayOneShot(mainEngine);
                break;
            case State.TRANSCENDING:
                audioSource.PlayOneShot(nextLevelSoundClip);
                break;
            case State.DYING:
                audioSource.PlayOneShot(deathSoundClip);
                break;
            default:
                audioSource.Stop();
                break;
        }
    }
}
