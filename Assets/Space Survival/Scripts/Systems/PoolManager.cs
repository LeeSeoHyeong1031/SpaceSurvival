using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
	private static PoolManager instance;
	public static PoolManager Instance => instance;

	public List<Transform> gameObjectPools;


	public ObjectPool<Projectile> projectilePool;
	public Projectile projectilePrefab;

	public ObjectPool<Enemy> enemyPool;
	public Enemy enemyPrefab;



	private void Awake()
	{
		if (instance != null)
		{
			DestroyImmediate(this);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

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
}
