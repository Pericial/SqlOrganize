﻿using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;
using Utils;
using SchemaJson;

namespace SchemaJsonSs
{
    public class BuildSchemaSs : BuildSchema
    {
        public BuildSchemaSs(Config config) : base(config)
        {
        }

        protected override List<String> GetTableNames()
        {
            using SqlConnection connection = new SqlConnection(Config.connection_string);
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.CommandText = @"
                SELECT TABLE_NAME
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG=@db_name";
            command.Connection = connection;
            command.Parameters.AddWithValue("db_name", Config.db_name);
            command.ExecuteNonQuery();
            using SqlDataReader reader = command.ExecuteReader();
            return DbDataReaderUtils.ColumnValues<string>(reader, "TABLE_NAME");
        }

        protected override List<Field> GetFieldsInfo(string tableName)
        {
            using SqlConnection connection = new SqlConnection(Config.connection_string);
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.CommandText = @"
    select 
	col.TABLE_NAME, 
	col.COLUMN_NAME, 
	col.COLUMN_DEFAULT, 
	IIF(col.IS_NULLABLE = 'YES', 1, 0) AS IS_NULLABLE, 
	col.DATA_TYPE, 
	col.CHARACTER_MAXIMUM_LENGTH, 
	col.NUMERIC_PRECISION,  
	col.NUMERIC_SCALE,
	INFO_FK.REFERENCED_TABLE_NAME, INFO_FK.REFERENCED_COLUMN_NAME, 
	IIF(INFO_PK.COLUMN_NAME IS NOT NULL, 1, 0) AS IS_PRIMARY_KEY, 
	IIF(INFO_U.COLUMN_NAME IS NOT NULL, 1, 0) AS IS_UNIQUE_KEY,
	IIF(INFO_FK2.COLUMN_NAME IS NOT NULL, 1, 0) AS IS_FOREIGN_KEY
FROM information_schema.tables tbl

INNER JOIN information_schema.columns col 
    ON col.table_name = tbl.table_name
    AND col.table_schema = tbl.table_schema

LEFT JOIN (

	SELECT 
		DISTINCT KCU1.TABLE_NAME AS TABLE_NAME 
		,KCU1.COLUMN_NAME AS COLUMN_NAME 
		,MIN(KCU2.TABLE_NAME) AS REFERENCED_TABLE_NAME 
		,MIN(KCU2.COLUMN_NAME) AS REFERENCED_COLUMN_NAME 

	FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 

	INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 
		ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
		AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
		AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 

	INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 
		ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG  
		AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA 
		AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME 
		AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION 

	GROUP BY KCU1.TABLE_NAME , KCU1.COLUMN_NAME

) AS INFO_FK ON (INFO_FK.TABLE_NAME = col.TABLE_NAME AND col.COLUMN_NAME = INFO_FK.COLUMN_NAME)


LEFT JOIN (
	SELECT  DISTINCT CPK.TABLE_NAME, CPK.COLUMN_NAME        
	FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TPK
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CPK
	ON (
		CPK.CONSTRAINT_NAME = TPK.CONSTRAINT_NAME
	)
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY'
) AS INFO_PK ON (INFO_PK.TABLE_NAME = Col.TABLE_NAME AND Col.COLUMN_NAME = INFO_PK.COLUMN_NAME)

LEFT JOIN (
	SELECT  DISTINCT CU.TABLE_NAME, CU.COLUMN_NAME        
	FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS  AS TU
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CU
	ON (
		CU.CONSTRAINT_NAME = TU.CONSTRAINT_NAME
	)
	WHERE CONSTRAINT_TYPE = 'UNIQUE'
) AS INFO_U ON (INFO_U.TABLE_NAME = Col.TABLE_NAME AND Col.COLUMN_NAME = INFO_U.COLUMN_NAME)

LEFT JOIN (
	SELECT DISTINCT CF.TABLE_NAME, CF.COLUMN_NAME        
	FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS  AS TF
	INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CF
	ON (
		CF.CONSTRAINT_NAME = TF.CONSTRAINT_NAME
	)
	WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
) AS INFO_FK2 ON (INFO_FK2.TABLE_NAME = Col.TABLE_NAME AND Col.COLUMN_NAME = INFO_FK2.COLUMN_NAME)
WHERE tbl.table_type = 'base table' AND tbl.TABLE_CATALOG=@db_name AND tbl.TABLE_NAME = @table_name;
";
            command.Connection = connection;
            command.Parameters.AddWithValue("db_name", Config.db_name);
            command.Parameters.AddWithValue("table_name", tableName);

            command.ExecuteNonQuery();
            using SqlDataReader reader = command.ExecuteReader();
            return reader.ConvertToListOfObject<Field>();

        }

    }
}