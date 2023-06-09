using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlOrganize
{
    public class Entity
    {
        /**
        Propiedades con sufijo "add" o "sub" existen para facilitar la configuracion
        Seran agregadas o quitadas de su atributo asociado en la inicializacion
        */
        public Db db { get; set; }

        /*
        Es necesario que se defina como propiedad con get y set, para poder 
        invocar dinamicamente al atributo mediante this!.GetType().GetProperty(name);
        */
        public string name { get; set; }
        
        public string alias { get; set; }

        public string? schema { get; set;  }

        public string pk { get; set; }
        public List<string> nf { get; set; }
        public List<string> nf_add { get; set; } 
        public List<string> nf_sub { get; set; }
        public List<string> mo { get; set; }
        public List<string> mo_add { get; set; }
        public List<string> mo_sub { get; set; }
        public List<string> oo { get; set; }
        public List<string> oo_add { get; set; }
        public List<string> oo_sub { get; set; }



        /* 
        array dinamico para identificar univocamente a una entidad en un momento determinado
        @example
        identifier = ["fecha_anio", "fecha_semestre","persona-numero_documento"]
        */
        public List<string> identifier { get; set; }

        /*
        Valores por defecto para ordenamiento
        @example ["field1"=>"asc","field2"=>"desc",...];
        */
        public Dictionary<string, string> order_default { get; set; }

        /*
        Valores no administrables
        @example ["field1","field2",...]
        */
        public List<string> no_admin { get; set; }

        /*
        Valores principales
        @example ["field1","field2",...]
        */
        public List<string> main { get; set; }

        /*
        Valores unicos
        Una entidad puede tener varios campos que determinen un valor unico
        @example ["field1","field2",...]
        */
        public List<string> unique { get; set; }
        public List<string> unique_add { get; set; }
        public List<string> unique_sub { get; set; }

        /*
        Valores no nulos        
        */
        public List<string> not_null { get; set; }
        public List<string> not_null_add { get; set; }
        public List<string> not_null_sub { get; set; }

        /*
        Valores unicos multiples
        Solo puede especificarse un juego de campos unique_multiple
        */
        public List<string> unique_multiple { get; set; }

        public string schema_ => String.IsNullOrEmpty(schema) ? schema : "";
        public string schema_name => schema + name;
        public string schema_name_alias => schema + name + " AS " + alias;

       
        protected List<Field> _fields(List<string> field_names)
        {
            List<Field> fields = new();
            foreach (string field_name in field_names)
            {
                fields.Add(db.field(name, field_name));
            }

            return fields;

        }

        /*
        fields no fk
        */
        public List<Field> fields_nf() => _fields(nf);

        /*
        fields many to one
        */
        public List<Field> fields_mo() => _fields(mo);

        /*
        fields one to one (local fk)
        */
        public List<Field> fields_oo() => _fields(oo);

        /*
        fields fk (mo + oo)
        */
        public List<Field> fields_fk() => fields_mo().Concat(fields_oo()).ToList();

        /*
        all fields except pk
        */
        public List<Field> fields_no_pk() => fields_nf().Concat(fields_mo()).ToList().Concat(fields_oo()).ToList();

        public List<Field> fields() => fields_nf().Concat(fields_mo()).ToList().Concat(fields_oo()).ToList();

        /*
        fields one to many
        its neccesary to iterate over all entities
        */
        public List<Field> fields_om()
        {
                List<Field> fields = new();

                foreach (string entity_name in db.entity_names())
                {
                    Entity e = db.entity(entity_name);
                    foreach (Field f in e.fields_mo())
                    {
                        if (f.entity_ref().name == this.name)
                        {
                            fields.Add(f);
                        }
                    }
                }

                return fields;
        }

        /*
        fields one to one without local fk
        fk pointed to entity outside
        its neccesary to iterate over all entities
        */
        public List<Field>? fields_oon()
        {
                List<Field> fields = new();

                foreach (string entity_name in db.entity_names())
                {
                    Entity e = db.entity(entity_name);
                    foreach (Field f in e.fields_oo())
                    {
                        if (f.entity_ref().name == this.name)
                        {
                            fields.Add(f);
                        }
                    }
                }

                return fields;
        }

        /*
        fields referenced (om + oon)
        fk pointed to entity outside
        its neccesary to iterate over all entities
        */
        public List<Field>? fields_ref() => fields_om().Concat(fields_oon()).ToList();

    }
}