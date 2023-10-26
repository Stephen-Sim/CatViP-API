﻿using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class PostImage
{
    public long Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public bool IsBloodyContent { get; set; }

    public long PostId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
