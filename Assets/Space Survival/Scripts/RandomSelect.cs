using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class RandomSelect : MonoBehaviour
{
	public GameObject[] skills;

	public void OnEnable()
	{
		List<GameObject> list = new List<GameObject>();
		while (list.Count < 2)
		{
			int random = Random.Range(0, skills.Length);
			if (list.Contains(skills[random])) continue;
			list.Add(skills[random]);
			skills[random].SetActive(true);
		}
	}

	public void OnDisable()
	{
		foreach (GameObject skill in skills)
		{
			skill.gameObject.SetActive(false);
		}
	}
}
