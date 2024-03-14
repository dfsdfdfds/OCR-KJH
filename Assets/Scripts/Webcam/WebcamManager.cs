using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour
{
	public RawImage rawImage;

    void Start()
	{
		// Check if rawImage is assigned
		if (rawImage == null)
		{
			Debug.LogError("RawImage component not assigned. Please assign it in the inspector.");
			return;
		}

		// Create a WebCamTexture and start it
		WebCamTexture webcamTexture = new WebCamTexture();

		// Set the RawImage's texture to the WebCamTexture
		rawImage.texture = webcamTexture;

		// Optional: Adjust the RawImage's aspect ratio
		rawImage.GetComponent<AspectRatioFitter>().aspectRatio = (float)webcamTexture.width / webcamTexture.height;

		// Start the webcam
		webcamTexture.Play();
	}
}
