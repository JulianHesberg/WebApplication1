using infrastructure;

namespace service;

public class Validators
{
    public bool ValidArticle(Article article)
    {
        if (ValidHeadline(article) && ValidBody(article) && ValidAuthor(article))
            return true;

        return false;
    }
    public bool ValidHeadline(Article article)
    {
        if (5 >= article.Headline.Length && article.Headline.Length <= 30)
            return true;

        return false;
    }

    public bool ValidBody(Article article)
    {
        if (article.Body.Length <= 1000)
            return true;

        return false;
    }

    public bool ValidAuthor(Article article)
    {
        switch (article.Author)
        {
            case "Bob" :
                return true;
            case "Rob" :
                return true;
            case "Dob" :
                return true;
            case "Lob" :
                return true;
            default:
                return false;
        }
    }
}