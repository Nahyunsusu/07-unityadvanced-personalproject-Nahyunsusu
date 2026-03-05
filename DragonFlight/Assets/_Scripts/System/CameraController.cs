using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineSplineDolly _splineDolly;
    private float _initialDollyPosition;

    [Header("Speed Settings")]
    public float currentSpeed = 0f;
    public float acceleration = 1;
    public float maxSpeed     = 20f;

    private void Awake()
    {
        _splineDolly = GetComponent<CinemachineSplineDolly>();

        if (_splineDolly != null)
            _initialDollyPosition = _splineDolly.CameraPosition;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return; // 정지 상태면 카메라도 멈춤

        if (_splineDolly == null) return;

        currentSpeed = Mathf.Min(currentSpeed + (acceleration * Time.deltaTime), maxSpeed);

        _splineDolly.CameraPosition += currentSpeed * Time.deltaTime;
    }

    public void ResetCamera()
    {
        currentSpeed = 0f;
        if (_splineDolly != null)
        {
            _splineDolly.CameraPosition = _initialDollyPosition;
        }
        Debug.Log("카메라 위치 및 속도 초기화 완료");
    }
}