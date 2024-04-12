using TrackSense.API.Entities;
using TrackSense.API.Entities.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TrackSense.API.Services.ServiceUsers
{
    public class ManipulationUsers
    {
        private IDepotUsers m_depotUsers;
        private IConfiguration m_configuration;

        public ManipulationUsers(IDepotUsers p_depotUsers, IConfiguration configuration)
        {
            if (p_depotUsers == null)
            {
                throw new ArgumentNullException();
            }

            this.m_depotUsers = p_depotUsers;
            m_configuration = configuration;
        }

        public bool CheckUserToken(string p_token)
        {
            if (String.IsNullOrWhiteSpace(p_token))
            {
                throw new ArgumentNullException($"{nameof(p_token)} ne doit pas etre null ou vide - {nameof(ManipulationUsers)}.{nameof(CheckUserToken)}");
            }

            return this.m_depotUsers.CheckUserToken(p_token);
        }

        public bool UserExist(string p_userLogin)
        {
            return !String.IsNullOrEmpty(p_userLogin) 
                && this.m_depotUsers.UserExist(p_userLogin);
        }

        public string GenerateUserBearerToken(string p_userLogin, string p_userPassword)
        {
            if (String.IsNullOrWhiteSpace(p_userLogin) || String.IsNullOrWhiteSpace(p_userLogin))
            {
                throw new ArgumentNullException($"{nameof(p_userLogin)} et {p_userLogin} ne doivent pas etre nulls ou vides - {nameof(ManipulationUsers)}.{nameof(GenerateUserBearerToken)}");
            }
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, p_userLogin),
                };
                
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    m_configuration.GetSection("JWT:Secret").Value!));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: creds
                        
                    );

                // Write the token to a string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Print the token string
                //Console.WriteLine($"Token: {tokenString}");
                return tokenString;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating JWT token: {ex.Message}");
            }

            
        }
        public void Signup(string p_userLogin, string p_passWord, string p_email, string p_firstName, string p_lastName)
        {
            if (m_depotUsers.UserExist(p_userLogin))
            {
                throw new  InvalidOperationException("UserLogin is existed");
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword =  BCrypt.Net.BCrypt.HashPassword(p_passWord, salt);
            User newUser = new User()
            {
                UserFirstName = p_firstName,
                UserLastName = p_lastName,
                UserLogin = p_userLogin,
                UserEmail= p_email,
                Credential = new Credential()
                {
                    UserLogin = p_userLogin,
                    HashedPassword = hashedPassword,
                }
            };
            try
            {
                m_depotUsers.AddUser(newUser);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
          
        }

        public string Login(string p_userLogin, string p_password)
        {
            if (!m_depotUsers.UserExist( p_userLogin))
            {
                throw new InvalidOperationException("UserLogin not is existed");
            }
            //Verify password
            User user = this.m_depotUsers.GetUserByUserLogin(p_userLogin)!;
            
            bool matchedPassword = BCrypt.Net.BCrypt.Verify(p_password, user.Credential.HashedPassword);

            if (!matchedPassword)
            {
                throw new InvalidOperationException("Passwrood is incorrect");
            }
            return this.GenerateUserBearerToken(p_userLogin, p_password);

        }
        


        public IEnumerable<UserCompletedRide> GetCompletedRides(string p_userLogin)
        {
            if (string.IsNullOrWhiteSpace(p_userLogin) )
            {
                throw new ArgumentNullException($"{nameof(p_userLogin)} ne doit pas etre null ou vide - {nameof(ManipulationUsers)}.{nameof(GetCompletedRides)}");
            }

            IEnumerable<UserCompletedRide> completedRides = new List<UserCompletedRide>();

            if (m_depotUsers.UserExist(p_userLogin))
            {
                completedRides = this.m_depotUsers.GetUserCompletedRides(p_userLogin);
            }

            return completedRides;
        }

        public IEnumerable<UserPlannedRide> GetPlannedRides(string p_userLogin)
        {
            if (string.IsNullOrWhiteSpace(p_userLogin))
            {
                throw new ArgumentNullException($"{nameof(p_userLogin)} ne doit pas etre null ou vide - {nameof(ManipulationUsers)}.{nameof(GetPlannedRides)}");
            }

            IEnumerable<UserPlannedRide> plannedRides = new List<UserPlannedRide>();

            if (m_depotUsers.UserExist(p_userLogin))
            {
                plannedRides = this.m_depotUsers.GetUserPlannedRides(p_userLogin);
            }

            return plannedRides;
        }
    }
}
