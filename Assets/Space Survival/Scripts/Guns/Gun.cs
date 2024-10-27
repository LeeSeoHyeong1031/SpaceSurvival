using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public GunData gunData;

	private void Start()
	{
		StartCoroutine(FireCoroutine());
	}
	private IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(gunData.fireInterval);
			Fire();
		}
	}
	public virtual void Fire() { }
}
