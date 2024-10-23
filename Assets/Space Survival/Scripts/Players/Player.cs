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
	public float fireInterval = 1f; //공격간격
	public bool isFire; //적이 있을 때만 true

	public bool dead { get; private set; } //플레이어 죽음 여부

	public Projectile projectilePrefab; //투사체 프리팹
	public Animator tailAnimCtrl; //꼬리 컨트롤러
	public Animator playerAnimCtrl; //플레이어 컨트롤러
	private Rigidbody2D rb;

	[Header("PlayerInfo 관련 변수")]
	public Slider playerHpBar; //플레이어 hpBar
	public TextMeshProUGUI enemyKillsUI;
	public TextMeshProUGUI hpText;

	public float hpValue { get { return hp / maxHp; } }

	public GameObject GameOverUI;

	private Transform moveDir;
	private Transform fireDir;

	private void Awake()
	{
		moveDir = transform.Find("MoveDir");
		fireDir = transform.Find("FireDir");
		maxHp = hp;
		dead = false;
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		GameManager.Instance.player = this;
		_ = StartCoroutine(FireCoroutine());
	}
	void Update()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector2 moveDir = new Vector2(x, y);

		//체력UI
		playerHpBar.value = hpValue;
		hpText.text = $"{hp} / {maxHp}";
		//적킬UI
		enemyKillsUI.text = $"Kills : {enemyKills.ToString()}";
		//꼬리 Anim
		tailAnimCtrl.SetBool("IsMoving", moveDir.magnitude > 0.1f);


		Enemy targetEnemy = null;
		float targetDistnace = float.MaxValue;

		if (GameManager.Instance.enemies.Count == 0) isFire = false;
		else isFire = true;

		foreach (Enemy enemy in GameManager.Instance.enemies)
		{
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			if (distance < targetDistnace) //이전에 비교한 적보다 가까우면
			{
				targetEnemy = enemy;
				targetDistnace = distance;
			}
		}

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
		//transform.Translate(dir * moveSpeed * Time.deltaTime);
		rb.MovePosition(rb.position + (dir * moveSpeed * Time.fixedDeltaTime));
	}

	private IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(fireInterval);
			if (isFire == true) Fire();
		}
	}

	/// <summary>
	/// 투사체를 발사.
	/// </summary>
	public void Fire()
	{
		Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		projectile.transform.up = fireDir.up;
		projectile.damage = damage;

	}
	public void TakeDamage(float damage)
	{
		hp -= damage;
		playerAnimCtrl.SetTrigger("TakeDamage");
		if (hp <= 0)
		{
			hp = 0;
			playerHpBar.value = 0f;
			dead = true;
			GameOverUI.SetActive(true);
			gameObject.SetActive(false);
		}
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