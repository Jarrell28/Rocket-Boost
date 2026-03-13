using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    //Fixed Update is primarily used for physics logic
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        } else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void ProcessRotation()
    {
        if (rotation.IsPressed())
        {
            float rotationInput = rotation.ReadValue<float>();
            //Rotate left
            if(rotationInput < 0)
            {   
                rb.freezeRotation = true;
                transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
                rb.freezeRotation = false;
            } 
            //Rotate Right
            else if(rotationInput > 0)
            {
                rb.freezeRotation = true;
                transform.Rotate(Vector3.back * rotationStrength * Time.fixedDeltaTime);
                rb.freezeRotation = false;
            }

        }
    }
}
