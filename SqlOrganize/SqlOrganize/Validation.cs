using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utils;

namespace SqlOrganize
{
    /*
    Validacion basica
    */
    public class Validation
    {

        public object? value { get; set; } //valor a validar;

        public List<(string msg, string? type)> errors { get; set; } = new(); //log de errores

        public Validation(object? _value) {
            value = _value;
        }

        public Validation required()
        {
            if (value is null) {
                errors.Add(("Value is null", "required"));
            }
            return this;
        }

        public Validation type(string type)
        {
            switch (type)
            {
                case "string":
                    if(value is not String)
                    {
                        errors.Add(("Value is not string", "type"));

                    }
                break;
                case "integer":
                    if (value is int)
                    {
                        errors.Add(("Value is not string", "type"));

                    }
                break;
            }
            return this;
        }

        public bool is_success() {
            return (errors.IsNullOrEmpty()) ? true : false;
        }


    }
}