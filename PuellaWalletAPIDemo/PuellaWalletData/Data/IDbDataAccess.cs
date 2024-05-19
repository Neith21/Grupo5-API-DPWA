using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Data
{
    public interface IDbDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(string storedProcedure, P parameters, string connection = "default");
        Task<IEnumerable<T>> GetDataForeignAsync<T, U, P>(string storedProcedure, P parameters, Func<T, U, T>? map = null, string connection = "default", string splitOn = "Id");
        Task SaveDataAsync<T>(string storedProcedure, T parameters, string connection = "default");
    }
}
