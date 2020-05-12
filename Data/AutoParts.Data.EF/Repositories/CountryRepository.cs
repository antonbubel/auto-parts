namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class CountryRepository : Repository<long, Country>, ICountryRepository
    {
        public CountryRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}
