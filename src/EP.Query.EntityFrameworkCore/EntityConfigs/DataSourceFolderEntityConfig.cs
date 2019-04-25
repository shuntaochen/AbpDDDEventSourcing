using EP.Query.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Query.EntityConfigs
{
    public class DataSourceFolderEntityConfig : IEntityTypeConfiguration<DataSourceFolder>
    {
        public void Configure(EntityTypeBuilder<DataSourceFolder> entity)
        {

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatorUserId).HasColumnName("creator_user_id").HasColumnType("bigint(20)");

            entity.Property(e => e.DataSourceCount).HasColumnName("datasource").HasColumnType("int(11)");

            entity.Property(e => e.LastModifierUserId).HasColumnName("last_modifier_user_id").HasColumnType("bigint(20)");

            entity.Property(e => e.Level).HasColumnName("level").HasColumnType("int(11)");

            entity.Property(e => e.Name).HasColumnName("name").HasColumnType("varchar(40)");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("int(11)");

            entity.Property(e => e.ParentId).HasColumnName("parent_id").HasColumnType("int(11)");

            entity.Property(e => e.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");


            entity.ToTable("datasource_folders");
        }
    }
}
