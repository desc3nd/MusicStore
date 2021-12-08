using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.IServices
{
    public interface IUserService
    {
        public void AddUser(User user);
    }
}
