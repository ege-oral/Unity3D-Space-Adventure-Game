using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustObstacles : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Sin Wave.
        float cycles = Time.time / period; // continually growing over time.
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1.

        movementFactor = (rawSinWave + 1f) / 2; // recalculated to go from 0 to 1 so its cleaner.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
