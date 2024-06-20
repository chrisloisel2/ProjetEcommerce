using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using exemple.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserDao _userDao;

    public UserController(UserDao userDao)
    {
        _userDao = userDao;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<UserPartialDTO>>> GetUsers()
    // {
    //     var users = await _userDao.GetUsers();

	// 	return Ok(users.Select(u => new UserPartialDTO
	// 	{
	// 		UserId = u.UserId,
	// 		Name = u.Name,
	// 		Bio = u.Bio,
	// 		Purchases = u.Purchases.Select(p => p.PurchaseId).ToList()
	// 	}));
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userDao.GetUser(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToDTO());
    }

    // [HttpPost]
	// public async Task<ActionResult<User>> Login(UserLoginDto userLoginDto)
	// {
	// 	var user = await _userDao.Login(userLoginDto);
	// 	if (user == null)
	// 	{
	// 		return NotFound();
	// 	}

	// 	return Ok(user);
	// }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Purchases = userDto.Purchases.Select(p => new Purchase
            {
                PurchaseArticles = p.Articles.Select(a => new PurchaseArticle
                {
                    Article = new Article { Name = a.Name }
                }).ToList()
            }).ToList()
        };

        await _userDao.AddUser(user);

        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        await _userDao.UpdateUser(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userDao.DeleteUser(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
