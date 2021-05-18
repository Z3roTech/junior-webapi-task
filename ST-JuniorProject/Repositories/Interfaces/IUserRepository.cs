using ST_JuniorProject.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Repositories.Interfaces
{
    public interface IUserRepository<TDbModel> where TDbModel : UserModel
    {
        public List<TDbModel> GetAll();

        public TDbModel Get(int id);

        public TDbModel Create(TDbModel model);

        public TDbModel Update(TDbModel model);

        public void Delete(int id);
    }
}