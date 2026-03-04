using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected bool  _isLaunched;
    [SerializeField] protected float _activeTime    = 0;
    [SerializeField] protected float _maxActiveTime = 3;
    [SerializeField] protected int   _speed         = 30;
    [SerializeField] protected int   _damage;

    [SerializeField] protected Vector3 _rotationAngle = new Vector3(0, 0, 500);

    private SphereCollider _col;

    public bool ActiveInHierarchy => gameObject.activeInHierarchy;

    private void Start()
    {
        _col = GetComponent<SphereCollider>();
    }

    protected virtual void Update()
    {
        if (!_isLaunched) return;

        Move();

        _activeTime += Time.deltaTime;

        if (_activeTime >= _maxActiveTime)
        {
            ReturnToPool();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 상대방(Enemy)의 컴포넌트를 가져와 데미지 입히기
            // Enemy enemy = other.GetComponent<Enemy>();
            // if(enemy != null) enemy.TakeDamage(_damage);

            // 3. 충돌했으므로 총알은 풀로 반납
            ReturnToPool();
        }
    }

    public virtual void Launch()
    {
        _isLaunched = true;
        _activeTime = 0;
    }

    protected abstract void Move();

    protected void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }

    public void SetActive(bool setActive)
    {
        gameObject.SetActive(setActive);

        if (!setActive)
        {
            _isLaunched = false;
            _activeTime = 0;
        }
    }
}
