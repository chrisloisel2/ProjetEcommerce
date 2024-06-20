using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using exemple.Models;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ArticleDao _articleDao;

    public ArticleController(ArticleDao articleDao)
    {
        _articleDao = articleDao;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        var articles = await _articleDao.GetArticles();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _articleDao.GetArticle(id);

        if (article == null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<Article>> PostArticle(Article article)
    {
        await _articleDao.AddArticle(article);
        return CreatedAtAction("GetArticle", new { id = article.ArticleId }, article);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticle(int id, Article article)
    {
        if (id != article.ArticleId)
        {
            return BadRequest();
        }

        await _articleDao.UpdateArticle(article);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var success = await _articleDao.DeleteArticle(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
