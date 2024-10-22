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

	//초고수
	public float hpAmount { get { return hp / maxHp; } } //자주 계산되는 항목은 프로퍼티로 만들기
	
	private Transform target; //추적할 대상

	public Image hpBar;
	void Start()
	{
		target = GameObject.Find("Player").transform;
		maxHp = hp;
	}

	// Update is called once per frame
	void Update()
	{
		if (Player.dead == true) return;
		Vector2 moveDir = target.position - transform.position;
		Move(moveDir.normalized);
		//print(moveDir.magnitude); //vector.magnitude : 해당 벡터가 "방향벡터"로 간주될 때, 벡터의 길이
		//print(moveDir.normalized);

		hpBar.fillAmount = hpAmount;
	}

	private void Move(Vector2 dir) //dir 값이 커져도 1로 고정을 하고 싶은 경우.
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);
	}
	//OnHit, TakeDamage 보통 이렇게 씀(메서드 네임)
	public void TakeDamage(float damage)
	{
		hp -= damage;
		if(hp <= 0) //으앙 쥬금
		{
			Player.enemyKills++;
			Destroy(gameObject);
		}
	}
}
