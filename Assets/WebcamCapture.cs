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

        _webCamTexture.requestedHeight = 512;
        _webCamTexture.requestedWidth = 512;
        _webCamTexture.Play();
    }

    // Send texture to the shader.
    private void Update()
    {
        if (_material == null)
            return;

        _material.SetTexture("_WebcamTex", _webCamTexture);
    }
}
