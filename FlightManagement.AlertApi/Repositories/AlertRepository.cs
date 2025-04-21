using FlightManagement.AlertApi.Configurations;
using FlightManagement.AlertApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace FlightManagement.AlertApi.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly string _connectionString;

        public AlertRepository(IOptions<DbSettings> dbOptions)
        {
            _connectionString = dbOptions.Value.DefaultConnection;
        }

        public async Task CreateAsync(Alert alert)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_CreateAlert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AlertId", alert.AlertId);
            cmd.Parameters.AddWithValue("@UserId", alert.UserId);
            cmd.Parameters.AddWithValue("@Origin", alert.Origin);
            cmd.Parameters.AddWithValue("@Destination", alert.Destination);
            cmd.Parameters.AddWithValue("@FlightTime", (object?)alert.FlightTime ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<Alert?> GetByIdAsync(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_GetAlertById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AlertId", id);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();

            return new Alert
            {
                AlertId = reader.GetGuid(reader.GetOrdinal("AlertId")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                Origin = reader.GetString(reader.GetOrdinal("Origin")),
                Destination = reader.GetString(reader.GetOrdinal("Destination")),
                FlightTime = reader.IsDBNull(reader.GetOrdinal("FlightTime"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("FlightTime")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            };
        }

        public async Task<List<Alert>> GetByUserIdAsync(Guid userId)
        {
            var result = new List<Alert>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_GetAlertsByUserId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@UserId", userId);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var alert = new Alert
                {
                    AlertId = reader.GetGuid(reader.GetOrdinal("AlertId")),
                    UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                    Origin = reader.GetString(reader.GetOrdinal("Origin")),
                    Destination = reader.GetString(reader.GetOrdinal("Destination")),
                    FlightTime = reader.IsDBNull(reader.GetOrdinal("FlightTime"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("FlightTime")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                };

                result.Add(alert);
            }

            return result;
        }

        public async Task UpdateAsync(Guid id, Alert updated)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_UpdateAlert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AlertId", id);
            cmd.Parameters.AddWithValue("@Origin", updated.Origin);
            cmd.Parameters.AddWithValue("@Destination", updated.Destination);
            cmd.Parameters.AddWithValue("@FlightTime", (object?)updated.FlightTime ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("stp_DeleteAlert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AlertId", id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}