using Cool.Common.Enums;

namespace Cool.Common.RequestContext
{
    public interface IRequestContext
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
