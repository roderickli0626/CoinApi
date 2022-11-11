using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.UserService
{
    public class UserService : Service<tblUser>, IUserService
    {
        public UserService(CoinApiContext context) : base(context)
        {
        }
        public override tblUser Create(tblUser entity)
        {
            tblUser language = context.tblUser.Add(entity).Entity;
            context.SaveChanges();
            return language;
        }
        public override bool Delete(int id)
        {
            context.tblUser.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblUser> GetAll()
        {
            return context.tblUser.ToList();
        }
        public override List<tblUser> GetBy(Func<tblUser, bool> predicate)
        {
            return context.tblUser
                .Where(predicate)
                .ToList();
        }
        public override tblUser GetById(int id)
        {
            return context.tblUser
                .FirstOrDefault(x => x.UserID == id);
        }
        public tblUser GetByEmail(string email)
        {
            return context.tblUser.FirstOrDefault(u => u.Email == email);
        }
        public override bool Update(tblUser entity)
        {
            if (entity == null) return false;
            tblUser? user = context.tblUser.FirstOrDefault(x => x.UserID == entity.UserID);
            if (user == null) return false;
            user.Adress = entity.Adress;
            user.DeviceNumber = entity.DeviceNumber;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.ActiveAcount = entity.ActiveAcount;
            user.City = entity.City;
            user.Email = entity.Email;
            user.Country = entity.Country;
            user.LanguageNumber = entity.LanguageNumber;
            user.Password = entity.Password;

            context.tblUser.Update(user);
            context.SaveChanges();
            return true;

        }
    }
}
