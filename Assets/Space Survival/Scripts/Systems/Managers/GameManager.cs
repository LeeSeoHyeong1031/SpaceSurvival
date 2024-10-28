using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//게임 전체 진행을 총괄하는 오브젝트.
public class GameManager : SingletonManager<GameManager>
{
	internal List<Enemy> enemies = new List<Enemy>(); //씬에 존재하는 전체 적 List
	internal Player player; //씬에 존재하는 player

	protected override void Awake()
	{
		base.Awake();
	}
	public void RemoveAllEnemies()
	{
		List<Enemy> removeTargets = new List<Enemy>(enemies); //enemies 리스트를 복사

		foreach (Enemy removeTarget in removeTargets)
		{
			removeTarget.Die();
		}
	}

	//경험치 흭득 메서드
	public void GainExp(float exp)
	{
		player.exp += exp; //플레이어 경험치 흭득
		if (player.exp >= player.maxExp)
		{
			player.exp = player.exp - player.maxExp;
			LevelUp();
		}
		UIManager.Instance.UpdateExpUI(); //경험치 UI 업뎃
	}

	//레벨업 메서드
	public void LevelUp()
	{
		Stop();
		player.level++;
		UIManager.Instance.UpdateLevelUpUI();
	}

	public void Resume()
	{
		Time.timeScale = 1f;
	}

	public void Stop()
	{
		Time.timeScale = 0f;
	}

	//public void GameStart()
	//{
	//	SceneManager.LoadScene("GameScene");
	//}
}
