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
        GameManager.Instance.enemies.Add(this);//적 리스트에 자기 자신을 Add
        target = GameManager.Instance.player.transform;
        maxHp = hp;
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
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    //OnHit, TakeDamage 보통 이렇게 씀(메서드 네임)
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) //으앙 쥬금
        {
            Die();
        }
    }
    public void Die()
    {
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.enemyKills++;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.player.TakeDamage(damage);
        }
    }
}
