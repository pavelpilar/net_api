namespace DeveloperTest.Models
{
    public class NotFoundException : Exception
    {
    }

    public class AlreadyPaidException : Exception
    {
    }

    public class ReadonlyException : Exception
    {
    }

    public class ApiErrorResponse
    {
        public string Message { get; set; }
    }
}
