﻿namespace CodeBase.Web.ViewModels.Lectures
{
    using System.Collections.Generic;

    public class LectureListViewModel : PagingViewModel
    {
        public IEnumerable<LectureViewModel> Lectures { get; set; }
    }
}
