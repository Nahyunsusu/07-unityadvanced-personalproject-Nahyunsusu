using UnityEngine;

public class Lv3_Chase : Enemy
{
    private Vector3 _dir;

    public override void Init(float hp, float speed, float lifeTime)
    {
        base.Init(hp, speed, lifeTime);

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector3 targetPos = player.transform.position;
            _dir = (targetPos - transform.position).normalized;

            transform.LookAt(targetPos);
        }
        else
        {
            _dir = Vector3.back;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (gameObject.activeSelf)
        {
            transform.Translate(_dir * _speed * Time.deltaTime, Space.World);
        }
    }
}