using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCtrl : Gun
{
	public Missile missilePrefab;


	public override void Fire()
	{
		Missile missile = Instantiate(missilePrefab);
		missile.damage = gunData.damage;
		missile.moveSpeed = gunData.moveSpeed;
	}
}
