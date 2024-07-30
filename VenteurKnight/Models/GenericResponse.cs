namespace VenteurKnight.Models
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
