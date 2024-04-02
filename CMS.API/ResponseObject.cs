namespace CMS.API
{
    public class ResponseObject<T>
    {
        public bool IsSuccess { get; set; }
        public T? Result { get; set; }
        public string Message { get; set; }
    }
}
