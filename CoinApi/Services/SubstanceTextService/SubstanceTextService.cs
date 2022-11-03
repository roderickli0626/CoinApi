using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.SubstanceTextService
{
    public class SubstanceTextService : Service<tblSubstanceText>, ISubstanceTextService
    {
        public SubstanceTextService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceText Create(tblSubstanceText entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceText subGroupText = context.tblSubstanceText.Add(entity).Entity;
            context.SaveChanges();
            return subGroupText;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceText.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceText> GetAll()
        {
            return context.tblSubstanceText.ToList();
        }
        public override List<tblSubstanceText> GetBy(Func<tblSubstanceText, bool> predicate)
        {
            return context.tblSubstanceText
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceText GetById(int id)
        {
            return context.tblSubstanceText
                .FirstOrDefault(x => x.Id    == id);
        }
        public override bool Update(tblSubstanceText entity)
        {
            if (entity == null) return false;
            tblSubstanceText? substanceText = context.tblSubstanceText.FirstOrDefault(x => x.Id == entity.Id);
            if (substanceText == null) return false;
            substanceText.SubstanceID = entity.SubstanceID;
            substanceText.Description = entity.Description;
            substanceText.Language = entity.Language;

            context.tblSubstanceText.Update(substanceText);
            context.SaveChanges();
            return true;
        }
    }
}
