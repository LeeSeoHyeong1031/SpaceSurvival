using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float maxHp; //최대 체력
	public float hp = 100f;
	public float damage = 5f; //공격력
	public float moveSpeed = 5f; //이동속도
	public int enemyKills = 0;
	public float exp = 0;
	public float maxExp = 1000;
	public int level = 1;
	public float expValue { get { return exp / maxExp; } }

	public bool dead { get; private set; } //플레이어 죽음 여부

	public Projectile projectilePrefab; //투사체 프리팹
	public Animator tailAnimCtrl; //꼬리 컨트롤러
	public Animator playerAnimCtrl; //플레이어 컨트롤러

	private Rigidbody2D rb;
	private Scanner scanner;

	public float hpValue { get { return hp / maxHp; } }


	private Transform moveDir;
	private Transform fireDir;

	public Transform targetEnemy = null;

	private void Awake()
	{
		moveDir = transform.Find("MoveDir");
		fireDir = transform.Find("FireDir");
		maxHp = hp;
		dead = false;
		rb = GetComponent<Rigidbody2D>();
		scanner = GetComponent<Scanner>();
	}

	void Start()
	{
		GameManager.Instance.player = this;
	}
	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector2 moveDir = new Vector2(x, y);

		//꼬리 Anim
		tailAnimCtrl.SetBool("IsMoving", moveDir.magnitude > 0.1f);

		targetEnemy = scanner.GetNearestTarget();

		Vector2 fireDir = Vector2.zero;
		if (targetEnemy != null)
		{
			fireDir = targetEnemy.transform.position - transform.position;
		}

		Move(moveDir);
		if (moveDir.magnitude > 0.1f) this.moveDir.up = moveDir;
		this.fireDir.up = fireDir;
	}
	/// <summary>
	///Transform을 통해 게임 오브젝트를 움직이는 메서드.
	///RigidBody2D에 있는 MovePosition으로 변경.
	/// </summary>
	/// <param name="dir">이동 방향</param>
	public void Move(Vector2 dir)
	{
		rb.MovePosition(rb.position + (dir * moveSpeed * Time.fixedDeltaTime));
	}
	public void TakeDamage(float damage)
	{
		hp -= damage;
		playerAnimCtrl.SetTrigger("TakeDamage");
		if (hp <= 0)
		{
			hp = 0;
			dead = true;
			UIManager.Instance.GameOverUI.SetActive(true);
			gameObject.SetActive(false);
		}
		UIManager.Instance.UpdateHP();
	}

	//아이템 관련 함수
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Item"))
		{
			IItem item = collision.GetComponent<IItem>();
			if (item != null)
			{
				item.Use();
			}
		}
	}

	public void TakeHeal(float heal)
	{
		hp += heal;
		if (hp > maxHp)
		{
			hp = maxHp;
		}
	}
}