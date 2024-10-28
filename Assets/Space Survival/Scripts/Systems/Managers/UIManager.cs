using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
	[Header("PlayerInfo 관련 변수")]
	public Slider playerHpBar; //플레이어 hpBar
	public Slider playerExpBar; //플레이어 expBar
	public TextMeshProUGUI hpText;
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI enemiesKillsUI;
	[Header("-------------")]
	public Image levelUpUI;
	public GameObject GameOverUI;
	protected override void Awake()
	{
		base.Awake();
	}
	private IEnumerator Start()
	{
		yield return null; //한 프레임 쉬기.
		UpdateHP(); //처음 시작 시 체력 업데이트
		UpdateExpUI(); //처음 시작 시 경험치 업데이트
		levelUpUI.gameObject.SetActive(false); //처음 시작 시 레벨업 UI 끄기
	}
	public void UpdateHP()
	{
		playerHpBar.value = GameManager.Instance.player.hpValue;
		hpText.text = $"{GameManager.Instance.player.hp} / {GameManager.Instance.player.maxHp}";
	}

	public void UpdateEnemiesKillsUI()
	{
		GameManager.Instance.player.enemyKills++; //킬 누적
		enemiesKillsUI.text = $"Kills : {GameManager.Instance.player.enemyKills.ToString()}";
	}

	public void UpdateExpUI()
	{
		playerExpBar.value = GameManager.Instance.player.expValue;
	}

	public void UpdateLevelUpUI()
	{
		levelText.text = GameManager.Instance.player.level.ToString();
		levelUpUI.gameObject.SetActive(true);
		levelUpUI.GetComponent<RandomSelect>().enabled = true;
	}

	public void HideLevelUpUI()
	{
		GameManager.Instance.Resume();
		levelUpUI.GetComponent<RandomSelect>().enabled = false;
		levelUpUI.gameObject.SetActive(false);
	}
}
