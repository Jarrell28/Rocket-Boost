using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour   
{
    [SerializeField] float delayInvoke;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip stageCompleteSound;
    [SerializeField] ParticleSystem stageCompleteParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    bool isPlayable = true;
    bool isCollidable = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!isPlayable) return;
        if(!isCollidable) return;
        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartNextScene();
                break;
            case "Friendly":
                Debug.Log("Game Start!");
                break;
            default:
                StartCrashScene();
                break;
        }
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadLevel()
    {
        //Simple solution
        // int currentScene = SceneManager.GetActiveScene().buildIndex;
        // if(currentScene != 2)
        // {
        //     SceneManager.LoadScene(currentScene + 1);
        // } else
        // {
        //     SceneManager.LoadScene(0);
        // }

        //More organized solution
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void StartCrashScene()
    {
        Debug.Log("Crashed!");
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        isPlayable = false;
        
        Invoke("ReloadLevel", delayInvoke);
    }

    private void StartNextScene()
    {
        Debug.Log("Finished!");
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(stageCompleteSound);
        stageCompleteParticles.Play();
        isPlayable = false;

        Invoke("LoadLevel", delayInvoke);
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown("l")) LoadLevel();
        if(Input.GetKeyDown("c")) isCollidable = !isCollidable;
    }
       
}
