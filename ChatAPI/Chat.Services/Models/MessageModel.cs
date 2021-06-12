using System;
namespace Chat.Services.Models
{
    public class MessageModel
    {
        
            
            public string Content { get; set; }
            public string  CreatedAt { get; set; }
            public UserModel User { get; set; }
        
    }
}
