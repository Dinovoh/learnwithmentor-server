﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnWithMentorBLL.Interfaces;
using LearnWithMentorDTO;
using LearnWithMentorDAL.Entities;

namespace LearnWithMentorBLL.Services
{
    public class UserService: BaseService, IUserService
    {
        public UserService() : base()
        {
        }
        public UserDTO Get(int id)
        {
            User user = db.Users.Get(id);
            if (user == null)
                return null;
            return new UserDTO(user.Id,
                               user.FirstName,
                               user.LastName,
                               user.Roles.Name,
                               user.Blocked);
        }
        public bool BlockById(int id)
        {
            var item = db.Users.Get(id);
            if (item != null)
            {
                item.Blocked = true;
                db.Users.Update(item);
                return true;
            }
            return false;
        }
        public bool UpdateById(int id, UserDTO user)
        {
            bool modified = false;
            var item = db.Users.Get(id);
            if (item != null)
            {
                if (user.FirstName != null)
                {
                    item.FirstName = user.FirstName;
                    modified = true;
                }
                if (user.LastName != null)
                {
                    item.LastName = user.LastName;
                    modified = true;
                }
                if (user.Blocked != null)
                {
                    item.Blocked = user.Blocked.Value;
                    modified = true;
                }
                Role updatedRole;
                if (db.Roles.TryGetByName(user.Role, out updatedRole))
                {
                    item.Role_Id = updatedRole.Id;
                    modified = true;
                }
                db.Users.Update(item);
            }
            return modified;
        }
        public bool Add(UserLoginDTO userLoginDTO)
        {
            User toAdd = new User();
            toAdd.Email = userLoginDTO.Email;
            toAdd.Password = BCrypt.Net.BCrypt.HashPassword(userLoginDTO.Password);
            Role toAddRole;
            Role studentRole;
            db.Roles.TryGetByName("Student", out studentRole);
            toAdd.Role_Id = db.Roles.TryGetByName(userLoginDTO.Role, out toAddRole) ?
                toAddRole.Id : studentRole.Id;
            toAdd.FirstName = userLoginDTO.FirstName;
            toAdd.LastName = userLoginDTO.LastName;
            return true;
        }
        public IEnumerable<UserDTO> Search(string[] str, int? roleId)
        {
            var users = db.Users.Search(str, roleId);
            List<UserDTO> dtos = new List<UserDTO>();
            foreach (var user in users)
            {
                dtos.Add(new UserDTO(user.Id,
                                     user.FirstName,
                                     user.LastName,
                                     user.Roles.Name,
                                     user.Blocked));
            }
            return dtos;
        }
        public IEnumerable<UserDTO> GetUsersByRole(int role_id)
        {
            var users = db.Users.GetUsersByRole(role_id);
            if (users == null)
                return null;
            List<UserDTO> dtos = new List<UserDTO>();
            foreach (var user in users)
            {
                dtos.Add(new UserDTO(user.Id,
                                     user.FirstName,
                                     user.LastName,
                                     user.Roles.Name,
                                     user.Blocked));
            }
            return dtos;
        }
        public string ExtractFullName(int? id)
        {
            if (id == null)
                return null;
            User currentUser = db.Users.Get(id.Value);
            string fullName = null;
            if (currentUser != null)
                fullName = string.Concat(currentUser.FirstName, " ", currentUser.LastName);
            return fullName;
        }
    }
}
