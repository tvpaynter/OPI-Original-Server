namespace StatementIQ.Data.Common.Migrations.Simple
{
    internal class SimpleMigrationOptions
    {
        /// <summary>
        ///     Connection String to use for migrations
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Schema in which migration metadata will be stored
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        ///     DataBase provider type (SqlSql, PostgreSql)
        /// </summary>
        public string DbProvider { get; set; }
    }
}