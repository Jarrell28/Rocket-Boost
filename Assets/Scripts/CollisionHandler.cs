using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour   
{
    [SerializeField] float delayInvoke;
    void OnCollisionEnter(Collision collision)
    {
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
        GetComponent<AudioSource>().enabled = false;
        Invoke("ReloadLevel", delayInvoke);
    }

    private void StartNextScene()
    {
        Debug.Log("Finished!");
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke("LoadLevel", delayInvoke);
    }
}
