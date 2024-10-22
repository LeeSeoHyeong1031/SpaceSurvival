using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHp; //�ִ� ü��
    public float hp =100f; //ü��
    public float damage=5f; //���ݷ�
    public float moveSpeed=5f; //�̵��ӵ�

    private Vector2 dir;

    public static int enemyKills = 0;

    public TextMeshProUGUI enemyKillsUI;
    public GameObject fireIndicator;
    public static bool dead { get; private set; } //�÷��̾� ���� ����

    public Projectile projectilePrefab; //����ü ������
    public Slider playerHpBar; //�÷��̾� hpBar
    public float hpValue { get { return hp / maxHp; } }

    public GameObject GameOverUI;
    void Start()
    {
        maxHp = hp;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        dir = new Vector2(x, y);

        //ü��UI
        playerHpBar.value = hpValue;
        //��ųUI
        enemyKillsUI.text = $"Kills : {enemyKills.ToString()}";
        //�߻��ε�������
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 fireDir = mouseScreenPos - (Vector2)transform.position;
        fireIndicator.transform.up = fireDir;

        if (Input.GetButtonDown("Fire1"))
        {
            Fire(fireDir);
        }
    }
    private void FixedUpdate()
    {
        Move(dir.normalized);
    }
    /// <summary>
    ///Transform�� ���� ���� ������Ʈ�� �����̴� �޼���.
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// ����ü�� �߻�.
    /// </summary>
    public void Fire(Vector2 dir)
    {
        //Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.up = dir;
        projectile.damage = damage;
        
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0) //���� ���
        {
            hp = 0;
            dead = true;
            playerHpBar.value = 0f;
            GameOverUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            TakeDamage(enemy.damage);
        }
    }
}
