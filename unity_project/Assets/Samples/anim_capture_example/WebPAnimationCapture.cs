using System;
using unity.libwebp.Interop;
using unity.libwebp;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Threading;

[RequireComponent(typeof(Camera))]

public unsafe class WebPAnimationCapture : MonoBehaviour
{
    bool capturing = false;
    float period = 0f;
    float elapsedTime = 0;
    float startTime = 0;
    int frameCount = 0;
    List<CaptureImage> frames = new List<CaptureImage>();
    Texture2D colorBuffer;
    string saveDir;

    [SerializeField]
    int width = 256;
    [SerializeField]
    int height = 256;
    [SerializeField]
    int frameRate = 15;
    [SerializeField]
    float captureTime = 2f;
    [SerializeField]
    string fileName = "test.webp";


    void Start()
    {
        saveDir = Application.dataPath + "/../Capture";
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }
    }

    [ContextMenu("Start Capture")]
    public void StartCapture()
    {
        var cam = GetComponent<Camera>();
        var rt = RenderTexture.GetTemporary(width, height);
        cam.targetTexture = rt;
        RenderTexture.active = rt;
        colorBuffer = new Texture2D(width, height, TextureFormat.RGBA32, false);

        frames.Clear();
        frameCount = 0;
        period = 1f / frameRate;
        elapsedTime = 0f;
        startTime = Time.time;
        capturing = true;

        Debug.Log("Start capture");
    }

    public void EndCapture()
    {
        capturing = false;

        Debug.Log("Start encoding");
        var thread = new Thread(Encode);
        thread.Start();

        var cam = GetComponent<Camera>();
        var rt = cam.targetTexture;
        cam.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
    }

    private int GetTimeStamp()
    {
        return frameCount * (int)(period * 1000f);
    }

    private void Encode()
    {
        WebPAnimEncoderOptions encOptions = new WebPAnimEncoderOptions();
        if (NativeLibwebpmux.WebPAnimEncoderOptionsInit(&encOptions) == 0)
        {
            throw new Exception("WebPAnimEncoderOptionsInit failed. Wrong version?");
        }

        // create encoder
        var encoder = NativeLibwebpmux.WebPAnimEncoderNew(width, height, &encOptions);

        foreach (var frame in frames)
        {
            frame.Flip();
            var webpData = frame.GetWebPData();

            WebPPicture pic = new WebPPicture();
            if (NativeLibwebp.WebPPictureInit(&pic) == 0)
            {
                throw new Exception("WebPPictureInit failed. Wrong version?");
            }

            pic.width = width;
            pic.height = height;
            pic.use_argb = 1;

            if (NativeLibwebp.WebPPictureImportRGBA(&pic, webpData.bytes, width * 4) == 0)
            {
                NativeLibwebp.WebPPictureFree(&pic);
                throw new Exception("WebPPictureImportRGBA failed. Wrong version?");
            }

            // add frame to encoder
            if (NativeLibwebpmux.WebPAnimEncoderAdd(encoder, &pic, GetTimeStamp(), null) == 0)
            {
                NativeLibwebp.WebPPictureFree(&pic);
                throw new Exception("WebPAnimEncoderAdd failed. Wrong version?");
            }

            frameCount++;

            NativeLibwebp.WebPPictureFree(&pic);
        }

        // finish encoding
        NativeLibwebpmux.WebPAnimEncoderAdd(encoder, null, GetTimeStamp(), null);

        WebPData resultWebpData = new WebPData();
        NativeLibwebpdemux.WebPDataInit(&resultWebpData);

        if (NativeLibwebpmux.WebPAnimEncoderAssemble(encoder, &resultWebpData) == 0)
        {
            NativeLibwebpdemux.WebPDataClear(&resultWebpData);
            throw new Exception("WebPAnimEncoderAssemble failed. Wrong version?");
        }

        NativeLibwebpmux.WebPAnimEncoderDelete(encoder);

        int size = (int)resultWebpData.size;
        byte[] result = new byte[size];
        Marshal.Copy((IntPtr)resultWebpData.bytes, result, 0, size);

        var dest = $"{saveDir}/{fileName}";
        File.WriteAllBytes(dest, result);
        Debug.Log($"Capture success!\nSaved file at {dest}");
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCapture();
        }
#endif
    }

    private void OnPostRender()
    {
        if (!capturing) { return; };

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= period)
        {
            colorBuffer.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            frames.Add(new CaptureImage(colorBuffer));

            elapsedTime = 0f;
        }

        if (Time.time > (startTime + captureTime))
        {
            EndCapture();
        }
    }
}
