using RealMembership.Implementation.EF;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.Implementation.EF.Tests
{
    public class TestDbContext : UserAccountDbContext<UserAccount>
    {
        public TestDbContext(string name) : base(name) { }

        public TestDbContext(DbConnection connection) : base(connection) { }

        public TestDbContext() : base() { }
    }
}
