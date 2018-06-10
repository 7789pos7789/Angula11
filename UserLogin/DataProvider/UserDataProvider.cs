
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UserLogin.Models;

namespace UserLogin.DataProvider
{
    public class UserDataProvider : IUserDataProvider
    {
        //private readonly string connectionString = "Server=localhost;Database=Login;Trusted_Connection=True;";
        private string connectionString = @"Server=127.0.0.1;Database=Login;User Id=sa; Password=7789pos7789;";
        //private readonly string connectionString = @"Data Source=127.0.0.1;Initial Catalog=Login;User ID=sa;Password=7789pos7789";
        private readonly LoginContext _logincontext;
        private SqlConnection sqlConnection;

        public async Task AddUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", user.UserId);
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@Email", user.Email);
                dynamicParameters.Add("@Password", user.Password);

                await sqlConnection.ExecuteAsync(
                    "spAddUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteUser(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                await sqlConnection.ExecuteAsync(
                    "spDeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<User> GetUser(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                return await sqlConnection.QuerySingleOrDefaultAsync<User>(
                    "spGetUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

       public async Task<IEnumerable<User>> GetUsers()
        { 
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                
                return await sqlConnection.QueryAsync<User>(
                    "spGetUsers",
                    null,
                    commandType: CommandType.StoredProcedure);
                    
            }
           
        }

        public async Task UpdateUser(User user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", user.UserId);
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@Email", user.Email);
                dynamicParameters.Add("@Password", user.Password);
                await sqlConnection.ExecuteAsync(
                    "spUpdateUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        /*public Task AddUser(User product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User product)
        {
            throw new NotImplementedException();
        }*/
    }
}
