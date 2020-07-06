namespace MyModels
{
    public class SearchModel
    {
        public string SearchBy { get; set; }
        public string SearchValue { get; set; }
        public string OrderBy { get; set; }
        public string AscDesc { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}