using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;


public class dinamicalText : MonoBehaviour {
	private float windChange;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		if (System.DateTime.Now.Second % Random.Range(2, 10) == 0 && System.DateTime.Now.Millisecond < 20)
			Change (Random.Range(300, 400) / 100f,
				Random.Range(740, 745));
			
	}

	IEnumerator Sleep(float timeForWait){
		yield return new WaitForSeconds (timeForWait);
	}

	void Change(object speed, object pressure){
		gameObject.GetComponent<Text> ().text = string.Format (sample, speed, pressure);
		
		//gameObject.GetComponent<Text> ().color = Color.red;
		//if(System.DateTime.Now.Second % 2 == 0)
		//gameObject.GetComponent<Text> ().color = Color.black;
		
	}


	static readonly string sample = @"Местоположение: Московская обл.
Долгота: 38°07′59″ в.д.
Широта: 56°17′59″ с.ш.
Направление ветра: Северо - Восток
Скорость ветра: {0}м/с
Атмосферное давление: {1}мм рт.ст.
Влажность: 95%
Температура: -7С
Канал МЧС: 192699";
}
