using FluentValidation.Results;

namespace ProjectInsta.Application.Services
{
    public class ResultService
    {
        public bool IsSucess { get; set; }
        public bool IsSucessDeleteLike { get; set; }
        public string Message { get; set; }
        public ICollection<ErrorValidation> Errors { get; set; }

        public static ResultService RequestError(string message, ValidationResult validationResult)
        {
            return new ResultService
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }

        public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
        {
            return new ResultService<T>
            {
                IsSucess = false,
                Message = message,
                Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }

        public static ResultService Fail(string message) => new ResultService { IsSucess = false, Message = message };
        public static ResultService<T> Fail<T>(string message) => new ResultService<T> { IsSucess = false, Message = message };

        public static ResultService Ok(string message) => new ResultService { IsSucess = true, IsSucessDeleteLike = false, Message = message };
        public static ResultService<T> Ok<T>(T data) => new ResultService<T> { IsSucess = true, IsSucessDeleteLike = false, Data = data };

    }
    public class ResultService<T>: ResultService 
    {
        public T Data { get; set; }
    }
}
