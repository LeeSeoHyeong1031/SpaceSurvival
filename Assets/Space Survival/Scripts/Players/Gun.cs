using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class Gun : ScriptableObject
{
	public float damage; // 공격력
	public float moveSpeed; //이동속도
	public float fireInterval; //발사 간격
}
