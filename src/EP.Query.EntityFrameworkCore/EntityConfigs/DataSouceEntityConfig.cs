using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using EP.Query.DataSource;

namespace EP.Query.EntityConfigs
{
    public class DataSouceEntityConfig : IEntityTypeConfiguration<DataSource.DataSource>
    {

        public void Configure(EntityTypeBuilder<DataSource.DataSource> entity)
        {
            entity.HasIndex(e => e.DataSourceFolderId);

            entity.Property(e => e.Id).HasColumnName("id").HasColumnType("int(11)");

            entity.Property(e => e.CreatorUserId).HasColumnName("creator_user_id").HasColumnType("bigint(20)");

            entity.Property(e => e.DataSourceFolderId).HasColumnName("datasource_folder_id").HasColumnType("int(11)");

            entity.Property(e => e.LastModifierUserId).HasColumnName("last_modifier_user_id").HasColumnType("bigint(20)");

            entity.Property(e => e.Name).HasColumnName("name").HasColumnType("varchar(40)");

            entity.Property(e => e.Remark).HasColumnName("remark").HasColumnType("varchar(500)");

            entity.Property(e => e.SourceContent).HasColumnName("source_content").HasColumnType("varchar(500)");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("int(11)");

            entity.Property(e => e.Type).HasColumnName("type").HasColumnType("tinyint");

            entity.HasOne(d => d.DataSourceFolder)
                .WithMany(p => p.DataSources)
                .HasForeignKey(d => d.DataSourceFolderId);

            entity.ToTable("datasouces");
        }
    }
}
