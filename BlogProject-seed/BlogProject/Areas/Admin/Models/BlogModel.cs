namespace BlogProject.Areas.Admin.Models
{
    public class BlogModel
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }

        public string CategoryName { get; set; }
        public string WriterName { get; set; }
    }
}
