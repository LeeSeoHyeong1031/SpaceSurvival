using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonManager<SkillManager>
{
	public GameObject[] skills;
	private void Start()
	{
		skills[0].SetActive(true);
		skills[0].GetComponent<Gun>().level++;
	}

	public void SkillLevelUp(Gun skill)
	{
		if (skill.level == 0)
		{
			FirstSkill(skill);
			return;
		}
		if (skill.level >= skill.gunData.damage.Length)
		{
			return;
		}
		skill.level++;
		skill.fireInterval = skill.gunData.fireInterval[skill.level];
	}

	public void FirstSkill(Gun skill)
	{
		for (int i = 0; i < skills.Length; i++)
		{
			if (skill.name == skills[i].name)
			{
				skills[i].SetActive(true);
				skill.level++;
			}
		}
	}
}
