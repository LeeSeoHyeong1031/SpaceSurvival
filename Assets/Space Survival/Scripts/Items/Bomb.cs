using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
	public ParticleSystem impactParticle;
	public void Use()
	{
		print("ÆøÅº ½ÀµæÇÔ.");
		GameManager.Instance.RemoveAllEnemies();
		var particle = Instantiate(impactParticle, transform.position, Quaternion.identity);
		particle.Play();
		Destroy(particle.gameObject, 2f);
		Destroy(gameObject); // ÆøÅº ¿ÀºêÁ§Æ® ÆÄ±«
	}
}
