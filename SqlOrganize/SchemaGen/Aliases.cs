using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SchemaGen
{


    public static class Aliases
    {
        /*
        Nombre del cual se definira el alias
        */
        /*
        Alias existentes para evitar duplicados
        */
        public static List<string> Existent { get; set; }

        /*
        Palabras reservadas para que no se definan como alias
        Generalmente son palabras reservadas del motor de base de datos
        */
        public static List<string> Reserved { get; set; } = new List<string>() {
        "ADD",
        "EXTERNAL",
        "PROCEDURE",
        "ALL",
        "FETCH",
        "PUBLIC",
        "ALTER",
        "FILE",
        "RAISERROR",
        "AND",
        "FILLFACTOR",
        "READ",
        "ANY",
        "FOR",
        "READTEXT",
        "AS",
        "FOREIGN",
        "RECONFIGURE",
        "ASC",
        "FREETEXT",
        "REFERENCES",
        "AUTHORIZATION",
        "FREETEXTTABLE",
        "REPLICATION",
        "BACKUP",
        "FROM",
        "RESTORE",
        "BEGIN",
        "FULL",
        "RESTRICT",
        "BETWEEN",
        "FUNCTION",
        "RETURN",
        "BREAK",
        "GOTO",
        "REVERT",
        "BROWSE",
        "GRANT",
        "REVOKE",
        "BULK",
        "GROUP",
        "RIGHT",
        "BY",
        "HAVING",
        "ROLLBACK",
        "CASCADE",
        "HOLDLOCK",
        "ROWCOUNT",
        "CASE",
        "IDENTITY",
        "ROWGUIDCOL",
        "CHECK",
        "IDENTITY_INSERT",
        "RULE",
        "CHECKPOINT",
        "IDENTITYCOL",
        "SAVE",
        "CLOSE",
        "IF",
        "SCHEMA",
        "CLUSTERED",
        "IN",
        "SECURITYAUDIT",
        "COALESCE",
        "INDEX",
        "SELECT",
        "COLLATE",
        "INNER",
        "SEMANTICKEYPHRASETABLE",
        "COLUMN",
        "INSERT",
        "SEMANTICSIMILARITYDETAILSTABLE",
        "COMMIT",
        "INTERSECT",
        "SEMANTICSIMILARITYTABLE",
        "COMPUTE",
        "INTO",
        "SESSION_USER",
        "CONSTRAINT",
        "IS",
        "SET",
        "CONTAINS",
        "JOIN",
        "SETUSER",
        "CONTAINSTABLE",
        "KEY",
        "SHUTDOWN",
        "CONTINUE",
        "KILL",
        "SOME",
        "CONVERT",
        "LEFT",
        "STATISTICS",
        "CREATE",
        "LIKE",
        "SYSTEM_USER",
        "CROSS",
        "LINENO",
        "TABLE",
        "CURRENT",
        "LOAD",
        "TABLESAMPLE",
        "CURRENT_DATE",
        "MERGE",
        "TEXTSIZE",
        "CURRENT_TIME",
        "NATIONAL",
        "THEN",
        "CURRENT_TIMESTAMP",
        "NOCHECK",
        "TO",
        "CURRENT_USER",
        "NONCLUSTERED",
        "TOP",
        "CURSOR",
        "NOT",
        "TRAN",
        "DATABASE",
        "NULL",
        "TRANSACTION",
        "DBCC",
        "NULLIF",
        "TRIGGER",
        "DEALLOCATE",
        "OF",
        "TRUNCATE",
        "DECLARE",
        "OFF",
        "TRY_CONVERT",
        "DEFAULT",
        "OFFSETS",
        "TSEQUAL",
        "DELETE",
        "ON",
        "UNION",
        "DENY",
        "OPEN",
        "UNIQUE",
        "DESC",
        "OPENDATASOURCE",
        "UNPIVOT",
        "DISK",
        "OPENQUERY",
        "UPDATE",
        "DISTINCT",
        "OPENROWSET",
        "UPDATETEXT",
        "DISTRIBUTED",
        "OPENXML",
        "USE",
        "DOUBLE",
        "OPTION",
        "USER",
        "DROP",
        "OR",
        "VALUES",
        "DUMP",
        "ORDER",
        "VARYING",
        "ELSE",
        "OUTER",
        "VIEW",
        "END",
        "OVER",
        "WAITFOR",
        "ERRLVL",
        "PERCENT",
        "WHEN",
        "ESCAPE",
        "PIVOT",
        "WHERE",
        "EXCEPT",
        "PLAN",
        "WHILE",
        "EXEC",
        "PRECISION",
        "WITH",
        "EXECUTE",
        "PRIMARY",
        "WITHIN GROUP",
        "EXISTS",
        "PRINT",
        "WRITETEXT",
        "EXIT",
        "PROC"
        };


        public static string WordsSeparator { get; set; } = "_";

        public static string GetAlias(string name, int length = 3)
        {
            string[] words = name.Split(WordsSeparator);
                
            string nameAux = "";
            if (words.Length > 1)
                foreach (string word in words)
                    nameAux += word[0];

            string aliasAux = name.Substring(0, length);

            char c = 'a';

            List<string> forbidden = new List<string>(Existent);
            forbidden.AddRange(Reserved);

            while (forbidden.Contains(aliasAux))
            {
                if (!Char.IsLetter(c) && !Char.IsNumber(c))
                {
                    c = 'a';
                    length++;
                    name.Substring(0, length);
                } else if (aliasAux.Length < length)
                    aliasAux += c;
                else
                {
                    StringBuilder sb = new StringBuilder(aliasAux);
                    sb[length-1] = c;
                    aliasAux = sb.ToString();
                }

                c = c.GetNextChar();
            }

            Existent.Add(aliasAux);
            return aliasAux;
        }
        

         



    }
}
