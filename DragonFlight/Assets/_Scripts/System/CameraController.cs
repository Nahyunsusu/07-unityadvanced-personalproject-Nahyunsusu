using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineSplineDolly _splineDolly;

    [Header("Speed Settings")]
    public float currentSpeed = 0f;
    public float acceleration = 1;
    public float maxSpeed     = 20f;

    private void Awake()
    {
        _splineDolly = GetComponent<CinemachineSplineDolly>();
    }

    private void Update()
    {
        if (_splineDolly == null) return;

        currentSpeed = Mathf.Min(currentSpeed + (acceleration * Time.deltaTime), maxSpeed);

        _splineDolly.CameraPosition += currentSpeed * Time.deltaTime;
    }
}