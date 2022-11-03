using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.SubstanceGroupTextService
{
    public class SubstanceGroupTextService : Service<tblSubstanceGroupText>, ISubstanceGroupTextService
    {
        public SubstanceGroupTextService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceGroupText Create(tblSubstanceGroupText entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceGroupText subGroup = context.tblSubstanceGroupText.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceGroupText.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceGroupText> GetAll()
        {
            return context.tblSubstanceGroupText.ToList();
        }
        public override List<tblSubstanceGroupText> GetBy(Func<tblSubstanceGroupText, bool> predicate)
        {
            return context.tblSubstanceGroupText
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceGroupText GetById(int id)
        {
            return context.tblSubstanceGroupText
                .FirstOrDefault(x => x.Id == id);
        }
        public override bool Update(tblSubstanceGroupText entity)
        {
            if (entity == null) return false;
            tblSubstanceGroupText? substanceGroupText = context.tblSubstanceGroupText.FirstOrDefault(x => x.Id == entity.Id);
            if (substanceGroupText == null) return false;
            substanceGroupText.GroupNumber = entity.GroupNumber;
            substanceGroupText.Description = entity.Description;
            substanceGroupText.Language = entity.Language;

            context.tblSubstanceGroupText.Update(substanceGroupText);
            context.SaveChanges();
            return true;
        }
    }
}
