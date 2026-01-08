using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CH.Helper;
[SupportedOSPlatform("windows")]
public class A
{
    public static string GetString(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return string.Empty;
        }
        return obj.ToString().Trim();
    }


    public static int GetInt(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0;
        }
        int result;
        if (int.TryParse(obj.ToString(), out result))
        {
            return result;
        }
        return 0;
    }

    public static decimal GetDecimal(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0;
        }

        decimal result;
        if (decimal.TryParse(obj.ToString(), out result))
        {
            return result;
        }

        return 0;
    }

    public static Bitmap GetBitmap(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0)
            return null;

        using (var ms = new System.IO.MemoryStream(bytes))
        {
            using (var bmp = new Bitmap(ms))
            {
                return new Bitmap(bmp);
            }
        }
    }

    public static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256
        );

        byte[] hash = pbkdf2.GetBytes(32);

        return Convert.ToBase64String(salt) + "." +
               Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split('.');
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = Convert.FromBase64String(parts[1]);

        var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256
        );

        byte[] computed = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, computed);
    }

    public static void SetDirectorySecurity(string linePath)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(linePath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        DirectorySecurity directorySecurity = new DirectorySecurity();
        SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        directorySecurity.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
        directorySecurity.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);
        new DirectoryInfo(linePath).SetAccessControl(directorySecurity);
    }

    public static Control[] GetAllControls(Control containerControl)
    {
        List<Control> list = new List<Control>();
        Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
        queue.Enqueue(containerControl.Controls);
        while (queue.Count > 0)
        {
            Control.ControlCollection controlCollection = queue.Dequeue();
            if (controlCollection == null || controlCollection.Count == 0)
            {
                continue;
            }

            foreach (Control item in controlCollection)
            {
                list.Add(item);
                queue.Enqueue(item.Controls);
            }
        }

        return list.ToArray();
    }
}
