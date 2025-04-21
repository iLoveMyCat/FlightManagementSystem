using FlightManagement.AlertApi.Configurations;
using FlightManagement.AlertApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace FlightManagement.AlertApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IOptions<DbSettings> dbOptions)
        {
            _connectionString = dbOptions.Value.DefaultConnection;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_GetUserById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", id);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (!reader.HasRows) return null;

            await reader.ReadAsync();

            return new User
            {
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                DeviceToken = reader.IsDBNull(reader.GetOrdinal("DeviceToken")) ? null : reader.GetString(reader.GetOrdinal("DeviceToken")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_UserExists", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", id);

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToBoolean(result);
        }

        public async Task CreateIfNotExistsAsync(User user)
        {
            if (await ExistsAsync(user.UserId)) return;

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_CreateUser", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateDeviceTokenAsync(Guid userId, string deviceToken)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_UpdateDeviceToken", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@DeviceToken", deviceToken);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
