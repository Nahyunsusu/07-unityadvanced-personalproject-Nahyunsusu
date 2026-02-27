using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected bool      _isLaunched;
    [SerializeField] protected float     _activeTime    = 0;
    [SerializeField] protected float     _maxActiveTime = 3;
    [SerializeField] protected int       _speed         = 30;
    [SerializeField] protected int       _damage;

    [SerializeField] protected Vector3 _rotationAngle = new Vector3(0, 0, 500);

    public bool ActiveInHierarchy => gameObject.activeInHierarchy;

    public virtual void Launch()
    {
        _isLaunched = true;
        _activeTime = 0;
    }

    protected virtual void Update()
    {
        if (!_isLaunched) return;

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        _activeTime += Time.deltaTime;

        if (_activeTime >= _maxActiveTime)
        {
            ReturnToPool();
            return;
        }
    }

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
