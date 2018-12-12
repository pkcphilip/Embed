using System;
using System.Threading.Tasks;
using Embed.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Embed.Persistance.Repositories.Auth
{
    /// <summary>
    /// Represents the authentication repository class.
    /// </summary>
    public interface IAuthRepository: IDisposable
    {
        /// <summary>
        /// Find user based credential.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password of the account.</param>
        /// <returns>The existing identity user.</returns>
        Task<IdentityUser> FindUser(string userName, string password);

        /// <summary>
        /// Register new user to system.
        /// </summary>
        /// <param name="userModel">The user to be registered.</param>
        /// <returns>The created identity result.</returns>
        Task<IdentityResult> RegisterUser(UserModel userModel);
    }
}