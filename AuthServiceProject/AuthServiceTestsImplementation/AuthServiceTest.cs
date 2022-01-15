using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Services.JWTServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSiteAPI.Services.Implementations;

namespace AuthServiceProject
{
    [TestClass]
    public class AuthServiceTest
    {
        private readonly AuthService _authService;
        string expectedUserEmail = "stanley@gmail.com";
        string password = "testpassword";
        public AuthServiceTest()
        {
            AppUser user = new AppUser()
            {
                Id = "hugiuuhhk",
                Email = "stanley@gmail.com",
                FirstName = "Stanley",
                LastName = "Jekwu",
                PasswordHash = "testpassword",

            };
            var _userManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null,
                null, null, null, null, null, null, null);
            _userManager.Setup(s => s.FindByEmailAsync(expectedUserEmail)).ReturnsAsync(user);
            _userManager.Setup(s => s.GetRolesAsync(user)).ReturnsAsync(new List<string> { });

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _principalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
            var _signInManager = new Mock<SignInManager<AppUser>>(_userManager.Object,
                _contextAccessor.Object, _principalFactory.Object, null, null, null);
            var _jwtservice = new Mock<IJwtService>();


            _signInManager.Setup(s => s.PasswordSignInAsync(user, password, false, false))
                .ReturnsAsync(SignInResult.Success);
            _jwtservice.Setup(s => s.GenerateJWTToken(user, It.IsAny<List<string>>())).Returns("hjhjhj");

            _authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtservice.Object);
        }
        //Valid case Test
        [TestMethod()]
        public async Task LoginTestWithCorrectCredentials()
        {
            var res = await _authService.Login(expectedUserEmail, password, false);
            Assert.IsTrue(res.Status);
        }

        //InValid case Test
        [TestMethod()]
        public async Task LoginTestWithWrongCredentials()
        {
            var res = await _authService.Login("wrongemail", "wrongPassword", false);
            Assert.IsFalse(res.Status);
        }

        //InValid case Test
        [TestMethod]
        public async Task UserEmailTest()
        {
            var res = await _authService.Login("wrongemail", password, false);
            Assert.IsFalse(res.Status);
        }

        //InValid case Test
        [TestMethod]
        public async Task UserPasswordTest()
        {
            var res = await _authService.Login(expectedUserEmail, "wrongPassword", false);
            Assert.IsFalse(res.Status);

        }
    }
}
