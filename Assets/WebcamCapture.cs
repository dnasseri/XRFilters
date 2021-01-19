using UnityEngine;

public class WebcamCapture : MonoBehaviour
{
    // Material used to play with the webcam texture.
    [SerializeField]
    private Material _material = default;

    /// Texture containing the webcam capture, will be sent to the shader.
    private WebCamTexture _webCamTexture;

    // Initialize the webcam texture and start capturing.
    private void Start()
    {
        if (_webCamTexture == null)
        {
            _webCamTexture = new WebCamTexture();
        }

        // Screen.height
        // Screen.width
        _webCamTexture.requestedHeight = 640;
        _webCamTexture.requestedWidth = 480;
        _webCamTexture.Play();

    }

    // Send texture to the shader.
    private void Update()
    {
        // float webcamAspectRatio = _webCamTexture.width / _webCamTexture.height;
        // this wont work because requested height and width only have an effect when set while the camera is not running
        // _webCamTexture.requestedHeight = _webCamTexture.height;
        // _webCamTexture.requestedWidth = _webCamTexture.width;

        if (_material == null)
            return;

        _material.SetTexture("_WebcamTex", _webCamTexture);
    }
}
