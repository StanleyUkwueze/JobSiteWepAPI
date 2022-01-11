//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.AspNetCore.Identity;
//using Models;
//using Moq;
//using Services.JWTServices;
//using WebSiteAPI.Services.Implementations;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using NUnit.Framework;

//namespace WebSiteAPI.Services.AuthService_Test
//{
//   public class AuthenticationServiceTests
//    {
//        //private Mock<UserManager<AppUser>> _userManager;
//        //private Mock<SignInManager<AppUser>> _signInManager;
//        //private Mock<IPasswordHasher<AppUser>> _mockPasswordHasher;
//        //private AuthService _authService;
//        //private JwtService _jwtService;


//        public void SetUp()
//        {
//           // _mockPasswordHasher = new Mock<IPasswordHasher<AppUser>>();
//           var _userManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
//           var _contextAccessor = new Mock<IHttpContextAccessor>();
//            var _principalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
//           var  _signInManager = new Mock<SignInManager<AppUser>>(_userManager.Object,_contextAccessor.Object,_principalFactory.Object,null,null,null,null);
//            var _jwtservice = new Mock<IJwtService>();
//            var _authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtservice.Object);
//        }

//        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsForCorrectEmail()
//        {
//            string expectedUserEmail = "stanley@gmail.com";
//            string password = "testpassword";

//            AppUser user = new AppUser()
//            {
//                Email = expectedUserEmail,
//                FirstName = "Stanley",
//                LastName = "Jekwu"
                
//            };
//            var _userManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
//            var _contextAccessor = new Mock<IHttpContextAccessor>();
//            var _principalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
//            var _signInManager = new Mock<SignInManager<AppUser>>(_userManager.Object, _contextAccessor.Object, _principalFactory.Object, null, null, null, null);
//            var _jwtservice = new Mock<IJwtService>();
//            var _authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtservice.Object);
//           // _userManager.Setup(s => s.FindByEmailAsync(expectedUserEmail)).ReturnsAsync(user);

//            _userManager.Setup(s => s.FindByEmailAsync(expectedUserEmail)).ReturnsAsync(user);
//            _userManager.Setup(s => s.GetRolesAsync(user)).ReturnsAsync(new List<string> { } );
//            _signInManager.Setup(s => s.PasswordSignInAsync(user, password, false, false)).ReturnsAsync(SignInResult.Success);
//            _jwtservice.Setup(s => s.GenerateJWTToken(user, new List<string> { })).Returns("");


//            var res = await _authService.Login(expectedUserEmail, password, false);

//            Assert.IsTrue(res.Status);
            
//        }
//        public static Mock<UserManager<AppUser>> MockUserManager<AppUser>(List<AppUser> ls) where AppUser : class
//        {
//            string expectedUserEmail = "testuserEmail";

        

//            var store = new Mock<IUserStore<AppUser>>();
//            var mgr = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
//            mgr.Object.UserValidators.Add(new UserValidator<AppUser>());
//            mgr.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
           

//          mgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser, string>((x, y) => ls.Add(x));
//            mgr.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
//          // mgr.Setup(x => x.FindByEmailAsync(expectedUserEmail)).Returns(IdentityResult.Success)
//            return mgr;
//        }
//    }
//    }

