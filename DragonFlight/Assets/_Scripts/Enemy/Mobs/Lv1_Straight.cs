using UnityEngine;

public class Lv1_Straight : Enemy
{
    protected override void Update()
    {
        base.Update();

        if (gameObject.activeSelf)
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime, Space.World);
        }
    }
}
