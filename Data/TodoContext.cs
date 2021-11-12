using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;
using TodoWebAPI.Models;

namespace TodoWebAPI.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoWebAPI.Models.Todo> Todo { get; set; }
        public DbSet<TodoWebAPI.Models.User> User { get; set; }
    }
}
