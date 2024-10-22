using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
	public void Use()
	{
		//Die에서 Remove땜에 앞으로 모두 땅겨져서 어떤 오브젝트는 사라지고 어떤거는 남는 현상 발생
		//그래서 리스트 맨 끝부터 삭제. <--- 뒤에서 당져질 일이 없음.
		for (int i = GameManager.Instance.enemies.Count - 1; i >= 0; i--)
		{
			GameManager.Instance.enemies[i].Die();
		}
		Destroy(gameObject); // 폭탄 오브젝트 파괴
	}
}
