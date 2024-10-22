using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IItem
{
	public void Use()
	{
		//Die���� Remove���� ������ ��� �������� � ������Ʈ�� ������� ��Ŵ� ���� ���� �߻�
		//�׷��� ����Ʈ �� ������ ����. <--- �ڿ��� ������ ���� ����.
		for (int i = GameManager.Instance.enemies.Count - 1; i >= 0; i--)
		{
			GameManager.Instance.enemies[i].Die();
		}
		Destroy(gameObject); // ��ź ������Ʈ �ı�
	}
}
