using System.Data.Entity;
using OlegTask.EF.Migrations;

namespace OlegTask.EF
{
    public class OlegTaskMigrator : MigrateDatabaseToLatestVersion<OlegTaskContext, OlegTaskConfiguration>
    {
    }
}