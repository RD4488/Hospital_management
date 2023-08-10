using HospitalManagement.DataAccess.Models;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.DataAccess.DataAccess;

public class HospitalManagementData : IHospitalManagementData
{
    private readonly ISqlDataAccess sql;
    public HospitalManagementData(ISqlDataAccess sql)
    {
        this.sql = sql;
    }
    public async Task<dynamic> CreateUser(UserModel user)
    {
        var results = await sql.LoadData<dynamic, dynamic>("dbo.spUser_CreateUser",
                                new { user.FirstName, user.LastName, user.Email, Role = user.Role.ToString(), Password = Encrypt(user.Password) },
                                "Default");
        return results.FirstOrDefault();
    }
    private static string Encrypt(string clearText)
    {
        string EncryptionKey = "RITESHAESKEY123";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public async Task<UserModel> CheckCredentials(string userName, string password)
    {
        var results = await sql.LoadData<UserModel, dynamic>("dbo.spUser_CheckCredentials",
                                new { Email = userName, Password = Encrypt(password) }, "Default");
        return results.FirstOrDefault();
    }
}
