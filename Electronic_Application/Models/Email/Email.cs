using Microsoft.AspNetCore.Http;

namespace Electronic_Application.Models.Email
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailToId { get; set; }
        public List<IFormFile> Attachments { get; set; }

        //public string EmailToId { get; set; }
        //public string EmailToName { get; set; }
        //public string EmailSubject { get; set; }
        //public string EmailBody { get; set; }
    }
}
