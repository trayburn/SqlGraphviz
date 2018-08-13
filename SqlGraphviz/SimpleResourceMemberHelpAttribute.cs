using Args.Help;

namespace SqlGraphviz
{
	partial class Program
	{
		public class SimpleResourceMemberHelpAttribute : ResourceMemberHelpAttributeBase
		{
			private readonly string description;

			public SimpleResourceMemberHelpAttribute(string description)
			{
				this.description = description;
			}

			public override string GetHelpText()
			{
				return description;
			}
		}
	}
}
