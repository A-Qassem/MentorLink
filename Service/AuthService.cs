using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto)
        {
            // Check if email already exists
            var existingEmail = await _unitOfWork.Trainees.GetByEmailAsync(signUpDto.Email);
            if (existingEmail != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email is already registered"
                };
            }

            // Hash password
            var passwordHash = HashPassword(signUpDto.Password);

            // Create new trainee
            var trainee = new Trainee
            {
                Name = signUpDto.Name,
                Email = signUpDto.Email,
                PasswordHash = passwordHash,
                Phone = signUpDto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Trainees.AddAsync(trainee);
            await _unitOfWork.SaveChangesAsync();

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(trainee);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            trainee.RefreshToken = refreshToken;
            trainee.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Success = true,
                Message = "Registration successful",
                Token = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    TokenType = "Bearer"
                },
                User = new TraineeDto
                {
                    TraineeId = trainee.TraineeId,
                    Name = trainee.Name,
                    Email = trainee.Email,
                    Phone = trainee.Phone,
                    Level = trainee.Level,
                    PictureUrl = trainee.PictureUrl
                }
            };
        }

        public async Task<AuthResponseDto> SignInAsync(SignInDto signInDto)
        {
            // Find trainee by email
            var trainee = await _unitOfWork.Trainees.GetByEmailAsync(signInDto.Email);
            if (trainee == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // Verify password
            if (!VerifyPassword(signInDto.Password, trainee.PasswordHash))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // Update last login
            trainee.LastLoginAt = DateTime.UtcNow;

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(trainee);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            trainee.RefreshToken = refreshToken;
            trainee.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    TokenType = "Bearer"
                },
                User = new TraineeDto
                {
                    TraineeId = trainee.TraineeId,
                    Name = trainee.Name,
                    Email = trainee.Email,
                    Phone = trainee.Phone,
                    Level = trainee.Level,
                    PictureUrl = trainee.PictureUrl
                }
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var trainee = await _unitOfWork.Trainees.GetByRefreshTokenAsync(refreshToken);
            if (trainee == null || trainee.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid refresh token"
                };
            }

            // Generate new tokens
            var accessToken = _jwtService.GenerateAccessToken(trainee);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Update refresh token
            trainee.RefreshToken = newRefreshToken;
            trainee.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Success = true,
                Message = "Token refreshed successfully",
                Token = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    TokenType = "Bearer"
                }
            };
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var trainee = await _unitOfWork.Trainees.GetByRefreshTokenAsync(refreshToken);
            if (trainee == null)
            {
                return false;
            }

            trainee.RefreshToken = null;
            trainee.RefreshTokenExpiryTime = null;
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }
    }

    public interface IAuthService
    {
        Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto);
        Task<AuthResponseDto> SignInAsync(SignInDto signInDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}
