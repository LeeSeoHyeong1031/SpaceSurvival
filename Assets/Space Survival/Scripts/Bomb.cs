using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
    public void Use()
    {
        print("��ź ������.");
        GameManager.Instance.RemoveAllEnemies();
        Destroy(gameObject); // ��ź ������Ʈ �ı�
    }
}
