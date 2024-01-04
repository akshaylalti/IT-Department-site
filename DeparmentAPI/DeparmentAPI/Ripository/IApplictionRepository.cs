using DeparmentAPI.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeparmentAPI.Ripository
{
    public interface IApplictionRepository
    {
        Task<IdentityResult> SignUpAsync(SignInModel signInModel);
         Task<string> LogInAsync(LoginModel loginModel);
        Task<ApplictionUser> GetUserbyIdAsync(string UserId);
    }
}
