using Microsoft.AspNetCore.Mvc;
using LoginAndReg.Models;
using Microsoft.AspNetCore.Identity;

public class UsersController : Controller
{
  private MyContext _context;

  public UsersController(MyContext context)
  {
    _context = context;
  }


  [HttpGet("/registration")]
  public IActionResult Registration()
  {
    return View("Registration");
  }


  [HttpPost("/register")]
  public IActionResult Register(User newUser)
  {
    if (ModelState.IsValid)
    {
      if (_context.Users.Any(u => u.Email == newUser.Email))
      {
        ModelState.AddModelError("Email", "Email is already in use");
      }
    }

    if (ModelState.IsValid == false)
    {
      return Registration();
    }

    PasswordHasher<User> HashWord = new PasswordHasher<User>();
    newUser.Password = HashWord.HashPassword(newUser, newUser.Password);

    _context.Users.Add(newUser);
    _context.SaveChanges();

    HttpContext.Session.SetInt32("UUID", newUser.UserId);

    return RedirectToAction("Success", "Home");
  }


  [HttpGet("/login")]
  public IActionResult Login()
  {
    return View("Login");
  }


  [HttpPost("/login/user")]
  public IActionResult LoginUser(LoginUser loginUser)
  {
    if (ModelState.IsValid == false)
    {
      return Login();
    }

    User? dbUser = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

    if (dbUser == null)
    {
      ModelState.AddModelError("Email", "and password don't match");
      return Login();
    }

    PasswordHasher<LoginUser> HashWord = new PasswordHasher<LoginUser>();
    PasswordVerificationResult pwCompare = HashWord.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

    if (pwCompare == 0)
    {
      ModelState.AddModelError("Password", "doesn't match this email");
      return Login();
    }

    HttpContext.Session.SetInt32("UUID", dbUser.UserId);
    return RedirectToAction("Success", "Home");
  }


  [HttpPost("/logout")]
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return RedirectToAction("Registration");
  }
}