using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : SingletonManager<SceneCtrl>
{
	public void GameStart()
	{
		SceneManager.LoadScene("GameScene");
	}
}
