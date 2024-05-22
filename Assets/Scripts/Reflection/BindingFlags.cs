using System;

namespace Reflection
{
    [Flags]
    public enum BindingFlags
    {
        CreateInstance = 512, // 0x00000200
        DeclaredOnly = 2,
        Default = 0,
        DoNotWrapExceptions = 33554432, // 0x02000000
        ExactBinding = 65536, // 0x00010000
        FlattenHierarchy = 64, // 0x00000040
        GetField = 1024, // 0x00000400
        GetProperty = 4096, // 0x00001000
        IgnoreCase = 1,
        IgnoreReturn = 16777216, // 0x01000000
        Instance = 4,
        InvokeMethod = 256, // 0x00000100
        NonPublic = 32, // 0x00000020
        OptionalParamBinding = 262144, // 0x00040000
        Public = 16, // 0x00000010
        PutDispProperty = 16384, // 0x00004000
        PutRefDispProperty = 32768, // 0x00008000
        SetField = 2048, // 0x00000800
        SetProperty = 8192, // 0x00002000
        Static = 8,
        SuppressChangeType = 131072, // 0x00020000
    }
}