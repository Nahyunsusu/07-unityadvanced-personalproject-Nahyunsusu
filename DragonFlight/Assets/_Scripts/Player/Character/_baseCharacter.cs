using System.Collections;
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
    private int   _hp                = 3;
    private float _invincibilityTime = 10000;
    private bool  _isInvincible      = false;

    // Collider
    private BoxCollider _col;

    // Save
    private Vector3 _startPosition;

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
        _startPosition = transform.position;

        _offset = transform.position - _camTransform.position;

        if (_camTransform != null) 
            _lastCamPosition = _camTransform.position;

        _col = GetComponent<BoxCollider>();
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
        if (Time.timeScale == 0) return;

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

    private void OnTriggerEnter(Collider other)
    {
        if (_isInvincible) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();

            Debug.Log("적과 충돌! 무적 시작");
            _hp--;

            StartCoroutine(InvincibilityRoutine());

            if (_hp <= 0)
            {
                Debug.Log("플레이어 사망");
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.OnPlayerDie();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_col == null)
            _col = GetComponent<BoxCollider>();
        if (_col == null) return;

        Gizmos.color = Color.green;

        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(_col.center, _col.size);
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
        float targetTilt = _moveInput.x * -30f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetTilt);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
    }

    private IEnumerator InvincibilityRoutine()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(_invincibilityTime);

        _isInvincible = false;
        Debug.Log("무적 종료");
    }

    public void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;

        _hp = 3;
        _isInvincible = false;
        StopAllCoroutines(); 

        if (_camTransform != null) _lastCamPosition = _camTransform.position;

        Debug.Log("플레이어 스탯 및 위치 초기화 완료");
    }
}