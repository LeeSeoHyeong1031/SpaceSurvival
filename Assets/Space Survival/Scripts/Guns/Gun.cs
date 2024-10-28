using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GunData gunData;
	public float fireInterval;
	public int level = 0;

	public virtual void Start()
	{
		StartCoroutine(FireCoroutine());
	}
	private IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(fireInterval);
			Fire();
		}
	}
	public virtual void Fire() { }
}
