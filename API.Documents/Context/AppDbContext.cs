using API.Documents.Models;
using API.Documents.Models.EFCore;
using API.Documents.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Documents.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentLine> DocumentLines{ get; set; }
        public DbSet<DocumentLineVariant> DocumentLineVariants { get; set; }
        public DbSet<DocumentLineBundle> DocumentLineBundles { get; set; }
        public DbSet<DocumentLineBundleElement> DocumentLineBundleElements { get; set; }

        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Base
            base.OnModelCreating(modelBuilder);
            /*Database.EnsureCreated();
            Database.Migrate();*/

            //Index
            modelBuilder.Entity<Document>()
                .HasIndex(i => new { i.CompanyId, i.DocumentNumber, i.DocumentType })
                .IsUnique();

            //Relations
            modelBuilder.Entity<Document>()
                .HasMany(x => x.Lines)
                .WithOne(y => y.Document)
                .HasForeignKey("do_id")
                .IsRequired();

            modelBuilder.Entity<DocumentLine>()
                .HasOne(x => x.DocumentLineVariant)
                .WithOne(y => y.DocumentLine)
                .HasForeignKey<DocumentLineVariant>();

            modelBuilder.Entity<DocumentLine>()
                .HasOne(x => x.DocumentLineBundle)
                .WithOne(y => y.DocumentLine)
                .HasForeignKey<DocumentLineBundle>();

            modelBuilder.Entity<DocumentLineBundle>()
                .HasMany(x => x.BundleElements)
                .WithOne(y => y.DocumentLineBundle)
                .HasForeignKey("lb_id");

            //BaseModel columns name
            modelBuilder.Entity<Document>().Property(x => x.Id).HasColumnName("do_id");
            modelBuilder.Entity<Document>().Property(x => x.HashId).HasColumnName("do_hash_id");
            modelBuilder.Entity<Document>().Property(x => x.CreatedAt).HasColumnName("do_created_at");
            modelBuilder.Entity<Document>().Property(x => x.HashCreatedAt).HasColumnName("do_hash_created_at");
            modelBuilder.Entity<Document>().Property(x => x.ModifiedAt).HasColumnName("do_modified_at");
            modelBuilder.Entity<Document>().Property(x => x.HashModifiedAt).HasColumnName("do_hash_modified_at");
            modelBuilder.Entity<Document>().Property(x => x.DeletedAt).HasColumnName("do_deleted_at");
            modelBuilder.Entity<Document>().Property(x => x.HashDeletedAt).HasColumnName("do_hash_deleted_at");
            modelBuilder.Entity<Document>().Property(x => x.IsDeleted).HasColumnName("do_is_deleted");

            modelBuilder.Entity<DocumentLine>().Property(x => x.Id).HasColumnName("dl_id");
            modelBuilder.Entity<DocumentLine>().Property(x => x.HashId).HasColumnName("dl_hash_id");
            modelBuilder.Entity<DocumentLine>().Property(x => x.CreatedAt).HasColumnName("dl_created_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.HashCreatedAt).HasColumnName("dl_hash_created_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.ModifiedAt).HasColumnName("dl_modified_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.HashModifiedAt).HasColumnName("dl_hash_modified_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.DeletedAt).HasColumnName("dl_deleted_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.HashDeletedAt).HasColumnName("dl_hash_deleted_at");
            modelBuilder.Entity<DocumentLine>().Property(x => x.IsDeleted).HasColumnName("dl_is_deleted");

            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.Id).HasColumnName("lv_id");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.HashId).HasColumnName("lv_hash_id");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.CreatedAt).HasColumnName("lv_created_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.HashCreatedAt).HasColumnName("lv_hash_created_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.ModifiedAt).HasColumnName("lv_modified_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.HashModifiedAt).HasColumnName("lv_hash_modified_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.DeletedAt).HasColumnName("lv_deleted_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.HashDeletedAt).HasColumnName("lv_hash_deleted_at");
            modelBuilder.Entity<DocumentLineVariant>().Property(x => x.IsDeleted).HasColumnName("lv_is_deleted");

            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.Id).HasColumnName("lb_id");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.HashId).HasColumnName("lb_hash_id");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.CreatedAt).HasColumnName("lb_created_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.HashCreatedAt).HasColumnName("lb_hash_created_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.ModifiedAt).HasColumnName("lb_modified_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.HashModifiedAt).HasColumnName("lb_hash_modified_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.DeletedAt).HasColumnName("lb_deleted_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.HashDeletedAt).HasColumnName("lb_hash_deleted_at");
            modelBuilder.Entity<DocumentLineBundle>().Property(x => x.IsDeleted).HasColumnName("lb_is_deleted");

            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.Id).HasColumnName("le_id");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.HashId).HasColumnName("le_hash_id");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.CreatedAt).HasColumnName("le_created_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.HashCreatedAt).HasColumnName("le_hash_created_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.ModifiedAt).HasColumnName("le_modified_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.HashModifiedAt).HasColumnName("le_hash_modified_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.DeletedAt).HasColumnName("le_deleted_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.HashDeletedAt).HasColumnName("le_hash_deleted_at");
            modelBuilder.Entity<DocumentLineBundleElement>().Property(x => x.IsDeleted).HasColumnName("le_is_deleted");

        }

        public override int SaveChanges()
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Entity is not BaseModel entry)
                    continue;

                entry.HashId = EncryptionService.EncryptString(entry.Id.ToString());
                var createdDate = DateTime.Now;
                entry.CreatedAt = createdDate;
                entry.HashCreatedAt = EncryptionService.EncryptString(createdDate.ToString());
                entry.HashCompanyId = EncryptionService.EncryptString(entry.CompanyId.ToString());
                entry.HashUserIdCreater = EncryptionService.EncryptString(entry.UserIdCreater);

                if (addedEntry.Entity is not Document entryDocument)
                    continue;

                entryDocument.HashDocumentNumber = EncryptionService.EncryptString(entryDocument.DocumentNumber);
            }

            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            foreach(var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Entity is not BaseModel entry)
                    continue;

                var modifiedDate = DateTime.Now;
                entry.ModifiedAt = modifiedDate;
                entry.HashModifiedAt = EncryptionService.EncryptString(modifiedDate.ToString());
                entry.HashUsedIdModifier = EncryptionService.EncryptString(entry.UsedIdModifier);
            }

            var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();
            foreach(var deletedEntry in deletedEntries)
            {
                if (deletedEntry.Entity is not BaseModel entry)
                    continue;

                var deletedDate = DateTime.Now;
                entry.DeletedAt = deletedDate;
                entry.IsDeleted = true;
                entry.HashDeletedAt = EncryptionService.EncryptString(deletedDate.ToString());
                entry.HashUserIdDeleter = EncryptionService.EncryptString(entry.UserIdDeleter);
                deletedEntry.State = EntityState.Modified;
            }

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Entity is not BaseModel entry)
                    continue;

                entry.HashId = EncryptionService.EncryptString(entry.Id.ToString());
                var createdDate = DateTime.Now;
                entry.CreatedAt = createdDate;
                entry.HashCreatedAt = EncryptionService.EncryptString(createdDate.ToString());
                entry.HashCompanyId = EncryptionService.EncryptString(entry.CompanyId.ToString());
                entry.HashUserIdCreater = EncryptionService.EncryptString(entry.UserIdCreater);

                if (addedEntry.Entity is not Document entryDocument)
                    continue;

                entryDocument.HashDocumentNumber = EncryptionService.EncryptString(entryDocument.DocumentNumber);
            }

            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Entity is not BaseModel entry)
                    continue;

                var modifiedDate = DateTime.Now;
                entry.ModifiedAt = modifiedDate;
                entry.HashModifiedAt = EncryptionService.EncryptString(modifiedDate.ToString());
                entry.HashUsedIdModifier = EncryptionService.EncryptString(entry.UsedIdModifier);
            }

            var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();
            foreach (var deletedEntry in deletedEntries)
            {
                if (deletedEntry.Entity is not BaseModel entry)
                    continue;

                var deletedDate = DateTime.Now;
                entry.DeletedAt = deletedDate;
                entry.IsDeleted = true;
                entry.HashDeletedAt = EncryptionService.EncryptString(deletedDate.ToString());
                entry.HashUserIdDeleter = EncryptionService.EncryptString(entry.UserIdDeleter);
                deletedEntry.State = EntityState.Modified;
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Entity is not BaseModel entry)
                    continue;

                entry.HashId = EncryptionService.EncryptString(entry.Id.ToString());
                var createdDate = DateTime.Now;
                entry.CreatedAt = createdDate;
                entry.HashCreatedAt = EncryptionService.EncryptString(createdDate.ToString());
                entry.HashCompanyId = EncryptionService.EncryptString(entry.CompanyId.ToString());
                entry.HashUserIdCreater = EncryptionService.EncryptString(entry.UserIdCreater);

                if (addedEntry.Entity is not Document entryDocument)
                    continue;

                entryDocument.HashDocumentNumber = EncryptionService.EncryptString(entryDocument.DocumentNumber);
            }

            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Entity is not BaseModel entry)
                    continue;

                var modifiedDate = DateTime.Now;
                entry.ModifiedAt = modifiedDate;
                entry.HashModifiedAt = EncryptionService.EncryptString(modifiedDate.ToString());
                entry.HashUsedIdModifier = EncryptionService.EncryptString(entry.UsedIdModifier);
            }

            var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();
            foreach (var deletedEntry in deletedEntries)
            {
                if (deletedEntry.Entity is not BaseModel entry)
                    continue;

                var deletedDate = DateTime.Now;
                entry.DeletedAt = deletedDate;
                entry.IsDeleted = true;
                entry.HashDeletedAt = EncryptionService.EncryptString(deletedDate.ToString());
                entry.HashUserIdDeleter = EncryptionService.EncryptString(entry.UserIdDeleter);
                deletedEntry.State = EntityState.Modified;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Entity is not BaseModel entry)
                    continue;

                entry.HashId = EncryptionService.EncryptString(entry.Id.ToString());
                var createdDate = DateTime.Now;
                entry.CreatedAt = createdDate;
                entry.HashCreatedAt = EncryptionService.EncryptString(createdDate.ToString());
                entry.HashCompanyId = EncryptionService.EncryptString(entry.CompanyId.ToString());
                entry.HashUserIdCreater = EncryptionService.EncryptString(entry.UserIdCreater);

                if (addedEntry.Entity is not Document entryDocument)
                    continue;

                entryDocument.HashDocumentNumber = EncryptionService.EncryptString(entryDocument.DocumentNumber);
            }

            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Entity is not BaseModel entry)
                    continue;

                var modifiedDate = DateTime.Now;
                entry.ModifiedAt = modifiedDate;
                entry.HashModifiedAt = EncryptionService.EncryptString(modifiedDate.ToString());
                entry.HashUsedIdModifier = EncryptionService.EncryptString(entry.UsedIdModifier);
            }

            var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();
            foreach (var deletedEntry in deletedEntries)
            {
                if (deletedEntry.Entity is not BaseModel entry)
                    continue;

                var deletedDate = DateTime.Now;
                entry.DeletedAt = deletedDate;
                entry.IsDeleted = true;
                entry.HashDeletedAt = EncryptionService.EncryptString(deletedDate.ToString());
                entry.HashUserIdDeleter = EncryptionService.EncryptString(entry.UserIdDeleter);
                deletedEntry.State = EntityState.Modified;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
