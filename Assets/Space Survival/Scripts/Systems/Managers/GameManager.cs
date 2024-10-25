using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
