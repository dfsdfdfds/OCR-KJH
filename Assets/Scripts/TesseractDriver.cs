using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class TesseractDriver
{
    private TesseractWrapper _tesseract;
    private static readonly List<string> fileNames = new() { "tessdata.tgz"};

    public string CheckTessVersion()
    {
        _tesseract = new TesseractWrapper();

        return null;
    }

    public void Setup(UnityAction onSetupComplete)
    {
#if UNITY_EDITOR
        OcrSetup(onSetupComplete);
#elif UNITY_ANDROID
        CopyAllFilesToPersistentData(fileNames, onSetupComplete);
#else
        OcrSetup(onSetupComplete);
#endif
    }

    public void OcrSetup(UnityAction onSetupComplete)
    {
        _tesseract = new TesseractWrapper();

#if UNITY_EDITOR
        string datapath = Path.Combine(Application.streamingAssetsPath, "tessdata");
#elif UNITY_ANDROID
        string datapath = Application.persistentDataPath + "/tessdata/";
#else
        string datapath = Path.Combine(Application.streamingAssetsPath, "tessdata");
#endif

        if (_tesseract.Init("eng", datapath))
        {
            Debug.Log("Init Successful");
            onSetupComplete?.Invoke();
        }
        else
        {
            Debug.LogError(_tesseract.GetErrorMessage());
        }
    }


    public string GetErrorMessage()
    {
        return _tesseract?.GetErrorMessage();
    }

    //데이터 output
    public string Recognize(Texture2D imageToRecognize)
    {
        return _tesseract.Recognize(imageToRecognize);
    }

    public Texture2D GetHighlightedTexture()
    {
        return _tesseract.GetHighlightedTexture();
    }
}