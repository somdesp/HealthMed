namespace EConstrumarket.Construmanager.Core.Domain.Test.Configurations
{
    public class RespLogin
    {
        public string message { get; set; }
        public bool success { get; set; }
        public Data data { get; set; }
        public List<object> errors { get; set; }
    }

    public class Data
    {
        public object tokenID { get; set; }
        public string accessToken { get; set; }
        public DateTime create { get; set; }
        public DateTime expires { get; set; }
    }


}
