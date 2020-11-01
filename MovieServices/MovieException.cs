using System;

namespace MovieServices
{
    [Serializable]
    public class MovieException : Exception
    {
        public int ExceptionCode { get; set; }

        public string ExceptionMessage { get; set; }

        public MovieException(ExceptionCode code, string message) : base(message)
        {
            ExceptionCode = (int)code;
            ExceptionMessage = message;
        }

    }
    public enum ExceptionCode
    {
        NotFound = 404
    }
}
