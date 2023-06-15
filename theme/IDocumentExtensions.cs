namespace Docable
{
    /// <summary>
    /// IDocument Extensions.
    /// </summary>
    public static class IDocumentExtensions
    {
        public static string GetBlogContentString(this IExecutionContext context, IDocument model)
        {
            var filePath = new System.Text.StringBuilder();
            filePath.Append("output").Append(context.GetLink(model)).Append(".html");
            var blogContent = System.IO.File.ReadAllText(filePath.ToString());
            var start = blogContent.IndexOf("<!-- Blog Content Start -->");
            var end = blogContent.IndexOf("<!-- Blog Content End -->");
            return blogContent[start..end];
        }
    }
}
