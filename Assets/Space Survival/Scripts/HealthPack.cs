using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
	public void Use()
	{
		//ü��ȸ�� +50 �ϰ� ����.
		GameManager.Instance.player.maxHp += 50;
		GameManager.Instance.player.hp += 50;
		Destroy(gameObject);
	}
}
