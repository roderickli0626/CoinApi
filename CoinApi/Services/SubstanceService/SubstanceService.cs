using CoinApi.Context;
using CoinApi.DB_Models;

namespace CoinApi.Services.SubstanceService
{
    public class SubstanceService : Service<tblSubstance>, ISubstanceService
    {
        public SubstanceService(CoinApiContext context) : base(context)
        {
        }
        public override tblSubstance Create(tblSubstance entity)
        {
            //tblLanguage? language;
            //using (context)
            //{
            //    language = context.tblLanguage.FromSqlRaw("Insert into tblLanguage values (" + entity.languageNumber + ",'" + entity.description + "')").FirstOrDefault();
            //    //context.Database.ExecuteSqlRaw("Insert into tblLanguage values (2, 'second')");
            //}

            tblSubstance subGroup = context.tblSubstance.Add(entity).Entity;
            context.SaveChanges();
            return subGroup;
        }
        public override bool Delete(int id)
        {
            context.tblSubstance.Remove(GetById(id));
            context.SaveChanges();
            return true;
        }
        public override List<tblSubstance> GetAll()
        {
            return context.tblSubstance.ToList();
        }
        public override List<tblSubstance> GetBy(Func<tblSubstance, bool> predicate)
        {
            return context.tblSubstance
                .Where(predicate)
                .ToList();
        }
        public override tblSubstance GetById(int id)
        {
            return context.tblSubstance
                .FirstOrDefault(x => x.SubstanceID == id);
        }
        public override bool Update(tblSubstance entity)
        {
            if (entity == null) return false;
            tblSubstance? substance = context.tblSubstance.FirstOrDefault(x => x.SubstanceID == entity.SubstanceID);
            if (substance == null) return false;
            substance.Hidde = entity.Hidde;
            substance.WavFile = entity.WavFile;

            context.tblSubstance.Update(substance);
            context.SaveChanges();
            return true;
        }
    }
}
