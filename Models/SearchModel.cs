﻿namespace MyModels
{
    public class SearchModel
    {
        public string SearchBy { get; set; }
        public string SearchValue { get; set; }
        public string OrderBy { get; set; } = "ascId";
        public int PageNumber { get; set; } = 1;
    }
}