using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
    public void Use()
    {
        print("ÆøÅº ½ÀµæÇÔ.");
        GameManager.Instance.RemoveAllEnemies();
        Destroy(gameObject); // ÆøÅº ¿ÀºêÁ§Æ® ÆÄ±«
    }
}
