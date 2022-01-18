using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using potential_sniffle.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace potential_sniffle.Infrastructure.Persistence
{
    public class ApplicationDbContext /*: ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext*/
    {
        //private readonly ICurrentUserService _currentUserService;
        //private readonly IDateTime _dateTime;
        //private readonly IDomainEventService _domainEventService;

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime) : base(options, operationalStoreOptions)
        //{
        //    _currentUserService = currentUserService;
        //    _domainEventService = domainEventService;
        //    _dateTime = dateTime;
        //}

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = _currentUserService.UserId;
        //                entry.Entity.Created = _dateTime.Now;

        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                entry.Entity.LastModified = _dateTime.Now;

        //                break;
        //        }
        //    }

        //    var events = ChangeTracker.Entries<IHasDomainEvent>().Select(x => x.Entity.DomainEvents).SelectMany(x => x).Where(domainEvent => !domainEvent.IsPublished).ToArray();

        //    var result = await base.SaveChangesAsync(cancellationToken);

        //    await DispatchEvents(events);

        //    return result;
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    base.OnModelCreating(builder);
        //}

        //private async Task DispatchEvents(DomainEvent[] events)
        //{
        //    foreach (var @event in events)
        //    {
        //        @event.IsPublished = true;

        //        await _domainEventService.Publish(@event);
        //    }
        //}
    }
}
