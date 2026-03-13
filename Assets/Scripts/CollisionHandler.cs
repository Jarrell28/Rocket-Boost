using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Picked up Fuel!");
                break;
            case "Finish":
                Debug.Log("Finished!");
                break;
            case "Friendly":
                Debug.Log("Game Start!");
                break;
            default:
                Debug.Log("Crashed!");
                ReloadLevel();
                break;
        }
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
