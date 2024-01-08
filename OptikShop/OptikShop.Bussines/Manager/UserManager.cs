using Microsoft.AspNetCore.DataProtection;
using OptikShop.Business.Dtos;

using OptikShop.Business.Service;
using OptikShop.Business.Types;
using OptikShop.Data.Entities;
using OptikShop.Data.Enums;
using OptikShop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Manager
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == addUserDto.Email.ToLower()).ToList();
            

            // hasMail içerisi dolu mu yoksa null mı kontrol etmem.

            if (hasMail.Any()) // hasMail is not null
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu Eposta adresli bir kullanıcı zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                FirstName = addUserDto.Firstname,
                LastName = addUserDto.LastName,
                Email = addUserDto.Email,
                Password = _dataProtector.Protect(addUserDto.Password),
                UserType = UserTypeEnum.user

            };

            _userRepository.Add(userEntity);

            return new ServiceMessage()
            {
                IsSucceed = true
            };
        }

        public UserInfoDto LoginUser(LoginUserDto loginUserDto)
        {
			var userEntity = _userRepository.Get(x => x.Email == loginUserDto.Email);

			if (userEntity is null)
			{
				return null;
				// form üzerinde gönderilen mail adresi ile eşleşen bir veri tabloda yoksa, oturum açılamayacağı için, geriye hiç bir bilgi dönmüyorum.
			}

			var rawPassword = _dataProtector.Unprotect(userEntity.Password);

			if (loginUserDto.Password == rawPassword)
			{
				return new UserInfoDto()
				{
					Id = userEntity.Id,
					FirstName = userEntity.FirstName,
					LastName = userEntity.LastName,
					UserType = userEntity.UserType,
					Email = userEntity.Email
				};
			}
			else
			{
				return null;
			}
		}
    }
}
