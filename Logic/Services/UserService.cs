using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Logic.Services
{
     public interface IUserService
    {
        List<UserDTO> GetUsers(int currentUserId);
        List<IdName> GetUserTypes(int currentUserId);
        UserDTO GetUser(int id);
        bool AddUser(UserDTO user, int currentUserId);
        bool UpdateUser(UserDTO user);
        bool DeleteUser(int id);
        void ChangeUser2Manager(int id);
      
        bool ChangeUserTypeOrLenderAndDelete(int oldLender, int userType, int? newLender);
        public List<userTypeDTO> GetAllUserType();
    }

    public class UserService : IUserService
    {
        //  בעמוד זה יש שורות מוסלשות כי רחל אמרה שנטפל בקוד הזה בשבוע של ההרשאות לא למחוק פנינה

        private IDBService dbService;

        public UserService(IDBService dbService)
        {
            this.dbService = dbService;
        }
        public List<UserDTO> GetUsers(int currentUserId)
        {
            var users = dbService.entities.Users.ToList();
            var currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == currentUserId);
            //  if (currentUser.UserType.Id == 5)
            if (currentUser.UserType.Id == (int)userTypeDTO.userUnderLender)
            {
                users = users.Where(x => x.ManagerId == currentUser.Id).ToList();
            }
            //else if (currentUser.UserType.Id == 2)
            else if (currentUser.UserType.Id == (int)userTypeDTO.lender) 
            {
                users = users.Where(x => x.LenderId == currentUser.Id).ToList();
            }
            return users.Select(x => new UserDTO()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                Password = "********",
                UserType = new IdName
                {
                    Id = x.UserTypeId,
                    Name = x.UserType.Description
                },
                IsActive = x.IsActive,
                IsYearlyPay = x.IsYearlyPay,
                RegisterDate = x.RegisterDate,
                PayDate = x.PayDate,

            }).ToList();


        }

        public UserDTO GetUser(int id)
        {
            var dbUser = dbService.entities.Users.FirstOrDefault(x => x.Id == id);
            if (dbUser != null)
            {
                var user = new UserDTO()
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Phone = dbUser.Phone,
                    Password = dbUser.Password,
                    UserType = new IdName
                    {
                        Id = dbUser.UserTypeId,
                        Name = dbUser.UserType.Description
                    },
                    IsActive = dbUser.IsActive,
                    IsYearlyPay = dbUser.IsYearlyPay,
                    RegisterDate = dbUser.RegisterDate,
                    PayDate = dbUser.PayDate,
                    Lender = new IdName()
                    {
                        Id = dbUser.LenderId != null ? (int)dbUser.LenderId : 0,
                        Name = dbUser.LenderId != null ? dbUser.Lender.FirstName + " " + dbUser.Lender.LastName : ""
                    }
                    ,
                    Manager = new IdName()
                    {
                        Id = dbUser.ManagerId > 0 ? (int)dbUser.ManagerId : 0,
                        Name = dbUser.ManagerId > 0 ? dbUser.Manager.FirstName + " " + dbUser.Manager.LastName : ""
                    }
                };

                return user;

            }
            return new UserDTO();
        }


        public bool AddUser(UserDTO newUser, int currentUserId)
        {
            bool isExist = dbService.entities.Users.Any(x => x.Email == newUser.Email);
            if (!isExist)
            {
                var dbuser = new User()
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Phone = newUser.Phone,
                    Password = newUser.Password,
                    UserTypeId = newUser.UserType.Id,
                    IsActive = newUser.IsActive,
                    IsYearlyPay = newUser.IsYearlyPay,
                    RegisterDate = newUser.RegisterDate,
                    PayDate = newUser.PayDate,

                };

                if (newUser.Lender.Id > 0)
                {
                    dbuser.LenderId = newUser.Lender.Id;
                }
                if (newUser.Manager.Id > 0)
                {
                    dbuser.ManagerId = newUser.Manager.Id;
                }
                var currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == currentUserId);
                if (currentUser != null)
                {
                    //if (currentUser.UserType.Id == 5)//האם אתה מנהל מלווים
                    //{
                    //    dbuser.UserTypeId = 2;
                    //    dbuser.ManagerId = currentUser.Id;
                    //}

                    //if (currentUser.UserType.Id == 2)//האם אתה מלווה
                    if (currentUser.UserType.Id == (int)userTypeDTO.lender)
                    {
                        //dbuser.UserTypeId = 6;
                        dbuser.UserTypeId = (int)userTypeDTO.presenceUser;

                        dbuser.LenderId = currentUser.Id;
                    }
                }
                else
                {
                    if (currentUser == null)
                    {
                        //dbuser.UserTypeId = 3;
                        dbuser.UserTypeId = (int)userTypeDTO.user;

                    }
                }
                dbService.entities.Users.Add(dbuser);
                dbService.Save();
                return false;
            }
            return true;
        }

        public bool UpdateUser(UserDTO user)
        {
            var dbUser = dbService.entities.Users.FirstOrDefault(x => x.Id == user.Id);
            if (dbUser != null)
            {
                if (dbService.entities.Users.Any(x => x.Id != user.Id && x.Email == user.Email))
                {
                    return true;
                }
                else
                {
                    dbUser.Email = user.Email;
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.Phone = user.Phone;
                    dbUser.Password = user.Password;
                    if (user.UserType.Id != dbUser.UserTypeId)
                    {
                        //if (dbUser.UserTypeId == 2)
                        if (dbUser.UserTypeId == (int)userTypeDTO.lender)

                        {

                            var change = dbService.entities.Users.Where(x => x.ManagerId == user.Id).ToList();
                            change.ForEach(x=> x.ManagerId = null);
                        }
                        //else if (dbUser.UserTypeId == 6)
                        else if (dbUser.UserTypeId == (int)userTypeDTO.presenceUser)
                        {
                            var change = dbService.entities.Users.Where(x => x.LenderId == user.Id).ToList();
                            change.ForEach(x => x.LenderId = null);
                        }
                    }
                    dbUser.UserTypeId = user.UserType.Id;
                    dbUser.IsActive = user.IsActive;
                    dbUser.IsYearlyPay = user.IsYearlyPay;
                    dbUser.RegisterDate = user.RegisterDate;
                    dbUser.PayDate = user.PayDate;
                    if (user.Lender.Id > 0)
                    {
                        dbUser.LenderId = user.Lender.Id;
                    }
                    if (user.Lender == null)
                    {
                        dbUser.LenderId = null;
                    }
                    //if (user.UserType.Id != 6)
                    if (user.UserType.Id != (int)userTypeDTO.presenceUser)
                    {
                        dbUser.LenderId = null;
                    }
                    //if (user.Manager == null || user.UserType.Id != 2)
                    if (user.Manager == null || user.UserType.Id != (int)userTypeDTO.lender)
                    {
                        dbUser.ManagerId = null;
                    }
                    else if (user.Manager.Id > 0)
                    {
                        dbUser.ManagerId = user.Manager.Id;
                    }


                    dbService.Save();
                    return false;
                }
            }
            return false;

        }

        public bool DeleteUser(int id)
        {
            var dbUser = dbService.entities.Users.FirstOrDefault(x => x.Id == id);
            if (dbUser != null)
            {
                dbUser.IsActive = false;
                dbService.Save();
                return true;
            }
            return false;

        }


        public void ChangeUser2Manager(int id)
        {
            var dbUser = dbService.entities.Users.FirstOrDefault(x => x.Id == id);
            if (dbUser != null)
            {
                //dbUser.UserTypeId = 1;
                dbUser.UserTypeId = (int)userTypeDTO.systemAdministrator;

                dbService.Save();
            }
        }

        public List<IdName> GetUserTypes(int currentUserId)
        {
            var query = dbService.entities.UserTypes.ToList();
            var currentUser = dbService.entities.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser != null)
            {
                // if (currentUser.UserType.Id != 1)
                if (currentUser.UserType.Id != (int)userTypeDTO.systemAdministrator)
                {
                    //query = query.Where(x => x.Id != 1).ToList();
                    query = query.Where(x => x.Id != (int)userTypeDTO.systemAdministrator).ToList();
                }
            }
            var userTypes = query.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description
            }).ToList();
            return userTypes;
        }



        public bool ChangeUserTypeOrLenderAndDelete(int oldLender, int userType, int? newLender)
        {

            var usersUnderLender = dbService.entities.Users.Where(x => x.LenderId == oldLender).ToList();
            foreach (var u in usersUnderLender)
            {
                u.LenderId = newLender;
                u.UserTypeId = userType;
            }

            dbService.Save();
            this.DeleteUser(oldLender);
            return true;
        }

        public List<userTypeDTO> GetAllUserType()
        {

            var userTypesArray = Enum.GetValues(typeof(userTypeDTO));
            var userTypesList = new List<userTypeDTO>();

            foreach (var userType in userTypesArray)
            {
                userTypesList.Add((userTypeDTO)userType);
            }

            return userTypesList;
        }

    }
}
