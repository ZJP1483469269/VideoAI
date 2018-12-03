using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_DOMAIN_ROUTE_LIST
    {

       public UInt32 uiTotal;

       [MarshalAs(UnmanagedType.ByValArray)]
       public IVS_DOMAIN_ROUTE[] stDomainRoute;

    }
}
