namespace CarsStore.Data
{
    using System;
    using System.Data.Entity;

    using CarsStore.Model;

    public class CarsStoreDbContex: DbContext
    {
        public CarsStoreDbContex()
            : base("CarsStoreConnection")
        {
        }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Dealer> Dealers { get; set; }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }
    }
}
