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
		//GameObject newGO;
		//newGO = new GameObject("Bullet");
		//newGO.transform.SetParent(transform);
		//gameObjectPools.Add(newGO.transform);

		projectilePool = new();
		projectilePool.prefab = projectilePrefab;


		//newGO = new GameObject("Enemy");
		//newGO.transform.SetParent(transform);
		//gameObjectPools.Add(newGO.transform);

		enemyPool = new ObjectPool<Enemy>();
		enemyPool.prefab = enemyPrefab;
	}

	public void Push<T>(T item, float delay) where T : MonoBehaviour
	{
		StartCoroutine(DelayPush(item, delay));
	}

	private IEnumerator DelayPush<T>(T item, float delay) where T : MonoBehaviour
	{
		yield return new WaitForSeconds(delay);

		if (typeof(T) == typeof(Projectile))
		{
			projectilePool.Push(item as Projectile);
		}
		else if (typeof(T) == typeof(Enemy))
		{
			enemyPool.Push(item as Enemy);
		}
	}
}
