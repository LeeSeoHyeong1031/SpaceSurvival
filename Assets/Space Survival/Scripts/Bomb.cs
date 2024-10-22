using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
    public void Use()
    {
        List<Enemy> list = GameManager.Instance.enemies;
        for (int i = 0; i < list.Count; i++)
        {
            Enemy enemy = list[0]; //0��° �� �Ҵ�
            GameManager.Instance.enemies.RemoveAt(0); //0��° ���� ����Ʈ���� ����
            GameManager.Instance.player.enemyKills++; //ų�� ����
            Destroy(enemy.gameObject); //�ش� ������Ʈ �ı�
            //ListƯ¡�� 0��°�� ����� �մ�� ���ϱ� �Ű������� ��� 0��.
        }
        Destroy(gameObject);
    }
}
