using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.SubstanceGroupService
{
    public class SubstanceGroupService : Service<tblSubstanceGroup>, ISubstanceGroupService
    {
        public SubstanceGroupService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceGroup Create(tblSubstanceGroup entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceGroup subGroup = context.tblSubstanceGroup.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceGroup.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceGroup> GetAll()
        {
            return context.tblSubstanceGroup.ToList();
        }
        public override List<tblSubstanceGroup> GetBy(Func<tblSubstanceGroup, bool> predicate)
        {
            return context.tblSubstanceGroup
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceGroup GetById(int id)
        {
            return context.tblSubstanceGroup
                .FirstOrDefault(x => x.GroupNumber == id);
        }
        public override bool Update(tblSubstanceGroup entity)
        {
            if (entity == null) return false;
            tblSubstanceGroup? substanceGroup = context.tblSubstanceGroup.FirstOrDefault(x => x.GroupNumber == entity.GroupNumber);
            if (substanceGroup == null) return false;
            substanceGroup.UserID = entity.UserID;
            substanceGroup.ViewYesNo = entity.ViewYesNo;

            context.tblSubstanceGroup.Update(substanceGroup);
            context.SaveChanges();
            return true;
        }
    }
}
