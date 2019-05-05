using Microsoft.AspNetCore.SignalR;
using SignalRSimpleSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSimpleSample.Hubs
{
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds { get; set; } = new HashSet<string>();
        public static string LastMessage { get; set; }
    }

    public class TestMessagesHub : Hub
    {
        public Task SendMessage(ChatMessage chatMessage)
        {
            UserHandler.LastMessage = chatMessage.Message;
            return Clients.All.SendAsync("Send", chatMessage);
        }
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            //连接时往终端发送最后一条消息
            Clients.Client(Context.ConnectionId).SendAsync("Send", new ChatMessage { Message = UserHandler.LastMessage, Sent = DateTime.Now });
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
