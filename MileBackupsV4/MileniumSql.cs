using System.Data;
using System.Data.Sql;

namespace MileBackupsV4
{
    public class MileniumSql
    {
        public void a()
        {
            var a = SqlDataSourceEnumerator.Instance.GetDataSources();

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dtDatabaseSources = instance.GetDataSources();

            //// Populate the data sources into DropDownList.            
            foreach (DataRow row in dtDatabaseSources.Rows)
            {
                if (!string.IsNullOrWhiteSpace(row["InstanceName"].ToString()))
                {

                }
            }
        }
    }
}
