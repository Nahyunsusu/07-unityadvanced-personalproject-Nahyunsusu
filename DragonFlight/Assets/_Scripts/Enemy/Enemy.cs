using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int   _score;

    public virtual void Init(float hp, float speed)
    {
        _hp = hp;
        _speed = speed;
    }

    protected virtual void Update()
    {
        if (transform.position.z < -10f) 
            ReturnToPool();
    }

    protected void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}