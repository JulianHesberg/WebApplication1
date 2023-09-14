using Dapper;

namespace infrastructure;

public class Repository
{
    public Article AddArticle(Article article)
    {
        var sql = $@"INSERT INTO news.articles(headline, body, author, articleimgurl)
                        VALUES(@headline, @body, @author, @articleImgUrl)
                        RETURNING
                        articleid as {nameof(Article.ArticleId)},
                        headline as {nameof(Article.Headline)},
                        body as {nameof(Article.Body)},
                        author as {nameof(Article.Author)},
                        articleimgurl as {nameof(Article.ArticleImgUrl)};";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new { headline = article.Headline, body = article.Body, author = article.Author, articleImgUrl = article.ArticleImgUrl });
        }
    }

    public Article GetArticleById(int articleId)
    {
        var sql = $@"SELECT * FROM news.articles WHERE articleid = @articleId;";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new { articleId });
        }
    }

    public bool DeleteArticleById(int articleId)
    {
        var sql = $@"DELETE FROM news.articles WHERE articleid = @articleId;";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { articleId }) == 1;
        }
    }

    public IEnumerable<NewsFeedItem> GetNewsFeed()
    {
        var sql = $@"SELECT
                    SUBSTRING(body FROM 1 FOR 50) as body,
                    headline,
                    articleimgurl,
                    articleid
                FROM news.articles;";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.Query<NewsFeedItem>(sql);
        }
    }

    public Article UpdateArticle(Article article)
    {
        var sql = $@"UPDATE news.articles
                    SET headline = @headline, body = @body, author = @author, articleimgurl = @articleimgurl
                    WHERE articleid = @articleId
                    RETURNING
                    articleid as {nameof(Article.ArticleId)},
                    headline as {nameof(Article.Headline)},
                    body as {nameof(Article.Body)},
                    author as {nameof(Article.Author)},
                    articleimgurl as {nameof(Article.ArticleImgUrl)};";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql,
                new
                {
                    headline = article.Headline, body = article.Body, author = article.Author,
                    articleimgurl = article.ArticleImgUrl, articleId = article.ArticleId
                });
        }
    }

    public IEnumerable<SearchArticleItem> SearchForArticles(string searchterm, int pagesize)
    {
        var sql = $@"SELECT headline, articleid, author FROM news.articles
                        WHERE LOWER(body) LIKE '%' || @searchterm || '%'
                        ORDER BY articleid
                        LIMIT @pagesize;";

        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.Query<SearchArticleItem>(sql, new { searchterm = searchterm.ToLower(), pagesize });
        }
    }

    public IEnumerable<SearchArticleItem> LevenshteinSearch(string searchTerm, int pageSize)
    {
        var sql = $@"SELECT headline, articleid, author FROM news.articles
                        ORDER BY levenshtein(headline, @searchTerm) ASC
                        LIMIT @pagesize;";
        
        using (var conn = Helper.DataSource.OpenConnection())
        {
            return conn.Query<SearchArticleItem>(sql, new { searchTerm, pageSize });
        }
    }
}