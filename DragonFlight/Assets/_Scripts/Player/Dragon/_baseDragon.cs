using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // 이 줄이 반드시 있어야 합니다!

enum BulletType
{
    Straight,
    Wide,
    Guided
}

public class _baseDragon : MonoBehaviour
{
    // 스테이터스
    protected int LV;
    protected float _speed;
    protected float _damage;

    // 투사체
    protected Muzzle[] _muzzles;

    private void Awake()
    {
        _muzzles = GetComponentsInChildren<Muzzle>();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        bool pressedJ = Keyboard.current.jKey.wasPressedThisFrame;

        if(pressedJ)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_muzzles != null)
        {
            foreach (Muzzle muzzle in _muzzles)
            {
                muzzle.LoadBullet();
            }
        }
    }
}