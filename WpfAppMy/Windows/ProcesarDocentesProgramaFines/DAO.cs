using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Windows.ProcesarDocentesProgramaFines
{
    internal class DAO
    {
        public List<Dictionary<string, object>> CursosConComisionPfid()
        {
            var q = ContainerApp.Db().Query("curso")
                .Fields()
                .Select("CONCAT($sede-numero, $comision-division, '/', $planificacion-anio, $planificacion-semestre) AS numero")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $comision-pfid IS NOT NULL
                ")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().ListDict(q);
        }

        public Dictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            var q = ContainerApp.db.Query(entityName).Where("$" + fieldName + " = @0").Parameters(value);
            return ContainerApp.dbCache.Dict(q);
        }

        public Dictionary<string, object>? RowByEntityUnique(string entityName, Dictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);

            if (source.ContainsKey(ContainerApp.config.id) && !source[ContainerApp.config.id].IsNullOrEmpty())
                q.Where("($" + ContainerApp.config.id + " != @0)").Parameters(source[ContainerApp.config.id]);
            if (!q.IsUnique())
                return null;

            return ContainerApp.dbCache.Dict(q);
        }
    }
}
