using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ImageExporter : MonoBehaviour
{
    private const int HalfResolutionX = 1920 / 2;
    private const int HalfResolutionY = 1080 / 2;

    public RectTransform backgroundToScreenShot;


    public void ScreenShotAndExportFile(string nameFile, int size)
    {
        StartCoroutine(CoroutineScreenShotAndExportFile(nameFile, size));
    }

    private IEnumerator CoroutineScreenShotAndExportFile(string nameFile, int size)
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(HalfResolutionX - size / 2, HalfResolutionY - size / 2, size, size), 0, 0);

        byte[] bytes = texture.EncodeToPNG();
        SaveFile(nameFile, bytes);

        Destroy(texture);
    }

    private void SaveFile(string nameFile, byte[] bytes)
    {
        File.WriteAllBytes($"C:/Users/ASUS/Desktop/AutoGen/{nameFile}.png", bytes);
    }

}