using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            PooledBullet pooledBullet = pooledBullets.Find(items => !items.isUsed);
            if(pooledBullet != null)
            {
                pooledBullet.isUsed = true;
                return pooledBullet.bullet;
            }
            return CreateNewPooledBullet();
        }

        public BulletController CreateNewPooledBullet()
        {
            PooledBullet bullet = new PooledBullet();
            bullet.bullet = new BulletController(bulletView, bulletScriptableObject);
            bullet.isUsed = true;
            return bullet.bullet;
        }

        public class PooledBullet
        {
            public BulletController bullet;
            public bool isUsed;
        }
    }
}
