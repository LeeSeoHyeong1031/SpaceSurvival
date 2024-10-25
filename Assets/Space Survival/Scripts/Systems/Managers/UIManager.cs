using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    [Header("PlayerInfo 관련 변수")]
    public Slider playerHpBar; //플레이어 hpBar
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI enemiesKillsUI;
    [Header("-------------")]
    public GameObject GameOverUI;
    protected override void Awake()
    {
        base.Awake();
    }
    private IEnumerator Start()
    {
        yield return null; //한 프레임 쉬기.
        UpdateHP(); //처음 시작 시 체력 업데이트
    }
    public void UpdateHP()
    {
        playerHpBar.value = GameManager.Instance.player.hpValue;
        hpText.text = $"{GameManager.Instance.player.hp} / {GameManager.Instance.player.maxHp}";
    }
}
