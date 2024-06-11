using Candidate.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Infrastructure.Data
{
    public class ProspectConfiguration : IEntityTypeConfiguration<Prospect>
    {
        public void Configure(EntityTypeBuilder<Prospect> builder)
        {
            builder.HasKey(t=>t.Email);
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(t => t.LinkedInProfileUrl).IsRequired();
            builder.Property(t => t.GitHubProfileUrl).IsRequired();
            builder.Property(t => t.Comment).IsRequired();
            builder.Property(t => t.FromDtm).IsRequired();
            builder.Property(t => t.ToDtm).IsRequired();
        }
    }
}
