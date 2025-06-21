namespace Shared
{
    public enum HttpErrorCode
    {
        Success = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }

    public class Response<T>
    {
        public HttpErrorCode CodeError { get; set; }
        public string Msj { get; set; }
        public T Data { get; set; }
    }

    public class RegistroResponse
    {
        public HttpErrorCode CodeError { get; set; }
        public string Msj { get; set; }
    }
}
