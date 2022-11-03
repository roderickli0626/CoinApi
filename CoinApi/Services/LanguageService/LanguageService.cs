using CoinApi.Context;
using CoinApi.DB_Models;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Services.LanguageService
{
    public class LanguageService : Service<tblLanguage>, ILanguageService
    {
        public LanguageService(CoinApiContext context) : base(context)
        {
        }
        public override tblLanguage Create(tblLanguage entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblLanguage language = context.tblLanguage.Add(entity).Entity;
            context.SaveChanges();
            return language;
        }
        public override bool Delete(int id) 
        {
            context.tblLanguage.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblLanguage> GetAll()
        {
            return context.tblLanguage.ToList();
        }
        public override List<tblLanguage> GetBy(Func<tblLanguage, bool> predicate)
        {
            return context.tblLanguage
                .Where(predicate)
                .ToList();
        }
        public override tblLanguage GetById(int id)
        {
            return context.tblLanguage
                .FirstOrDefault(x => x.languageNumber == id);
        }
        public override bool Update(tblLanguage entity)
        {
            if (entity == null) return false;
            tblLanguage? language = context.tblLanguage.FirstOrDefault(x => x.languageNumber == entity.languageNumber);
            if (language == null) return false;
            language.description = entity.description;

            context.tblLanguage.Update(language);
            context.SaveChanges();
            return true;
        }
    }
}
