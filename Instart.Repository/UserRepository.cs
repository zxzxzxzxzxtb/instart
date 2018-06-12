﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instart.Repository;
using Instart.Models;
using System.Data.SqlClient;
using Dapper;
using System.Linq.Expressions;

namespace Instart.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByIdAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [User] where Id = @Id and Status=1;";
                return conn.QuerySingleAsync<User>(sql, new { Id = id });
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "select * from [User] where UserName = @name and Status=1;";
                return await conn.QuerySingleAsync<User>(sql, new { name = name });
            }
        }

        public async Task<PageModel<User>> GetListAsync(int pageIndex, int pageSize)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string countSql = "select count(1) from [User] where Status=1;";
                int total = await conn.ExecuteScalarAsync<int>(countSql);
                if (total == 0)
                {
                    return new PageModel<User>();
                }
                
                string sql = $@"select * from (   
　　　　                            select *, ROW_NUMBER() over (Order by Id desc) as RowNumber from [User] where Status=1 
　　                            ) as b  
　　                            where RowNumber between {(pageIndex - 1) * pageIndex} and {pageIndex * pageIndex};";
                var list = await conn.QueryAsync<User>(sql);

                return new PageModel<User>
                {
                    Total = total,
                    Data = list?.ToList()
                };
            }
        }

        public async Task<bool> InsertAsync(User model)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                var fields = model.ToFields(new List<string> { nameof(model.Id) });
                if (fields == null || fields.Count == 0)
                {
                    return false;
                }

                string sql = $"insert into [User] ({string.Join(",", fields)}) values ({string.Join(",", fields.Select(n => "@" + n))});";
                return await conn.ExecuteAsync(sql, model) > 0;
            }
        }

        public async Task<bool> UpdatePasswordAsync(int id, string password)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [User] set Password=@Password,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id, Password = password }) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = DapperFactory.GetConnection())
            {
                string sql = "update [User] set Status=0,ModifyTime=GETDATE() where Id=@Id;";
                return await conn.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}
