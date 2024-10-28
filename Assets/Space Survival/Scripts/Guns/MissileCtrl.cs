using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCtrl : Gun
{
	public Missile missilePrefab;

	public override void Start()
	{
		base.Start();
		fireInterval = gunData.fireInterval[0];
	}

	public override void Fire()
	{
		Missile missile = Instantiate(missilePrefab);
		missile.damage = gunData.damage[level];
		missile.moveSpeed = gunData.moveSpeed;
	}
}
