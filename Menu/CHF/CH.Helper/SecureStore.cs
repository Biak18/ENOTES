
using System;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace CH.Helper;

[SupportedOSPlatform("windows")]
public static class SecureStore
{
    public static string Protect(string text)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        byte[] protectedData = ProtectedData.Protect(
            data,
            null,
            DataProtectionScope.CurrentUser
        );
        return Convert.ToBase64String(protectedData);
    }

    public static string Unprotect(string encrypted)
    {
        byte[] data = Convert.FromBase64String(encrypted);
        byte[] unprotectedData = ProtectedData.Unprotect(
            data,
            null,
            DataProtectionScope.CurrentUser
        );
        return Encoding.UTF8.GetString(unprotectedData);
    }
}
