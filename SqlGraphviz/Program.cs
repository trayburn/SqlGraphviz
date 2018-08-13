using Args;
using Args.Help;
using Args.Help.Formatters;
using Microsoft.EntityFrameworkCore;
using SqlGraphviz.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SqlGraphviz
{
	partial class Program
	{
		[ArgsModel(SwitchDelimiter = "/")]
		[SimpleResourceMemberHelp("SQL Server GraphViz Generator")]
		public class ArgumentsModel
		{
			[Description("Server Name")]
			public string Server { get; set; }
			[Description("Use Integrated Security")]
			public bool IntegratedSecurity { get; set; } = true;
			[Description("Display Help Text")]
			public bool Help { get; set; } = false;
		}

		static void Main(string[] args)
		{
			var model = Args.Configuration.Configure<ArgumentsModel>();
			var command = model.CreateAndBind(args);

			if (command.Help)
			{
				var help = new HelpProvider().GenerateModelHelp(model);
				var cfmt = new ConsoleHelpFormatter();
				Console.WriteLine(cfmt.GetHelp(help));
				return;
			}

			var context = new InformationSchemaDbContext();
			foreach (Table table in context.Set<Table>())
			{
				Console.WriteLine($"{table.Catalog}.{table.Schema}.{table.Name}");
			}

		}

		public class InformationSchemaDbContext : DbContext
		{
			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				optionsBuilder.UseSqlServer("Server=.;Database=Tasks;Integrated Security=true", opts =>
				{
					opts.CommandTimeout(10);
				});
				base.OnConfiguring(optionsBuilder);
			}
			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.ApplyConfiguration(new TableConfiguration());
				base.OnModelCreating(modelBuilder);
			}
		}
	}
}
