using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHp; //최대 체력
    public float hp =100f; //체력
    public float damage=5f; //공격력
    public float moveSpeed=5f; //이동속도

    private Vector2 dir;

    public static int enemyKills = 0;

    public TextMeshProUGUI enemyKillsUI;
    public GameObject fireIndicator;
    public static bool dead { get; private set; } //플레이어 죽음 여부

    public Projectile projectilePrefab; //투사체 프리팹
    public Slider playerHpBar; //플레이어 hpBar
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

        //체력UI
        playerHpBar.value = hpValue;
        //적킬UI
        enemyKillsUI.text = $"Kills : {enemyKills.ToString()}";
        //발사인디케이터
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
    ///Transform을 통해 게임 오브젝트를 움직이는 메서드.
    /// </summary>
    /// <param name="dir">이동 방향</param>
    public void Move(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 투사체를 발사.
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
        if (hp <= 0) //으앙 쥬금
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
