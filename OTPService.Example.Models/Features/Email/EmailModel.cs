namespace OTPService.Example.Models.Features.Email
{
    public class EmailModel
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        public bool IsUseTemplate { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
    }
}
