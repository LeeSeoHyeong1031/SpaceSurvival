using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//투사체
public class Projectile : MonoBehaviour
{
	public float damage = 10; //데미지
	public float moveSpeed = 5; //이동속도
	public float duration = 3; //지속시간

	public ParticleSystem impactParticle;
	void OnEnable()
	{
		PoolManager.Instance.Push(this, duration);
	}
	void Update()
	{
		Move(Vector2.up);
		//Physics2D.OverlapCircle();
	}
	public void Move(Vector2 dir)
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().TakeDamage(damage);
			var particle = Instantiate(impactParticle, other.transform.position, Quaternion.identity);
			particle.Play();
			Destroy(particle.gameObject, 2f);

			PoolManager.Instance.projectilePool.Push(this);
		}
	}
}
