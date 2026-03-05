using UnityEngine;

public class Bullet_Straight : Bullet
{
    protected override void Move()
    {
        Vector3 bulletMove = Vector3.forward * _speed * Time.deltaTime;

        float camSpeed = 0;
        if (CameraController.Instance != null)
        {
            camSpeed = CameraController.Instance.currentSpeed;
        }

        Vector3 cameraMove = Vector3.forward * camSpeed * Time.deltaTime;

        transform.Translate(bulletMove, Space.Self);
        transform.position += cameraMove;

        transform.Rotate(_rotationAngle * Time.deltaTime);
    }
}