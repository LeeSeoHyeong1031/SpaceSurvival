using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotgun : Gun
{
	public Transform target;
	public Transform[] shotPoints;

	public override void Fire()
	{
		target = GameManager.Instance.player.targetEnemy;
		if (target == null) return; // 타겟이 없다면 총을 쏘지 않음.

		List<Projectile> projectiles = new List<Projectile>();

		// 먼저 모든 발사체를 생성하고 위치와 방향을 설정
		foreach (Transform shotPoint in shotPoints)
		{
			Projectile proj = PoolManager.Instance.projectilePool.Pop();
			proj.transform.position = shotPoint.position;
			proj.transform.up = shotPoint.up;
			projectiles.Add(proj);
		}

		// 모든 발사체에 데미지와 속도를 설정하여 동시에 발사되도록 함
		foreach (Projectile proj in projectiles)
		{
			proj.damage = gunData.damage;
			proj.moveSpeed = gunData.moveSpeed;
		}
	}
}
