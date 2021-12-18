using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskOfKaspiBank.Models.Data
{
    public class TaskOfKaspiBankContext: IdentityDbContext<IdentityUser>
    {
        public TaskOfKaspiBankContext(DbContextOptions<TaskOfKaspiBankContext> options) : base(options) {}
    }
}