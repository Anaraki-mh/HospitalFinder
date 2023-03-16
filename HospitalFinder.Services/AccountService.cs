using HospitalFinder.Core.DataAccess;
using HospitalFinder.Domain.Users;
using HospitalFinder.WebEssentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Services
{
    public class AccountService : IAccountService
    {

        #region Properties and fields

        private IUserRepository _userRepository { get; }
        private ILockoutRepository _lockoutRepository { get; }

        #endregion


        #region Constructor

        public AccountService(IUserRepository userRepository, ILockoutRepository lockoutRepository)
        {
            _userRepository = userRepository;
            _lockoutRepository = lockoutRepository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Checks if an IP address is locked out or not
        /// </summary>
        /// <param name="ipAddress">IP address of the sender of the request</param>
        /// <returns>True for locked out or false for not locked out</returns>
        public async Task<bool> IsIPLockedOut(string ipAddress)
        {
            // Get lockout by IP address
            var lockout = await _lockoutRepository.GetByIPAsync(ipAddress);

            // If this IP does not have any corresponding lockouts return false
            if (lockout is null)
                return false;

            // If there is a lockout record for this IP, the IP is locked out and the expiration date time has not arrived, return true
            if (lockout.IsLockedOut == true && DateTime.UtcNow < lockout.LockoutExpirationDateTime)
                return true;

            // Otherwise delete the lockout record and return false
            await _lockoutRepository.DeleteAsync(lockout);
            return false;
        }

        /// <summary>
        /// Creates a lockout record for the IP address (if it doesnt already have one), adds to the failed login attempts and if the number of
        /// failed login attempts is higher than 5, locks out the IP
        /// </summary>
        /// <param name="ipAddress">IP address of the sender of the request</param>
        /// <returns></returns>
        public async Task Lockout(string ipAddress)
        {
            var lockout = await _lockoutRepository.GetByIPAsync(ipAddress);

            if (lockout is null)
            {
                lockout = await _lockoutRepository.CreateAsync(new Lockout()
                {
                    IPAddress = ipAddress,
                    FailedAttempts = 0,
                });
            }

            lockout.FailedAttempts++;

            if (lockout.FailedAttempts > 4)
            {
                lockout.IsLockedOut = true;
                lockout.LockoutExpirationDateTime = DateTime.UtcNow.AddHours(1);
            }

            await _lockoutRepository.UpdateAsync(lockout);
        }

        /// <summary>
        /// Checks existance of a user for the provided email address, then checks equality of the provided password hash with user's password hash
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's hashed password</param>
        /// <returns>A tuple of User and Login Success</returns>
        public async Task<(User, bool)> Login(string email, string password)
        {
            // Get user by email their email
            var user = await _userRepository.GetByEmailAsync(email);

            // If user is null...
            if (user is null)
            {
                // Return null for user and false for login
                return (null, false);
            }

            // If user exists and provided password hash matches users password...
            if (user.PasswordHash == Hash.SHA256(password))
            {
                // Return the user and true for successful login
                return (user, true);
            }

            // Otherwise return the user and false for failed login
            return (user, false);
        }

        /// <summary>
        /// Creates a new account with the provided email and password
        /// </summary>
        /// <param name="email">Account email</param>
        /// <param name="password">Account password</param>
        /// <returns>A boolean for success of the sign up process</returns>
        public async Task<bool> SignUp(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
                return false;

            await _userRepository.CreateAsync(new User
            {
                EmailAddress = email,
                PasswordHash = Hash.SHA256(password),
            });

            return true;
        }

        /// <summary>
        /// Generates a unique token, hashes it and saves it to user's TokenHash property 
        /// </summary>
        /// <param name="user">A User object</param>
        /// <returns>A unique token with the email of the user attached to the beginning of it</returns>
        public async Task<string> GenerateToken(User user)
        {
            string userEmail = Url.CreateUrlFriendlyString(user.EmailAddress.Substring(0, user.EmailAddress.IndexOf('@')));
            string token = userEmail + "_" + Guid.NewGuid().ToString().Replace("-", "");

            user.TokenHash = Hash.SHA256(token);
            user.TokenExpirationDateTime = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateAsync(user);
            return token;
        }

        /// <summary>
        /// Checks if the provided token is valid and hasn't expired
        /// </summary>
        /// <param name="token">User's token</param>
        /// <returns>A boolean representing the validity of the token</returns>
        public async Task<bool> IsTokenValid(string token)
        {
            User user = await _userRepository.GetByTokanHashAsync(Hash.SHA256(token));

            if (user is null || DateTime.UtcNow > user?.TokenExpirationDateTime)
                return false;

            return true;
        }
        #endregion

    }
}
