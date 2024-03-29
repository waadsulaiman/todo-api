﻿using Todo.Api.Data.Data.Models.Login;
using Todo.Api.Data.Data.Models.Register;
using Todo.Api.Data.Data.Models.Response;
using Todo.Api.Data.Data.Models.Token;
using Todo.Api.Data.Data.Objects;
using Todo.Api.Data.Services;

namespace Todo.Api.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class AuthMutations
    {
        #region SignInAsync

        /// <summary>
        /// Used for signing a user in.
        /// </summary>
        /// <param name="request">Represents the required data for signing a user in.</param>
        /// <param name="service">A service for authentication related operations.</param>
        /// <returns>
        /// A <see cref="Response{Token}"/>, containing the details of operation.
        /// </returns>
        public async Task<Response<Token>> SignInAsync(LoginRequest request, [Service] AuthService service)
        {
            var result = await service.SignInAsync(request);

            if (result.Succeeded)
            {
                return Response<Token>.Ok(result: result.Result);
            }

            return result.Fault.ErrorCode switch
            {
                ExceptionCodes.Code404NotFound => Response<Token>.NotFound(result.Fault.ErrorMessage),
                ExceptionCodes.Code401Unauthorized => Response<Token>.Unauthorized(result.Fault.ErrorMessage),
                _ => Response<Token>.BadRequest(result.Fault.ErrorMessage),
            };
        }

        #endregion SignInAsync

        #region RegisterAsync

        /// <summary>
        /// Used for registering a user.
        /// </summary>
        /// <param name="request">Represents the required data for signing a user in.</param>
        /// <param name="service">A service for authentication related operations.</param>
        /// <returns>
        /// A <see cref="Response{Token}"/>, containing the details of operation.
        /// </returns>
        public async Task<Response<Token>> RegisterAsync(RegisterRequest request, [Service] AuthService service)
        {
            var result = await service.RegisterAsync(request);

            if (result.Succeeded)
            {
                return Response<Token>.Created(result: result.Result);
            }

            return Response<Token>.BadRequest(result.Fault.ErrorMessage);
        }

        #endregion RegisterAsync
    }
}