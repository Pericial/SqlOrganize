
using ModelOrganizeMy;
using System.Configuration;

var c = new ConfigMy()
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString"),
    modelPath = ConfigurationManager.AppSettings.Get("modelPath"),
    configPath = ConfigurationManager.AppSettings.Get("configPath"),
    dbName = ConfigurationManager.AppSettings.Get("dbName"),
    idSource = "field_name",
    reservedEntities = new() {
        "wpwt_commentmeta",
        "wpwt_comments",
        "wpwt_e_events",
        "wpwt_e_submissions",
        "wpwt_e_submissions_actions_log",
        "wpwt_e_submissions_values",
        "wpwt_frmt_form_entry",
        "wpwt_frmt_form_entry_meta",
        "wpwt_frmt_form_reports",
        "wpwt_frmt_form_views",
        "wpwt_links",
        "wpwt_login_redirects",
        "wpwt_loginizer_logs",
        "wpwt_options",
        "wpwt_postmeta",
        "wpwt_posts",
        "wpwt_term_relationships",
        "wpwt_term_taxonomy",
        "wpwt_termmeta",
        "wpwt_terms",
        "wpwt_usermeta",
        "wpwt_users",
        "wpwt_yoast_indexable",
        "wpwt_yoast_indexable_hierarchy",
        "wpwt_yoast_migrations",
        "wpwt_yoast_primary_term",
        "wpwt_yoast_seo_links",
    }
};

BuildModelMy t = new(c);
foreach (var (key, field) in t.fields)
{
    foreach (var (k, f) in field)
    {
        if (f.name == "id")
            f.defaultValue = "max";
    }

}

t.CreateFileEntitites();

t.CreateFileFields();

