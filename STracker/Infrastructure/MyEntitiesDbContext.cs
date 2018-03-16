using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace STracker.Infrastructure
{
    public partial class MyEntities : DbContext
    {
        public MyEntities()
        : base("name=DataModel")
        {
            ((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
        }

        private void ObjectContext_SavingChanges(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override int SaveChanges()
        {
            foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                throw new Exception();
            }

            return 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }


    }
}