﻿using UnityEngine;
using System.IO;

public static class TextureHelper
{
	public static Texture2D GetTransparentPixel()
	{
		Texture2D pixel = new Texture2D(1, 1);
		pixel.SetPixel(0, 0, Color.clear);
		pixel.Apply();

		return pixel;
	}

	public static Texture2D GetScreenshot()
	{
#if UNITY_EDITOR
//		Application.CaptureScreenshot("Screenshot.png");
//		return Load("Screenshot.png");

		Vector2 offset = SCrossHelper.GetMainGameViewPosition();

		Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		screenshot.ReadPixels(new Rect(offset.x, offset.y, Screen.width, Screen.height), 0, 0);
		screenshot.Apply();
		
		return screenshot;
#else
		Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		screenshot.Apply();

		return screenshot;
#endif
	}

	public static void Save(this Texture2D texture, string path)
	{
		File.WriteAllBytes(path, texture.EncodeToPNG());
	}

	public static Texture2D Load(string path)
	{
		if (File.Exists(path))
		{
			Texture2D texture = new Texture2D(2, 2);
			texture.LoadImage(File.ReadAllBytes(path));
			
			return texture;
		}
		
		return null;
	}
	
	public static Sprite ToSprite(this Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
	}
}
