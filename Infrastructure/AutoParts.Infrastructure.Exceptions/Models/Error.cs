namespace AutoParts.Infrastructure.Exceptions.Models
{
    //using Resources;

    public class Error
    {
        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public Error(string code)
        {
            Code = code;
            //Description = ErrorMessages.ResourceManager.GetString(code);
        }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}

