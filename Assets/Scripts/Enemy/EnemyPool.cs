using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyView;
        private List<PooledEnemy> pooledEnemy = new List<PooledEnemy>();
        private EnemyData enemyData;


        public EnemyPool(EnemyView enemyView, EnemyData enemyData)
        {
            this.enemyView = enemyView;
            this.enemyData = enemyData;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemy.Count > 0)
            {
                PooledEnemy enemy = pooledEnemy.Find(items => !items.isUsed);
                if(enemy != null)
                {
                    enemy.isUsed = true;
                    return enemy.enemy;
                }
                
            }
            return  CreateNewPooledEnemy();
        }

        public void ReturnEnemy(EnemyController enemyToReturn)
        {
            PooledEnemy pooledEnemy = this.pooledEnemy.Find(items => items.enemy.Equals(enemyToReturn));
            pooledEnemy.isUsed = false;
        }

        public EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy newEnemy = new PooledEnemy();
            newEnemy.enemy = new EnemyController(enemyView,enemyData);
            newEnemy.isUsed = true;
            pooledEnemy.Add(newEnemy);
            return newEnemy.enemy;
        }

        public class PooledEnemy
        {
            public EnemyController enemy;
            public bool isUsed;
        }
    }
}
