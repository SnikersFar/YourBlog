﻿using System;
using System.Collections.Generic;

namespace YourBlog.EfStuff.DbModel
{
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public virtual Category IsCategory { get; set; }
        public virtual User Creator { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<Report> Reports { get; set; }   

    }
}
