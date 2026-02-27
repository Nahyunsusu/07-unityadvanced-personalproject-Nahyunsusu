using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class _baseCharacter : MonoBehaviour
{

    _baseDragon _dragon;

    // Camera
    public Transform camTransform; // 시네머신 카메라
    private Vector3 offset;
    private Vector3 lastCamPosition;

    // Move Component
    private CharacterController _controller;
    private InputAction moveAction;

    private Vector2 moveInput;
    private float speed = 15f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions["Move"];    
    }

    private void Start()
    {
        offset = transform.position - camTransform.position;
        if (camTransform != null) lastCamPosition = camTransform.position;
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled  += MoveCancel;

    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled  -= MoveCancel;

    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        Vector3 playerMove = transform.right * moveInput.x + transform.forward * moveInput.y;
        playerMove *= speed * Time.deltaTime;

        Vector3 cameraDelta = Vector3.zero;
        if (camTransform != null)
        {
            cameraDelta = camTransform.position - lastCamPosition;
            lastCamPosition = camTransform.position;
        }

        _controller.Move(playerMove + cameraDelta);
    }

    void LateUpdate()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
    void MoveCancel(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }
}