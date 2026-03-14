using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 1000f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    // [SerializeField] float boosterDelay = .7f;
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
            StartThrust();
        } else
        {
           StopThrust();

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
                RotateLeft();
            } 
            //Rotate Right
            else if(rotationInput > 0)
            {
                RotateRight();
            }

        } else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        mainBooster.Play();
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
    }

    private void StopThrust()
    {
        mainBooster.Stop();
        if (audioSource.isPlaying) audioSource.Stop();
    }

    private void RotateLeft()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
        rightBooster.Play();
    }

    private void RotateRight()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
        leftBooster.Play();
    }
}
