using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class Chat
{
    public long Id { get; set; }

    public string Message { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public long UserSendId { get; set; }

    public long UserReceiveId { get; set; }

    public virtual User UserReceive { get; set; } = null!;

    public virtual User UserSend { get; set; } = null!;
}
