using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class WebcamCaptureV2 : MonoBehaviour
{
    
    #region Editable attributes

    [SerializeField] Shader _visualizerShader = null;

    #endregion
    
    #region Compile-time constants
    WebCamTexture _webcamRaw;
    RenderTexture _webcamBuffer;
    Material _visualizer;
    #endregion

    void Start()
    {
        Application.targetFrameRate = 60;
        RenderPipelineManager.endFrameRendering += OnEndFrameRendering;

        _webcamRaw = new WebCamTexture();
        _webcamBuffer = new RenderTexture(1920, 1080, 0);
        _visualizer = new Material(_visualizerShader);

        _webcamRaw.Play();
    }

    void OnDestroy()
    {
        RenderPipelineManager.endFrameRendering -= OnEndFrameRendering;

        if (_webcamRaw != null) Destroy(_webcamRaw);
        if (_webcamBuffer != null) Destroy(_webcamBuffer);
        if (_visualizer != null) Destroy(_visualizer);
    }

    void Update()
    {
        // Do nothing if there is no update on the webcam.
        if (!_webcamRaw.didUpdateThisFrame) return;

        var vflip = _webcamRaw.videoVerticallyMirrored;
        var scale = new Vector2(1, vflip ? -1 : 1);
        var offset = new Vector2(0, vflip ? 1 : 0);
        Graphics.Blit(_webcamRaw, _webcamBuffer, scale, offset);
    }

    void OnEndFrameRendering(ScriptableRenderContext context, Camera[] cameras) {
        _visualizer.SetPass(0);
        _visualizer.SetTexture("_CameraFeed", _webcamBuffer);
         Graphics.DrawProceduralNow(MeshTopology.Quads, 4, 1);
    }
}
