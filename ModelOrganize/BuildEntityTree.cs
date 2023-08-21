using System.Data.Common;
using System.Text.RegularExpressions;
using Utils;

namespace ModelOrganize
{
    internal class BuildEntityTree
    {
        public Config Config;

        public Dictionary<string, Entity> Entities;

        public Dictionary<string, Dictionary<string, Field>> Fields;

        public string EntityName { get; set; }
        protected List<string> FieldIds = new();

        public BuildEntityTree(Config config, Dictionary<string, Entity> entities, Dictionary<string, Dictionary<string, Field>> fields, string entityName) {
            Config = config;
            Entities = entities;
            EntityName = entityName;
            Fields = fields;
        }

        public Dictionary<string, EntityTree> Build()
        {
            if (Entities[EntityName].fk.IsNullOrEmpty()) return new();
            
            List<string> entitiesVisited = new();
            return Fk(Entities[EntityName], entitiesVisited);
        }


        protected string GetFieldId(string name, string? alias = null, string separator = "_")
        {
            if (!FieldIds.Contains(name))
            {
                FieldIds.Add(name);
                return name;
            }

            if (alias != null)
            {
                name = name + separator + alias;
                return GetFieldId(name);
            }

            Match match = Regex.Match(name, @"\d");
            if (match.Success)
            {
                string number = match.Groups[match.Groups.Count - 1].Value;
                name = name.Replace(number, "");
                name += Convert.ToInt16(number) + 1;
                return GetFieldId(name);
            }

            name += "1";
            return GetFieldId(name);
        }

        protected Dictionary<string, EntityTree> Fk(Entity entity, List<string> entitiesVisited, string? alias = null)
        {
            entitiesVisited.Add(entity.name!);
            List<Field> fk = FieldsFkNotReferenced(entity, entitiesVisited);
            Dictionary<string, EntityTree> dict = new();
            foreach (Field field in fk)
            {
                string idSource = (Config.idSource == "field_name") ? field.name : field.refEntityName;
                string fieldId = GetFieldId(idSource, alias);

                EntityTree tree = new()
                {                    
                    fieldName = field.name,
                    refEntityName = field.refEntityName!,
                    refFieldName = field.refFieldName!,
                };

                if (!entitiesVisited.Contains(field.refEntityName!)) { }

                tree.children = Fk(Entities[field.refEntityName!], new List<string>(entitiesVisited), field.alias);

                dict[fieldId] = tree;
            }

            return dict;
        }

        public List<Field> FieldsFkNotReferenced(Entity e, List<string> referencedEntityNames)
        {
            List<Field> fields = new();

            foreach (string fieldName in e.fk)
            {
                var field = Fields[e.name][fieldName];
                if(!field.refEntityName.IsNullOrEmpty() && (!referencedEntityNames.Contains(field.refEntityName!)))
                    fields.Add(field);
            }

            return fields;
        }

    }

   
}
