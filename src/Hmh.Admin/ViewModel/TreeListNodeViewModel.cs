using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hmh.Admin.ViewModel
{
    public class CategoryTreeListNodeViewModel
    {
        public int Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public int SortCode { get; set; }
        public int Profit { get; set; }
        public int Distribution { get; set; }
    }

    public class ShowCategoryTreeListNodeViewModel
    {
        public int Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public int SortCode { get; set; }
        public string Link { get; set; }
        public string Logo { get; set; }
        public bool IsShow { get; set; }
    }
}