using System.Collections.Generic;
using DCDesktop.Models;

namespace DCDesktop.Response;

public class ChannelListResponse
{
    public ChannelListData Data { get; set; } = new();
}

public class ChannelListData
{
    public List<Channel> Channels { get; set; } = new();
}

public class ChannelMessagesListResponse
{
    public ChannelMessagesListData Data { get; set; } = new();
}

public class ChannelMessagesListData
{
    public List<Message> Messages { get; set; } = new();
}