using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPMuxFrameInfo
    {
        public WebPData bitstream;      // image data: can be a raw VP8/VP8L bitstream
                                        // or a single-image WebP file.
        public int x_offset;   // x-offset of the frame.
        public int y_offset;   // y-offset of the frame.
        public int duration;   // duration of the frame (in milliseconds).

        public WebPChunkId id;          // frame type: should be one of WEBP_CHUNK_ANMF
                                        // or WEBP_CHUNK_IMAGE
        public WebPMuxAnimDispose dispose_method;   // Disposal method for the frame.
        public WebPMuxAnimBlend blend_method;       // Blend operation for the frame.

        [NativeTypeName("uint32_t[1]")]
        public fixed uint pad[1];
    }
}
