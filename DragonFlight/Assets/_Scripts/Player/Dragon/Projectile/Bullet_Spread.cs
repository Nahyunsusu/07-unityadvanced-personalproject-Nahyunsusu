using UnityEngine;

public class Bullet_Spread : Bullet
{
    protected override void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        ApplyCameraMovement();
    }
}
