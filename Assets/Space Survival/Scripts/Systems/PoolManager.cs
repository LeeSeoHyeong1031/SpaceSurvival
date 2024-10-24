using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance => instance;

    [Header("������Ʈ(�Ǵ� ������)��/�� ���� �� ���� ��ġ�� �־�� �մϴ�.")]
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

    //������Ʈ ���� ���� �Լ�
    public GameObject ActivateObj(int index)
    {
        GameObject obj = null;
        //1.�ش� �ε����� ��Ȱ��ȭ ������Ʈ�� �ִ��� �˻�.
        //�ش� �ε����� �ִ� ����� ������ŭ for�� ����
        for (int i = 0; i < pools[index].Count; i++)
        {
            //�ش� �������� ������Ʈ�� ���̾��Űâ���� �����ִٸ� => ����� �� �ִٴ� ��
            if (!pools[index][i].activeInHierarchy)
            {
                obj = pools[index][i];
                obj.SetActive(true); //Ȱ��ȭ ���Ѽ� ����
                return obj;
            }
        }

        //2.�ش� �ε����� ��Ȱ��ȭ ������Ʈ�� �ϳ��� ���ٸ�
        //�����
        //�ش� �ε����� �ִ� ������Ʈ ����
        obj = Instantiate(prefabs[index], poolParents[index]);
        //���� ������Ʈ �ش� �ε����� Add()
        pools[index].Add(obj);
        //Ȱ��ȭ ���Ѽ� ����
        obj.SetActive(true);

        return obj;
    }
}
