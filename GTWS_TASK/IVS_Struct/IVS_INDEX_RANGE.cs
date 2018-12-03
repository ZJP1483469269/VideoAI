using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_INDEX_RANGE
    {
         public uint uiFromIndex;
         public uint uiToIndex;
    }
}
