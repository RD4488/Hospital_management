namespace HospitalManagement.DataAccess.DataAccess;
public interface ISqlDataAccess
{
    Task<List<T>> LoadData<T, U>(string storedProcedure, U Parameters, string connectionStringName);
    Task SaveData<T>(string storedProcedure, T Parameters, string connectionStringName);
}