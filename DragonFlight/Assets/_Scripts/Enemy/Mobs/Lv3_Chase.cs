using UnityEngine;

public class Lv3_Curve : Enemy
{
    protected override void Update()
    {
        base.Update();

        if (gameObject.activeSelf)
        {
            Vector3 moveVec = Vector3.back * _speed * Time.deltaTime;

            transform.Translate(moveVec, Space.World);
        }
    }
}
