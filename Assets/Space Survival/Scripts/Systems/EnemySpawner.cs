using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints = null; //스폰할 위치의 Transform컴포넌트 배열
    public GameObject enemyPrefab; //적 프리팹
    [SerializeField]
    private float spawnInterval = 1f; //스폰 간격
    Coroutine enemySpawn; //적소환 코루틴을 담을 객체

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
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); //스폰 간격 만큼 기다리기
        }
    }
}
