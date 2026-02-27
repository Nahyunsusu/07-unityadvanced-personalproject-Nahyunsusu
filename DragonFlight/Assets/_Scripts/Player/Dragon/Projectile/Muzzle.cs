using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private Bullet    _bullet;
    [SerializeField] private int       _bulletPoolSize = 50;
    [SerializeField] private Transform _FirePoint;

    private List<Bullet> _bullets = new List<Bullet>();

    private void Awake()
    {
        for (int i = 0; i < _bulletPoolSize; i++)
        {
            Bullet tempObejct = Instantiate(_bullet);
            tempObejct.SetActive(false);
            _bullets.Add(tempObejct);
        }
    }

    public void LoadBullet()
    {
        Bullet _curBullet = GetPooledBullet();

        if (_curBullet != null)
        {
            _curBullet.transform.position = _FirePoint.position;
            _curBullet.transform.rotation = _FirePoint.rotation;
            _curBullet.SetActive(true);

            Bullet bullet = _curBullet.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Launch();
            }
        }
    }

    private Bullet GetPooledBullet()
    {
        foreach (Bullet bullet in _bullets)
        {
            if (!bullet.ActiveInHierarchy)
            {
                return bullet;
            }
        }

        Bullet newRocket = Instantiate(_bullet);
        newRocket.SetActive(false);
        _bullets.Add(newRocket);
        return newRocket;
    }
}