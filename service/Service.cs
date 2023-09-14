using System.ComponentModel.DataAnnotations;
using infrastructure;

namespace service;

public class Service
{
    private readonly Repository _repository;
    
    public Service(Repository repository)
    {
        _repository = repository;
    }

    public Article AddArticle(Article article)
    {
            return _repository.AddArticle(article);
    }

    public Article GetArticleById(int articleId)
    {
        return _repository.GetArticleById(articleId);
    }

    public void DeleteArticleById(int articleId)
    {
        if (!_repository.DeleteArticleById(articleId))
            throw new Exception("Could not delete article with given Id");
    }

    public IEnumerable<NewsFeedItem> GetNewsFeed()
    {
        return _repository.GetNewsFeed();
    }

    public Article UpdateArticle(Article article)
    {
        return _repository.UpdateArticle(article);
    }

    public IEnumerable<SearchArticleItem> SearchForArticles(string searchterm, int pagesize)
    {
        if (searchterm.Length < 3)
            throw new Exception("Search term must be at least 3 characters");
        if (pagesize < 1)
            throw new Exception("Page Size must be 1 or greater");
        return _repository.SearchForArticles(searchterm, pagesize);
        //return _repository.LevenshteinSearch(searchterm, pagesize);
    }


}