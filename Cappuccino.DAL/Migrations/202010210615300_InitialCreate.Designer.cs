using System.CodeDom.Compiler;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace Cappuccino.DAL.Migrations
{

    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class InitialCreate : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(InitialCreate));

        string IMigrationMetadata.Id
        {
            get { return "202010210615300_InitialCreate"; }
        }

        string IMigrationMetadata.Source
        {
            get { return null; }
        }

        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
