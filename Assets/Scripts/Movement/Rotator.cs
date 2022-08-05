using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Vector3 rotationVector;
    
    private float rotationRate = 0f;

    void Update()
    {
        rotationRate += Time.deltaTime * rotationSpeed;
        transform.localRotation = Quaternion.Euler(rotationVector * rotationRate);
    }
}
