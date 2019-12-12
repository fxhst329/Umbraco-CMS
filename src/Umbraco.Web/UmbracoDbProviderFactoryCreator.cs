using System;
using System.Data.Common;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Umbraco.Web
{
    public class UmbracoDbProviderFactoryCreator : IDbProviderFactoryCreator
    {
        private readonly string _defaultProviderName;

        public UmbracoDbProviderFactoryCreator(string defaultProviderName)
        {
            _defaultProviderName = defaultProviderName;
        }

        public DbProviderFactory CreateFactory()
        {
            return CreateFactory(_defaultProviderName);
        }

        public DbProviderFactory CreateFactory(string providerName)
        {
            if (string.IsNullOrEmpty(providerName)) return null;

            return DbProviderFactories.GetFactory(providerName);
        }

        // gets the sql syntax provider that corresponds, from attribute
        public ISqlSyntaxProvider GetSqlSyntaxProvider(string providerName)
        {
            switch (providerName)
            {
                case Constants.DbProviderNames.SqlCe:
                    return new SqlCeSyntaxProvider();
                case Constants.DbProviderNames.SqlServer:
                    return new SqlServerSyntaxProvider();
                default:
                    throw new InvalidOperationException($"Unknown provider name \"{providerName}\"");
            }
        }
    }
}
