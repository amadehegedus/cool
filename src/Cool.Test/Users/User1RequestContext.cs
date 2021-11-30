using System;
using System.Collections.Generic;
using System.Text;
using Cool.Common.Enums;
using Cool.Common.RequestContext;

namespace Cool.Test.Users
{
    public class User1RequestContext : IRequestContext
    {
        public string UserName { get; set; } = "testUser1";
        public string Name { get; set; } = "Teszt Elek";
        public string Email { get; set; } = "test@test.com";
        public Role Role { get; set; } = Role.User;
    }
}
