using AutoMapper;
using DeparmentAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeparmentAPI.Ripository
{
    public class ApplictionRepository : IApplictionRepository
    {
        private readonly UserManager<ApplictionUser> _userManager;
        private readonly SignInManager<ApplictionUser> _signInManager;
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private int _counter = 0;
        private Random random = new Random();

        public ApplictionRepository(UserManager<ApplictionUser> userManager,
            SignInManager<ApplictionUser> signInManager, UserContext userContext,
            IConfiguration configuration, IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userContext = userContext;
            _configuration = configuration;
            _mapper = mapper;
            int randomNumber = random.Next(100, 1000);

            // Assign the random number to the private variable
            _counter = randomNumber;
        }

        public async Task<IdentityResult> SignUpAsync(SignInModel signInModel)
        {
            string customUserId = $"20302{_counter:D2}";
            var user = new ApplictionUser()
            {
                Id = customUserId,
                FirstName = signInModel.FirstName,
                LastName = signInModel.LastName,
                MotherName = signInModel.MotherName,
                FatherName = signInModel.FatherName,
                AadharNumber = signInModel.AadharNumber,
                Address = signInModel.Address,
                City = signInModel.City,
                State = signInModel.State,
                Gender = signInModel.Gender,
                Category = signInModel.Category,
                DOB = signInModel.DOB,
                Religion = signInModel.Religion,
                Nationality = signInModel.Nationality,
                Course = signInModel.Course,
                Specialisations = signInModel.Specialisations,
                PinCode = signInModel.PinCode,
                PhoneNumber = signInModel.Phone,
                Email = signInModel.Email,
                UserName = customUserId,
            };
            _counter++;
            return await _userManager.CreateAsync(user, signInModel.Password);
            //var response = await _userManager.CreateAsync(user, signInModel.Password);
            //if (response.Succeeded)
            //{
            //    return customUserId;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public async Task<string> LogInAsync(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Id, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name , loginModel.Id),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() )
        };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplictionUser> GetUserbyIdAsync(string UserId)
        {
            var record = await _userContext.ApplictionUsers.FindAsync(UserId);
            return _mapper.Map<ApplictionUser>(record);
        }
        public async Task<ApplictionUser> UpdateUserDetails(ApplictionUser user)
        {
            try
            {
                var userDetails = _userContext.ApplictionUsers.FirstOrDefault(d => d.Id == user.Id);

                if (userDetails == null)
                {
                    return new();
                }

                userDetails = new ApplictionUser()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MotherName = user.MotherName,
                    FatherName = user.FatherName,
                    AadharNumber = user.AadharNumber,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    Gender = user.Gender,
                    Category = user.Category,
                    DOB = user.DOB,
                    Religion = user.Religion,
                    Nationality = user.Nationality,
                    Course = user.Course,
                    Specialisations = user.Specialisations,
                    PinCode = user.PinCode,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                };

                _userContext.Update(userDetails);
                _userContext.SaveChanges();
                return userDetails;
            }
            catch (Exception ex)
            {
                return new();
            }



        }
    }
}
