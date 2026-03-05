using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [System.Serializable]
    public struct BulletInfo
    {
        public Bullet    Prefab;
        public string    bulletName;
        public int       poolSize;
        public Transform firePoint;
    }

    [SerializeField] private List<BulletInfo> _bulletInfo;

    private List<List<Bullet>> _pools = new List<List<Bullet>>();

    private void Awake()
    {
        for (int i = 0; i < _bulletInfo.Count; i++)
        {
            List<Bullet> newPool = new List<Bullet>();

            for (int j = 0; j < _bulletInfo[i].poolSize; j++)
            {
                if (_bulletInfo[i].Prefab == null) continue;

                Bullet temp = Instantiate(_bulletInfo[i].Prefab);
                temp.SetActive(false);
                newPool.Add(temp);
            }
            _pools.Add(newPool);
        }
    }

    public void LoadBullet(int damage, int bulletIndex)
    {
        if (bulletIndex < 0 || bulletIndex >= _pools.Count) return;

        List<Bullet> targetPool = _pools[bulletIndex];
        BulletInfo info = _bulletInfo[bulletIndex];

        Bullet _curBullet = GetPooledBullet(targetPool, info.Prefab);

        if (_curBullet != null)
        {
            _curBullet.transform.position = info.firePoint.position;
            _curBullet.transform.rotation = info.firePoint.rotation;

            _curBullet.SetActive(true);
            _curBullet.Launch(damage);
        }
    }

    private Bullet GetPooledBullet(List<Bullet> pool, Bullet prefab)
    {
        foreach (Bullet bullet in pool)
        {
            if (!bullet.ActiveInHierarchy) return bullet;
        }

        Bullet newBullet = Instantiate(prefab);
        newBullet.SetActive(false);
        pool.Add(newBullet);
        return newBullet;
    }
}