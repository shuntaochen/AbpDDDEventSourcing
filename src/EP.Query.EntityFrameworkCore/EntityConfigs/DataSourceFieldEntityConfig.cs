using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EP.Query.DataSource;

namespace EP.Query.EntityConfigs
{
    public class DataSourceFieldEntityConfig : IEntityTypeConfiguration<DataSourceField>
    {
        public void Configure(EntityTypeBuilder<DataSourceField> entity)
        {

            entity.HasIndex(e => e.DataSourceId);

            entity.Property(e => e.Id).HasColumnName("id").HasColumnType("int(11)");

            entity.Property(e => e.DataSourceId).HasColumnName("datasource_id").HasColumnType("int(11)");

            entity.Property(e => e.DisplayText).HasColumnName("display_text").HasColumnType("varchar(50)");

            entity.Property(e => e.Filter).HasColumnName("filter").HasColumnType("varchar(500)");

            entity.Property(e => e.Name).HasColumnName("name").HasColumnType("varchar(50)");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("int(11)");

            entity.Property(e => e.Type).HasColumnName("type").HasColumnType("varchar(100)");

            entity.HasOne(d => d.DataSource)
                .WithMany(p => p.DataSourceFields)
                .HasForeignKey(d => d.DataSourceId);

            entity.ToTable("datasource_fields");
        }
    }
}
