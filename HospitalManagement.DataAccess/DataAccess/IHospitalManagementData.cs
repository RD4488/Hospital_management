using HospitalManagement.DataAccess.Models;

namespace HospitalManagement.DataAccess.DataAccess;

public interface IHospitalManagementData
{
    Task<dynamic> CreateUser(UserModel user);
    Task<UserModel> CheckCredentials(string userName, string password);
}
