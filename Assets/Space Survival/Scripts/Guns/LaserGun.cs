using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LaserGun : Gun
{
	public Transform target;

	public override void Fire()
	{
		target = GameManager.Instance.player.targetEnemy;
		if (target == null) return; //≈∏∞Ÿ¿Ã æ¯¥Ÿ∏È √—¿ª ΩÓ¡ˆ æ ¿Ω.

		Projectile proj = PoolManager.Instance.projectilePool.Pop();
		proj.transform.position = transform.position;
		proj.transform.up = target.position - transform.position;
		proj.damage = gunData.damage;
		proj.moveSpeed = gunData.moveSpeed;
	}
}
