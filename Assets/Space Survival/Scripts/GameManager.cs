using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��ü ������ �Ѱ��ϴ� ������Ʈ.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;
    internal List<Enemy> enemies = new List<Enemy>(); //���� �����ϴ� ��ü �� List
    internal Player player; //���� �����ϴ� player

    private void Awake()
    {
        //c#���� this�� �ڱ� �ڽ� ������Ʈ�� �ش� �׷��� Destoy�ȿ� this�� �־�θ�
        //�� ������Ʈ�� �����. ���ӿ�����Ʈ ��ü�� ���� �ϰ������ �Ű������ȿ� gameobject�� �־�θ� ��.
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
