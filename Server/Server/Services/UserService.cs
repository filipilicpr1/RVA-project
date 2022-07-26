﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Server.Dto.UserDto;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;
using System.Text;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly IConfigurationSection _secretKey;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenMakerFactory _tokenMakerFactory;
        private readonly IValidation<User> _userValidation;

        public UserService(IConfiguration config, IUnitOfWork unitOfWork, IMapper mapper, ITokenMakerFactory tokenMakerFactory, IValidation<User> userValidation)
        {
            _unitOfWork = unitOfWork;
            _secretKey = config.GetSection("SecretKey");
            _mapper = mapper;
            _tokenMakerFactory = tokenMakerFactory;
            _userValidation = userValidation;
        }

        public async Task<List<LogDTO>> GetLogs(string username)
        {
            string fileName = "logs/logs" + DateTime.Now.Year.ToString() + ".txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                StreamReader sr = new StreamReader(fs);
                string line;
                List<LogDTO> logs = new List<LogDTO>();
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    string logUsername = line.Substring(31).Split(' ')[1];
                    if (!String.Equals(username, logUsername))
                    {
                        continue;
                    }
                    LogDTO log = new LogDTO();
                    log.Timestamp = line.Substring(0, 23);
                    log.EventType = line.Substring(32, 3);
                    log.Message = line.Substring(line.LastIndexOf(':') + 2);
                    logs.Add(log);
                }
                sr.Close();
                logs.Reverse();
                return logs;
            }
            
        }

        public async Task<AuthDTO> Login(LoginDTO loginDTO)
        {
            User user = await _unitOfWork.Users.FindByUsername(loginDTO.Username);
            if(user == null)
            {
                throw new Exception("Invalid username");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }
            ITokenMaker tokenMaker = _tokenMakerFactory.CreateTokenMaker(Enums.ETokenMaker.JWT);
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            string tokenString = tokenMaker.CreateToken(user, secretKey);
            AuthDTO authDTO = _mapper.Map<AuthDTO>(user);
            authDTO.Token = tokenString;
            return authDTO;
        }

        public async Task<DisplayUserDTO> RegisterUser(NewUserDTO newUserDTO)
        {
            User user = _mapper.Map<User>(newUserDTO);
            ValidationResult result = _userValidation.Validate(user);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            bool userExists = await _unitOfWork.Users.FindByUsername(user.Username) != null;
            if(userExists)
            {
                throw new Exception("User with username " + user.Username + " already exists");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.Save();
            return _mapper.Map<DisplayUserDTO>(user);
        }

        public async Task<DisplayUserDTO> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            User user = await _unitOfWork.Users.FindByUsername(updateUserDTO.Username);
            if(user == null || user.Id != updateUserDTO.Id)
            {
                throw new Exception("Invalid request");
            }
            user.Name = updateUserDTO.Name;
            user.LastName = updateUserDTO.LastName;
            ValidationResult result = _userValidation.Validate(user);
            if (!result.IsValid)
            {
                throw new Exception(result.Message);
            }
            await _unitOfWork.Save();
            return _mapper.Map<DisplayUserDTO>(user);
        }
    }
}
