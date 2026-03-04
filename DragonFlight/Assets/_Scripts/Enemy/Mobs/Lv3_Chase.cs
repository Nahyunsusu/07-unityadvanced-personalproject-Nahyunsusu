using UnityEngine;

public class Lv3_Curve : Enemy
{
    private float _horizontalDirection = 1f;
    [SerializeField] private float _curveSpeed = 5f;

    protected override void Update()
    {
        base.Update();

        if (gameObject.activeSelf)
        {
            Vector3 moveVec = Vector3.back * _speed * Time.deltaTime;

            moveVec += Vector3.right * _horizontalDirection * _curveSpeed * Time.deltaTime;

            transform.Translate(moveVec, Space.World);

            CheckSide();
        }
    }

    private void CheckSide()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x <= 0.05f)
        {
            _horizontalDirection = 1f; // 오른쪽으로 튕김
        }
        else if (viewPos.x >= 0.95f)
        {
            _horizontalDirection = -1f; // 왼쪽으로 튕김
        }
    }
}
