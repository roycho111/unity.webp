using System;

namespace unity.libwebp.Interop
{
    public partial struct EncodedFrame
    {
        public WebPMuxFrameInfo sub_frame_;  // Encoded frame rectangle.
        public WebPMuxFrameInfo key_frame_;  // Encoded frame if it is a key-frame.
        public int is_key_frame_;            // True if 'key_frame' has been chosen.
    }
}
