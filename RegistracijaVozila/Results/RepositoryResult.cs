namespace RegistracijaVozila.Results
{
    public class RepositoryResult<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string? Message { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public static RepositoryResult<T> Ok(T data) =>
            new RepositoryResult<T> { Data = data, Success = true };

        public static RepositoryResult<T> Ok() =>
        new RepositoryResult<T> { Success = true };

        public static RepositoryResult<T> Ok(T data, string message)
        {
            return new RepositoryResult<T> { Data = data, Success = true, Message = message };
        }

        public static RepositoryResult<T> Fail(string message) =>
            new RepositoryResult<T> {Success = false, Message = message};

        public static RepositoryResult<T> Fail(IEnumerable<string> errors)
        {
            return new RepositoryResult<T>
            {
                Success = false,
                Errors = errors.ToList()
            };
        }

    }
}
