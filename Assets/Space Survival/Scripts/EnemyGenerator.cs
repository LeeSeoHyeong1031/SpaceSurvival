using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Transform[] spawnPoints = null; //������ ��ġ�� Transform������Ʈ �迭
    public GameObject enemyPrefab; //�� ������
    [SerializeField]
    private float spawnInterval = 0.5f; //���� ����
    Coroutine enemySpawn; //����ȯ �ڷ�ƾ�� ���� ��ü

    private void Start()
    {
        enemySpawn = StartCoroutine(EnemySpawn());
    }
    private void Update()
    {
        //�÷��̾ �׾��ٸ� �� ��ȯ ���߱�
        if (Player.dead == true)
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
            Instantiate(enemyPrefab,spawnPoint.position,Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval); //���� ���� ��ŭ ��ٸ���
        }
    }
}
