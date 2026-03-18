using System;
using System.Collections;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed = .5f;
    [SerializeField] float movementDelay = 0;
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    float timer = 0.0f;
    float delayTimer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdatePosition(movementDelay);
    }

    void UpdatePosition(float movementDelay)
    {
        StartCoroutine(DelayAction(movementDelay));
        
    }

    IEnumerator DelayAction(float movementDelay)
    {
        yield return new WaitForSeconds(movementDelay);


        if(movementDelay > 0)
        {
            delayTimer += Time.deltaTime;
            movementFactor = Mathf.PingPong(delayTimer * speed, 1f);
            transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
        }else
        {
            movementFactor = Mathf.PingPong(timer * speed, 1f);
            transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
        }
        
    }
}
