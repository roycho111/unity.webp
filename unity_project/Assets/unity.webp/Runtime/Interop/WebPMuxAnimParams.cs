using System;

namespace unity.libwebp.Interop
{
    public unsafe partial struct WebPMuxAnimParams
    {
        [NativeTypeName("uint32_t")]
        public uint bgcolor;  // Background color of the canvas stored (in MSB order) as:
                              // Bits 00 to 07: Alpha.
                              // Bits 08 to 15: Red.
                              // Bits 16 to 23: Green.
                              // Bits 24 to 31: Blue.
        public int loop_count;    // Number of times to repeat the animation [0 = infinite].
    }
}
