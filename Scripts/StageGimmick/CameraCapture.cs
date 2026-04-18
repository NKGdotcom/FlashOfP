using UnityEngine;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// 指定したカメラの映像をキャプチャし、
/// 保存とUIへの表示を行う
/// </summary>
public class CameraCapture : MonoBehaviour
{
    [Header("カメラ設定")]
    [Tooltip("撮影対象のカメラ")]
    [SerializeField] private Camera targetCamera;

    [Header("UI設定")]
    [Tooltip("撮影した画像を表示するUI")]
    [SerializeField] private Image cameraImage;
    [Tooltip("撮影時に同時に表示する追加UI")]
    [SerializeField] private Image capture;

    [Header("解像度")]
    [SerializeField] private int resWidth = 1920;
    [SerializeField] private int resHeight = 1080;

    // 現在UIに表示している画像データを保持する変数
    private Texture2D currentTexture;
    private Sprite currentSprite;

    private void Awake()
    {
        if(targetCamera == null) { Debug.LogError("targetCameraが参照されていません"); return; }
        if(cameraImage == null) { Debug.LogError("cameraImageが参照されていません"); return; }
        if(capture == null) { Debug.LogError("captureが参照されていません"); return; }
    }

    /// <summary>
    /// カメラの映像をキャプチャし、保存・UI表示を行う
    /// </summary>
    public void CaptureAndSave()
    {
        //古いデータが残っていれば破棄
        if (currentTexture != null) Destroy(currentTexture);
        if (currentSprite != null) Destroy(currentSprite);

        //カメラの映像をRenderTextureに書き出す
        RenderTexture _rt = new RenderTexture(resWidth, resHeight, 24);
        targetCamera.targetTexture = _rt;

        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        targetCamera.Render();

        //RenderTextureからTexture2Dにピクセルデータを読み込む
        RenderTexture.active = _rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        //カメラの設定を元に戻し、不要なRenderTextureを破棄
        targetCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(_rt);

        //完成した画像を保持
        currentTexture = screenShot;

        //PNGとして保存
        SaveTextureToPNG(currentTexture);

        //UI偽っと
        currentSprite = Sprite.Create(currentTexture, new Rect(0, 0, resWidth, resHeight), new Vector2(0.5f, 0.5f));
        SetCameraImage(currentSprite);
    }

    /// <summary>
    /// 撮影した画像をUIセットして表示する
    /// </summary>
    /// <param name="newSprite"></param>
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

    /// <summary>
    /// 表示している画像をクリアし、UIを非表示する
    /// </summary>
    public void ClearCameraImage()
    {
        cameraImage.sprite = null;
        cameraImage.gameObject.SetActive(false);
        capture.gameObject.SetActive(false);
    }

    /// <summary>
    /// Texture2DをPNG形式で端末に保存する
    /// </summary>
    /// <param name="texture"></param>
    private void SaveTextureToPNG(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        string directoryPath = Application.persistentDataPath + "/Screenshots";

        // フォルダが存在しなければ作成する
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // $記号を使ったモダンな文字列結合（yyyyMMdd_HHmmss で日時をファイル名に）
        string filename = $"{directoryPath}/Capture_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";

        File.WriteAllBytes(filename, bytes);
        Debug.Log($"画像を保存しました: {filename}");
    }
}