using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IListService
    {
        ListsDTO GetAllLists();
        List<IdName> GetList(IdNameDB item);
        bool AddItem(IdNameDB idName);
        bool DeleteItem(IdNameDB idName);
        bool UpdateItem(IdNameDB idName);
    }
    public class ListService : IListService
    {
        private IDBService dbService;
        public ListService(IDBService dbService)
        {
            this.dbService = dbService;
        }

        public ListsDTO GetAllLists()
        {
            var lists = new ListsDTO();
            lists.UserTypes = dbService.entities.UserTypes.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description
            }).ToList();
            lists.Cities = dbService.entities.Cities.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            lists.Areas = dbService.entities.Areas.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description,
                Type = x.Type
            }).ToList();
            lists.UrgencyDebts = dbService.entities.UrgencyDebts.Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.Description
            }).ToList();
            lists.Lenders = dbService.entities.Users.Where(x => x.UserTypeId == 2 && x.IsActive).Select(x => new IdName()
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName
            }).ToList();
            return lists;
        }

        public List<IdName> GetList(IdNameDB item)
        {
            List<IdName> list = new List<IdName>();
            if (item.TableCode == TableCode.UserTypes)
            {
                list = dbService.entities.UserTypes.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description
                }).ToList();
            }
            else if (item.TableCode == TableCode.Cities)
            {
                list = dbService.entities.Cities.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            else if (item.TableCode == TableCode.Status)
            {
                list = dbService.entities.Statuses.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description
                }).ToList();
            }
            else if (item.TableCode == TableCode.UrgencyDebt)
            {
                list = dbService.entities.UrgencyDebts.Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description
                }).ToList();
            }
            else if (item.TableCode == TableCode.Areas)
                list = dbService.entities.Areas.Where(x => x.Type == item.Type).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.Description,
                    Type = x.Type
                }).ToList();
            else if (item.TableCode == TableCode.Users)
            {
                list = dbService.entities.Users.Where(x => x.UserTypeId == item.Type && x.IsActive).Select(x => new IdName()
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName
                }).ToList();
            }
            return list;
        }

        public bool AddItem(IdNameDB idName)
        {
            switch (idName.TableCode)
            {
                case TableCode.UserTypes:
                    {
                        return AddUserType(idName);
                    }
                case TableCode.Cities:
                    {
                        return AddCity(idName);
                    }
                case TableCode.Areas:
                    {
                        return AddSubject(idName);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return AddUrgencyDebt(idName);
                    }
                default:
                    break;
            }
            return false;
        }

        public bool DeleteItem(IdNameDB idName)
        {
            switch (idName.TableCode)
            {
                case TableCode.UserTypes:
                    {
                        return DeleteUserType(idName.Id);
                    }
                case TableCode.Cities:
                    {
                        return DeleteCity(idName.Id);
                    }
                case TableCode.Areas:
                    {
                        return DeleteSubject(idName.Id);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return DeleteUrgencyDebt(idName.Id);
                    }
                default:
                    break;
            }
            return false;
        }

        public bool UpdateItem(IdNameDB idName)
        {
            switch (idName.TableCode)
            {
                case TableCode.UserTypes:
                    {
                        return UpdateUserType(idName);
                    }
                case TableCode.Cities:
                    {
                        return UpdateCity(idName);
                    }
                case TableCode.Areas:
                    {
                        return UpdateSubject(idName);
                    }
                case TableCode.UrgencyDebt:
                    {
                        return UpdateUrgencyDebt(idName);
                    }
                default:
                    break;
            }
            return false;

        }

        private bool UpdateCity(IdNameDB idName)
        {
            var dbCity = dbService.entities.Cities.FirstOrDefault(x => x.Id == idName.Id);
            if (dbCity == null) return false;
            dbCity.Name = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateUserType(IdName idName)
        {
            var dbUserType = dbService.entities.UserTypes.FirstOrDefault(x => x.Id == idName.Id);
            if (dbUserType == null) return false;
            dbUserType.Description = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateSubject(IdName idName)
        {
            var dbSubject = dbService.entities.Areas.FirstOrDefault(x => x.Id == idName.Id);
            if (dbSubject == null) return false;
            dbSubject.Description = idName.Name;
            dbService.Save();
            return true;
        }

        private bool UpdateUrgencyDebt(IdName idName)
        {
            var dBurgencyDebt = dbService.entities.UrgencyDebts.FirstOrDefault(x => x.Id == idName.Id);
            if (dBurgencyDebt != null)
            {
                dBurgencyDebt.Description = idName.Name;
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool DeleteUserType(int userTypeId)
        {
            var userType = dbService.entities.UserTypes.FirstOrDefault(x => x.Id == userTypeId);
            if (userType != null)
            {
                dbService.entities.UserTypes.Remove(dbService.entities.UserTypes.FirstOrDefault(x => x.Id == userTypeId));
                dbService.Save();
                return true;
            }
            return false;

        }

        private bool DeleteCity(int id)
        {
            var city = dbService.entities.Cities.FirstOrDefault(x => x.Id == id);
            if (city != null)
            {
                dbService.entities.Cities.Remove(dbService.entities.Cities.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool DeleteSubject(int id)
        {
            var ar = dbService.entities.User2Areas.FirstOrDefault(x => x.Id == id);
            if (ar != null)
            {
                dbService.entities.Areas.Remove(dbService.entities.Areas.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool DeleteUrgencyDebt(int id)
        {
            if (dbService.entities.UrgencyDebts.Any(x => x.Id == id))
            {
                dbService.entities.UrgencyDebts.Remove(dbService.entities.UrgencyDebts.FirstOrDefault(x => x.Id == id));
                dbService.Save();
                return true;
            }
            return false;
        }

        private bool AddUserType(IdName idName)
        {
            if (dbService.entities.UserTypes.Any(x => x.Description == idName.Name)) return false;
            var newItem = new UserType()
            {
                Description = idName.Name
            };
            dbService.entities.UserTypes.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddCity(IdNameDB idName)
        {
            if (dbService.entities.Cities.Any(x => x.Name == idName.Name)) return false;
            var newItem = new City()
            {
                Name = idName.Name
            };
            dbService.entities.Cities.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddSubject(IdNameDB idName)
        {
            if (dbService.entities.Areas.Any(x => x.Description == idName.Name))
            {
                return false;
            }
            var newItem = new Area()
            {
                Description = idName.Name,
                Type = (int)idName.Type
            };
            dbService.entities.Areas.Add(newItem);
            dbService.Save();
            return true;
        }

        private bool AddUrgencyDebt(IdNameDB idName)
        {
            if (dbService.entities.UrgencyDebts.Any(x => x.Description == idName.Name))
            {
                return false;
            }
            var newItem = new UrgencyDebt()
            {
                Description = idName.Name
            };
            dbService.entities.UrgencyDebts.Add(newItem);
            dbService.Save();
            return true;
        }
    }
}
