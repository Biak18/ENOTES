using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CH.Helper;

public abstract class BaseDataProvider
{
    private static readonly Dictionary<string, SqlParameter[]> ParameterCache
        = new Dictionary<string, SqlParameter[]>();
    private static readonly object cacheLock = new();

    public static void LoadParameters(SqlCommand cmd)
    {
        string key = cmd.CommandText.ToLower();

        lock (cacheLock)
        {
            if (ParameterCache.TryGetValue(key, out var cachedParams))
            {
                foreach (var p in cachedParams)
                    cmd.Parameters.Add(((ICloneable)p).Clone());
                return;
            }

            SqlCommandBuilder.DeriveParameters(cmd);

            var paramCopies = cmd.Parameters
                .Cast<SqlParameter>()
                .Select(x => (SqlParameter)((ICloneable)x).Clone())
                .ToArray();

            ParameterCache[key] = paramCopies;

            cmd.Parameters.Clear();
            foreach (var p in paramCopies)
                cmd.Parameters.Add(((ICloneable)p).Clone());
        }
    }
}

