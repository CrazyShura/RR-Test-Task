using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CardDead : UnityEvent<Card> { }
public static class PublicStuff
{
	#region Fields]
	static List<string> names = new List<string> { "Dude", "Guy", "Crab", "Bird", "Boar", "Snake" };
	static List<string> prephics = new List<string> { "Hard", "Big", "Solid", "Thic", "Strong", "Agile" };
	static List<Texture> textures = new List<Texture>();
	static bool imageLibraryReady = false;
	static UnityEvent imagesLoaded = new UnityEvent();
	static Hand hand;
	#endregion

	#region Properties
	public static bool ImageLibraryReady { get => imageLibraryReady;}
	public static Hand Hand { get => hand;}
	public static List<string> Names { get => names;}
	public static List<string> Prephics { get => prephics;}
	#endregion

	#region Methods
	public static IEnumerator FillImageLibrary()
	{
		for (int i = 0; i < 10; i++)
		{
			UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://picsum.photos/325/225");
			yield return request.SendWebRequest();
			if (request.isNetworkError || request.isHttpError)
			{
				Debug.LogError("Error on request");
			}
			else
			{
				textures.Add(((DownloadHandlerTexture)request.downloadHandler).texture);
				Debug.Log("New image added");
				if (textures.Count >= 10)
				{
					Debug.Log("Image library is ready");
					imageLibraryReady = true;
					imagesLoaded.Invoke();
				}
			}
		}
	}

	public static void SetHand(Hand HandToSet)
	{
		if (hand == null)
		{
			hand = HandToSet;
		}
		else
		{
			Debug.LogError("Tring to set more then one hand");
		}
	}

	public static Texture GetTexture()
	{
		return textures[Random.Range(0, textures.Count)];
	}

	public static void AddListenerToImagesLoaded(UnityAction Action)
	{
		imagesLoaded.AddListener(Action);
	}
	#endregion
}
