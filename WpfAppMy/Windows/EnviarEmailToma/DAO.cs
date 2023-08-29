﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMy.Windows.EnviarEmailToma
{
    internal class DAO
    {
        public List<Dictionary<string, object>> TomaAll()
        {
            var q = ContainerApp.Db().Query("toma")
                .Fields()
                .Size(0)
                .Where(@"
                    $calendario-anio = @0 
                    AND $calendario-semestre = @1 
                ")
                .Order("$comision-pfid ASC")
                .Parameters("2023", "2");

            return ContainerApp.DbCache().ListDict(q);
        }
    }
}