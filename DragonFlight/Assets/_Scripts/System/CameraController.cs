using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private CinemachineSplineDolly _splineDolly;
    private float _initialDollyPosition;

    [Header("Speed Settings")]
    public float currentSpeed = 0f;
    public float acceleration = 5;
    public float maxSpeed     = 200f;

    private void Awake()
    {
        Instance = this;

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