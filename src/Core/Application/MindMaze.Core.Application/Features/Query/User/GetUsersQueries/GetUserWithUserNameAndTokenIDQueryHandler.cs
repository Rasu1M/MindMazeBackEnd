﻿using MediatR;
using Microsoft.IdentityModel.Tokens;
using MindMaze.Core.Application.Interfaces;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.User;
using MindMaze.Core.Domain.Resultobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Features.Query.User.GetUsersQueries
{
    public class GetUserWithUserNameAndTokenIDQueryHandler : IRequestHandler<GetUserWithUserNameAndTokenIDQuery, CustomResult<UserInfoResponse>>
    {
        private readonly IReadGenericRepository<Users> _read;

        public GetUserWithUserNameAndTokenIDQueryHandler(IReadGenericRepository<Users> read)
        {
            _read = read;
        }

        public async Task<CustomResult<UserInfoResponse>> Handle(GetUserWithUserNameAndTokenIDQuery request, CancellationToken cancellationToken)
        {
            

            var user = await _read.Getasync(x => x.UserName == request.UserName && x.IDToken == request.TokenID);

            if (user == null)
                return CustomResult<UserInfoResponse>.Failure(new Error("User not found"));

            return new UserInfoResponse()
            {
                UserName = user.UserName,
                IDToken = user.IDToken,
                point = user.Point
            };


        }
    }
}
