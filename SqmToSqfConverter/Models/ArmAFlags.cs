using System;

namespace SqmToSqfConverter.Models
{
    [Flags]
    public enum ArmAFlags : int
    {
        Unknown1 = 1 << 0,
        Unknown2 = 1 << 1,
        SetOnGround = 1 << 2,
        Unknown4 = 1 << 3,
        Unknown5 = 1 << 4,
        Unknown6 = 1 << 5,
        Unknown7 = 1 << 6,
        Unknown8 = 1 << 7,
        Unknown9 = 1 << 8,
        Unknown10 = 1 << 9,

        //Custom
        Unit = 1 << 10
    }
}
