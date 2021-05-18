using ST_JuniorProject.Models.Base;
using ST_JuniorProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST_JuniorProject.Repositories.Implementations
{
    public class UserRepository<TDbModel> : IUserRepository<TDbModel> where TDbModel : UserModel
    {
        private AppDBContext context { set; get; }

        public UserRepository(AppDBContext context)
        {
            this.context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            context.Set<TDbModel>().Add(model);
            context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var toDel = context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            context.Set<TDbModel>().Remove(toDel);
            context.SaveChanges();
        }

        public TDbModel Get(int id)
        {
            return context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }

        public List<TDbModel> GetAll()
        {
            return context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpd = context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpd != null)
                toUpd = model;
            context.Update(toUpd);
            context.SaveChanges();
            return toUpd;
        }
    }
}