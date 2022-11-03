using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.SubstanceForGroupService
{
    public class SubstanceForGroupService : Service<tblSubstanceForGroup>, ISubstanceForGroupService
    {
        public SubstanceForGroupService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstanceForGroup Create(tblSubstanceForGroup entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstanceForGroup subForGroup = context.tblSubstanceForGroup.Add(entity).Entity;
            context.SaveChanges();
            return subForGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstanceForGroup.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstanceForGroup> GetAll()
        {
            return context.tblSubstanceForGroup.ToList();
        }
        public override List<tblSubstanceForGroup> GetBy(Func<tblSubstanceForGroup, bool> predicate)
        {
            return context.tblSubstanceForGroup
                .Where(predicate)
                .ToList();
        }
        public override tblSubstanceForGroup GetById(int id)
        {
            return context.tblSubstanceForGroup
                .FirstOrDefault(x => x.Id == id);
        }
        public override bool Update(tblSubstanceForGroup entity)
        {
            if (entity == null) return false;
            tblSubstanceForGroup? substanceForGroup = context.tblSubstanceForGroup.FirstOrDefault(x => x.Id == entity.Id);
            if (substanceForGroup == null) return false;
            substanceForGroup.SubstanceID = entity.SubstanceID;
            substanceForGroup.GroupNumber = entity.GroupNumber;

            context.tblSubstanceForGroup.Update(substanceForGroup);
            context.SaveChanges();
            return true;
        }
    }
}
