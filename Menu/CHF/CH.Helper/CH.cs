using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace CH.Helper;

// Control Helper
[SupportedOSPlatform("windows")]
public class CtrH
{
    public static IEnumerable<Control> GetAll(Control control, Type type)
    {
        IEnumerable<Control> enumerable = control.Controls.Cast<Control>();
        return from c in enumerable.SelectMany((Control ctrl) => GetAll(ctrl, type)).Concat(enumerable)
               where c.GetType() == type
               select c;
    }
}
