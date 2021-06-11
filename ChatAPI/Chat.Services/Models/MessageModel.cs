using System;
namespace Chat.Services.Models
{
    public class MessageModel
    {
        
            
            public string Content { get; set; }
            public DateTime Date { get; set; }
            public string From { get; set; }
            public string Room { get; set; }
            public string Avatar { get; set; }
        
    }
}
