using System;
using System.Threading.Tasks;
using Embed.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Embed.Persistance.Repositories.Auth
{
    public interface IAuthRepository: IDisposable
    {
        Task<IdentityUser> FindUser(string userName, string password);
        Task<IdentityResult> RegisterUser(UserModel userModel);
    }
}