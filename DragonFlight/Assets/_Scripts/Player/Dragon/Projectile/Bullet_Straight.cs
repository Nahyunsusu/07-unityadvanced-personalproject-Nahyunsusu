using UnityEngine;

public class Bullet_Straight : Bullet
{
    protected override void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
