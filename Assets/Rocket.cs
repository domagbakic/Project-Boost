using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    AudioSource audioSource;
    Rigidbody rigidBody;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    public enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }        
	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) //ignore collisions when dead
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Boost":
                mainThrust = 3500f;
                break;
            case "Finish":      
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); // parametar treba dodat
                break;
            default:                
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f); // parametar treba dodat
                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Rotate()
    {        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; //manual control of rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume normaly physics
    }

    private void Thrust()
    {
        float thrustingThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustingThisFrame);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
