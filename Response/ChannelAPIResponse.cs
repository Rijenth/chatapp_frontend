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