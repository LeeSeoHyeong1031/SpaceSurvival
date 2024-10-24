using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
	private List<T> pool = new List<T>();
	public T prefab;

	public void Push(T item)
	{
		pool.Add(item);
		for (int i = 0; i < PoolManager.Instance.gameObjectPools.Count; i++)
		{
			if (item.name == PoolManager.Instance.gameObjectPools[i].name)
				item.transform.SetParent(PoolManager.Instance.gameObjectPools[i], false);
		}
		item.gameObject.SetActive(false);
	}

	//MonoBehaviour를 상속 받고 있지 않기 떄문에 StartCoroutine을 사용할 수 없다. 
	//그래서 파티클 보류. 고칠 방법이 떠오르지 않음.
	//public void Push(T item, float delay)
	//{
	//	pool.Add(item);
	//	for (int i = 0; i < PoolManager.Instance.gameObjectPools.Count; i++)
	//	{
	//		if (item.name == PoolManager.Instance.gameObjectPools[i].name)
	//			item.transform.SetParent(PoolManager.Instance.gameObjectPools[i], false);
	//	}
	//	StartCoroutine(Delay(item, delay));
	//}

	//private IEnumerator Delay(T item, float delay)
	//{
	//	yield return new WaitForSeconds(delay);
	//	item.gameObject.SetActive(false);
	//}

	public T Pop()
	{
		T res = null;
		if (pool.Count <= 0)
		{
			res = Object.Instantiate(prefab);
			pool.Add(res);
		}
		res = pool[0];
		pool.Remove(res);
		return res;
	}
}