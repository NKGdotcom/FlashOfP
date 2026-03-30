using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    [Header("撮影するカメラ")]
    [SerializeField] private Camera targetCamera;
    [Header("画像をセット")]
    [SerializeField] private Image cameraImage;
    [SerializeField] private Image capture;

    [Header("解像度")]
    [SerializeField] private int resWidth = 1920;
    [SerializeField] private int resHeight = 1080;

    // メモリリークを防ぐため、現在UIに表示している画像データを保持する変数
    private Texture2D currentTexture;
    private Sprite currentSprite;

    // このメソッドをUIボタンなどから呼び出します
    public void CaptureAndSave()
    {
        cameraImage.gameObject.SetActive(true);
        capture.gameObject.SetActive(true);
        if (targetCamera == null)
        {
            Debug.LogWarning("ターゲットカメラが設定されていません。");
            return;
        }

        if (currentTexture != null) Destroy(currentTexture);
        if (currentSprite != null) Destroy(currentSprite);

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        targetCamera.targetTexture = rt;

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        targetCamera.Render();

        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        currentTexture = screenShot;

        byte[] bytes = currentTexture.EncodeToPNG();
        string directoryPath = Application.persistentDataPath + "/Screenshots";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string filename = string.Format("{0}/Capture_{1}.png", directoryPath, System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        File.WriteAllBytes(filename, bytes);
        Debug.Log($"画像を保存しました: {filename}");

        currentSprite = Sprite.Create(currentTexture, new Rect(0, 0, resWidth, resHeight), new Vector2(0.5f, 0.5f));
        SetCameraImage(currentSprite);
    }

    public void SetCameraImage(Sprite newSprite)
    {
        if (cameraImage != null)
        {
            cameraImage.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("Camera Imageが設定されていません。");
        }
    }

    public void ClearCameraImage()
    {
        if (cameraImage != null)
        {
            cameraImage.sprite = null;
            cameraImage.gameObject.SetActive(false);
            capture.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Camera Imageが設定されていません。");
        }
    }
}