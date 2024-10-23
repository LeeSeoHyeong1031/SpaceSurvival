using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
	public ParticleSystem impactParticle;
	public void Use()
	{
		print("��ź ������.");
		GameManager.Instance.RemoveAllEnemies();
		var particle = Instantiate(impactParticle, transform.position, Quaternion.identity);
		particle.Play();
		Destroy(particle.gameObject, 2f);
		Destroy(gameObject); // ��ź ������Ʈ �ı�
	}
}
