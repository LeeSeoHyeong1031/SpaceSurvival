using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	//public float maxHp = 10f //�ϼ�
	private float maxHp;
	public float hp = 10f; //ü��
	public float damage = 10f; //���ݷ�
	public float moveSpeed = 3f; //�̵� �ӵ�

	public float attackInterval = 0.5f; //���� ����
	public float lastAttackTime = 0f; //������ ���� �ð�

	//�ʰ��
	public float hpAmount { get { return hp / maxHp; } } //���� ���Ǵ� �׸��� ������Ƽ�� �����

	private Transform target; //������ ���

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
		GameManager.Instance.enemies.Add(this);//�� ����Ʈ�� �ڱ� �ڽ��� Add
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

	private void Move(Vector2 dir) //dir ���� Ŀ���� 1�� ������ �ϰ� ���� ���.
	{
		rb.MovePosition(rb.position + (dir * moveSpeed * Time.fixedDeltaTime));
	}
	//OnHit, TakeDamage ���� �̷��� ��(�޼��� ����)
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
		GameManager.Instance.enemies.Remove(this); //������ ����Ʈ���� ����
		GameManager.Instance.player.enemyKills++; //ų����
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
