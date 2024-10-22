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

    //�ʰ��
    public float hpAmount { get { return hp / maxHp; } } //���� ���Ǵ� �׸��� ������Ƽ�� �����

    private Transform target; //������ ���

    public Image hpBar;
    void Start()
    {
        GameManager.Instance.enemies.Add(this);//�� ����Ʈ�� �ڱ� �ڽ��� Add
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

    private void Move(Vector2 dir) //dir ���� Ŀ���� 1�� ������ �ϰ� ���� ���.
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    //OnHit, TakeDamage ���� �̷��� ��(�޼��� ����)
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) //���� ���
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
