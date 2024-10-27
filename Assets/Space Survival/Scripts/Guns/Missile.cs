using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Missile : MonoBehaviour
{
	[Tooltip("플레이어 기준 미사일 랜덤한 범위 설정")]
	public Vector2 minMaxRange;
	public LayerMask targetLayer;
	[Tooltip("미사일 타격 범위")]
	public float range;
	public float moveSpeed;
	public float damage;
	public float duration = 1f; //지속시간

	private Animator missileCtrl;

	private void Awake()
	{
		missileCtrl = GetComponentInChildren<Animator>();
	}

	public IEnumerator Start()
	{
		yield return null; //한 프레임 쉬기.
		Vector3 dest = Random.insideUnitCircle.normalized * Random.Range(minMaxRange.x, minMaxRange.y); //랜덤한 방향으로 x~y 안에 범위
		if (dest.y < 0) dest.y = Mathf.Abs(dest.y); //만약 dest의 y가 음수라면 양수로 바꿔 무조건 x축 보다 위에 오게 하기.
		transform.position = dest + GameManager.Instance.player.transform.position + new Vector3(0, 5, 0); //플레이어 위치 + dest + +y쪽으로 5만큼 올려주기.
		StartCoroutine(HitWaitForSeconds());
		Destroy(gameObject, duration + 1f);
	}

	private void Update()
	{
		Move();
	}
	public void Move()
	{
		transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
	}

	public IEnumerator HitWaitForSeconds()
	{
		missileCtrl.SetTrigger("MissileBlink");
		yield return new WaitForSeconds(duration);
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
		if (colls.Length > 0)
		{
			foreach (Collider2D coll in colls)
			{
				coll.GetComponent<Enemy>().TakeDamage(damage);
			}
			Destroy(gameObject);
		}
	}
}
