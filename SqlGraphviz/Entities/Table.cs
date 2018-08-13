using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlGraphviz.Entities
{
	public class Table
	{
		public string Catalog { get; set; }
		public string Schema { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
	}
	public class TableConfiguration : IEntityTypeConfiguration<Table>
	{
		public void Configure(EntityTypeBuilder<Table> builder)
		{
			builder.ToTable("TABLES", "INFORMATION_SCHEMA");
			builder.Property(e => e.Catalog)
				.HasColumnName("TABLE_CATALOG");
			builder.Property(e => e.Schema)
				.HasColumnName("TABLE_SCHEMA");
			builder.Property(e => e.Name)
				.HasColumnName("TABLE_NAME");
			builder.Property(e => e.Type)
				.HasColumnName("TABLE_TYPE");
			builder.HasKey(row => new { row.Catalog, row.Schema, row.Name });
		}
	}
}
