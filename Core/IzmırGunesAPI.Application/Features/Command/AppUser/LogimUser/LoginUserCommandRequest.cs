using MediatR;

namespace IzmirGunesAPI.Application.Features.Command.AppUser.LogimUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {

        
        public string BranchCode { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string DbName { get; set; }
      

    }
}
