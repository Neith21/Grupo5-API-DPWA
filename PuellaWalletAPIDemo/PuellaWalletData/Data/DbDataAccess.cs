﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Data
{
    public class DbDataAccess : IDbDataAccess
    {
        private readonly IConfiguration _configuration;

        public DbDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T, P>(
            string storedProcedure, P parameters, string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            return await dbConnection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SaveDataAsync<T>(
            string storedProcedure,
            T parameters,
            string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            await dbConnection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> GetDataForeignAsync<T, U, P>(
            string storedProcedure,
            P parameters,
            Func<T, U, T>? map = null,
            string connection = "default",
            string splitOn = "Id")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            if (map == null)
            {
                return await dbConnection.QueryAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            else
            {
                return await dbConnection.QueryAsync<T, U, T>(
                    storedProcedure,
                    map,
                    parameters,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}