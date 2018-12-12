using Embed.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.Persistance.Repositories.Auth
{
    /// <summary>
    /// Represents the authentication repository class.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        /// <summary>
        /// Sets the injected application Db context.
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Sets the identity user manager.
        /// </summary>
        private UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthRepository"/> class.
        /// </summary>
        public AuthRepository()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
        }

        /// <summary>
        /// Register new user to system.
        /// </summary>
        /// <param name="userModel">The user to be registered.</param>
        /// <returns>The created identity result.</returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        /// <summary>
        /// Find user based credential.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password of the account.</param>
        /// <returns>The existing identity user.</returns>
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        /// <summary>
        /// Dispose all the resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();

        }
    }
}
