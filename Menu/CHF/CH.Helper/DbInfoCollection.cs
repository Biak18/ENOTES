using System.Collections.Generic;

namespace CH.Helper;

public class DbInfoCollection : List<DbInfo>
{
    public void AddInfo(DbInfo info) => this.Add(info);
}
