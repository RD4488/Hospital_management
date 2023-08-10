using HospitalManagement.DataAccess.Models;

namespace HospitalManagement.DataAccess.DataAccess;

public interface IHospitalManagementData
{
    Task<dynamic> CreateUser(UserModel user);
    Task<dynamic> UpdateUser(UserModel user);
    Task<dynamic> DeleteUser(string email);
    Task<UserModel> CheckCredentials(string userName, string password);
}
