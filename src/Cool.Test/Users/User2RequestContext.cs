using System;
using System.Collections.Generic;
using System.Text;
using Cool.Common.Enums;
using Cool.Common.RequestContext;

namespace Cool.Test.Users
{
    public class User2RequestContext : IRequestContext
    {
        public string UserName { get; set; } = "testUser2";
        public string Name { get; set; } = "Teszt Béla";
        public string Email { get; set; } = "test2@test.com";
        public Role Role { get; set; } = Role.User;
    }
}
