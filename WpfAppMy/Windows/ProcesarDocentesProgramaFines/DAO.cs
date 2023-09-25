﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WpfAppMy.Windows.ProcesarDocentesProgramaFines
{
    internal class DAO
    {
        public IEnumerable<string> PfidComisiones()
        {
            var q = ContainerApp.Db().Query("comision")
                .Fields("pfid")
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                    AND $pfid IS NOT NULL
                ")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().Column<string>(q);
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

        public IDictionary<string, object> TomaActiva(string idCurso)
        {
            var q = ContainerApp.Db().Query("toma")
                .Size(0)
                .Where(@"
                    $curso = @0 
                    AND $estado = 'Aprobada'
                    AND ($estado_contralor = 'Pasar' OR $estado_contralor = 'Pendiente')
                ")
                .Parameters(idCurso);

            return ContainerApp.DbCache().Dict(q);
        }


        public IDictionary<string, object>? RowByEntityFieldValue(string entityName, string fieldName, object value)
        {
            var q = ContainerApp.db.Query(entityName).Where("$" + fieldName + " = @0").Parameters(value);
            return ContainerApp.dbCache.Dict(q);
        }

        public IDictionary<string, object>? RowByEntityUnique(string entityName, IDictionary<string, object> source)
        {
            var q = ContainerApp.db.Query(entityName).Unique(source);

            if (source.ContainsKey(ContainerApp.config.id) && !source[ContainerApp.config.id].IsNullOrEmpty())
                q.Where("($" + ContainerApp.config.id + " != @0)").Parameters(source[ContainerApp.config.id]);

            return ContainerApp.dbCache.Dict(q);
        }
    }
}
