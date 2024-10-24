using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Transform[] spawnPoints = null; //������ ��ġ�� Transform������Ʈ �迭
	public float spawnInterval = 1f; //���� ����
	Coroutine enemySpawn; //�� ���� �ڷ�ƾ�� ���� ����

	private void Start()
	{
		enemySpawn = StartCoroutine(EnemySpawn());
	}
	private void Update()
	{
		//�÷��̾ �׾��ٸ� �� ��ȯ ���߱�
		if (GameManager.Instance.player.dead == true)
		{
			StopCoroutine(enemySpawn);
		}
	}

	private IEnumerator EnemySpawn()
	{
		while (true)
		{
			int random = Random.Range(0, spawnPoints.Length); //������ ���� ��
			Transform spawnPoint = spawnPoints[random];
			//Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
			Enemy enemy = PoolManager.Instance.enemyPool.Pop();
			enemy.transform.position = spawnPoint.position;
			enemy.transform.eulerAngles = spawnPoint.eulerAngles;
			yield return new WaitForSeconds(spawnInterval); //���� ���� ��ŭ ��ٸ���
		}
	}
}
