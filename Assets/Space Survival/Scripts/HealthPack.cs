using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float healAmount = 0f;
    public void Use()
    {
        //ü��ȸ�� +50 �ϰ� ����.
        print("ȸ���� ������.");
        GameManager.Instance.player.TakeHeal(healAmount);
        Destroy(gameObject);
    }
}
