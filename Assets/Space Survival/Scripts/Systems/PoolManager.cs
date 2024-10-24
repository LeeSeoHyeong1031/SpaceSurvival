using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance => instance;

    [Header("오브젝트(또는 프리팹)을/를 넣을 때 같은 위치에 있어야 합니다.")]
    public GameObject[] prefabs;
    public List<GameObject>[] pools;
    public Transform[] poolParents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private PoolManager() { }


    private void Start()
    {
        InitObjPool();
    }

    private void InitObjPool()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();

            GameObject obj = Instantiate(prefabs[i], poolParents[i]);
            obj.SetActive(false);
            pools[i].Add(obj);
        }
    }

    //오브젝트 갖고 오는 함수
    public GameObject ActivateObj(int index)
    {
        GameObject obj = null;
        //1.해당 인덱스에 비활성화 오브젝트가 있는지 검사.
        //해당 인덱스에 있는 요소의 개수만큼 for문 돌기
        for (int i = 0; i < pools[index].Count; i++)
        {
            //해당 프리팹의 오브젝트가 하이어라키창에서 꺼져있다면 => 사용할 수 있다는 뜻
            if (!pools[index][i].activeInHierarchy)
            {
                obj = pools[index][i];
                obj.SetActive(true); //활성화 시켜서 리턴
                return obj;
            }
        }

        //2.해당 인덱스에 비활성화 오브젝트가 하나도 없다면
        //만들기
        //해당 인덱스에 있는 오브젝트 생성
        obj = Instantiate(prefabs[index], poolParents[index]);
        //만든 오브젝트 해당 인덱스에 Add()
        pools[index].Add(obj);
        //활성화 시켜서 리턴
        obj.SetActive(true);

        return obj;
    }
}
