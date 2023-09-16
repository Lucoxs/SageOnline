using API.Documents.Models;
using API.Documents.Models.EFCore;
using API.Documents.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Documents.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.EnsureCreated();
            Database.Migrate();
            modelBuilder.Entity<Document>()
                .HasIndex(i => new { i.CompanyId, i.DocumentNumber, i.DocumentType })
                .IsUnique();
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
                entry.HashUserIdDeleted = EncryptionService.EncryptString(entry.UserIdDeleted);
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
                entry.HashUserIdDeleted = EncryptionService.EncryptString(entry.UserIdDeleted);
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
                entry.HashUserIdDeleted = EncryptionService.EncryptString(entry.UserIdDeleted);
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
                entry.HashUserIdDeleted = EncryptionService.EncryptString(entry.UserIdDeleted);
                deletedEntry.State = EntityState.Modified;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
