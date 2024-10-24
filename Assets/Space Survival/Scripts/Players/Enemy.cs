using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//public float maxHp = 10f //하수
	private float maxHp;
	public float hp = 10f; //체력
	public float damage = 10f; //공격력
	public float moveSpeed = 3f; //이동 속도

	public float attackInterval = 0.5f; //공격 간격
	public float lastAttackTime = 0f; //마지막 공격 시간

	//초고수
	public float hpAmount { get { return hp / maxHp; } } //자주 계산되는 항목은 프로퍼티로 만들기

	private Transform target; //추적할 대상

	public Image hpBar;
	private Rigidbody2D rb;
	private Collider2D cd;
	public ParticleSystem impaceParticle;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cd = GetComponent<Collider2D>();
	}
	void OnEnable()
	{
		GameManager.Instance.enemies.Add(this);//적 리스트에 자기 자신을 Add
		cd.enabled = true;
		hp = 10f;
		maxHp = hp;
	}
	private void Start()
	{
		target = GameManager.Instance.player.transform;
	}

	void Update()
	{
		if (GameManager.Instance.player.dead == true) return;
		Vector2 moveDir = target.position - transform.position;
		Move(moveDir.normalized);

		hpBar.fillAmount = hpAmount;
	}

	private void Move(Vector2 dir) //dir 값이 커져도 1로 고정을 하고 싶은 경우.
	{
		rb.MovePosition(rb.position + (dir * moveSpeed * Time.fixedDeltaTime));
	}
	//OnHit, TakeDamage 보통 이렇게 씀(메서드 네임)
	public void TakeDamage(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		GameManager.Instance.enemies.Remove(this); //적관리 리스트에서 제거
		GameManager.Instance.player.enemyKills++; //킬누적
		cd.enabled = false;
		PoolManager.Instance.enemyPool.Push(this);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (Time.time >= lastAttackTime)
		{
			lastAttackTime = Time.time + attackInterval;
			if (other.CompareTag("Player"))
			{
				GameManager.Instance.player.TakeDamage(damage);
				var particle = Instantiate(impaceParticle, other.transform.position, Quaternion.identity);
				particle.Play();
				Destroy(particle, 2f);
			}
		}
	}
}
