using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class Gun : ScriptableObject
{
	public float damage; // ���ݷ�
	public float moveSpeed; //�̵��ӵ�
	public float fireInterval; //�߻� ����
}
