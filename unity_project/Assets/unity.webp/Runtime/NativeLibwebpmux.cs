using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unity.libwebp.Interop;

namespace unity.libwebp
{

    public static unsafe partial class NativeLibwebpmux
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        const string DLL_NAME = "libwebpmux";
#elif UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        const string DLL_NAME = "webpmux";
#elif UNITY_ANDROID
		const string DLL_NAME = "webpmux";
#elif UNITY_IOS
		const string DLL_NAME = "__Internal";
#elif UNITY_WEBGL
		const string DLL_NAME = "__Internal";
#endif



        [NativeTypeName("#define WEBP_MUX_ABI_VERSION 0x0109")]
        public const int WEBP_MUX_ABI_VERSION = 0x0109;



        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPGetMuxVersion();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPMux* WebPMuxCreateInternal([NativeTypeName("const WebPData *")] WebPData* param0, int param1, int param2);

        public static WebPMux* WebPMuxCreate([NativeTypeName("const WebPData *")] WebPData* bitstream, int copy_data)
        {
            return WebPMuxCreateInternal(bitstream, copy_data, WEBP_MUX_ABI_VERSION);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPMuxError WebPMuxPushFrame(WebPMux* mux, [NativeTypeName("const WebPMuxFrameInfo *")] WebPMuxFrameInfo* frame, int copy_data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPMuxError WebPMuxSetAnimationParams(WebPMux* mux, [NativeTypeName("const WebPMuxAnimParams *")] WebPMuxAnimParams* param);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPMuxError WebPMuxAssemble(WebPMux* mux, WebPData* assembled_data);


        // WebPAnimEncoder API
        // This API allows encoding (possibly) animated WebP images.
        // Code Example:
        /*
          WebPAnimEncoderOptions enc_options;
          WebPAnimEncoderOptionsInit(&enc_options);
          // Tune 'enc_options' as needed.
          WebPAnimEncoder* enc = WebPAnimEncoderNew(width, height, &enc_options);
          while(<there are more frames>) {
            WebPConfig config;
            WebPConfigInit(&config);
            // Tune 'config' as needed.
            WebPAnimEncoderAdd(enc, frame, timestamp_ms, &config);
          }
          WebPAnimEncoderAdd(enc, NULL, timestamp_ms, NULL);
          WebPAnimEncoderAssemble(enc, webp_data);
          WebPAnimEncoderDelete(enc);
          // Write the 'webp_data' to a file, or re-mux it further.
        */

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimEncoderOptionsInitInternal(WebPAnimEncoderOptions* param0, int param1);

        public static int WebPAnimEncoderOptionsInit(WebPAnimEncoderOptions* enc_options)
        {
            return WebPAnimEncoderOptionsInitInternal(enc_options, WEBP_MUX_ABI_VERSION);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern WebPAnimEncoder* WebPAnimEncoderNewInternal(int param0, int param1, [NativeTypeName("const WebPAnimEncoderOptions *")] WebPAnimEncoderOptions* param2, int param3);

        public static WebPAnimEncoder* WebPAnimEncoderNew(int width, int height, [NativeTypeName("const WebPAnimEncoderOptions *")] WebPAnimEncoderOptions* enc_options)
        {
            return WebPAnimEncoderNewInternal(width, height, enc_options, WEBP_MUX_ABI_VERSION);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimEncoderAdd(WebPAnimEncoder* enc, [NativeTypeName("struct WebPPicture *")] WebPPicture* frame, int timestamp_ms, [NativeTypeName("const struct WebPConfig *")] WebPConfig* config);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int WebPAnimEncoderAssemble(WebPAnimEncoder* enc, WebPData* webp_data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void WebPAnimEncoderDelete(WebPAnimEncoder* enc);
    }
}
