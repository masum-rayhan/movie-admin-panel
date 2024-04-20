using Authentication.Models;

namespace Authentication.ViewModels
{
    public class AuthViewModel
    {
        public IEnumerable<Auth> Auths { get; set; }
        public Auth NewAuth { get; set; }
    }
}
