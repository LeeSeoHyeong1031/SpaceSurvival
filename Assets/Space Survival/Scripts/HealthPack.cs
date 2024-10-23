using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float healAmount = 0f;
    public void Use()
    {
        //체력회복 +50 하고 싶음.
        print("회복약 습득함.");
        GameManager.Instance.player.TakeHeal(healAmount);
        Destroy(gameObject);
    }
}
