using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Fuel":
                break;
            case "Finished":
                break;
            case "Friendly":
                break;
            default:
                break;
        }
    }
}
