using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class _baseCharacter : MonoBehaviour
{
    _baseDragon _dragon;

    // Camera
    public Transform _camTransform; // 시네머신 카메라
    private Vector3 _offset;
    private Vector3 _lastCamPosition;

    private Vector3 _cameraDelta;
    public Vector3 CameraDelta => _cameraDelta;

    // Move Component
    private CharacterController _controller;
    private InputAction _moveAction;

    private Vector2 _moveInput;
    private float _speed = 30f;

    // Stat
    private float _invincibilityTime = 2;

    //////////////////////////////////////////////////////

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions["Move"];

        if (GameObject.FindWithTag("MainCamera") != null)
        {
            _camTransform = GameObject.FindWithTag("MainCamera").transform;
        }

    }

    private void Start()
    {
        _offset = transform.position - _camTransform.position;

        if (_camTransform != null) 
            _lastCamPosition = _camTransform.position;
    }

    private void OnEnable()
    {
        _moveAction.performed += OnMove;
        _moveAction.canceled  += MoveCancel;
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled  -= MoveCancel;
    }

    private void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        Move();

        Rotate();
    }

    void LateUpdate()
    {
        float currentY = transform.position.y;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        Vector3 targetWorldPos = Camera.main.ViewportToWorldPoint(viewPos);

        targetWorldPos.y = currentY;

        transform.position = targetWorldPos;
    }

    

    //////////////////////////////////////////////////////
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }
    void MoveCancel(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    private void Move()
    {
        // Move
        Vector3 playerMove = Vector3.right * _moveInput.x + Vector3.forward * _moveInput.y;
        playerMove *= _speed * Time.deltaTime;

        _cameraDelta = Vector3.zero;
        if (_camTransform != null)
        {
            _cameraDelta     = _camTransform.position - _lastCamPosition;

            _cameraDelta.y = 0;

            _lastCamPosition = _camTransform.position;
        }

        _controller.Move(playerMove + _cameraDelta);
    }

    private void Rotate()
    {
        // Rotate
        float targetTilt = _moveInput.x * -30f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
    }
}