using infrastructure;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Controllers;

[ApiController]
public class ArticleController : ControllerBase
{
    private readonly Service _service;

    public ArticleController(Service service)
    {
        _service = service;
    }

    [Route("api/articles")]
    [HttpPost]
    public Article AddArticle(Article article)
    {
        return _service.AddArticle(article);
    }

    [Route("api/articles/{articleId}")]
    [HttpGet]
    public Article GetArticleById([FromRoute]int articleId)
    {
        return _service.GetArticleById(articleId);
    }

    [Route("api/feed")]
    [HttpGet]
    public IEnumerable<NewsFeedItem> GetNewsFeed()
    {
        return _service.GetNewsFeed();
    }

    [Route("api/articles/{articleId}")]
    [HttpDelete]
    public object DeleteArticleById([FromRoute]int articleId)
    {
        _service.DeleteArticleById(articleId);
        return "article successfully deleted";
    }

    [Route("api/articles/{articleId}")]
    [HttpPut]
    public Article UpdateArticle(Article article)
    {
        return _service.UpdateArticle(article);
    }

    [Route("api/articles")]
    [HttpGet]
    public IEnumerable<SearchArticleItem> SearchForArticles([FromQuery]string searchTerm, [FromQuery] int pagesize)
    {
        return _service.SearchForArticles(searchTerm, pagesize);
    }

}