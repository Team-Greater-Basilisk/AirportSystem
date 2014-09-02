#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the FluentMappingGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.OpenAccess.Metadata.Relational;

namespace JsonReportModel
{

	public partial class FluentModelMetadataSource : FluentMetadataSource
	{
		
		protected override IList<MappingConfiguration> PrepareMapping()
		{
			List<MappingConfiguration> mappingConfigurations = new List<MappingConfiguration>();

            var reportMapping = new MappingConfiguration<Report>();

            reportMapping.MapType(report => new
                {
                    RepodrtID = report.ReportID,
                    From = report.From,
                    To = report.To,
                    SelltiketsCount = report.SellTiketsCount,
                    Year = report.Year
                }).ToTable("Reports");

            reportMapping.HasProperty(r => r.ReportID).IsIdentity();

            mappingConfigurations.Add(reportMapping);

			return mappingConfigurations;
		}
		
		protected override void SetContainerSettings(MetadataContainer container)
		{
			container.Name = "FluentModel";
			container.DefaultNamespace = "JsonReportModel";
			container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
			container.NameGenerator.RemoveCamelCase = false;
		}
	}
}
#pragma warning restore 1591