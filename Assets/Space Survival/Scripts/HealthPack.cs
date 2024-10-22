using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
	public void Use()
	{
		//체력회복 +50 하고 싶음.
		GameManager.Instance.player.maxHp += 50;
		GameManager.Instance.player.hp += 50;
		Destroy(gameObject);
	}
}
