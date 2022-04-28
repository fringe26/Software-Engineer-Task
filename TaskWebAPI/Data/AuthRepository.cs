using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskWebAPI.Models;

namespace TaskWebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
           
            
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found!"; // not good to write that =>Hacker will find out that user exist and try to hack password ... Username or Password is Wrong- the best answer here;
                return response;
            }
            else if (!IsVerifiedPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = $"Wrong Password";
                return response;
            }
            else
            {
                //response.Data = user.Id.ToString(); // Send Token back there
                response.Data = CreateToken(user);
                return response;
            }
           
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(await UserExist(user.Name))
            {
                response.Success = false;
                response.Message = $"User with name:${user.Name} - already Exists!";
                return response;
            }

            CreateHashPassword(password,out byte[] passwordHash,out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            response.Data = user.Id;
            return response;
            
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }


        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsVerifiedPasswordHash(string password,byte[] passwordHashDb, byte[] passwordSaltDb)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSaltDb))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //need to check byte by byte so need LOOP

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHashDb[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
            };

            //Next need Symetric security Key FUCK IT OLEKSANDR! => this is the secret key from appsettings json file
            //To Access file appsettings.json motherfucker I need to dependency Injection IConfiguration

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
                );

            //with Symmetric Key we create Signing Credential
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Security Token Descriptor use to  gets the information to create a final token
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Now we need a new JWTSecurityToken Handler and use this and TokenDescriptor to create token

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);



            return tokenHandler.WriteToken(token); // to return it like String
        }
    }
}
