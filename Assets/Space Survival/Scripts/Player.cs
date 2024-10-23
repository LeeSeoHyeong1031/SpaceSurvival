using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHp; //�ִ� ü��
    public float hp = 100f;
    public float damage = 5f; //���ݷ�
    public float moveSpeed = 5f; //�̵��ӵ�
    public int enemyKills = 0;
    public float fireInterval = 1f; //���ݰ���

    public bool dead { get; private set; } //�÷��̾� ���� ����

    public Projectile projectilePrefab; //����ü ������

    [Header("PlayerInfo ���� ����")]
    public Slider playerHpBar; //�÷��̾� hpBar
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

        //ü��UI
        playerHpBar.value = hpValue;
        hpText.text = $"{hp} / {maxHp}";
        //��ųUI
        enemyKillsUI.text = $"Kills : {enemyKills.ToString()}";


        Enemy targetEnemy = null;
        float targetDistnace = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistnace) //������ ���� ������ ������
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
        this.moveDir.up = moveDir;
        this.fireDir.up = fireDir;
    }
    /// <summary>
    ///Transform�� ���� ���� ������Ʈ�� �����̴� �޼���.
    /// </summary>
    /// <param name="dir">�̵� ����</param>
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            Fire();
        }
    }

    /// <summary>
    /// ����ü�� �߻�.
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
        if (hp <= 0)
        {
            hp = 0;
            dead = true;
            playerHpBar.value = 0f;
            GameOverUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    //������ ���� �Լ�
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