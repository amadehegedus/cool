using System;
using System.Collections.Generic;
using System.Text;
using Cool.Common.Enums;
using Cool.Common.RequestContext;

namespace Cool.Test.Users
{
    public class AdminRequestContext : IRequestContext
    {
        public string UserName { get; set; } = "admin";
        public string Name { get; set; } = "Admin Admin";
        public string Email { get; set; } = "admin@admin.com";
        public Role Role { get; set; } = Role.Admin;
    }
}
