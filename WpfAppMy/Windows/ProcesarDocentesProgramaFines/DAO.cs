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
        public List<Dictionary<string, object>> ComisionesConPfid()
        {
            var q = ContainerApp.Db().Query("comisiones")
                .Fields()
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $pfid IS NOT NULL
                ")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().ListDict(q);
        }

        public string IdCurso(string pfidComision, string asignaturaCodigo)
        {
            var q = ContainerApp.Db().Query("curso")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $comision-pfid = @0 
                    AND $asignatura-codigo = @1
                    AND $calendario-anio = @2
                    AND $calendario-semestre = @3
                ")
                .Parameters(pfidComision, asignaturaCodigo, "2023", "2");

            return ContainerApp.DbCache().Value<string>(q);
        }

        public string TomaActiva(string idCurso)
        {
            var q = ContainerApp.Db().Query("curso")
                .Fields("id")
                .Size(0)
                .Where(@"
                    $curso = @0 
                    AND $asignatura-codigo = @1
                    AND $calendario-anio = @2
                    AND $calendario-semestre = @3
                ")
                .Parameters(idCurso);

            return ContainerApp.DbCache().Value<string>(q);
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
