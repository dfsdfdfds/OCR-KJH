using UnityEngine;
using UnityEngine.UI;

public class TesseractDemoScript : MonoBehaviour
{
    //외부 이미지 사용시
    //[SerializeField] private Texture2D imageToRecognize;
    [SerializeField] private Text displayText;
    [SerializeField] private RawImage outputImage;
    [SerializeField] private RawImage webcamSource;
    //[SerializeField] private RawImage recognizedFrameDisplay; // Assuming you want to show the recognized frame separately

    private Texture2D _webcamTexture; // Store the webcam frame here

    private TesseractDriver _tesseractDriver;
    private string _text = "";
    private Texture2D _texture;

    private void Start()
    {
        //Texture2D texture = new Texture2D(imageToRecognize.width, imageToRecognize.height, TextureFormat.ARGB32, false);
        //texture.SetPixels32(imageToRecognize.GetPixels32());
        //texture.Apply();

        _tesseractDriver = new TesseractDriver();
    }


    private void StartRecognizeWebcam()
    {
        if (webcamSource.texture == null)
        {
            Debug.LogError("Webcam texture is not available.");
            return;
        }
        Texture2D webcamTexture = WebcamTextureToTexture2D(webcamSource.texture as WebCamTexture);

        _webcamTexture = webcamTexture; // Save the current frame

        //// Optionally, directly display this frame as the recognized image
        //if (recognizedFrameDisplay != null)
        //{
        //	recognizedFrameDisplay.texture = _webcamTexture;
        //	AdjustImageDisplay(recognizedFrameDisplay, _webcamTexture);
        //}

        StartRecognize(webcamTexture);
    }


    public void StartRecognize(Texture2D outputTexture)
    {
        _texture = outputTexture;

        // _text = "";
        //AddToTextDisplay(_tesseractDriver.CheckTessVersion());
        _tesseractDriver.Setup(OnSetupCompleteRecognize);
    }

    //Texture2D 변환
    private Texture2D WebcamTextureToTexture2D(WebCamTexture webCamTexture)
    {
        if (webCamTexture == null) return null;

        Texture2D texture = new(webCamTexture.width, webCamTexture.height);
        texture.SetPixels(webCamTexture.GetPixels());
        texture.Apply();

        return texture;
    }

    private void OnSetupCompleteRecognize()
    {
        //output data 추출 진행 코드(다른 rawimage 에 추출 화면 로드시 사용)
        //AddToTextDisplay(_tesseractDriver.Recognize(_texture));
        //output data 추출 진행 코드

        //output data 추출 진행 코드
        _tesseractDriver.Recognize(_texture);
        //output data 추출 진행 코드

        //에러 메세지 text에 그려주는 
        //AddToTextDisplay(_tesseractDriver.GetErrorMessage(), true);
        //SetImageDisplay();
    }

    ////추출된 text 값 추출 하여 UI 에 표시(사용x)
    //private void AddToTextDisplay(string text, bool isError = false)
    //{
    //    if (string.IsNullOrWhiteSpace(text)) return;
    //    _text += (string.IsNullOrWhiteSpace(displayText.text) ? "" : "\n") + text;

    //    if (isError)
    //        Debug.LogError(text);
    //    else
    //        Debug.Log(text);
    //}

    private void LateUpdate()
    {
        //displayText.text = _text;
    }

    //private void SetImageDisplay()
    //{
    //    if (_tesseractDriver.GetHighlightedTexture() != null)
    //    {
    //        outputImage.texture = _tesseractDriver.GetHighlightedTexture();
    //        AdjustImageDisplay(outputImage, _tesseractDriver.GetHighlightedTexture());
    //    }
    //    else if (_webcamTexture != null) // Fallback if no highlighted texture is available
    //    {
    //        outputImage.texture = _webcamTexture;
    //        AdjustImageDisplay(outputImage, _webcamTexture);
    //    }
    //}

    //// 추출 이미지 크기 조정 도와주는
    //private void AdjustImageDisplay(RawImage display, Texture texture)
    //{
    //    RectTransform rectTransform = display.GetComponent<RectTransform>();
    //    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
    //        rectTransform.rect.width * texture.height / texture.width);
    //}
}