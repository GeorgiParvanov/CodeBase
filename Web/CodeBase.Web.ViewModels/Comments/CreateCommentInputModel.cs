﻿namespace CodeBase.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int LectureId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
