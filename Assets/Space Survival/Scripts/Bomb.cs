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
            Enemy enemy = list[0]; //0번째 적 할당
            GameManager.Instance.enemies.RemoveAt(0); //0번째 적을 리스트에서 제거
            GameManager.Instance.player.enemyKills++; //킬수 누적
            Destroy(enemy.gameObject); //해당 오브젝트 파괴
            //List특징상 0번째를 지우면 앞당겨 지니까 매개변수가 계속 0임.
        }
        Destroy(gameObject);
    }
}
