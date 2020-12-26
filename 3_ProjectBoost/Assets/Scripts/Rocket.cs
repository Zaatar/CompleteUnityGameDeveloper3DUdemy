using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;
    [SerializeField]
    AudioClip mainEngine;
    float loadSceneTimer = 1f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {
        ALIVE, DYING, TRANSCENDING
    }
    State state = State.ALIVE;
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
            Rotate();
        }
    }

    void OnCollisionEnter(Collision collision){
        if(state != State.ALIVE)
            return;

        switch(collision.gameObject.tag)
        {
            case "Friendly": 
                print("OK");
                break;
            case "Finish":
                state = State.TRANSCENDING;
                Invoke("LoadNextScene", loadSceneTimer);
                state = State.ALIVE;
                break;
            default:
                state = State.DYING;
                Invoke("LoadFirstScene", loadSceneTimer);
                break;
        }
    }

    private void RespondToThrustInput(){
        if(Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            audioSource.Stop();
        }
    }

    private void ApplyThrust(){
        float thrustVelocity = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrustVelocity);
            if(!audioSource.isPlaying && state == State.ALIVE)
                audioSource.PlayOneShot(mainEngine);
                print("Playing main engine");
    }

    private void Rotate() {
        rigidBody.freezeRotation = true; // take manual control of rotation;
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if(Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

    private void LoadNextScene(){
        if(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex+1) != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadFirstScene(){
        SceneManager.LoadScene(0);
    }
}
