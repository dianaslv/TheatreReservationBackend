namespace Theatre.Web.Core.Helpers.Network.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccesfull { get; set; }
        public string Message { get; set; }
    }
    
    public class Response
    {
        public bool IsSuccesfull { get; set; }
        public string Message { get; set; }
    }
}