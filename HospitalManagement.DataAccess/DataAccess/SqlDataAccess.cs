using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManagement.DataAccess.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration config;

    public SqlDataAccess(IConfiguration config)
    {
        this.config = config;
    }
    public async Task<List<T>> LoadData<T, U>(string storedProcedure, U Parameters, string connectionStringName)
    {
        string connectionString = config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(storedProcedure, Parameters,
            commandType: CommandType.StoredProcedure);
        return rows.ToList();

    }
    public async Task SaveData<T>(string storedProcedure, T Parameters, string connectionStringName)
    {
        string connectionString = config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(storedProcedure, Parameters, commandType: CommandType.StoredProcedure);
    }
}
