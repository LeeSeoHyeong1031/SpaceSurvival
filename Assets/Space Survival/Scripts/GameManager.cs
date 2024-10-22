using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 전체 진행을 총괄하는 오브젝트.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;
    internal List<Enemy> enemies = new List<Enemy>(); //씬에 존재하는 전체 적 List
    internal Player player; //씬에 존재하는 player

    private void Awake()
    {
        //c#에서 this는 자기 자신 컴포넌트만 해당 그래서 Destoy안에 this만 넣어두면
        //이 컴포넌트만 사라짐. 게임오브젝트 자체를 삭제 하고싶으면 매개변수안에 gameobject를 넣어두면 됌.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private GameManager() { }
}
