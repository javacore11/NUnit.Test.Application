namespace NUnit.Test.Application.Models.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }
        public object? User { get; set; }
    }
}
