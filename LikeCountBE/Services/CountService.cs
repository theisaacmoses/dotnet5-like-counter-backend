using System;
using System.Collections.Generic;
using LikeCountBE.Repos;
using Microsoft.Extensions.Logging;

namespace LikeCountBE.Services
{
    public class CountService : ICountService
    {
        private readonly ILogger<CountService> _logger;
        private readonly CountRepo _repo;

        public CountService(ILogger<CountService> logger, CountRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public int? GetCount()
        {
            var result = new List<int?>();
            try
            {
                _logger.LogInformation(_repo.Connection.ConnectionString);
                _repo.Connection.Open();
                using var cmd = _repo.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM count";
                var reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _repo.Connection.Close();
            return result.Count > 0 ? result[0] : null;
        }

        public bool UpdateCount(int count)
        {
            var result = false;
            try
            {
                _logger.LogInformation(_repo.Connection.ConnectionString);
                _repo.Connection.Open();
                using var cmd = _repo.Connection.CreateCommand();
                cmd.CommandText = $"UPDATE count set value = {count}";
                var res = cmd.ExecuteNonQuery();
                if(res > 0)
                    result = true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            _repo.Connection.Close();
            return result;
        }
    }

    public interface ICountService {
        int? GetCount();
        bool UpdateCount(int count);
    }

}
