using MySql.Data.MySqlClient;
using ModelOrganize;
using Utils;

namespace ModelOrganizeMy
{
    /*
    mysql mappings (similar to sql server): 
    https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
    */
    public class BuildModelMy: BuildModel
    {
        public BuildModelMy(ConfigMy config) : base(config)
        {
        }

        protected override List<Column> GetColumns(string tableName)
        {
            using MySqlConnection connection = new MySqlConnection(Config.connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand();
            command.CommandText = @"
select 
	DISTINCT col.TABLE_NAME, 
	col.COLUMN_NAME, 
	col.COLUMN_DEFAULT, 
	IF(col.IS_NULLABLE = 'YES', 1, 0) AS IS_NULLABLE, 
	col.DATA_TYPE, 
	col.CHARACTER_MAXIMUM_LENGTH, 
	col.NUMERIC_PRECISION,  
	col.NUMERIC_SCALE,
	INFO_FK.REFERENCED_TABLE_NAME,
	INFO_FK.REFERENCED_COLUMN_NAME,
    IF(POSITION('(' IN col.COLUMN_TYPE), 
        CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(col.COLUMN_TYPE, '(', -1), ')', 1) AS UNSIGNED),
        NULL
    ) AS MAX_LENGTH,
	IF(INFO_FK.TABLE_NAME is not NULL, 1, 0) AS IS_FOREIGN_KEY,
	IF(INFO_PK.TABLE_NAME is not NULL, 1, 0) AS IS_PRIMARY_KEY,
    IF(col.COLUMN_TYPE LIKE '%unsigned', 1, 0) as IS_UNSIGNED 

FROM information_schema.tables as tbl

INNER JOIN information_schema.columns as col 
    ON (col.TABLE_NAME = tbl.TABLE_NAME
    AND col.TABLE_SCHEMA  = tbl.TABLE_SCHEMA)
    
LEFT JOIN (

	SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, MAX(KEY_COLUMN_USAGE.REFERENCED_TABLE_NAME) as REFERENCED_TABLE_NAME, MAX(KEY_COLUMN_USAGE.REFERENCED_COLUMN_NAME) as REFERENCED_COLUMN_NAME
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
    WHERE (REFERENCED_TABLE_NAME IS NOT NULL) AND (REFERENCED_COLUMN_NAME IS NOT NULL)
    group by TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME

) AS INFO_FK ON (INFO_FK.TABLE_SCHEMA = col.TABLE_SCHEMA AND INFO_FK.TABLE_NAME = col.TABLE_NAME AND INFO_FK.COLUMN_NAME = col.COLUMN_NAME)

left join (
	select 	TABLE_NAME, COLUMN_NAME
	from INFORMATION_SCHEMA.columns
	where COLUMN_KEY = 'PRI'
) as INFO_PK on (INFO_PK.TABLE_NAME = COL.TABLE_NAME and INFO_PK.COLUMN_NAME = COL.COLUMN_NAME)

WHERE (COL.TABLE_SCHEMA = @dbName) AND (COL.TABLE_NAME = @table_name)
order by COL.TABLE_NAME, COL.ORDINAL_POSITION;
";
            command.Connection = connection;
            command.Parameters.AddWithValue("dbName", Config.dbName);
            command.Parameters.AddWithValue("table_name", tableName);

            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();
            return reader.ColOfObj<Column>();

        }

        protected override Dictionary<string, List<string>> GetInfoUnique(string tableName)
        {
            using MySqlConnection connection = new MySqlConnection(Config.connectionString);
            connection.Open();
            using MySqlCommand command = new MySqlCommand();
            command.CommandText = @"
SELECT DISTINCT col.COLUMN_NAME, con.CONSTRAINT_NAME
FROM information_schema.columns as col 
INNER JOIN information_schema.key_column_usage kcu ON (kcu.TABLE_NAME = col.TABLE_NAME AND col.COLUMN_NAME = kcu.COLUMN_NAME) 
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS con ON (con.TABLE_NAME = col.TABLE_NAME AND kcu.CONSTRAINT_NAME = con.constraint_name)
WHERE 
(COL.TABLE_SCHEMA = @dbName) 
AND (COL.TABLE_NAME =  @table_name) 
AND (con.CONSTRAINT_TYPE = 'UNIQUE')
order by con.CONSTRAINT_NAME, COL.COLUMN_NAME;
";
            command.Connection = connection;
            command.Parameters.AddWithValue(

"dbName", Config.dbName);
            command.Parameters.AddWithValue("table_name", tableName);

            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();
            var list = reader.Serialize();
            var response = new Dictionary<string, List<string>>();

            if (!list.IsNullOrEmpty())
                foreach (var item in list)
                {
                    if (!response.ContainsKey(item["CONSTRAINT_NAME"].ToString()!))
                        response[item["CONSTRAINT_NAME"].ToString()!] = new();

                    response[item["CONSTRAINT_NAME"].ToString()!].Add(item["COLUMN_NAME"].ToString()!);
                }


            return response;
        }

        protected override List<string> GetTableNames()
        {
            using MySqlConnection connection = new MySqlConnection(Config.connectionString);
            connection.Open();
            using MySqlCommand command = new();
            command.CommandText = @"SHOW TABLES FROM " + Config.dbName;
            command.Connection = connection;
            command.ExecuteNonQuery();
            using MySqlDataReader reader = command.ExecuteReader();
            return SqlUtils.ColumnValues<string>(reader, 0);
        }

     
    }
}
