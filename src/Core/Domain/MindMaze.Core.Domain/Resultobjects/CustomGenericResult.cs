using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Domain.Resultobjects
{
    public class CustomResult<TResponse> : CustomResult
    {

        private readonly TResponse _response;
        private CustomResult(TResponse? response, bool issucces, Error error) : base(true, null)
        {
            _response = response;
        }

        private CustomResult(Error error) : base(false, error)
        {
        }

        public TResponse Response => _response;



        public static CustomResult<TResponse> Success(TResponse response)
        {

            return new CustomResult<TResponse>(response, true, null);
        }

        public static CustomResult<TResponse> Failure(Error error)
        {
            if (error == null)

                return CustomResult<TResponse>.Failure(new Error("You can not create Failure Result wihtout errors"));


            return new CustomResult<TResponse>(error);
        }

        public static implicit operator CustomResult<TResponse>(TResponse response) => Success(response);

    }
}
