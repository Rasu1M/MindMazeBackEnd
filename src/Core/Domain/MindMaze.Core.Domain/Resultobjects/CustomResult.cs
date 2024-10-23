using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.Resultobjects
{
    public class CustomResult
    {
        protected CustomResult(bool isSuccess, Error errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; set; }

        public Error Errors { get; set; }

        public static CustomResult Success() {

            return new CustomResult(true, null);
        }

        public static CustomResult Failure(Error error)
        {
            if (error == null)
            {

                return CustomResult.Failure(new Error("You can not create Failure Result wihtout errors"));
            }

            return new CustomResult(false, error);
        }

        public static implicit operator CustomResult(bool value) => CustomResult.Success();

    }

    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
