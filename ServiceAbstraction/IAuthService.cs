using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto);
        Task<AuthResponseDto> SignInAsync(SignInDto signInDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}
