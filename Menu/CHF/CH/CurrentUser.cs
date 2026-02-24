using CH.Helper;
using System.Data;
using System.Runtime.Versioning;

namespace CH;
[SupportedOSPlatform("windows")]
public class CurrentUser
{
    public string CompanyCode { get; }
    public string UserID { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Address { get; }
    public string Department { get; }
    public string Role { get; }
    public string GroupCode { get; }
    public string EmployeeCode { get; }
    public string Position { get; }
    public string PassWord { get; }

    public CurrentUser(DataRow dto)
    {
        CompanyCode = A.GetString(dto["CD_COM"]);
        UserID = A.GetString(dto["CD_USER"]);
        UserName = A.GetString(dto["NM_USER"]);
        Email = A.GetString(dto["DC_EMAIL"]);
        Address = A.GetString(dto["DC_ADDRESS1"]);
        Role = A.GetString(dto["FG_ROLE"]);
        PassWord = A.GetString(dto["DC_PASSWORD"]);

        //Department = A.GetString(dto[""]);
        //GroupCode = A.GetString(dto[""]);
        //EmployeeCode = A.GetString(dto[""]);
        //Position = A.GetString(dto[""]);
    }
}
