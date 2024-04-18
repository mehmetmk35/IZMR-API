using IzmirGunesAPI.Application.DTOs;
using MediatR;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.LogimUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {


        public LoginProp Login { get; set; }


    }
}
