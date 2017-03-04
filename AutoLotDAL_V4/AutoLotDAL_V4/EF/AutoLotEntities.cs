namespace AutoLotDAL_V4.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AutoLotDAL_V4.Models;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using Interception;
    using System.Data.Entity.Core.Objects;

    public class AutoLotEntities : DbContext
    {
        // Контекст настроен для использования строки подключения "AutoLotEntities" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "AutoLotDAL_V4.EF.AutoLotEntities" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "AutoLotEntities" 
        // в файле конфигурации приложения.
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //  DbInterception.Add(new ConsoleWriterInterceptor());
            // DatabaseLogger.StartLogging();
            // DbInterception.Add(DatabaseLogger);
            //      var context = (this as IObjectContextAdapter).ObjectContext;
            //      context.ObjectMaterialized += OnObjectMaterialized;
            //      context.SavingChanges += OnSavingChanges;
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e =>e.Inventory)
                .WillCascadeOnDelete(false);
        }
        static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt", true);
        protected override void Dispose(bool disposing)
        {
            //DbInterception.Remove(DatabaseLogger);
            //DatabaseLogger.StopLogging();
            base.Dispose(disposing);
            
        }
        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            //Sender is of type ObjectContext.  Can get current and original values, and 
            //cancel/modify the save operation as desired.
            //var context = sender as ObjectContext;
            //if (context == null) return;
            //foreach (ObjectStateEntry item in 
            //    context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
            //{
            //    //Do something important here
            //    if ((item.Entity as Inventory)!=null)
            //    {
            //        var entity = (Inventory) item.Entity;
            //        if (entity.Color == "Red")
            //        {
            //            item.RejectPropertyChanges(nameof(entity.Color));
            //        }
            //    }
            //}

        }
        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
        }


    }
   }