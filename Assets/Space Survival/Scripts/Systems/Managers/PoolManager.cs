using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : SingletonManager<PoolManager>
{
    public List<Transform> gameObjectPools;


    public ObjectPool<Projectile> projectilePool;
    public Projectile projectilePrefab;

    public ObjectPool<Enemy> enemyPool;
    public Enemy enemyPrefab;



    protected override void Awake()
    {
        base.Awake();
        GameObject newGO;
        newGO = new GameObject("Projectile");
        newGO.transform.SetParent(transform);
        gameObjectPools.Add(newGO.transform);

        projectilePool = new();
        projectilePool.prefab = projectilePrefab;


        newGO = new GameObject("Enemy");
        newGO.transform.SetParent(transform);
        gameObjectPools.Add(newGO.transform);

        enemyPool = new ObjectPool<Enemy>();
        enemyPool.prefab = enemyPrefab;
    }

    //private IEnumerator Delay(T item, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Push(item);
    //}
}
