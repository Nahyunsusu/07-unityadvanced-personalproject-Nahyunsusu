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
    protected int _muzzleNum;

    private float _fireRate = 0.2f; // 발사 간격 (초)
    private float _lastFireTime;

    // Collider
    private BoxCollider _boxCol;
    public BoxCollider BoxCol => _boxCol;

    private void Awake()
    {
        _muzzles = GetComponentsInChildren<Muzzle>();
        _muzzleNum = 1;
    }

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        bool holdingJ = Keyboard.current.jKey.isPressed;

        if (holdingJ && Time.time >= _lastFireTime + _fireRate)
        {
            Shoot();
            _lastFireTime = Time.time; 
        }
    }

    private void Shoot()
    {
        if (_muzzles != null)
        {
            for(int i=0;i<_muzzleNum;i++)
            {
                _muzzles[i].LoadBullet();
            }
        }
    }

    private void PlusMuzzle()
    {
        _muzzleNum++;
    }

}