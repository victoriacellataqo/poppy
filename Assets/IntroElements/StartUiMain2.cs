using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using UnityEngine.Networking;
// Token: 0x02000089 RID: 137
public class StartUiMain2 : MonoBehaviour
{

	public static int x = 0;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Button btn;
	public RawImage secreen1;
	public RawImage secreen2;
	public RawImage secreen3;
	public RawImage secreen4;

	public string jsons1, jsons2, jsons3, jsons4;

	IEnumerator Start()
	{
		int z;
		btn.gameObject.SetActive(false);
		string url = "https://appaco.wassrecordings.com/roadto100k/YodoSceneIntro.json";
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null)
		{
			Processjson(www.text);
		}

		

	}

	private void Processjson(string jsonString)
	{
		JsonData jsonvale = JsonMapper.ToObject(jsonString);

		
		try
		{
			jsons1 = jsonvale["secreen1"].ToString();
			jsons2 = jsonvale["secreen2"].ToString();
			jsons3 = jsonvale["secreen3"].ToString();
			jsons4 = jsonvale["secreen4"].ToString();

			StartCoroutine(DownloadImage(jsons1, secreen1));
			StartCoroutine(DownloadImage(jsons2, secreen2));
			StartCoroutine(DownloadImage(jsons3, secreen3));
			StartCoroutine(DownloadImage(jsons4, secreen4));
			btn.gameObject.SetActive(true);
		}
		catch (Exception e) { }
	}

	IEnumerator DownloadImage(string MediaUrl,RawImage rawimage)
	{
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
		yield return request.SendWebRequest();
		if (request.isNetworkError || request.isHttpError)
			Debug.Log(request.error);
		else
			rawimage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
	}
	IEnumerator waiter()
	{
		//btn.gameObject.SetActive(false);

		//Wait for 2 seconds
		yield return new WaitForSeconds(2);
	//	btn.gameObject.SetActive(true);

	}
	public void loadscnes1()
	{

		if (x % 4 == 0)
		{
			StartCoroutine(waiter());
			text1.gameObject.SetActive(false);
			text2.gameObject.SetActive(true);
			text3.gameObject.SetActive(false);
			text4.gameObject.SetActive(false);

			secreen1.gameObject.SetActive(false);
			secreen2.gameObject.SetActive(true);
			secreen3.gameObject.SetActive(false);
			secreen4.gameObject.SetActive(false);


		}
		if (x % 4 == 1)
		{

			StartCoroutine(waiter());
			text1.gameObject.SetActive(false);
			text2.gameObject.SetActive(false);
			text3.gameObject.SetActive(true);
			text4.gameObject.SetActive(false);
			secreen1.gameObject.SetActive(false);
			secreen2.gameObject.SetActive(false);
			secreen3.gameObject.SetActive(true);
			secreen4.gameObject.SetActive(false);


		}
		if (x % 4 == 2)
		{

			StartCoroutine(waiter());
			text1.gameObject.SetActive(false);
			text2.gameObject.SetActive(false);
			text3.gameObject.SetActive(false);
			text4.gameObject.SetActive(true);

			secreen1.gameObject.SetActive(false);
			secreen2.gameObject.SetActive(false);
			secreen3.gameObject.SetActive(false);
			secreen4.gameObject.SetActive(true);



		}
		if (x % 4 == 3)
		{


			StartCoroutine(waiter());
			text1.gameObject.SetActive(true);
			text2.gameObject.SetActive(false);
			text3.gameObject.SetActive(false);
			text4.gameObject.SetActive(false);

			secreen1.gameObject.SetActive(true);
			secreen2.gameObject.SetActive(false);
			secreen3.gameObject.SetActive(false);
			secreen4.gameObject.SetActive(false);


		}
		if (x == 15)
		{

			text1.gameObject.SetActive(true);
			text2.gameObject.SetActive(false);
			text3.gameObject.SetActive(false);
			text4.gameObject.SetActive(false);

			secreen1.gameObject.SetActive(true);
			secreen2.gameObject.SetActive(false);
			secreen3.gameObject.SetActive(false);
			secreen4.gameObject.SetActive(false);

			btn.gameObject.SetActive(false);
			
		}
		x++;
	}
}

