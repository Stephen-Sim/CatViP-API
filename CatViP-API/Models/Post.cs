﻿using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class Post
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public bool Status { get; set; }

    public long UserId { get; set; }

    public long PostTypeId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<MentionedCat> MentionedCats { get; set; } = new List<MentionedCat>();

    public virtual ICollection<PostImage> PostImages { get; set; } = new List<PostImage>();

    public virtual PostType PostType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
