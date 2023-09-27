using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.PostgreSQL;

namespace RevealSdk.Server.Reveal
{
    public class DataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDataSourceItemAsync(IRVUserContext userContext, string dashboardId, RVDataSourceItem dataSourceItem)
        {
            if (dataSourceItem is RVPostgresDataSourceItem sqlServerDsi)
            {
                //update underlying data source
                ChangeDataSourceAsync(userContext, sqlServerDsi.DataSource);
                if (userContext.UserId != "designer" && userContext.UserId != "guest")
                {
                    sqlServerDsi.Database = "mydb";
                    sqlServerDsi.CustomQuery = $"SELECT * FROM \"{sqlServerDsi.Table}\" WHERE \"Territory\" = '{userContext.UserId}'";
                    Console.WriteLine(sqlServerDsi.CustomQuery);
                }
            }

            return Task.FromResult(dataSourceItem);
        }

        public Task<RVDashboardDataSource> ChangeDataSourceAsync(IRVUserContext userContext, RVDashboardDataSource dataSource)
        {
            if (dataSource is RVPostgresDataSource sqlDatasource)
            {
                sqlDatasource.Host = "localhost";
                sqlDatasource.Schema = "public";
                sqlDatasource.Port = 5433;
                if (userContext.UserId == "designer")
                {
                    sqlDatasource.Database = "mockdb";
                } else
                {
                    sqlDatasource.Database = "mydb";
                }
            }

            return Task.FromResult(dataSource);
        }
    }
}
