using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
	public float healAmount = 0f;
	public ParticleSystem impactParticle;
	public void Use()
	{
		//ü��ȸ�� +50 �ϰ� ����.
		print("ȸ���� ������.");
		GameManager.Instance.player.TakeHeal(healAmount);
		var particle = Instantiate(impactParticle, transform.position, Quaternion.identity);
		particle.Play();
		Destroy(particle.gameObject, 2f);
		Destroy(gameObject);
	}
}
