using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Transform[] spawnPoints = null; //스폰할 위치의 Transform컴포넌트 배열
	public float spawnInterval = 1f; //스폰 간격
	Coroutine enemySpawn; //적 스폰 코루틴을 담을 변수

	private void Start()
	{
		enemySpawn = StartCoroutine(EnemySpawn());
	}
	private void Update()
	{
		//플레이어가 죽었다면 적 소환 멈추기
		if (GameManager.Instance.player.dead == true)
		{
			StopCoroutine(enemySpawn);
		}
	}

	private IEnumerator EnemySpawn()
	{
		while (true)
		{
			int random = Random.Range(0, spawnPoints.Length); //스폰할 랜덤 값
			Transform spawnPoint = spawnPoints[random];
			//Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
			Enemy enemy = PoolManager.Instance.enemyPool.Pop();
			enemy.transform.position = spawnPoint.position;
			enemy.transform.eulerAngles = spawnPoint.eulerAngles;
			yield return new WaitForSeconds(spawnInterval); //스폰 간격 만큼 기다리기
		}
	}
}
