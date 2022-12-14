using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVector;

    Vector3 startingPos;
    float movementFactor;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //continiously growing over time
        const float tau = Mathf.PI * 2; // constant of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f; //recalculate to go from 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
