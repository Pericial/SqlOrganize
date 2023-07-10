using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlOrganize
{
    /*
    Mapear campos para que sean entendidos por el motor de base de datos.

    Define SQL, cada motor debe tener su propia clase mapping de forma tal que
    sea traducido de forma correcta.

    Ejemplo de subclase opcional:

    -class ComisionMapping: Mapping:
        def numero(self):
           return '''
    CONCAT("+self.pf()+"sed.numero, "+self.pt()+".division)
'''

    Las subclases deben soportar la sintaxis del motor que se encuentran utilizando.
    */
    public class Logging
    {

        /*
        Ejemplo
                   
        [
            "asignatura": {
                {"level": LEVEL_ERROR, "msg": "No puede estar vacío", type: "required"}
            }
            "plan": [
                {"level": LEVEL_WARNING, "msg: "No tiene cargas horarias asociadas", type: "user"}
            }
            "numero": {
                {"level": LEVEL_ERROR, "msg": "No es unico", type:"not_unique"}
                {"level": LEVEL_WARNING, "msg": "Esta fuera del rango permitido", type:"out_of_range"}
            }
        }
        */
        public Dictionary<string, List<(Level level, string msg, string? type)>> logs { get; } = new ();

        public enum Level
        {
            Info,
            Warning,
            Error,
        }

        public List<(Level level, string msg, string? type)> logs_key(string key)
        {
            return logs.ContainsKey(key) ? logs[key] : null;
        }

        /*
        Vaciar logs de una determinada llave

        Reasignar level
        */
        public void ResetLogs(string key)
        {
            if (logs.ContainsKey(key))
            {
                logs.Remove(key);
            }
        }
            
        public void AddLog(string key, string msg, string type = null, Level level = 0)
        {
            if (!logs.ContainsKey(key))
                logs[key] = new List<(Level level, string msg, string? type)> { };

            logs[key].Add((level, msg, type));
            logs[key].Sort((x, y) => {
                return x.level.CompareTo(y.level);
            });
        }

        public void AddError(string key, string msg, string type = null)
        {
            AddLog(key, msg, type, Level.Error);
        }

        public Level? level_key(string key)
        {
            if (!logs.ContainsKey(key))
                return null;

            Level level = Level.Info;
            foreach (var log in logs[key])
            {
                if (log.level > level)
                {
                    level = log.level;
                }
            }

            return level;
        }

        public bool Error()
        {
            foreach(var (key, logsEntity) in logs)
            {
                foreach(var log in logsEntity)
                {
                    if (log.level == Level.Error) return true;
                }
            }
            return false;
        }

    }
}