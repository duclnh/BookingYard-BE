﻿using Fieldy.BookingYard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldy.BookingYard.Persistence.Configurations
{
    public class CourtConfiguration : IEntityTypeConfiguration<Court>
    {
        public void Configure(EntityTypeBuilder<Court> builder)
        {
            builder.Property(x => x.Id).HasColumnName("CourtID");
            builder.Property(x => x.CourtPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}