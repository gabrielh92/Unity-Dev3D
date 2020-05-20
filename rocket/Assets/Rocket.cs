using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 25f;

    [SerializeField] AudioClip thrustSfx;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] AudioClip finishSfx;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] float levelLoadDelay = 2.5f;

    enum State { ALIVE, DEAD, TRANSITION };

    Rigidbody rigidbody;
    AudioSource audioSource;
    State state = State.ALIVE;
    bool canCollide = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(state == State.ALIVE){
            Thrust();
            Move();
        }

        if(Debug.isDebugBuild) {
            RespondToDebugKeys();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(state != State.ALIVE || !canCollide) return;
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                state = State.TRANSITION;
                audioSource.Stop();
                audioSource.PlayOneShot(finishSfx);
                finishParticles.Play();
                Invoke("OnFinish", levelLoadDelay);
                break;
            default:
                state = State.DEAD;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSfx);
                deathParticles.Play();
                Invoke("OnDeath", levelLoadDelay);
                break;
        }
    }

    private void OnFinish() {
        LoadNextLevel();
    }

    private void OnDeath() {
        LoadCurrentLevel();
    }

    private void LoadNextLevel() {
        int nextSceneIdx = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIdx == SceneManager.sceneCountInBuildSettings) nextSceneIdx = 0;
        SceneManager.LoadScene(nextSceneIdx);
    }

    private void LoadCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Move() {
        rigidbody.angularVelocity = Vector3.zero; // take manual control of rotation
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) {
            transform.Rotate(rotationSpeed * Vector3.forward);
        } else if(Input.GetKey(KeyCode.D)) {
            transform.Rotate(rotationSpeed * -Vector3.forward);
        }
    }

    private void Thrust() {
        if(Input.GetKey(KeyCode.Space)) {
            rigidbody.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(thrustSfx);
            }
            thrustParticles.Play();
        } else {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void RespondToDebugKeys() {
        if(Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C)) {
            canCollide = !canCollide;
        }
    }
}
