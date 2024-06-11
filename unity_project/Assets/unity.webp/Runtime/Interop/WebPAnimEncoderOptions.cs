namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPAnimEncoderOptions
    {
        public WebPMuxAnimParams anim_params;  // Animation parameters.

        public int minimize_size;       // If true, minimize the output size (slow). Implicitly
                                        // disables key-frame insertion.
        public int kmin;
        public int kmax;            // Minimum and maximum distance between consecutive key
                                    // frames in the output. The library may insert some key
                                    // frames as needed to satisfy this criteria.
                                    // Note that these conditions should hold: kmax > kmin
                                    // and kmin >= kmax / 2 + 1. Also, if kmax <= 0, then
                                    // key-frame insertion is disabled; and if kmax == 1,
                                    // then all frames will be key-frames (kmin value does
                                    // not matter for these special cases).
        public int allow_mixed;     // If true, use mixed compression mode; may choose
                                    // either lossy and lossless for each frame.
        public int verbose;         // If true, print info and warning messages to stderr.

        [NativeTypeName("uint32_t[4]")]
        public fixed uint padding[4];  // Padding for later use.
    }
}
