
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Input;

namespace Halloween.Input
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct MonocerosKeyboardState
    {
        [FieldOffset(0)]
        internal KeyboardState State;
        [FieldOffset(0)]
        internal readonly uint PackedState0;
        [FieldOffset(4)]
        internal readonly uint PackedState1;
        [FieldOffset(8)]
        internal readonly uint PackedState2;
        [FieldOffset(12)]
        internal readonly uint PackedState3;
        [FieldOffset(16)]
        internal readonly uint PackedState4;
        [FieldOffset(20)]
        internal readonly uint PackedState5;
        [FieldOffset(24)]
        internal readonly uint PackedState6;
        [FieldOffset(28)]
        internal readonly uint PackedState7;
    }
}
